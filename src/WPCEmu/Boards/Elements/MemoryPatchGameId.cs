using System.Diagnostics;

namespace WPCEmu.Boards.Elements
{
    public static class MemoryPatchGameId
    {
        const byte MEDIEVALMADNESS_GAMEID_LO = 20;
        const byte MEDIEVALMADNESS_GAMEID_HI = 99;

        public static MemoryPatch run(MemoryPatch memoryPatch, ushort gameIdMemoryPosition)
        {
            Debug.Print("add memorypatch {0}", gameIdMemoryPosition);
            memoryPatch.addPatch(gameIdMemoryPosition, MEDIEVALMADNESS_GAMEID_LO);
            memoryPatch.addPatch((ushort) (gameIdMemoryPosition + 1), MEDIEVALMADNESS_GAMEID_HI);
            return memoryPatch;
        }
    }
}
