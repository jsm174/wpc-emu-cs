using System.Collections.Generic;
using System.Linq;

namespace WPCEmu.Boards.Elements
{
    public class MemoryPatch
    {
        const ushort MEMORY_LIMIT = 0x4000;

        public struct Patch
        {
            public ushort offset;
            public byte value;
            public bool isVolatile;
        }

        Dictionary<ushort, Patch> patch;

        public static MemoryPatch GetInstance()
        {
            return new MemoryPatch();
        }

        public MemoryPatch()
        {
            patch = new Dictionary<ushort, Patch>();
        }

        public void addPatch(ushort memoryOffset, byte value, bool isVolatile = false)
        {
            patch.Add(memoryOffset, new Patch
            {
                offset = memoryOffset,
                value = value,
                isVolatile = isVolatile
            });
        }

        public void removePatch(ushort memoryOffset)
        {
            patch.Remove(memoryOffset);
        }

        public void removeVolatileEntries()
        {
            ushort[] entriesToRemove = patch.Where(kvp => kvp.Value.isVolatile)
                        .Select(kvp => kvp.Key)
                        .ToArray();

            foreach (ushort entry in entriesToRemove)
            {
                patch.Remove(entry);
            }
        }

        // memoryOffset is the unmapped memory address (0x0000 - 0xFFFF)
        public Patch? hasPatch(ushort memoryOffset)
        {
            if (patch.ContainsKey(memoryOffset))
            {
                return patch[memoryOffset];
            }
            return null;
        }

        public byte[] applyPatchesToExposedMemory(byte[] _ram)
        {
            byte[] ram = _ram.Take(_ram.Length).ToArray();

            foreach (var item in patch.Values)
            {
                if (item.offset < MEMORY_LIMIT)
                {
                    ram[item.offset] = item.value;
                }
            }

            return ram;
        }
    }
}
