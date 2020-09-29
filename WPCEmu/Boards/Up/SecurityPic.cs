using System.Linq;
using System.Diagnostics;
using System;

namespace WPCEmu.Boards.Up
{
    public class SecurityPic
    {
        const int PIC_SERIAL_SIZE = 16;
        const byte WPC_PIC_RESET = 0x00;
        const byte WPC_PIC_COUNTER = 0x0D;
        const byte WPC_PIC_UNLOCK = 0x20;
        const byte INITIAL_BYTE = 0xFF;
        const byte DEFAULT_SCRAMBLER_VALUE = 0xA5;

        // 123456: Serial number (it's that is registered on the labels)
        // 12345: Serial number n°2 (this number has no known utility)
        // 123: Unlock key of the matrix's contacts key (ALWAYS at the value of 123, it is used to generate the corresponding unlock code)
        const int MEDIEVAL_MADNESS_GAME_ID = 559;

        const string GAME_SERIAL_SUFFIX = " 123456 12345 123";

        byte[] unlockCode;
        int unlockCodeCounter;
        byte[] picSerialNumber;
        public byte serialNumberScrambler;
        public byte[] originalPicSerialNumber;
        public byte lastByteWrite;
        byte writesUntilUnlockNeeded;

        public static SecurityPic GetInstance(int machineNumber = MEDIEVAL_MADNESS_GAME_ID)
        {
            return new SecurityPic(machineNumber);
        }

        public SecurityPic(int machineNumber = MEDIEVAL_MADNESS_GAME_ID)
        {
            string gameSerialNumber = machineNumber + GAME_SERIAL_SUFFIX;
            Debug.Print("gameSerialNumber {0}", gameSerialNumber);

            unlockCode = new byte[3];
            unlockCodeCounter = 0;
            picSerialNumber = Enumerable.Repeat((byte)0x00, PIC_SERIAL_SIZE).ToArray();
            serialNumberScrambler = DEFAULT_SCRAMBLER_VALUE;

            picSerialNumber[10] = 0x12;
            picSerialNumber[2] = 0x34;

            uint tmp = (uint) (100 * (gameSerialNumber[1] - '0') +
                               10 * (gameSerialNumber[8] - '0') +
                               (gameSerialNumber[5] - '0'));
            tmp += (uint) (5 * picSerialNumber[10]);
            tmp = (tmp * 0x1BCD) + 0x1F3F0;
            picSerialNumber[1] = (byte) ((tmp >> 16) & 0xFF);
            picSerialNumber[11] = (byte) ((tmp >> 8) & 0xFF);
            picSerialNumber[9] = (byte) (tmp & 0xFF);

            tmp = (uint) (10000 * (gameSerialNumber[2] - '0') +
                          1000 * (gameSerialNumber[18] - '0') +
                          100 * (gameSerialNumber[0] - '0') +
                          10 * (gameSerialNumber[9] - '0') +
                          (gameSerialNumber[7] - '0'));
            tmp += (uint) (2 * this.picSerialNumber[10] + this.picSerialNumber[2]);
            tmp = (tmp * 0x107F) + 0x71E259;
            picSerialNumber[7] = (byte) ((tmp >> 24) & 0xFF);
            picSerialNumber[12] = (byte) ((tmp >> 16) & 0xFF);
            picSerialNumber[0] = (byte) ((tmp >> 8) & 0xFF);
            picSerialNumber[8] = (byte) (tmp & 0xFF);

            tmp = (uint) (1000 * (gameSerialNumber[19] - '0') +
                          100 * (gameSerialNumber[4] - '0') +
                          10 * (gameSerialNumber[6] - '0') +
                          (gameSerialNumber[17] - '0'));
            tmp += picSerialNumber[2];
            tmp = (tmp * 0x245) + 0x3D74;
            picSerialNumber[3] = (byte) ((tmp >> 16) & 0xFF);
            picSerialNumber[14] = (byte) ((tmp >> 8) & 0xFF);
            picSerialNumber[6] = (byte) (tmp & 0xFF);

            tmp = (uint) (10000 * ('9' - gameSerialNumber[15]) +
                          1000 * ('9' - gameSerialNumber[14]) +
                          100 * ('9' - gameSerialNumber[13]) +
                          10 * ('9' - gameSerialNumber[12]) +
                          ('9' - gameSerialNumber[11]));
            picSerialNumber[15] = (byte) ((tmp >> 8) & 0xFF);
            picSerialNumber[4] = (byte) (tmp & 0xFF);
            originalPicSerialNumber = picSerialNumber.ToArray();

            tmp = (uint) (100 * (gameSerialNumber[0] - '0') +
                          10 * (gameSerialNumber[1] - '0') +
                          (gameSerialNumber[2] - '0'));

            tmp = (uint) (((tmp >> 8) & 0xFF) * (0x100 * gameSerialNumber[17] + gameSerialNumber[19]) +
                          (tmp & 0xFF) * (0x100 * gameSerialNumber[18] + gameSerialNumber[17]));

            unlockCode[0] = (byte) ((tmp >> 16) & 0xFF);
            unlockCode[1] = (byte) ((tmp >> 8) & 0xFF);
            unlockCode[2] = (byte) ((tmp) & 0xFF);
        }

        public void reset()
        {
            lastByteWrite = INITIAL_BYTE;
            writesUntilUnlockNeeded = 0x20;
            picSerialNumber = originalPicSerialNumber.ToArray();

            Debug.Print("RESET SECURITY PIC");
        }

        public byte read(Func<byte, byte> getRowFunction = null)
        {
            if (lastByteWrite == WPC_PIC_COUNTER)
            {
                Debug.Print("R_WPC_PIC_COUNTER {0}", writesUntilUnlockNeeded);
                return writesUntilUnlockNeeded;
            }

            if (lastByteWrite >= 0x16 && lastByteWrite <= 0x1F)
            {
                byte col = (byte) (lastByteWrite - 0x15);
                return getRowFunction != null ? getRowFunction(col) : col;
            }

            if ((lastByteWrite & 0xF0) == 0x70)
            {
                byte ret = picSerialNumber[lastByteWrite & 0x0F];
                serialNumberScrambler = (byte) (((serialNumberScrambler >> 4) | (lastByteWrite << 4)) & 0xFF);
                picSerialNumber[5] = (byte) (((picSerialNumber[5] ^ serialNumberScrambler) + picSerialNumber[13]) & 0xFF);
                picSerialNumber[13] = (byte) (((picSerialNumber[13] + serialNumberScrambler) ^ picSerialNumber[5]) & 0xFF);
                Debug.Print("serialNumberScrambler {0}", serialNumberScrambler);
                return ret;
            }

            Debug.Print("UNKNOWN_READ");
            return 0;
        }

        public void write(byte data)
        {
            lastByteWrite = data;

            if (unlockCodeCounter > 0)
            {
                if (unlockCode[3 - unlockCodeCounter] == data)
                {
                    Debug.Print("Correct code sent to pic sent: {0}, codeW: {1}, expected: {2}", data, unlockCodeCounter, unlockCode[3 - unlockCodeCounter]);
                }
                else
                {
                    Debug.Print("Wrong code sent to pic sent: {0}, codeW: {1}, expected: {2}", data, unlockCodeCounter, unlockCode[3 - unlockCodeCounter]);
                }
                unlockCodeCounter--;
                return;
            }

            if (data == WPC_PIC_RESET)
            {
                serialNumberScrambler = DEFAULT_SCRAMBLER_VALUE;
                picSerialNumber[5] = (byte) (picSerialNumber[0] ^ picSerialNumber[15]);
                picSerialNumber[13] = (byte) (picSerialNumber[2] ^ picSerialNumber[12]);
                writesUntilUnlockNeeded = 0x20;
                Debug.Print("W_INIT_PIC {0}", serialNumberScrambler);
                return;
            }

            if (data == WPC_PIC_UNLOCK)
            {
                Debug.Print("W_WPC_PIC_UNLOCK");
                unlockCodeCounter = 3;
                return;
            }

            if (data == WPC_PIC_COUNTER)
            {
                Debug.Print("W_WPC_PIC_COUNTER");
                writesUntilUnlockNeeded = (byte) ((writesUntilUnlockNeeded - 1) & 0x1F);
                return;
            }
        }

        public byte getScrambler()
        {
            return serialNumberScrambler;
        }
    }
}
