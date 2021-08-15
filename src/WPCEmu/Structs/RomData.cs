namespace WPCEmu
{
    public struct RomData
    {
        public ushort romSizeMBit;
        public byte[] systemRom;
        public byte[] gameRom;
        public ushort? gameIdMemoryLocation;
        public string fileName;
        public bool skipWpcRomCheck;
        public bool hasSecurityPic;
        public bool wpc95;
        public bool hasAlphanumericDisplay;
        public bool preDcsSoundboard;
        public MemoryPositionData[] memoryPosition;
    }
}