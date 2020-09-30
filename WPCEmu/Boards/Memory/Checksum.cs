using System.Linq;

namespace WPCEmu.Boards.Memory
{
    public static class Checksum
    {
        public struct ChecksumData
        {
            public ushort? dataStartOffset;
            public ushort? dataEndOffset;
            public ushort? checksumOffset;
            public string checksum;
            public string name;
        }

        const ushort INITIAL_VALUE = 0xFFFF;

        public static ushort checksum16(byte[] uint8Array)
        {
            ushort[] uint16Array = uint8Array.Select(b => (ushort)b).ToArray();
            ushort sum = uint16Array.Aggregate((total, currentValue) => (ushort) (total + currentValue));
            return (ushort) (INITIAL_VALUE - sum);
        }
    }
}
