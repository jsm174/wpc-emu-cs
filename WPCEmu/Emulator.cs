using System;
using System.Diagnostics;
using WPCEmu.Rom;
using WPCEmu.Boards;
using System.Reflection;

namespace WPCEmu
{
    public class Emulator
    {
        const int TICKS_PER_MILLISECOND = 2000;
        long startTime;

        public WpcCpuBoard cpuBoard;
        UiState uiFacade;

        public Emulator(RomObject romObject)
        {
            cpuBoard = WpcCpuBoard.getInstance(romObject);
            uiFacade = UiState.getInstance(null);
        }

        public void start()
        {
            Debug.Print("Start WPC Emulator");
            startTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            cpuBoard.start();
        }

        WpcCpuBoard.State getUiState(bool includeExpensiveData = true)
        {
            var uiState = cpuBoard.getState();
            var asicChangedState = uiFacade.getChangedAsicState((WpcCpuBoard.Asic)uiState.asic, includeExpensiveData);
            uiState.asic = asicChangedState;

            var runtime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() - startTime;
            // TODO should be renamed to averageTicksPerMs
            uiState.opsMs = (int)(uiState.cpuState.tickCount / runtime);
            uiState.runtime = runtime;
            return uiState;
        }

        public WpcCpuBoard.State getState()
        {
            return cpuBoard.getState();
        }

        bool? setState(WpcCpuBoard.State stateObject)
        {
            return cpuBoard.setState(stateObject);
        }

        void registerAudioConsumer(Action<SoundBoardCallbackData> playbackIdCallback)
        {
            cpuBoard.registerSoundBoardCallback(playbackIdCallback);
        }

        // MAIN LOOP
        public int executeCycle(int ticksToRun = 500, int tickSteps = 4)
        {
            return cpuBoard.executeCycle(ticksToRun, tickSteps);
        }

        int executeCycleForTime(int advanceByMs, int tickSteps)
        {
            int ticksToAdvance = TICKS_PER_MILLISECOND * advanceByMs;
            return executeCycle(ticksToAdvance, tickSteps);
        }

        void setCabinetInput(byte value)
        {
            cpuBoard.setCabinetInput(value);
        }

        public void setSwitchInput(byte switchNr, bool? optionalValue = null)
        {
            cpuBoard.setSwitchInput(switchNr, optionalValue);
        }

        void setFliptronicsInput(string value, bool? optionalValue = null)
        {
            cpuBoard.setFliptronicsInput(value, optionalValue);
        }

        public void toggleMidnightMadnessMode()
        {
            cpuBoard.toggleMidnightMadnessMode();
        }

        public void setDipSwitchByte(byte dipSwitch)
        {
            cpuBoard.setDipSwitchByte(dipSwitch);
        }

        public byte getDipSwitchByte()
        {
            return cpuBoard.getDipSwitchByte();
        }

        public void reset()
        {
            Debug.Print("RESET!");
            startTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            cpuBoard.reset();
        }

        public string version()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /**
        * Initialize the WPC-EMU
        * @function
        * @param {Object} romObject, rom data (sound and main game). NOTE: DCS sound roms are not implemented yet
        * @param {Object} metaData, meta data about the current game
        * @return {promise} promise contains a new Emulator instance.
        * @example
        *
        * const romObject = {
        *   u06: Uint8Array(524288),
        * };
        * const metaData = {
        *   features: ['securityPic'], // needed for WPC-S games
        *   fileName: 'harr_lx2.rom',
        *   skipWpcRomCheck: true,     // speedup bootup for WPC games
        *   memoryPosition: [          // information about the game ram state (optional)
        *    { offset: 0x3B2, description: 'current player', type: 'uint8' }
        *   ]
        * };
        * wpcEmu.initVMwithRom(romObject, metaData)
        *   .then((emu) => {
        *     ...
        *   }
        */

        public static Emulator initVMwithRom(RomBinary romObject, RomMetaData? metaData = null)
        {
            Debug.Print("initVMwithRom {0} {1}", romObject, metaData);

            RomData romData = RomHelper.parse(romObject, metaData);

            var _romObject = new RomObject
            {
                romSizeMBit = romData.romSizeMBit,
                systemRom = romData.systemRom,
                fileName = romData.fileName,
                gameRom = romData.gameRom,
                gameIdMemoryLocation = romData.gameIdMemoryLocation,
                wpc95 = romData.wpc95,
                hasSecurityPic = romData.hasSecurityPic,
                skipWpcRomCheck = romData.skipWpcRomCheck,
                hasAlphanumericDisplay = romData.hasAlphanumericDisplay,
                preDcsSoundboard = romData.preDcsSoundboard
            };

            return new Emulator(_romObject);
        }
    }
}