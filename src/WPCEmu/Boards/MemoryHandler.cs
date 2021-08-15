using System.Linq;
using System.Diagnostics;
using WPCEmu.Boards.Memory;

namespace WPCEmu.Boards
{
    public class MemoryHandler
    {
        /*
         * write to RAM of the WPC-EMU and optionally update checksum of certain parts
         *
         * Example
         * HIGHSCORE MM, starts at 0x1D29, ends at 0x1D48, 16bit checksum is at 0x1D49 and 0x1D4A, here is a dump
         *  0x42, 0x52, 0x45, 0x0, 0x44, 0x0, 0x0, 0x0, 0x4c, 0x46, 0x53, 0x0, 0x40, 0x0, 0x0, 0x0, 0x5a, 0x41, 0x50, 0x0, 0x36, 0x0, 0x0, 0x0, 0x52, 0x43, 0x46, 0x0, 0x32, 0x0, 0x0, 0x0, 0xfb, 0x8f
         *
         *  -> data is 0x42, 0x52, 0x45, 0x0, 0x44, 0x0, 0x0, 0x0, 0x4c, 0x46, 0x53, 0x0, 0x40, 0x0, 0x0, 0x0, 0x5a, 0x41, 0x50, 0x0, 0x36, 0x0, 0x0, 0x0, 0x52, 0x43, 0x46, 0x0, 0x32, 0x0, 0x0, 0x0
         *  -> checksum is 0xfb, 0x8f
         *     add 0xfb and 0x8f equals 1136 (0x470), checksum16 is calculated by 0xffff - (sum of bytes)
         */

        public byte[] ram;
        public ChecksumData[] checksumPositions;

        public static MemoryHandler getInstance(MemoryPosition? config, byte[] ram)
        {
            return new MemoryHandler(config, ram);
        }

        public MemoryHandler(MemoryPosition? config, byte[] ram)
        {
            if (config != null && config?.checksum != null)
            {
                checksumPositions = ((MemoryPosition)config).checksum.Where(entry =>
                {
                    return entry.dataStartOffset != null && entry.dataEndOffset != null && entry.checksumOffset != null;
                }).ToArray();
            }
            this.ram = ram;
            Debug.Print("MEMORY_WRITE_HANDLER_INITIALIZED");
        }

        /**
         * modify emulator memory
         * @param {Number} offset of memory, must be between 0..0x3FFF
         * @param {Number|String|Array} value to write to the memory location
         */
        public void writeMemory(ushort offset, object value)
        {
            if (value.GetType() == typeof(byte[]))
            {
                foreach (var tempByte in (byte[])value)
                {
                    ram[offset++] = tempByte;
                }
            }
            else if (value.GetType() == typeof(string))
            {
                foreach (var tempChar in (string)value)
                {
                    ram[offset++] = (byte) (tempChar - '0');
                }
            }
            else if (value.GetType() == typeof(byte))
            {
                ram[offset] = (byte)value;
            }

            if (checksumPositions != null)
            {
                var _needToUpdateChecksum = _needsChecksumUpdate(offset);

                if (_needToUpdateChecksum == null)
                {
                    return;
                }

                var needToUpdateChecksum = (ChecksumData)_needToUpdateChecksum;
                byte[] ramRangeToChecksum = ram.Skip((ushort)needToUpdateChecksum.dataStartOffset).Take((ushort)needToUpdateChecksum.dataEndOffset - (ushort)needToUpdateChecksum.dataStartOffset + 1).ToArray();
                ushort checksum = Checksum.checksum16(ramRangeToChecksum);
                ram[(ushort)needToUpdateChecksum.checksumOffset] = (byte)((checksum >> 8) & 0xFF);
                ram[(ushort)(needToUpdateChecksum.checksumOffset + 1)] = (byte)(checksum & 0xFF);
            }
        }

        ChecksumData? _needsChecksumUpdate(ushort offset)
        {
            foreach (var checksumPosition in checksumPositions)
            {
                if (offset >= checksumPosition.dataStartOffset && offset <= checksumPosition.dataEndOffset)
                {
                    return checksumPosition;
                }
            }
            return null;
        }
    }
}