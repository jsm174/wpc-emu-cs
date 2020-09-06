namespace WPCEmu.Boards.Elements
{
    public static class MemoryProtection
    {
        // calculate memory protection, ripped from pinmame
        readonly static byte[] SWAP_NIBBLE = { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };

        public static ushort getMemoryProtectionMask(ushort value)
        {
            return (ushort) (0xFFFF & (SWAP_NIBBLE[value & 0x0F] + (value & 0xF0) + 0x10) << 8);
        }
    }
}
