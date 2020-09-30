using System;
using System.Text;
using System.Linq;
using WPCEmu.Boards.Elements;
using WPCEmu.Boards.Memory;

namespace WPCEmu.Boards
{
    public class UiState
    {
        const ushort DMD_PAGE_SIZE = 0x200;
        const int MAXIMAL_STRING_LENGTH = 32;

        const string ENCODING_BCD = "bcd";
        const string ENCODING_STRING = "string";
        const string ENCODING_UINT8 = "uint8";

        readonly string[] SUPPORTED_ENCODINGS = new string[] { ENCODING_STRING, ENCODING_UINT8, ENCODING_BCD };

        public struct MemoryPosition
        {
            public ushort? offset;
            public string description;
            public string type;
            public int? length;
            public object value;
        }

        public struct MemoryPositionInitObject
        {
            public MemoryPosition[] knownValues;
        }

        public struct State
        {
            public byte[] ram;
            public MemoryPosition[] memoryPosition;
            public SoundBoard.State sound;
            public CpuBoardAsic.State wpc;
            public OutputDmdDisplay.State dmd;
        }

        public struct OldState
        {
            public byte[][] videoRam;
            public byte[] dmdShadedBuffer;
            public byte[] lampState;
            public byte[] solenoidState;
            public byte[] inputState;
        };

        public MemoryPosition[] memoryPosition;
        OldState oldState;
        byte[][] videoRam;

        public static UiState GetInstance(MemoryPositionInitObject? memoryPosition = null)
        {
            return new UiState(memoryPosition);
        }

        public UiState(MemoryPositionInitObject? memoryPosition = null)
        {
            this.memoryPosition = null;

            if (memoryPosition != null && memoryPosition?.knownValues != null)
            {
                this.memoryPosition = memoryPosition?.knownValues.Where(entry =>
                {
                    return entry.offset != null && Array.IndexOf(SUPPORTED_ENCODINGS, entry.type) != -1;
                }).ToArray();
            }

            oldState = new OldState
            {
                videoRam = new byte[16][],
                dmdShadedBuffer = new byte[] { },
                lampState = new byte[] { },
                solenoidState = new byte[] { },
                inputState = new byte[] { }
            };

            videoRam = new byte[16][];
        }

        bool getVideoRamDiff(byte[] videoMemory)
        {
            bool changedFrames = false;
            for (var i = 0; i < 16; i++)
            {
                byte[] tempDmdFrame = videoMemory.Skip(i * DMD_PAGE_SIZE).Take(DMD_PAGE_SIZE).ToArray(); //i * DMD_PAGE_SIZE, (i + 1) * DMD_PAGE_SIZE);
                bool changedFrame = oldState.videoRam[i] != null ? !tempDmdFrame.SequenceEqual(oldState.videoRam[i]) : true;

                if (changedFrame)
                {
                    changedFrames = true;
                    videoRam[i] = tempDmdFrame;
                    oldState.videoRam[i] = tempDmdFrame.Take(tempDmdFrame.Length).ToArray();
                }
            }
            return changedFrames;
        }

        MemoryPosition[] parseMemoryPosition(byte[] ram)
        {
            if (memoryPosition == null)
            {
                return null;
            }
            return memoryPosition.Select(entry => {
                switch (entry.type)
                {
                    case ENCODING_UINT8:
                        var length = (entry.length != null) ? entry.length : 1;
                        if (length == 1)
                        {
                            entry.value = ram[(ushort)entry.offset];
                        }
                        else
                        {
                            UInt32 value = 0;
                            for (var n = 0; n < length; n++)
                            {
                                if (entry.offset + n < ram.Length)
                                {
                                    byte shl = (byte)((length - n - 1) * 8);
                                    value += (UInt32)(ram[(ushort)entry.offset + n] << shl);
                                }
                            }
                            entry.value = value;
                        }
                        break;

                    case ENCODING_STRING:
                        var offset = (ushort)entry.offset;
                        var dump = "";
                        while (ram[offset] > 31 && ram[offset] < 128 && dump.Length < MAXIMAL_STRING_LENGTH)
                        {
                            dump += Encoding.UTF8.GetString(new byte[] { ram[offset++] });
                        }
                        entry.value = dump;
                        break;

                    case ENCODING_BCD:
                        var bcdLength = entry.length != null ? (int)entry.length : 2;
                        var number = Bcd.toNumber(ram.Skip((ushort)entry.offset).Take(bcdLength).ToArray());
                        entry.value = number;
                        break;

                    default:
                        entry.value = "TYPE_INVALID";
                        break;
                }

                return entry;
            }).ToArray();
        }

        public State getChangedAsicState(WpcCpuBoard.Asic state, bool includeExpensiveData = true)
        {
            var display = (OutputDmdDisplay.State)state.display;

            bool dmdShadedBufferChanged = !display.dmdShadedBuffer.SequenceEqual(oldState.dmdShadedBuffer);
            bool lampStateChanged = !state.wpc.lampState.SequenceEqual(oldState.lampState);
            bool solenoidStateChanged = !state.wpc.solenoidState.SequenceEqual(oldState.solenoidState);
            bool inputStateChanged = !state.wpc.inputState.SequenceEqual(oldState.inputState);

            if (dmdShadedBufferChanged)
            {
                oldState.dmdShadedBuffer = display.dmdShadedBuffer.Take(display.dmdShadedBuffer.Length).ToArray();
            }
            if (lampStateChanged)
            {
                oldState.lampState = state.wpc.lampState.Take(state.wpc.lampState.Length).ToArray();
            }
            if (solenoidStateChanged)
            {
                oldState.solenoidState = state.wpc.solenoidState.Take(state.wpc.solenoidState.Length).ToArray();
            }
            if (inputStateChanged)
            {
                oldState.inputState = state.wpc.inputState.Take(state.wpc.inputState.Length).ToArray();
            }

            bool videoRamChanged = includeExpensiveData == false ? false : getVideoRamDiff(((OutputDmdDisplay.State)state.display).videoRam);
            MemoryPosition[] memoryPosition = parseMemoryPosition(state.ram);
            byte[] ram = includeExpensiveData == false ? null : state.ram;

            return new State
            {
                ram = ram,
                memoryPosition = memoryPosition,
                sound = state.sound,
                wpc = new CpuBoardAsic.State
                {
                    diagnosticLed = state.wpc.diagnosticLed,
                    lampState = lampStateChanged ? state.wpc.lampState : null,
                    solenoidState = solenoidStateChanged ? state.wpc.solenoidState : null,
                    generalIlluminationState = state.wpc.generalIlluminationState,
                    inputState = inputStateChanged ? state.wpc.inputState : null,
                    diagnosticLedToggleCount = state.wpc.diagnosticLedToggleCount,
                    midnightModeEnabled = state.wpc.midnightModeEnabled,
                    irqEnabled = state.wpc.irqEnabled,
                    activeRomBank = state.wpc.activeRomBank,
                    time = state.wpc.time,
                    blankSignalHigh = state.wpc.blankSignalHigh,
                    watchdogExpiredCounter = state.wpc.watchdogExpiredCounter,
                    watchdogTicks = state.wpc.watchdogTicks,
                    zeroCrossFlag = state.wpc.zeroCrossFlag,
                    inputSwitchMatrixActiveColumn = state.wpc.inputSwitchMatrixActiveColumn,
                    lampRow = state.wpc.lampRow,
                    lampColumn = state.wpc.lampColumn,
                    wpcSecureScrambler = state.wpc.wpcSecureScrambler,
                },
                dmd = new OutputDmdDisplay.State
                {
                    scanline = display.scanline,
                    dmdPageMapping = display.dmdPageMapping,
                    activepage = display.activepage,
                    videoRam = videoRamChanged ? videoRam.SelectMany(b => b).ToArray() : null,
                    dmdShadedBuffer = dmdShadedBufferChanged ? display.dmdShadedBuffer : null,
                    videoOutputBuffer = display.videoOutputBuffer,
                    nextActivePage = display.nextActivePage,
                    requestFIRQ = display.requestFIRQ,
                    ticksUpdateDmd = display.ticksUpdateDmd,
                }
            };
        }
    }
}