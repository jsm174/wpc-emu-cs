namespace WPCEmu
{
    public struct RomObject
    {
        public ushort romSizeMBit;
        public byte[] systemRom;
        public string fileName;
        public byte[] gameRom;
        public ushort? gameIdMemoryLocation;
        public bool hasSecurityPic;
        public bool wpc95;
        public bool skipWpcRomCheck;
        public bool hasAlphanumericDisplay;
        public bool preDcsSoundboard;
        public MemoryPosition memoryPosition;
    }
}