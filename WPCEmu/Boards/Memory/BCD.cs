using System.Linq;

namespace WPCEmu.Boards.Memory
{
    public static class BCD
    {
        // based on https://gist.githubusercontent.com/joaomaia/3892692/raw/cb5eaef7ff9b6103490d4fc29b04cf95c5fde0b1/bcd2number.js

        /**
         * convert a BCD encoded Uint8Array and convert it to a number
         * @param {Uint8Array} bcd number to convert to number
         * @returns {Number} decoded number
         */
        public static long toNumber(byte[] bcd)
        {
            long n = 0;
            long m = 1;
            for (int i = 0; i < bcd.Length; i++)
            {
                n += (bcd[bcd.Length - 1 - i] & 0x0F) * m;
                n += ((bcd[bcd.Length - 1 - i] >> 4) & 0x0F) * m * 10;
                m *= 100;
            }
            return n;
        }

        /**
         * converts a number to a BCD encoded number (Uint8Array)
         * @param {Number} number to convert
         * @returns {Uint8Array} encoded BCD number
         */
        public static byte[] toBCD(long number)
        {
            byte[] bcd = Enumerable.Repeat((byte)0x00, 32).ToArray();

            int size = 0;
            while (number != 0)
            {
                bcd[size] = (byte)(number % 10);
                number = (number / 10) | 0;
                bcd[size] += (byte)((number % 10) << 4);
                number = (number / 10) | 0;
                size++;
            }
            // reverse byte order
            return bcd.Take(size).Reverse().ToArray();
        }
    }
}
