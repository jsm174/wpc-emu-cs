using System;
using System.Diagnostics;
using System.Linq;
using WPCEmu.Boards;

namespace WPCEmu.Rom
{
    public static class RomParser
    {
        const uint WPC_ROM_SIZE_1MBIT = 128 * 1024;
        static readonly byte[] WPC_VALID_ROM_SIZES_IN_MBIT = { 1, 2, 4, 8 };
        const ushort SYSTEM_ROM_SIZE_BYTES = 32 * 1024;
        const ushort SYSTEM_ROM_CHECKSUM_CORRECTION_OFFSET = 0x7FEC;

        static readonly string[] PRE_DCS_SOUNDBOARD = { "wpcDmd", "wpcFliptronics" };

        public struct Roms
        {
            public byte[] u06;
        }

        public struct RomMetaData
        {
            public string fileName;
            public bool skipWpcRomCheck;
            public string[] features;
            public MemoryHandler.MemoryPositionData[] memoryPosition;
        }

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
            public MemoryHandler.MemoryPositionData[] memoryPosition;
        }

        public struct RomObject
        {
            public ushort romSizeMBit;
            public bool hasSecurityPic;
            public bool wpc95;
            public byte[] systemRom;
            public string fileName;
            public byte[] gameRom;
            public ushort? gameIdMemoryLocation;
            public bool skipWpcRomCheck;
            public bool hasAlphanumericDisplay;
            public bool preDcsSoundboard;
            public MemoryHandler.MemoryPosition memoryPosition;
        }

        static byte[] getCpuBoardSystemRom(byte[] u06Rom)
        {
            var systemRom = u06Rom.Skip(u06Rom.Length - SYSTEM_ROM_SIZE_BYTES).Take(SYSTEM_ROM_SIZE_BYTES).ToArray();
            Debug.Print("systemRom checksum correction {0}", systemRom[SYSTEM_ROM_CHECKSUM_CORRECTION_OFFSET]);
            Debug.Print("systemRom checksum {0}", systemRom[SYSTEM_ROM_CHECKSUM_CORRECTION_OFFSET + 1]);
            Debug.Print("systemRom version {0}", systemRom[SYSTEM_ROM_CHECKSUM_CORRECTION_OFFSET + 2]);
            return systemRom;
        }

        static byte[] getCpuBoardGameRom(byte[] u06Rom)
        {
            return u06Rom.Take(u06Rom.Length - SYSTEM_ROM_SIZE_BYTES).ToArray();
        }

        public static RomData parse(Roms? _uInt8Roms = null, RomMetaData? _metaData = null)
        {
            if (_uInt8Roms == null)
            {
                throw new Exception("INVALID_ROM_DATA");
            }

            var uInt8Roms = (Roms)_uInt8Roms;

            Debug.Print("LOAD_GAME_ROM");
            var u06 = uInt8Roms.u06;
            var romSizeMBit = (byte) (u06.Length / WPC_ROM_SIZE_1MBIT);
            Debug.Print("systemRom (u06) size: {0} bytes, mbit size: {1}", u06.Length, romSizeMBit);

            //var hasNotAlignedSize = !Number.isInteger(romSizeMBit);
            var hasInvalidSize = Array.IndexOf(WPC_VALID_ROM_SIZES_IN_MBIT, romSizeMBit) == -1;
            if (/*hasNotAlignedSize ||*/ hasInvalidSize)
            {
                Debug.Print("romSizeMBit {0}", romSizeMBit);
                throw new Exception("INVALID_ROM_SIZE");
            }

            // Contains the last 32 KiB of Game ROM
            var systemRom = getCpuBoardSystemRom(u06);
            var gameRom = getCpuBoardGameRom(u06);
            var gameIdMemoryLocation = GameId.search(gameRom, systemRom);
            var hasFeatures = _metaData != null && _metaData?.features != null;
            var romData = new RomData
            {
                romSizeMBit = romSizeMBit,
                systemRom = systemRom,
                gameRom = gameRom,
                gameIdMemoryLocation = gameIdMemoryLocation
            };

            if (_metaData != null)
            {
                var metaData = (RomMetaData)_metaData;
                romData.fileName = metaData.fileName != null ? metaData.fileName : "Unknown";
                romData.skipWpcRomCheck = metaData.skipWpcRomCheck;
                romData.hasSecurityPic = hasFeatures && Array.IndexOf(metaData.features, "securityPic") != -1;
                romData.wpc95 = hasFeatures && Array.IndexOf(metaData.features, "wpc95") != -1;
                romData.hasAlphanumericDisplay = hasFeatures && Array.IndexOf(metaData.features, "wpcAlphanumeric") != -1;
                romData.preDcsSoundboard = hasFeatures && (Array.IndexOf(metaData.features, PRE_DCS_SOUNDBOARD[0]) != -1 || Array.IndexOf(metaData.features, PRE_DCS_SOUNDBOARD[1]) != -1);
                romData.memoryPosition = metaData.memoryPosition;
            }
            else
            {
                romData.fileName = "Unknown";
            }

            return romData;
        }
    }
}