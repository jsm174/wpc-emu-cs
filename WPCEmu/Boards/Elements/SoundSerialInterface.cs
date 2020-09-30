using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace WPCEmu.Boards.Elements
{
    public class SoundSerialInterface
    {
        public struct SoundBoardCallbackData
        {
            public string command;
            public ushort id;
            public byte channel;
            public byte value;
        };

        const byte SAMPLE_ID_STOP = 0x00;

        const byte DCS_VOLUME_COMMAND = 0x55;
        const byte DCS_VOLUME_GLOBAL = 0xAA;
        const ushort DCS_CHANNEL_0_OFF = 0x3E0;
        const ushort DCS_CHANNEL_1_OFF = 0x3E1;
        const ushort DCS_CHANNEL_2_OFF = 0x3E2;
        const ushort DCS_CHANNEL_3_OFF = 0x3E3;
        const ushort DCS_CHANNEL_4_OFF = 0x3E4;
        const ushort DCS_CHANNEL_5_OFF = 0x3E5;
        const ushort DCS_CHANNEL_6_OFF = 0x3E6;
        const ushort DCS_GET_MAIN_VERSION = 0x3E7;
        const ushort DCS_GET_MINOR_VERSION = 0x3E8;
        const ushort DCS_UNKNOWN3D2 = 0x3D2;
        const ushort DCS_UNKNOWN3D3 = 0x3D3;

        /*
        Note: there are some hardcoded sample id's related to the test menu. these samples
        should be present in the audio file and are not handled special.
        #define SND_TEST_DOWN			(0x3D4)
        #define SND_TEST_UP				(0x3D5)
        #define SND_TEST_ABORT		(0x3D6)
        #define SND_TEST_CONFIRM	(0x3D7)
        #define SND_TEST_ALERT		(0x3D8) // Coin Door open
        #define SND_TEST_HSRESET	(0x3D9)
        #define SND_TEST_CHANGE		(0x3DA)
        #define SND_TEST_ENTER		(0x3DB)
        #define SND_TEST_ESCAPE		(0x3DC)
        #define SND_TEST_SCROLL		(0x3DD)
        */

        const byte PREDCS_VOLUME_COMMAND = 0x79;
        const byte PREDCS_EXTENDED_COMMAND = 0x7A;

        const string COMMAND_PLAYSAMPLE = "PLAYSAMPLE";
        const string COMMAND_STOPSOUND = "STOPSOUND";
        const string COMMAND_MAINVOLUME = "MAINVOLUME";
        const string COMMAND_CHANNELOFF = "CHANNELOFF";

        const byte READ_NO_DATA_IS_AVAILABLE = 0x00;
        const byte READ_CONTROL_NO_DATA_IS_AVAILABLE = 0xFF;
        const byte READ_DATA_IS_AVAILABLE = 0x80;

        const byte DEFAULT_VOLUME = 8;

        bool isPreDcsSoundBoard;
        Action<SoundBoardCallbackData> soundBoardCallback;
        public byte volume;
        int lastUnknownControlWrite;
        Queue<byte> writeQueue;
        Queue<byte> readQueue;

        public static SoundSerialInterface GetInstance(bool isPreDcsSoundBoard)
        {
            return new SoundSerialInterface(isPreDcsSoundBoard);
        }

        public SoundSerialInterface(bool isPreDcsSoundBoard)
        {
            this.isPreDcsSoundBoard = isPreDcsSoundBoard;
            soundBoardCallback = (data) => { };
            writeQueue = new Queue<byte>();
            readQueue = new Queue<byte>();
            volume = DEFAULT_VOLUME;
            lastUnknownControlWrite = -1;
        }

        public void reset()
        {
            writeQueue.Clear();
            readQueue.Clear();
            volume = DEFAULT_VOLUME;
            _callbackStopAllSamples();
            lastUnknownControlWrite = -1;
        }

        public void registerCallBack(Action<SoundBoardCallbackData> callbackFunction)
        {
            soundBoardCallback = callbackFunction;
        }

        public byte readData()
        {
            if (readQueue.Count == 0)
            {
                Debug.Print("READ_DATA_EMPTY");
                return READ_NO_DATA_IS_AVAILABLE;
            }
            Debug.Print("READ_DATA");
            return readQueue.Dequeue();
        }

        public void writeData(byte value)
        {
            Debug.Print("DATA_WRITE {0}", value);
            writeQueue.Enqueue(value);

            if (isPreDcsSoundBoard)
            {
                _processPreDcsSoundCommand();
            }
            else
            {
                _processDcsSoundCommand();
            }
        }

        public byte readControl()
        {
            if (readQueue.Count == 0)
            {
                Debug.Print("READ_CONTROL_EMPTY");
                return READ_CONTROL_NO_DATA_IS_AVAILABLE;
            }
            Debug.Print("READ_CONTROL_AVAILABLE");
            return READ_DATA_IS_AVAILABLE;
        }

        public bool writeControl(byte value)
        {
            Debug.Print("CONTROL_WRITE {0}", value);
            if (value == 0x00)
            {
                //Reset is triggered by the sound-board
                return true;
            }

            if (value == 0x01)
            {
                // ignore write 1 - as the transition from 1 -> 0 triggers reset. we however only check if a 0 is written to reset
                return false;
            }

            if (value != lastUnknownControlWrite)
            {
                Debug.Print("SND: UNKNOWN CONTROL WRITE: {0}", value);
                lastUnknownControlWrite = value;
            }

            return false;
        }

        void _processPreDcsSoundCommand()
        {
            bool pendingVolumeCommand = writeQueue.ToArray()[0] == PREDCS_VOLUME_COMMAND;

            if (pendingVolumeCommand)
            {
                if (writeQueue.Count != 3)
                {
                    // WAIT UNTIL ALL BYTES ARRIVE
                    return;
                }
                byte? volume = SoundVolumeConvert.getRelativeVolumePreDcs(writeQueue.ToArray()[1], writeQueue.ToArray()[2]);
                if (volume != null)
                {
                    this.volume = (byte)volume;
                    _callbackMainVolume(this.volume);
                    Debug.Print("VOLUME_SET {0}", this.volume);
                }
                writeQueue.Clear();
                return;
            }

            bool isExtendedCommand = writeQueue.ToArray()[0] == PREDCS_EXTENDED_COMMAND;
            if (isExtendedCommand)
            {
                if (writeQueue.Count > 1)
                {
                    ushort extendedCommand = (ushort)((PREDCS_EXTENDED_COMMAND << 8) + writeQueue.ToArray()[1]);
                    _callbackPlaySample(extendedCommand);
                    writeQueue.Clear();
                }
            }
            else
            {
                _callbackPlaySample(writeQueue.ToArray()[0]);
                writeQueue.Clear();
            }
        }

        void _processDcsSoundCommand()
        {
            if (writeQueue.Count < 2)
            {
                return;
            }

            bool pendingVolumeCommand = writeQueue.ToArray()[0] == DCS_VOLUME_COMMAND;
            if (pendingVolumeCommand)
            {
                Debug.Print("-> pendingVolumeCommand {0}", writeQueue.Count);
                if (writeQueue.Count != 4)
                {
                    // WAIT UNTIL ALL BYTES ARRIVE
                    return;
                }

                bool changeGlobalVolume = writeQueue.ToArray()[1] == DCS_VOLUME_GLOBAL;
                if (changeGlobalVolume)
                {
                    byte? volume = SoundVolumeConvert.getRelativeVolumeDcs(writeQueue.ToArray()[2], writeQueue.ToArray()[3]);
                    if (volume != null)
                    {
                        this.volume = (byte)volume;
                        _callbackMainVolume(this.volume);
                        Debug.Print("VOLUME_SET {0}", this.volume);
                    }
                }
                else
                {
                    Debug.Print("ONLY GLOBAL VOLUME SUPPORTED NOW! ;(");
                }
                writeQueue.Clear();
                return;
            }

            ushort sampleId = (ushort)((writeQueue.ToArray()[0] << 8) | writeQueue.ToArray()[1]);
            switch (sampleId)
            {
                case SAMPLE_ID_STOP:
                    Debug.Print("-> STOP ALL SAMPLES");
                    _callbackStopAllSamples();
                    break;

                case DCS_CHANNEL_0_OFF:
                case DCS_CHANNEL_1_OFF:
                case DCS_CHANNEL_2_OFF:
                case DCS_CHANNEL_3_OFF:
                case DCS_CHANNEL_4_OFF:
                case DCS_CHANNEL_5_OFF:
                case DCS_CHANNEL_6_OFF:
                    {
                        byte channel = (byte)(sampleId - DCS_CHANNEL_0_OFF);
                        _callbackChannelOff(channel);
                        break;
                    }

                case DCS_UNKNOWN3D2: //SAFE CRACKER: need to reply else soundboard is reset
                case DCS_UNKNOWN3D3: //AFM + CONGO: need to reply else soundboard is reset
                    readQueue.Enqueue(0x1);
                    break;

                case DCS_GET_MAIN_VERSION:
                    Debug.Print("DCS_GET_MAIN_VERSION");
                    readQueue.Enqueue(0x10);
                    break;
                case DCS_GET_MINOR_VERSION:
                    Debug.Print("DCS_GET_MINOR_VERSION");
                    readQueue.Enqueue(0x1);
                    break;

                default:
                    _callbackPlaySample(sampleId);
                    break;
            }
            writeQueue.Clear();
        }

        void _callbackStopAllSamples()
        {
            soundBoardCallback(new SoundBoardCallbackData
            {
                command = COMMAND_STOPSOUND
            });
        }

        void _callbackPlaySample(ushort id)
        {
            if (id == SAMPLE_ID_STOP)
            {
                _callbackStopAllSamples();
                return;
            }
            soundBoardCallback(new SoundBoardCallbackData
            {
                command = COMMAND_PLAYSAMPLE,
                id = id
            });
        }

        void _callbackChannelOff(byte channel)
        {
            soundBoardCallback(new SoundBoardCallbackData
            {
                command = COMMAND_CHANNELOFF,
                channel = channel
            });
        }

        void _callbackMainVolume(byte value)
        {
            soundBoardCallback(new SoundBoardCallbackData
            {
                command = COMMAND_MAINVOLUME,
                value = value
            });
        }
    }
}
