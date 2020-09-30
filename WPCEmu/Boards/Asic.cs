using System;
using System.Linq;
using System.Diagnostics;
using WPCEmu.Boards.Elements;
using WPCEmu.Boards.Static;
using WPCEmu.Boards.Up;
using System.Collections.Generic;

/**
 * All read/write requests from the CPU are first seen by the ASIC, which can
 * then either respond to it directly if it is an internal function, or forward
 * the request to another device
 * this file emulates the ASIC CHIP
 */

namespace WPCEmu.Boards
{
    public class CpuBoardAsic
    {
        public static class OP
        {
            public const ushort WPC_FLIPTRONICS_FLIPPER_PORT_A = 0x3FD4;

            public const ushort WPC_SOLENOID_GEN_OUTPUT = 0x3FE0;
            public const ushort WPC_SOLENOID_HIGHPOWER_OUTPUT = 0x3FE1;
            public const ushort WPC_SOLENOID_FLASH1_OUTPUT = 0x3FE2;
            public const ushort WPC_SOLENOID_LOWPOWER_OUTPUT = 0x3FE3;
            public const ushort WPC_LAMP_ROW_OUTPUT = 0x3FE4;
            public const ushort WPC_LAMP_COL_STROBE = 0x3FE5;
            public const ushort WPC_GI_TRIAC = 0x3FE6;
            public const ushort WPC_SW_JUMPER_INPUT = 0x3FE7;
            public const ushort WPC_SWITCH_CABINET_INPUT = 0x3FE8;

            //PRE SECURITY PIC
            public const ushort WPC_SWITCH_ROW_SELECT = 0x3FE9;
            public const ushort WPC_SWITCH_COL_SELECT = 0x3FEA;

            //SECURITY PIC
            public const ushort WPC_PICREAD = 0x3FE9;
            public const ushort WPC_PICWRITE = 0x3FEA;

            public const ushort WPC_EXTBOARD1 = 0x3FEB;
            public const ushort WPC_EXTBOARD2 = 0x3FEC;
            public const ushort WPC_EXTBOARD3 = 0x3FED;
            //aka WPC_EXTBOARD4
            public const ushort WPC95_FLIPPER_COIL_OUTPUT = 0x3FEE;
            //aka WPC_EXTBOARD5
            public const ushort WPC95_FLIPPER_SWITCH_INPUT = 0x3FEF;

            public const ushort WPC_LEDS = 0x3FF2;
            public const ushort WPC_RAM_BANK = 0x3FF3;
            public const ushort WPC_SHIFTADDRH = 0x3FF4;
            public const ushort WPC_SHIFTADDRL = 0x3FF5;
            public const ushort WPC_SHIFTBIT = 0x3FF6;
            public const ushort WPC_SHIFTBIT2 = 0x3FF7;
            public const ushort WPC_PERIPHERAL_TIMER_FIRQ_CLEAR = 0x3FF8;
            public const ushort WPC_ROM_LOCK = 0x3FF9;

            public const ushort WPC_CLK_HOURS_DAYS = 0x3FFA;
            public const ushort WPC_CLK_MINS = 0x3FFB;
            public const ushort WPC_ROM_BANK = 0x3FFC;

            //WPC_PROTMEM
            public const ushort WPC_RAM_LOCK = 0x3FFD;

            //WPC_PROTMEMCTRL - aka CLOCK CHANGE
            public const ushort WPC_RAM_LOCKSIZE = 0x3FFE;
            public const ushort WPC_ZEROCROSS_IRQ_CLEAR = 0x3FFF;
        }

        Dictionary<ushort, string> REVERSEOP = new Dictionary<ushort, string>();

        public struct State
        {
            public byte diagnosticLed;
            public byte[] lampState;
            public byte lampRow;
            public byte lampColumn;
            public byte[] solenoidState;
            public byte[] generalIlluminationState;
            public byte[] inputState;
            public byte inputSwitchMatrixActiveColumn;
            public int diagnosticLedToggleCount;
            public bool irqEnabled;
            public byte activeRomBank;
            public byte zeroCrossFlag;
            public int ticksZeroCross;
            public ushort? memoryProtectionMask;
            public bool firqSourceDmd;
            public int irqCountGI;
            public bool midnightModeEnabled;
            public string time;
            public bool blankSignalHigh;
            public int watchdogExpiredCounter;
            public int watchdogTicks;
            public byte wpcSecureScrambler;
        };

        readonly byte[] PAGESIZE_MAP = new byte[] { 0x00, 0x07, 0x0F, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x3F };
        const byte WPC_PROTECTED_MEMORY_UNLOCK_VALUE = 0xB4;

        const ushort NVRAM_CLOCK_YEAR_HI = 0x1800;
        const ushort NVRAM_CLOCK_YEAR_LO = 0x1801;
        const ushort NVRAM_CLOCK_MONTH = 0x1802;
        const ushort NVRAM_CLOCK_DAY_OF_MONTH = 0x1803;
        const ushort NVRAM_CLOCK_DAY_OF_WEEK = 0x1804;
        const ushort NVRAM_CLOCK_HOUR = 0x1805;
        const ushort NVRAM_CLOCK_IS_VALID = 0x1806;
        const ushort NVRAM_CLOCK_CHECKSUM_TIME = 0x1807;
        const ushort NVRAM_CLOCK_CHECKSUM_DATE = 0x1808;

        const byte WPC_ZC_BLANK_RESET = 0x02;
        const byte WPC_ZC_WATCHDOG_RESET = 0x04;
        const byte WPC_ZC_IRQ_ENABLE = 0x10;
        const byte WPC_ZC_IRQ_CLEAR = 0x80;

        const byte WPC_FIRQ_CLEAR_BIT = 0x80;

        readonly long MIDNIGHT_MADNESS_TIME = new DateTimeOffset(DateTime.Parse("December 17, 1995 23:59:45")).ToUnixTimeMilliseconds();

        WpcCpuBoard.InterruptCallback interruptCallback;
        byte pageMask;
        public byte[] ram;
        bool hardwareHasSecurityPic;
        InputSwitchMatrix inputSwitchMatrix;
        OutputLampMatrix outputLampMatrix;
        OutputSolenoidMatrix outputSolenoidMatrix;
        OutputGeneralIllumination outputGeneralIllumination;
        SecurityPic securityPic;
        bool periodicIRQTimerEnabled;
        public byte romBank;
        int diagnosticLedToggleCount;
        byte oldDiagnostigLedState;
        bool _firqSourceDmd = false;
        int irqCountGI;
        public byte zeroCrossFlag = 0;
        int ticksZeroCross = 0;
        public ushort? memoryProtectionMask = null;
        long midnightMadnessMode;
        bool midnightModeEnabled;
        public bool blankSignalHigh;
        int watchdogTicks;
        int watchdogExpiredCounter;
        byte dipSwitchSetting;

        public static CpuBoardAsic GetInstance(WpcCpuBoard.InitObject initObject)
        {
            return new CpuBoardAsic(initObject);
        }

        public CpuBoardAsic(WpcCpuBoard.InitObject initObject)
        {
            foreach (var fieldInfo in typeof(OP).GetFields())
            {
                object value = fieldInfo.GetValue(null);

                if (value is ushort)
                {
                    if (!REVERSEOP.ContainsKey((ushort)value))
                    {
                        REVERSEOP.Add((ushort)value, fieldInfo.Name);
                    }
                }
            }

            interruptCallback = initObject.interruptCallback;
            pageMask = PAGESIZE_MAP[initObject.romSizeMBit];
            //if (pageMask == 0)
            //{
            //    throw new Exception("PAGEMASK_EMPTY");
            //}
            Debug.Print("pageMask calculated pageMask: {0}, romSizeMBit: {1}", pageMask, initObject.romSizeMBit);
            ram = initObject.ram;
            hardwareHasSecurityPic = initObject.romObject != null && initObject.romObject?.hasSecurityPic == true;

            inputSwitchMatrix = InputSwitchMatrix.GetInstance();
            outputLampMatrix = OutputLampMatrix.GetInstance(Timing.CALL_UPDATELAMP_AFTER_TICKS);
            outputSolenoidMatrix = OutputSolenoidMatrix.GetInstance(Timing.CALL_UPDATESOLENOID_AFTER_TICKS);

            bool isWpc95 = initObject.romObject != null && initObject.romObject?.wpc95 == true;
            outputGeneralIllumination = OutputGeneralIllumination.GetInstance(isWpc95);
            securityPic = SecurityPic.GetInstance();
            periodicIRQTimerEnabled = true;
            romBank = 0;
            diagnosticLedToggleCount = 0;
            oldDiagnostigLedState = 0;
            _firqSourceDmd = false;
            irqCountGI = 0;
            zeroCrossFlag = 0;
            ticksZeroCross = 0;
            memoryProtectionMask = null;
            midnightMadnessMode = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            midnightModeEnabled = false;
            blankSignalHigh = true;
            watchdogTicks = 0;
            watchdogExpiredCounter = 0;
            dipSwitchSetting = DipSwitchCountry.USA;
        }

        public void reset()
        {
            Debug.Print("RESET_ASIC");
            periodicIRQTimerEnabled = true;
            romBank = 0;
            diagnosticLedToggleCount = 0;
            oldDiagnostigLedState = 0;
            _firqSourceDmd = false;
            irqCountGI = 0;
            zeroCrossFlag = 0;
            ticksZeroCross = 0;
            memoryProtectionMask = null;
            if (hardwareHasSecurityPic)
            {
                securityPic.reset();
            }
            midnightMadnessMode = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            midnightModeEnabled = false;
            blankSignalHigh = true;
            watchdogTicks = Timing.WATCHDOG_ARMED_FOR_TICKS;
            watchdogExpiredCounter = 0;
        }

        public State getState()
        {
            var time = getTime().ToString("HH:mm:ss");
            if (midnightModeEnabled)
            {
                time += " MM!";
            }
            return new State {
                diagnosticLed = ram[OP.WPC_LEDS],
                lampState = outputLampMatrix.lampState,
                lampRow = outputLampMatrix.activeRow,
                lampColumn = outputLampMatrix.activeColumn,
                solenoidState = outputSolenoidMatrix.solenoidState,
                generalIlluminationState = outputGeneralIllumination.getNormalizedState(),
                inputState = inputSwitchMatrix.switchState,
                inputSwitchMatrixActiveColumn = inputSwitchMatrix.activeColumn,
                diagnosticLedToggleCount = diagnosticLedToggleCount,
                irqEnabled = periodicIRQTimerEnabled,
                activeRomBank = romBank,
                zeroCrossFlag = zeroCrossFlag,
                ticksZeroCross = ticksZeroCross,
                memoryProtectionMask = memoryProtectionMask,
                firqSourceDmd = _firqSourceDmd,
                irqCountGI = irqCountGI,
                midnightModeEnabled = midnightModeEnabled,
                time = time,
                blankSignalHigh = blankSignalHigh,
                watchdogExpiredCounter = watchdogExpiredCounter,
                watchdogTicks = watchdogTicks,
                wpcSecureScrambler = securityPic.getScrambler()
            };
        }

        public bool? setState(State? _wpcState = null)
        {
            if (_wpcState == null)
            {
                return false;
            }
            var wpcState = (State)_wpcState;
            inputSwitchMatrix.activeColumn = wpcState.inputSwitchMatrixActiveColumn;
            diagnosticLedToggleCount = wpcState.diagnosticLedToggleCount;
            periodicIRQTimerEnabled = wpcState.irqEnabled;
            romBank = wpcState.activeRomBank;
            zeroCrossFlag = wpcState.zeroCrossFlag;
            ticksZeroCross = wpcState.ticksZeroCross;
            memoryProtectionMask = wpcState.memoryProtectionMask;
            _firqSourceDmd = wpcState.firqSourceDmd;
            irqCountGI = wpcState.irqCountGI;
            midnightModeEnabled = wpcState.midnightModeEnabled;
            blankSignalHigh = wpcState.blankSignalHigh == true;
            watchdogExpiredCounter = wpcState.watchdogExpiredCounter;
            //if (typeof wpcState.lampState === 'object')
            //{
            outputLampMatrix.lampState = wpcState.lampState.Take(wpcState.lampState.Length).ToArray();
            //}
            //if (typeof wpcState.solenoidState === 'object')
            //{
            outputSolenoidMatrix.solenoidState = wpcState.solenoidState.Take(wpcState.solenoidState.Length).ToArray();
            //}
            //if (typeof wpcState.generalIlluminationState === 'object')
            //{
            outputGeneralIllumination.generalIlluminationState = wpcState.generalIlluminationState.Take(wpcState.generalIlluminationState.Length).ToArray();
            //}
            //if (typeof wpcState.inputState === 'object')
            //{
            inputSwitchMatrix.switchState = wpcState.inputState.Take(wpcState.inputState.Length).ToArray();
            //}
            return null;
        }

        public void setZeroCrossFlag()
        {
            Debug.Print("SET_ZEROCROSS_FLAG");
            zeroCrossFlag = 0x01;
        }

        public void setCabinetInput(byte value)
        {
            Debug.Print("setCabinetInput {0}", value);
            inputSwitchMatrix.setCabinetKey((byte)(value & 0xFF));
        }

        public void setSwitchInput(byte switchNr, bool? optionalValue = null)
        {
            Debug.Print("setSwitchInput {0} {1}", switchNr, optionalValue);
            inputSwitchMatrix.setInputKey((byte)(switchNr & 0xFF), optionalValue);
        }

        public void setFliptronicsInput(string value, bool? optionalValue = null)
        {
            inputSwitchMatrix.setFliptronicsInput(value, optionalValue);
        }

        public void firqSourceDmd(bool fromDmd)
        {
            Debug.Print("firqSourceDmd {0}", fromDmd);
            _firqSourceDmd = fromDmd == true;
        }

        public void toggleMidnightMadnessMode()
        {
            midnightMadnessMode = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            midnightModeEnabled = !midnightModeEnabled;
        }

        public void setDipSwitchByte(byte dipSwitch)
        {
            Debug.Print("setDipSwitchByte {0}", dipSwitch);
            dipSwitchSetting = dipSwitch;
        }

        public byte getDipSwitchByte()
        {
            return dipSwitchSetting;
        }

        public void executeCycle(int ticksExecuted)
        {
            ticksZeroCross += ticksExecuted;
            if (ticksZeroCross >= Timing.CALL_ZEROCLEAR_AFTER_TICKS)
            {
                ticksZeroCross -= Timing.CALL_ZEROCLEAR_AFTER_TICKS;
                setZeroCrossFlag();
            }

            watchdogTicks -= ticksExecuted;
            //Debug.Print("watchdogTicks {0}", watchdogTicks)
            if (watchdogTicks < 0)
            {
                Debug.Print("WATCHDOG_EXPIRED", watchdogTicks);
                watchdogTicks = Timing.WATCHDOG_ARMED_FOR_TICKS;
                watchdogExpiredCounter++;
            }

            outputLampMatrix.executeCycle(ticksExecuted);
            outputSolenoidMatrix.executeCycle(ticksExecuted);
        }

        public bool isMemoryProtectionEnabled()
        {
            return ram[OP.WPC_RAM_LOCK] == WPC_PROTECTED_MEMORY_UNLOCK_VALUE;
        }

        DateTime getTime()
        {
            if (!midnightModeEnabled)
            {
                return DateTime.Now;
            }
            return new DateTime(1970, 1, 1).AddMilliseconds(MIDNIGHT_MADNESS_TIME + new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() - midnightMadnessMode);
        }

        public void write(ushort offset, byte value)
        {
            ram[offset] = value;

            switch (offset)
            {
                // save value and bail out
                case OP.WPC_RAM_LOCK:
                case OP.WPC_RAM_BANK:
                case OP.WPC_CLK_HOURS_DAYS:
                case OP.WPC_CLK_MINS:
                case OP.WPC_SHIFTADDRH:
                case OP.WPC_SHIFTADDRL:
                case OP.WPC_SHIFTBIT:
                case OP.WPC_SHIFTBIT2:
                case OP.WPC_ROM_LOCK:
                case OP.WPC_EXTBOARD1:
                case OP.WPC_EXTBOARD2:
                case OP.WPC_EXTBOARD3:
                case OP.WPC95_FLIPPER_SWITCH_INPUT:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    break;

                case OP.WPC_FLIPTRONICS_FLIPPER_PORT_A:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    outputSolenoidMatrix.writeFliptronic((byte)((~value) & 0xFF));
                    break;

                case OP.WPC95_FLIPPER_COIL_OUTPUT:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    outputSolenoidMatrix.writeFliptronic(value);
                    break;

                case OP.WPC_RAM_LOCKSIZE:
                    if (isMemoryProtectionEnabled())
                    {
                        memoryProtectionMask = (byte)MemoryProtection.getMemoryProtectionMask(value);
                        Debug.Print("UPDATED_MEMORY_PROTECTION_MASK {0}", memoryProtectionMask);
                    }
                    else
                    {
                        Debug.Print("MEMORY_PROTECTION_DISABLED", value);
                    }
                    break;

                case OP.WPC_SWITCH_COL_SELECT:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    if (hardwareHasSecurityPic)
                    {
                        securityPic.write(value);
                        return;
                    }
                    inputSwitchMatrix.setActiveColumn(value);
                    break;

                case OP.WPC_GI_TRIAC:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    outputGeneralIllumination.update(value, irqCountGI);
                    break;

                case OP.WPC_LAMP_ROW_OUTPUT:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    outputLampMatrix.setActiveRow(value);
                    break;

                case OP.WPC_LAMP_COL_STROBE:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    outputLampMatrix.setActiveColumn(value);
                    break;

                case OP.WPC_PERIPHERAL_TIMER_FIRQ_CLEAR:
                    Debug.Print("WRITE {0} _firqSourceDmd: {1}, {2}", REVERSEOP[offset], _firqSourceDmd, value);
                    _firqSourceDmd = false;
                    break;

                case OP.WPC_SOLENOID_GEN_OUTPUT:
                case OP.WPC_SOLENOID_HIGHPOWER_OUTPUT:
                case OP.WPC_SOLENOID_FLASH1_OUTPUT:
                case OP.WPC_SOLENOID_LOWPOWER_OUTPUT:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    outputSolenoidMatrix.write(offset, value);
                    break;

                case OP.WPC_LEDS:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    if (value != oldDiagnostigLedState)
                    {
                        Debug.Print("DIAGNOSTIC_LED_TOGGLE {0} {1}", oldDiagnostigLedState, value);
                        diagnosticLedToggleCount++;
                        oldDiagnostigLedState = value;
                    }
                    break;

                case OP.WPC_ROM_BANK:
                    {
                        byte bank = (byte)(value & pageMask);
                        Debug.Print("SELECT WPC_ROM_BANK {0}, {1}", value, bank);
                        // only 6 bits
                        romBank = bank;
                        break;
                    }

                case OP.WPC_ZEROCROSS_IRQ_CLEAR:
                    {
                        //Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);

                        if ((value & WPC_ZC_WATCHDOG_RESET) != 0)
                        {
                            // the watchdog will be tickled every 1ms by the IRQ (or after 2049 ticks)
                            watchdogTicks = Timing.WATCHDOG_ARMED_FOR_TICKS;
                            Debug.Print("WPC_ZC_WATCHDOG_RESET: RESET WATCHDOG {0}", watchdogTicks);
                        }

                        if (blankSignalHigh && ((value & WPC_ZC_BLANK_RESET) != 0))
                        {
                            // like the watchdog, blanking is reset regulary
                            Debug.Print("CLEAR_BLANKING_SIGNAL");
                            blankSignalHigh = false;
                        }

                        if ((value & WPC_ZC_IRQ_CLEAR) != 0)
                        {
                            Debug.Print("WRITE WPC_ZEROCROSS_IRQ_CLEAR {0}", value);
                            //Increment irq count - This is the best way to know an IRQ was serviced as this register is written immediately during the IRQ code.
                            irqCountGI++;
                            //TODO cpu_set_irq_line(WPC_CPUNO, M6809_IRQ_LINE, CLEAR_LINE); ??
                        }

                        bool timerEnabled = (value & WPC_ZC_IRQ_ENABLE) > 0;
                        if (timerEnabled != periodicIRQTimerEnabled)
                        {
                            Debug.Print("WRITE WPC_ZEROCROSS_IRQ_CLEAR periodic timer changed {0}", value);
                            //The periodic interrupt can be disabled/enabled by writing to the ASIC's WPC_ZEROCROSS_IRQ_CLEAR register.
                            periodicIRQTimerEnabled = timerEnabled;
                        }

                        break;
                    }

                default:
                    Debug.Print("W_NOT_IMPLEMENTED {0} {1}", offset, value /*'0x' + offset.toString(16)*/);
                    Debug.Print("ASIC_WRITE_NOT_IMPLEMENTED {0} {1}", offset, value /*'0x' + offset.toString(16)*/);
                    break;
            }
        }

        public byte read(ushort offset)
        {
            switch (offset)
            {
                case OP.WPC_LEDS:
                case OP.WPC_RAM_BANK:
                case OP.WPC_ROM_LOCK:
                case OP.WPC_EXTBOARD1:
                case OP.WPC_EXTBOARD2:
                case OP.WPC_EXTBOARD3:
                case OP.WPC95_FLIPPER_COIL_OUTPUT:
                    Debug.Print("READ {0} {1}", REVERSEOP[offset], ram[offset]);
                    return ram[offset];

                case OP.WPC95_FLIPPER_SWITCH_INPUT:
                    Debug.Print("READ {0}", REVERSEOP[offset]);
                    return inputSwitchMatrix.getFliptronicsKeys();

                case OP.WPC_FLIPTRONICS_FLIPPER_PORT_A:
                    Debug.Print("READ {0}", REVERSEOP[offset]);
                    return inputSwitchMatrix.getFliptronicsKeys();

                case OP.WPC_RAM_LOCK:
                case OP.WPC_RAM_LOCKSIZE:
                    Debug.Print("READ {0} {1}", REVERSEOP[offset], ram[offset]);
                    return ram[offset];

                case OP.WPC_SWITCH_CABINET_INPUT:
                    Debug.Print("READ {0}", REVERSEOP[offset]);
                    return inputSwitchMatrix.getCabinetKey();

                case OP.WPC_ROM_BANK:
                    Debug.Print("READ {0} {1}", REVERSEOP[offset], ram[offset]);
                    return (byte) (ram[offset] & pageMask);

                case OP.WPC_SWITCH_ROW_SELECT:
                    Debug.Print("READ {0}", REVERSEOP[offset]);
                    if (hardwareHasSecurityPic)
                    {
                        return securityPic.read((col) => {
                            return inputSwitchMatrix.getRow(col);
                        });
                    }
                    return inputSwitchMatrix.getActiveRow();

                case OP.WPC_SHIFTADDRH:
                    {
                        byte temp = (byte)((ram[OP.WPC_SHIFTADDRH] +
                                ((ram[OP.WPC_SHIFTADDRL] + (ram[OP.WPC_SHIFTBIT] >> 3)) >> 8)
                               ) & 0xFF);
                        Debug.Print("READ {0} {1}", REVERSEOP[offset], temp);
                        return temp;
                    }
                case OP.WPC_SHIFTADDRL:
                    {
                        byte temp = (byte)((ram[OP.WPC_SHIFTADDRL] + (ram[OP.WPC_SHIFTBIT] >> 3)) & 0xFF);
                        Debug.Print("READ {0} {1}", REVERSEOP[offset], temp);
                        return temp;
                    }
                case OP.WPC_SHIFTBIT:
                case OP.WPC_SHIFTBIT2:
                    Debug.Print("READ {0} {1}", REVERSEOP[offset], ram[offset]);
                    return (byte) (1 << (ram[offset] & 0x07));

                case OP.WPC_CLK_HOURS_DAYS:
                    {
                        //temp = new Date();
                        DateTime temp = getTime();

                        Debug.Print("READ WPC_CLK_HOURS_DAYS");
                        // checksum needs to be stored in RAM
                        ushort checksum = 0;
                        checksum += ram[NVRAM_CLOCK_YEAR_HI] = (byte) (temp.Year >> 8);
                        checksum += ram[NVRAM_CLOCK_YEAR_LO] = (byte) (temp.Year & 0xFF);
                        //month (1-12),
                        checksum += ram[NVRAM_CLOCK_MONTH] = (byte) (temp.Month + 1);
                        //day of month (1-31)
                        checksum += ram[NVRAM_CLOCK_DAY_OF_MONTH] = (byte) temp.Day;
                        //day of the week (0-6, 0=Sunday)
                        checksum += ram[NVRAM_CLOCK_DAY_OF_WEEK] = (byte) (temp.DayOfWeek + 1);
                        //hour (0-23)
                        checksum += ram[NVRAM_CLOCK_HOUR] = 0;
                        //0 means invalid, 1 means valid
                        checksum += ram[NVRAM_CLOCK_IS_VALID] = 1;
                        checksum = (ushort) (0xFFFF - checksum);
                        ram[NVRAM_CLOCK_CHECKSUM_TIME] = (byte) (checksum >> 8);
                        ram[NVRAM_CLOCK_CHECKSUM_DATE] = (byte) (checksum & 0xFF);
                        return (byte) temp.Hour;
                    }

                case OP.WPC_CLK_MINS:
                    {
                        DateTime temp = getTime();
                        Debug.Print("READ WPC_CLK_MINS");
                        return (byte) temp.Minute;
                    }

                case OP.WPC_SW_JUMPER_INPUT:
                    //SW1 SW2 W20 W19 Country(SW4-SW8)
                    Debug.Print("READ WPC_SW_JUMPER_INPUT {0}", dipSwitchSetting);
                    return dipSwitchSetting;

                case OP.WPC_ZEROCROSS_IRQ_CLEAR:
                    {
                        if (zeroCrossFlag != 0)
                        {
                            Debug.Print("RESET GI ZC COUNT");
                            irqCountGI = 0;
                        }
                        byte temp = (byte)(zeroCrossFlag << 7 | (ram[offset] & 0x7F));
                        Debug.Print("READ WPC_ZEROCROSS_IRQ_CLEAR {0} {1}", temp, (zeroCrossFlag != 0) ? "ZCF_SET" : "ZCF_NOTSET");
                        zeroCrossFlag = 0;
                        return temp;
                    }

                case OP.WPC_PERIPHERAL_TIMER_FIRQ_CLEAR:
                    Debug.Print("READ WPC_PERIPHERAL_TIMER_FIRQ_CLEAR {0}", _firqSourceDmd);
                    return (byte) (_firqSourceDmd == true ? 0x00 : WPC_FIRQ_CLEAR_BIT);

                default:
                    Debug.Print("R_NOT_IMPLEMENTED {0} {1}", offset, ram[offset] /*'0x' + offset.toString(16)*/);
                    Debug.Print("ASIC_READ_NOT_IMPLEMENTED {0} {1}", offset, ram[offset] /*'0x' + offset.toString(16)*/);
                    break;
            }

            return 0;
        }
    }
}

/*
  wpcemu:boards:wpc W_NOT_IMPLEMENTED 0x3ff3 0 +57ms
Address	  Format	 Description
$3FE0     Byte     WPC_SOLENOID_GEN_OUTPUT (7-0: W: Enables for solenoids 25-29) or 25-28???
$3FE1     Byte     WPC_SOLENOID_HIGHPOWER_OUTPUT (7-0: W: Enables for solenoids 1-8)
$3FE2     Byte     WPC_SOLENOID_FLASH1_OUTPUT (7-0: W: Enables for solenoids 17-24)
$3FE3     Byte     WPC_SOLENOID_LOWPOWER_OUTPUT (7-0: W: Enables for solenoids 9-16)
$3FE4     Byte     WPC_LAMP_ROW_OUTPUT (7-0: W: Lamp matrix row output)
$3FE5     Byte     WPC_LAMP_COL_STROBE (7-0: W: Enables for solenoids 9-16)
                    7-0: W: Lamp matrix column strobe, At most one bit in this register should be set.
                    If all are clear, then no controlled lamps are enabled.
$3FE6     Byte     WPC_GI_TRIAC
                    7: W: Flipper enable relay
                    5: W: Coin door enable relay
                    4-0: W: General illumination enables
$3FE7     Byte     WPC_SW_JUMPER_INPUT (7-0: R: Jumper/DIP switch inputs)
$3FE8     Byte     WPC_SW_CABINET_INPUT
                    7: R: Fourth coin switch
                    6: R: Right coin switch
                    5: R: Center coin switch
                    4: R: Left coin switch
                    3: R: Enter (Begin Test) button
                    2: R: Up button
                    1: R: Down button
                    0: R: Escape (Service Credit) button
$3fe9     Byte     WPC_SW_ROW_INPUT
                    7-0: R: Readings for the currently selected switch column.
                    Bit 0 corresponds to row 1, bit 1 to row 2, and so on.
                    A '1' indicates active voltage level.  For a mechanical switch,
                    this means the switch is closed.  For an optical switch, this
                    means the switch is open.
$3fea     Byte     WPC_SW_COL_STROBE, W: Switch column enable
$3FEB     Byte     WPC_EXTBOARD1 (On DMD games, this is a general I/O that is used for machine-specific purposes)
$3FEC     Byte     WPC_EXTBOARD2 (On DMD games, this is a general I/O that is used for machine-specific purposes)
$3FED     Byte     WPC_EXTBOARD3 (On DMD games, this is a general I/O that is used for machine-specific purposes)
0x3FF0             WPC_ASIC_BASE
$3FF2     Byte     WPC_LEDS (7: R/W: The state of the diagnostic LED. >0=Off >1=On)
                    - blink once, it indicates a problem with the CPU ROM
                    - blink twice, the game RAM is faulty, or, again, traces, etc
                    - blink thrice, there's a problem with the ASIC, or again, traces, etc.
$3FF3     Byte     WPC_RAM_BANK    :
$3FF4     Byte     WPC_SHIFTADDRH
$3FF5     Byte     WPC_SHIFTADDRL
                    15-0: R/W: The base address for the bit shifter.
                    Writing to this address initializes the shifter.
                    Reading from this address after a shift command returns the
                    shifted address.
$3FF6     Byte     WPC_SHIFTBIT
                    7-0: W: Sets the bit position for a shift command.
                    7-0: R: Returns the output of the last shift command as a bitmask.
$3FF7     Byte     WPC_SHIFTBIT2
$3FF8     Byte     WPC_PERIPHERAL_TIMER_FIRQ_CLEAR R: bit 7 0=DMD, 1=SOUND? W: Clear FIRQ line
$3FF9     Byte     WPC_ROM_LOCK
$3FFA	    Byte	   WPC_CLK_HOURS_DAYS (7-0: R/W: The time-of-day hour counter)
$3FFB	    Byte	   WPC_CLK_MINS (7-0: R/W: The time-of-day minute counter)
$3FFC	    Byte	   WPC_ROM_BANK (5-0: R/W)
                    5-0: R/W: The page of ROM currently mapped into the banked region (0x4000-0x7FFF).
                    Pages 62 and 63 correspond to the uppermost 32KB, and are not normally mapped
                    because those pages are accessible in the fixed region (0x8000-0xFFFF).
                    Page numbers are consecutive.  Page 0 corresponds to the lowest address in a
                    1MB device.  If a smaller ROM is installed, the uppermost bits of this register
                    are effectively ignored.
$3FFD     Byte     WPC_RAM_LOCK
$3FFE     Byte     WPC_RAM_LOCKSIZE
$3FFF     Byte     WPC_ZEROCROSS_IRQ_CLEAR aka WPC_WATCHDOG
                    7: R: Set to 1 when AC is currently at a zero crossing, or 0 otherwise.
                    7: W: Writing a 1 here clears the source of the periodic timer interrupt.
                    4: R/W: Periodic timer interrupt enable
                    >0=Periodic IRQ disabled
                    >1=Periodic IRQ enabled
                    2: W: Writing a 1 here resets the watchdog.
*/