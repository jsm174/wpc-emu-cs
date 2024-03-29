using System;

namespace WPCEmu.Boards.Elements
{
    public static class Bitmagic
    {
        public static byte findMsbBit(byte uint8Value)
        {
            int index = Array.IndexOf(new byte[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 }, uint8Value);
            return (byte)(index > -1 ? index + 1 : 0);
        }

        public static byte setMsbBit(byte uint8Value = 0)
        {
            return (uint8Value >= 0 && uint8Value <= 7) ? (new byte[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 })[uint8Value] : (byte) 0;
        }
    }
}
