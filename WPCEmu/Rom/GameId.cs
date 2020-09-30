using System;
using System.Diagnostics;

namespace WPCEmu.Rom
{
    public static class GameId
    {
        const byte MAGIC_SEQUENCE_LENGTH = 7;

        const byte PREFIX_MAGIC_1 = 0xEC;
        const byte PREFIX_MAGIC_2 = 0x9F;
        const byte POSTFIX_MAGIC_1 = 0x83;
        const byte POSTFIX_MAGIC_2 = 0x12;
        const byte POSTFIX_MAGIC_3 = 0x34;

        /**
         * gameRom is the ROM file without the last 32kb (the systemRom)
         * systemRom is the bootable ROM (APPLE OS)
         */
        public static ushort? search(byte[] gameRom, byte[] systemRom)
        {
            Debug.Print("SEARCH GAME ID IN ROM");
            int foundGameIdLink = 0;
            for (var x = 0; x < gameRom.Length - MAGIC_SEQUENCE_LENGTH; x++)
            {
                var prefixMatches = gameRom[x] == PREFIX_MAGIC_1 && gameRom[x + 1] == PREFIX_MAGIC_2;
                if (prefixMatches)
                {
                    var suffixMatches = gameRom[x + 4] == POSTFIX_MAGIC_1 &&
                      gameRom[x + 5] == POSTFIX_MAGIC_2 &&
                      gameRom[x + 6] == POSTFIX_MAGIC_3;
                    if (suffixMatches && ++foundGameIdLink == 2)
                    {
                        var gameIdMemoryPosition = (ushort) ((gameRom[x + 2] << 8) + gameRom[x + 3]);
                        gameIdMemoryPosition -= 0x8000;
                        if (gameIdMemoryPosition < 0)
                        {
                            throw new Exception("INVALID_MEMORY_POSITION_" + gameIdMemoryPosition);
                        }
                        var gameIdMemoryLocation = (ushort) ((systemRom[gameIdMemoryPosition] << 8) + systemRom[gameIdMemoryPosition + 1]);
                        Debug.Print("GAMEID_FOUND {0}", gameIdMemoryLocation);
                        Debug.Print("GAMEID_FOUND {0}", gameIdMemoryLocation);
                        return gameIdMemoryLocation;
                    }
                }
            }
            Debug.Print("GAME ID NOT FOUND");
            return null;
        }
    }
}