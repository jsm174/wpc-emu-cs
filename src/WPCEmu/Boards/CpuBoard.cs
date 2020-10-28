using System;
using System.Linq;
using System.Diagnostics;
using WPCEmu.Boards.Elements;
using WPCEmu.Boards.Static;
using WPCEmu.Boards.Up;
using memoryMapper = WPCEmu.Boards.Mapper.Memory;
using hardwareMapper = WPCEmu.Boards.Mapper.Hardware;

namespace WPCEmu.Boards
{
    public class WpcCpuBoard
    {
        public struct InitObject
        {
            public InterruptCallbackData interruptCallback;
            public ushort romSizeMBit;
            public RomObject? romObject;
            public byte[] ram;
            public bool hasAlphanumericDisplay;
        }

        public struct Asic
        {
            public byte[] ram;
            public CpuBoardAsic.State wpc;
            public object display;
            public SoundBoard.State sound;
            public MemoryPositionData[] memoryPosition;
            public OutputDmdDisplay.State dmd;
        }

        public struct State
        {
            public Asic? asic;
            public string romFileName;
            public Cpu6809.State cpuState;
            public int protectedMemoryWriteAttempts;
            public int memoryWrites;
            public int ticksIrq;
            public byte version;
            public int opsMs;
            public long runtime;
        };

        const ushort ROM_BANK_SIZE = 16 * 1024;
        const byte SERIALIZED_STATE_VERSION = 5;

        public byte[] ram;
        ushort romSizeMBit;
        byte[] systemRom;
        string romFileName;
        public byte[] gameRom;
        MemoryPatch memoryPatch;
        MemoryHandler memoryWriteHandler;
        public Cpu6809 cpu;
        public CpuBoardAsic asic;
        SoundBoard soundBoard;
        DisplayBoard displayBoard;
        ExternalIo externalIo;
        int ticksIrq;
        int protectedMemoryWriteAttempts;
        int memoryWrites;
        bool hasAlphaNumbericDisplay;

        public static WpcCpuBoard getInstance(RomObject romObject)
        {
            return new WpcCpuBoard(romObject);
        }

        public WpcCpuBoard(RomObject romObject)
        {
            ram = Enumerable.Repeat((byte)0, memoryMapper.MEMORY_ADDR_HARDWARE).ToArray();
            romSizeMBit = romObject.romSizeMBit;
            systemRom = romObject.systemRom;
            romFileName = romObject.fileName;
            gameRom = romObject.gameRom;
            memoryPatch = MemoryPatch.getInstance();
            if (romObject.gameIdMemoryLocation != null)
            {
                MemoryPatchGameId.run(memoryPatch, (ushort)romObject.gameIdMemoryLocation);
            }
            if (romObject.skipWpcRomCheck)
            {
                Debug.Print("skipWpcRomCheck TRUE");
                MemoryPatchSkipBootCheck.run(memoryPatch);
            }
            memoryWriteHandler = MemoryHandler.getInstance(romObject.memoryPosition, ram);

            cpu = Cpu6809.getInstance(_write8, _read8);

            InterruptCallbackData interruptCallback = new InterruptCallbackData
            {
                irq = cpu.irq,
                firqFromDmd = () =>
                {
                    cpu.firq();
                    asic.firqSourceDmd(true);
                },
                reset = cpu.reset
            };

            InitObject initObject = new InitObject
            {
                interruptCallback = interruptCallback,
                romSizeMBit = romSizeMBit,
                romObject = romObject,
                ram = ram,
                hasAlphanumericDisplay = romObject.hasAlphanumericDisplay
            };

            hasAlphaNumbericDisplay = romObject.hasAlphanumericDisplay;
            asic = CpuBoardAsic.getInstance(initObject);
            soundBoard = SoundBoard.getInstance(initObject);
            displayBoard = DisplayBoard.getInstance(initObject);
            externalIo = ExternalIo.getInstance();

            ticksIrq = 0;
            protectedMemoryWriteAttempts = 0;
            memoryWrites = 0;
        }

        public void reset()
        {
            Debug.Print("RESET_CPU_BOARD");
            ticksIrq = 0;
            protectedMemoryWriteAttempts = 0;
            memoryWrites = 0;

            memoryPatch.removeVolatileEntries();
            displayBoard.reset();
            soundBoard.reset();
            asic.reset();
            cpu.reset();
        }

        public State getState()
        {
            var asic = new Asic
            {
                ram = memoryPatch.applyPatchesToExposedMemory(ram),
                wpc = this.asic.getState(),
                display = displayBoard.getState(),
                sound = soundBoard.getState()
            };

            var cpuState = cpu.getState();

            return new State
            {
                asic = asic,
                romFileName = romFileName,
                cpuState = cpuState,
                protectedMemoryWriteAttempts = protectedMemoryWriteAttempts,
                memoryWrites = memoryWrites,
                ticksIrq = ticksIrq,
                version = SERIALIZED_STATE_VERSION
            };
        }

        public bool? setState(State? _stateObject = null)
        {
            if (_stateObject == null || _stateObject?.asic == null || _stateObject?.version != SERIALIZED_STATE_VERSION)
            {
                Debug.Print("INVALID_STATE_VERSION_OR_DATA");
                return false;
            }
            var stateObject = (State)_stateObject;
            cpu.setState(stateObject.cpuState);
            protectedMemoryWriteAttempts = stateObject.protectedMemoryWriteAttempts;
            memoryWrites = stateObject.memoryWrites;
            ticksIrq = stateObject.ticksIrq;
            asic.setState(stateObject.asic?.wpc);
            displayBoard.setState(stateObject.asic?.display);
            soundBoard.setState(stateObject.asic?.sound);
            //if(typeof stateObject.asic.ram === 'object')
            ram = stateObject.asic?.ram.Take((int)stateObject.asic?.ram.Length).ToArray();
            //}
            return null;
        }

        public void setCabinetInput(byte value)
        {
            asic.setCabinetInput(value);
        }

        public void setSwitchInput(byte switchNr, bool? optionalValue = null)
        {
            asic.setSwitchInput(switchNr, optionalValue);
        }

        public void setFliptronicsInput(string value, bool? optionalValue = null)
        {
            asic.setFliptronicsInput(value, optionalValue);
        }

        public void toggleMidnightMadnessMode()
        {
            asic.toggleMidnightMadnessMode();
        }

        public void setDipSwitchByte(byte dipSwitch)
        {
            asic.setDipSwitchByte(dipSwitch);
        }

        public byte getDipSwitchByte()
        {
            return asic.getDipSwitchByte();
        }

        public void registerSoundBoardCallback(Action<SoundBoardCallbackData> callbackFunction)
        {
            soundBoard.registerSoundBoardCallback(callbackFunction);
        }

        public void start()
        {
            Debug.Print("RESET_SYSTEM");
            reset();
            Debug.Print("PC", cpu.getState().regPC);
        }

        public int executeCycle(int ticksToRun, int tickSteps)
        {
            int ticksExecuted = 0;
            while (ticksExecuted < ticksToRun)
            {
                var singleTicks = cpu.steps(tickSteps);
                ticksExecuted += singleTicks;
                ticksIrq += singleTicks;
                if (ticksIrq >= Timing.CALL_IRQ_AFTER_TICKS)
                {
                    ticksIrq -= Timing.CALL_IRQ_AFTER_TICKS;
                    // TODO isPeriodicIrqEnabled setting is from freeWpc project, unknown if the "real" WPC system implements this too
                    // some games needs a manual irq trigger if this is implemented (like indiana jones)
                    //if (this.asic.periodicIRQTimerEnabled) {
                    cpu.irq();
                    //}
                }

                displayBoard.executeCycle(singleTicks);
                asic.executeCycle(singleTicks);
            }

            Debug.Print("CPUSTATE ticks: {0}, irqExecuted: {1}, irqMissed: {2}, firqExecuted: {3}, firqMissed: {4}", cpu.tickCount, cpu.irqCount, cpu.missedIRQ, cpu.firqCount, cpu.missedFIRQ);
            return ticksExecuted;
        }

        void writeMemory(ushort offset, byte value)
        {
            Debug.Print("writeMemory {0}", offset);
            memoryWriteHandler.writeMemory(offset, value);
        }

        byte _read8(ushort offset)
        {
            //if (!offset && offset !== 0)
            //{
            //    throw new TypeError('CPU_MEMORY_READ_BUG_DETECTED!');
            //}

            var _memoryPatch = memoryPatch.hasPatch(offset);
            if (_memoryPatch != null)
            {
                var memoryPatch = (MemoryPatchData)_memoryPatch;
                return memoryPatch.value;
            }

            var address = memoryMapper.getAddress(offset);
            //Debug.Print("read from adr {0}, offset: {1}", address, "0x" + offset.ToString("X4");
            switch (address.subsystem)
            {

                case memoryMapper.SUBSYSTEM_RAM:
                    //debug('READ RAM', address.offset);
                    return ram[address.offset];

                case memoryMapper.SUBSYSTEM_HARDWARE:
                    //debug('READ SUBSYSTEM_HARDWARE', address.offset);
                    return _hardwareRead(address.offset);

                case memoryMapper.SUBSYSTEM_BANKSWITCHED:
                    //debug('READ SUBSYSTEM_BANKSWITCHED', address.offset);
                    return _bankswitchedRead(address.offset);

                case memoryMapper.SUBSYSTEM_SYSTEMROM:
                    //debug('READ ROM', address.offset);
                    return systemRom[address.offset];

                default:
                    throw new Exception("INVALID_READ_SUBSYSTEM");
            }
        }

        void _write8(ushort offset, byte value)
        {
            //if (!offset && offset !== 0)
            //{
            //    console.log('CPU_MEMORY_WRITE_BUG_DETECTED', { offset, value });
            //    throw new TypeError('CPU_MEMORY_WRITE_BUG_DETECTED!');
            //}

            value &= 0xFF;
            var address = memoryMapper.getAddress(offset);
            //Debug.Print("write to adr {0} offset: {1} {2}", address, "0x" + offset.ToString("X4"), value);
            switch (address.subsystem)
            {

                case memoryMapper.SUBSYSTEM_RAM:
                    if (asic.isMemoryProtectionEnabled() || asic.memoryProtectionMask == null ||
                      (offset & asic.memoryProtectionMask) != asic.memoryProtectionMask)
                    {
                        ram[address.offset] = value;
                        memoryWrites++;
                    }
                    else
                    {
                        Debug.Print("DID_NOT_WRITE_MEMORY_PROTECTED {0} {1}", "0x" + offset.ToString("X4"), value);
                        protectedMemoryWriteAttempts++;
                    }
                    break;

                case memoryMapper.SUBSYSTEM_HARDWARE:
                    _hardwareWrite(offset, value);
                    break;

                case memoryMapper.SUBSYSTEM_SYSTEMROM:
                    Debug.Print("SYSTEMROM_WRITE offset: {0}, value: {1}", "0x" + offset.ToString("X4"), value);
                    //Debug.Print("SYSTEMROM_WRITE offset: {0}, value: {1}", "0x" + offset.ToString("X4"), value);
                    systemRom[address.offset] = value;
                    break;

                default:
                    Debug.Print("CPU_WRITE8_FAIL {0} {1} {2}", /*JSON.stringify(*/address/*)*/, offset, value);
                    //throw new Exception("INVALID_WRITE_SUBSYSTEM_0x" + offset.ToString("X4"));
                    break;
            }
        }

        public byte _bankswitchedRead(ushort offset)
        {
            byte activeBank = asic.romBank;
            uint pageOffset = (uint) (offset + activeBank * ROM_BANK_SIZE);
            if (pageOffset < gameRom.Length)
            {
                Debug.Print("R_GAMEROM_BANK {0}, {1}, {2}, data: {3}, romlength: {4}", activeBank, offset, pageOffset, gameRom[pageOffset], gameRom.Length);
                return gameRom[pageOffset];
            }
            return 0;
        }

        void _hardwareWrite(ushort offset, byte value)
        {
            // write/mirror value to ram, so its visible using the memory monitor
            ram[offset] = value;

            var address = hardwareMapper.getAddress(offset, hasAlphaNumbericDisplay);
            switch (address.subsystem)
            {
                case hardwareMapper.SUBSYSTEM_DISPLAY:
                    displayBoard.write(offset, value);
                    break;
                case hardwareMapper.SUBSYSTEM_EXTERNAL_IO:
                    externalIo.write(offset, value);
                    break;
                case hardwareMapper.SUBSYSTEM_SOUND:
                    soundBoard.writeInterface(offset, value);
                    break;
                case hardwareMapper.SUBSYSTEM_WPCIO:
                    asic.write(offset, value);
                    break;
                default:
                    Debug.Print("{0}", address);
                    throw new Exception("CPUBOARD_INVALID_HW_WRITE");
            }
        }

        byte _hardwareRead(ushort offset)
        {
            var address = hardwareMapper.getAddress(offset, hasAlphaNumbericDisplay);
            switch (address.subsystem)
            {
                case hardwareMapper.SUBSYSTEM_DISPLAY:
                    return displayBoard.read(offset);
                case hardwareMapper.SUBSYSTEM_EXTERNAL_IO:
                    return externalIo.read(offset);
                case hardwareMapper.SUBSYSTEM_SOUND:
                    return soundBoard.readInterface(offset);
                case hardwareMapper.SUBSYSTEM_WPCIO:
                    return asic.read(offset);
                default:
                    Debug.Print("{0}", address);
                    throw new Exception("INVALID_HW_READ");
            }
        }
    }
}