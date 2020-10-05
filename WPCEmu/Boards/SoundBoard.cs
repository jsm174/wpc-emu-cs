using System;
using System.Diagnostics;
using WPCEmu.Boards.Elements;

/**
 * All read/write requests from the CPU are first seen by the ASIC, which can
 * then either respond to it directly if it is an internal function, or forward
 * the request to another device
 * this file emulates the ASIC CHIP
 */

namespace WPCEmu.Boards
{
    public class SoundBoard
    {
        public struct State
        {
            public byte volume;
            public int readDataBytes;
            public int writeDataBytes;
            public int readControlBytes;
            public int writeControlBytes;
        };

        public static class OP
        {
            public const ushort WPC_LATCH_READ = 0x3000;
            public const ushort WPC_LATCH_WRITE = 0x3C00;

            public const ushort WPC_SOUND_CONTROL_STATUS = 0x3FDD;
            public const ushort WPC_SOUND_DATA = 0x3FDC;
        }

        SoundSerialInterface soundSerialInterface;
        int readDataBytes;
        int writeDataBytes;
        int readControlBytes;
        int writeControlBytes;
        public int resetCount;

        /**
         * Create a new instance of the sound board, compatible with preDCS, DCS and WPC-95 Sound Boards
         * @function
         * @param {Object} initObject JSON Configuration Object
         * @param {Array} initObject.romObject.preDcsSoundboard preDCS boards use 8bit to communicate with the CPU board, DCS and later use 16 bit
         * @return {SoundBoard} instance
         */

        public static SoundBoard getInstance(WpcCpuBoard.InitObject initObject)
        {
            return new SoundBoard(initObject);
        }

        public SoundBoard(WpcCpuBoard.InitObject initObject)
        {
            bool isPreDcsSoundBoard = initObject.romObject?.preDcsSoundboard == true;
            soundSerialInterface = SoundSerialInterface.getInstance(isPreDcsSoundBoard);
            readDataBytes = 0;
            writeDataBytes = 0;
            readControlBytes = 0;
            writeControlBytes = 0;
            resetCount = 0;
        }

        public void reset()
        {
            Debug.Print("RESET_SOUNDBOARD");
            soundSerialInterface.reset();
            readDataBytes = 0;
            writeDataBytes = 0;
            readControlBytes = 0;
            writeControlBytes = 0;
            resetCount++;
        }

        public State getState()
        {
            return new State
            {
                volume = soundSerialInterface.volume,
                readDataBytes = readDataBytes,
                writeDataBytes = writeDataBytes,
                readControlBytes = readControlBytes,
                writeControlBytes = writeControlBytes
            };
        }

        public bool? setState(State? _soundState = null)
        {
            if (_soundState == null)
            {
                return false;
            }
            var soundState = (State)_soundState;
            soundSerialInterface.volume = soundState.volume;
            readDataBytes = soundState.readDataBytes;
            writeDataBytes = soundState.writeDataBytes;
            readControlBytes = soundState.readControlBytes;
            writeControlBytes = soundState.writeControlBytes;
            return null;
        }

        public bool? registerSoundBoardCallback(Action<SoundBoardCallbackData> callbackFunction = null)
        {
            if (callbackFunction == null)
            {
                Debug.Print("ERROR: INVALID CALLBACK FUNCTION");
                return false;
            }
            soundSerialInterface.registerCallBack(callbackFunction);
            return null;
        }

        // Interface from CPU board
        public void writeInterface(ushort offset, byte value)
        {
            switch (offset)
            {
                case OP.WPC_SOUND_CONTROL_STATUS:
                    {
                        writeControlBytes++;
                        var needReset = soundSerialInterface.writeControl(value);
                        if (needReset)
                        {
                            reset();
                        }
                        break;
                    }

                case OP.WPC_SOUND_DATA:
                    if (resetCount > 20 && value == 0)
                    {
                        resetCount = 0;
                        Debug.Print("ignore first 0 after multiple soundboard reset!");
                        return;
                    }
                    Debug.Print("WRITE WPC_LATCH_WRITE {0}", value);
                    writeDataBytes++;
                    soundSerialInterface.writeData(value);
                    break;

                default:
                    Debug.Print("wpcemu:boards:sound-board W_NOT_IMPLEMENTED {0} {1}", "0x" + offset.ToString("X4"), value);
                    Debug.Print("SND_W_NOT_IMPLEMENTED {0} {1}", "0x" + offset.ToString("X4"), value);
                    break;
            }
        }

        public byte readInterface(ushort offset)
        {
            switch (offset)
            {
                case OP.WPC_SOUND_CONTROL_STATUS:
                    {
                        readControlBytes++;
                        var soundControlValue = soundSerialInterface.readControl();
                        Debug.Print("READ_WPC_SOUND_CONTROL_STATUS {0}", soundControlValue);
                        return soundControlValue;
                    }

                case OP.WPC_SOUND_DATA:
                    {
                        readDataBytes++;
                        var soundDataValue = soundSerialInterface.readData();
                        Debug.Print("READ WPC_LATCH_READ {0}", soundDataValue);
                        return soundDataValue;
                    }

                default:
                    Debug.Print("wpcemu:boards:sound-board R_NOT_IMPLEMENTED {0}", "0x" + offset.ToString("X4"));
                    Debug.Print("SND_R_NOT_IMPLEMENTED {0}", "0x" + offset.ToString("X4"));
                    return 0;
            }
        }
    }
}

/*
18:28:10.430 browser.js:133 wpcemu:boards:externalIO W_NOT_IMPLEMENTED +15s 0x3fdf 144
18:28:10.431 browser.js:133 wpcemu:boards:externalIO W_NOT_IMPLEMENTED +0ms 0x3fde 0
18:28:10.431 browser.js:133 wpcemu:boards:externalIO W_NOT_IMPLEMENTED +1ms 0x3fdc 1
18:28:10.432 browser.js:133 wpcemu:boards:externalIO W_NOT_IMPLEMENTED +0ms 0x3fdb 249
$2000-$37FF	Expansion (maybe security chip of WPC-S and WPC-95)
$3FC0-$3FDF	    External I/O control
                Address	  Format	 Description
                $3FC0     Byte     WPC_PARALLEL_STATUS_PORT
                $3FC1     Byte     WPC_PARALLEL_DATA_PORT
                $3FC2     Byte     WPC_PARALLEL_STROBE_PORT
                $3FC3     Byte     WPC_SERIAL_DATA_OUTPUT
                $3FC4     Byte     WPC_SERIAL_CONTROL_OUTPUT
                $3FC5     Byte     WPC_SERIAL_BAUD_SELECT
                $3FC6     Byte     WPC_TICKET_DISPENSE, Ticket dispenser board
                $3FD1     Byte     sound? only for GEN_WPCALPHA_1?
                $3FD4     Byte     WPC_FLIPTRONIC_PORT_A
                $3FD6     Byte     WPC_FLIPTRONIC_PORT_B (Ununsed)
                $3FDC     Byte     WPCS_DATA (7-0: R/W: Send/receive a byte of data to/from the sound board)
                                   WPC_SOUNDIF
                $3FDD     Byte     WPCS_CONTROL_STATUS aka WPC_SOUNDBACK
                                    7: R: WPC sound board read ready
                                    0: R: DCS sound board read ready
                                    or RW: R: Sound data availble, W: Reset soundboard ?
*/