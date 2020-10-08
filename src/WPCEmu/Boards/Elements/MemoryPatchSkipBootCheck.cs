using System.Diagnostics;

namespace WPCEmu.Boards.Elements
{
    public static class MemoryPatchSkipBootCheck
    {
        // Disable ROM checksum check when booting (U6)
        // NOTE: enabling this will make FreeWPC games crash!
        public static MemoryPatch run(MemoryPatch memoryPatch)
        {
            Debug.Print("add memorypatch");
            memoryPatch.addPatch(0xFFEC, 0x00);
            memoryPatch.addPatch(0xFFED, 0xFF);
            return memoryPatch;
        }
    }
}
