using System;

namespace WPCEmu.Boards.Mapper
{
    public static class Dmd
    {
        public struct Model
        {
            public ushort offset;
            public string subsystem;
            public byte? bank;
        }

        const ushort MEMORY_ADDR_DMD_PAGE3000 = 0x3000;
        const ushort MEMORY_ADDR_DMD_PAGE3200 = 0x3200;
        const ushort MEMORY_ADDR_DMD_PAGE3400 = 0x3400;
        const ushort MEMORY_ADDR_DMD_PAGE3600 = 0x3600;
        const ushort MEMORY_ADDR_DMD_PAGE_LOW = 0x3800;
        const ushort MEMORY_ADDR_DMD_PAGE_HIGH = 0x3A00;

        public const string SUBSYSTEM_DMD_VIDEORAM = "videoram";
        public const string SUBSYSTEM_CMD = "command";

        static Model buildReturnModel(ushort offset, string subsystem, byte? bank)
        {
            return new Model
            {
                offset = offset,
                subsystem = subsystem,
                bank = bank
            };
        }

        public static Model getAddress(int? offset)
        {
            if (!offset.HasValue) 
            {
                throw new Exception("DMD_GET_ADDRESS_UNDEFINED");
            }
            if (offset < MEMORY_ADDR_DMD_PAGE3000)
            {
                throw new Exception("INVALID_DMD_ADDRESSRANGE_" + offset);
            }

            offset &= 0xFFFF;
            if (offset < MEMORY_ADDR_DMD_PAGE3200)
            {
                return buildReturnModel((ushort)(offset - MEMORY_ADDR_DMD_PAGE3000), SUBSYSTEM_DMD_VIDEORAM, 2);
            }
            if (offset < MEMORY_ADDR_DMD_PAGE3400)
            {
                return buildReturnModel((ushort)(offset - MEMORY_ADDR_DMD_PAGE3200), SUBSYSTEM_DMD_VIDEORAM, 3);
            }
            if (offset < MEMORY_ADDR_DMD_PAGE3600)
            {
                return buildReturnModel((ushort)(offset - MEMORY_ADDR_DMD_PAGE3400), SUBSYSTEM_DMD_VIDEORAM, 4);
            }
            if (offset < MEMORY_ADDR_DMD_PAGE_LOW)
            {
                return buildReturnModel((ushort)(offset - MEMORY_ADDR_DMD_PAGE3600), SUBSYSTEM_DMD_VIDEORAM, 5);
            }
            if (offset < MEMORY_ADDR_DMD_PAGE_HIGH)
            {
                return buildReturnModel((ushort)(offset - MEMORY_ADDR_DMD_PAGE_LOW), SUBSYSTEM_DMD_VIDEORAM, 0);
            }
            if (offset < MEMORY_ADDR_DMD_PAGE_HIGH + 0x200)
            {
                return buildReturnModel((ushort)(offset - MEMORY_ADDR_DMD_PAGE_HIGH), SUBSYSTEM_DMD_VIDEORAM, 1);
            }
            return buildReturnModel((ushort)offset, SUBSYSTEM_CMD, null);
        }
    }
}