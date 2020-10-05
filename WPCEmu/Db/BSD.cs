using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class BramStokersDracula : IDb
    {
        public string name => "WPC-Fliptronics: Bram Stoker's Dracula";
        public string version => "";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "drac_p11", "drac_p12", "drac_l1", "drac_d1", "drac_l2c" },
            gameName = "Bram Stoker's Dracula",
            id = "drac"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "dracu_l1.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "L DROP TARGET" },
            new SwitchMapping { id = "16", name = "L DROP SCORE" },
            new SwitchMapping { id = "17", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "TOP 3LANE L" },
            new SwitchMapping { id = "26", name = "TOP 3LANE M" },
            new SwitchMapping { id = "27", name = "TOP 3LANE R" },
            new SwitchMapping { id = "28", name = "R RAMP SCORE" },

            new SwitchMapping { id = "31", name = "UNDER SHOOT RAMP" },
            new SwitchMapping { id = "34", name = "LAUNCH BALL" },
            new SwitchMapping { id = "35", name = "LEFT DRAIN" },
            new SwitchMapping { id = "36", name = "LEFT RETURN" },
            new SwitchMapping { id = "37", name = "RIGHT RETURN" },
            new SwitchMapping { id = "38", name = "RIGHT DRAIN" },

            new SwitchMapping { id = "41", name = "TROUGH 1 BALL" },
            new SwitchMapping { id = "42", name = "TROUGH 2 BALLS" },
            new SwitchMapping { id = "43", name = "TROUGH 3 BALLS" },
            new SwitchMapping { id = "44", name = "TROUGH 4 BALLS" },
            new SwitchMapping { id = "48", name = "OUTHOLE" },

            new SwitchMapping { id = "51", name = "OPTO TR LANE" },
            new SwitchMapping { id = "52", name = "OPTO MAG LPOCKET" },
            new SwitchMapping { id = "53", name = "OPTO CASTLE 1" },
            new SwitchMapping { id = "54", name = "OPTO CASTLE 2" },
            new SwitchMapping { id = "55", name = "OPTO BL POPPER" },
            new SwitchMapping { id = "56", name = "OPTO TL POPPER" },
            new SwitchMapping { id = "57", name = "OPTO CASTLE 3" },
            new SwitchMapping { id = "58", name = "MYSTERY HOLE" },

            new SwitchMapping { id = "61", name = "LEFT JET" },
            new SwitchMapping { id = "62", name = "RIGHT JET" },
            new SwitchMapping { id = "63", name = "BOTTOM JET" },
            new SwitchMapping { id = "64", name = "LEFT SLING" },
            new SwitchMapping { id = "65", name = "RIGHT SLING" },
            new SwitchMapping { id = "66", name = "LEFT 3BANK T" },
            new SwitchMapping { id = "67", name = "LEFT 3BANK M" },
            new SwitchMapping { id = "68", name = "LEFT 3BANK B" },

            new SwitchMapping { id = "71", name = "OPTO CASTLE POP" },
            new SwitchMapping { id = "72", name = "OPTO COFFIN POP" },
            new SwitchMapping { id = "73", name = "OPTO LRAMP ENTRY" },
            new SwitchMapping { id = "77", name = "R RAMP UP" },

            new SwitchMapping { id = "81", name = "MAGNET LEFT" },
            new SwitchMapping { id = "82", name = "BALL ON MAGNET" },
            new SwitchMapping { id = "83", name = "MAGNET RIGHT" },
            new SwitchMapping { id = "84", name = "L RAMP SCORE" },
            new SwitchMapping { id = "85", name = "L RAMP DIVERTED" },
            new SwitchMapping { id = "86", name = "MID 3BANK L" },
            new SwitchMapping { id = "87", name = "MID 3BANK M" },
            new SwitchMapping { id = "88", name = "MID 3BANK R" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-bsd.jpg",
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#D43A33",
            "#5DACDD",
            "#E85D2B",
            "#5A4383"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                "41", "42", "43", "44",
                "51", "52", "53", "54", "55", "56", "57", "71", "72", "73",
                "F2", "F4"
            },
            initialAction = new InitialAction[]
            {
                new InitialAction
                {
                    delayMs = 1000,
                    source = "cabinetInput",
                    value = 16
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x88, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x3F7, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x1680, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1686, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x168C, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1692, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x180C, name = "STAT_GAME_ID", type = "string" },
                new MemoryPositionData { offset = 0x1883, name = "STAT_GAMES_STARTED", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x1889, name = "STAT_TOTAL_PLAYS", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x188F, name = "STAT_TOTAL_FREE_PLAYS", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18BF, name = "STAT_MINUTES_ON", description = "Minutes powered on", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18B9, name = "STAT_PLAYTIME", description = "Minutes playing", type = "uint8", length = 5 },
                new MemoryPositionData { offset = 0x18C5, name = "STAT_BALLS_PLAYED", type = "uint8", length = 5 },
                new MemoryPositionData { offset = 0x18CB, name = "STAT_TILT_COUNTER", type = "uint8", length = 5 },
                new MemoryPositionData { offset = 0x18E9, name = "STAT_1_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18EF, name = "STAT_2_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18F5, name = "STAT_3_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18FB, name = "STAT_4_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },

                new MemoryPositionData { offset = 0x1D17, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D1A, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D1F, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D22, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D27, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D2A, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D2F, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D32, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D39, name = "HISCORE_CHAMP_NAME", description = "Greatest Vampire Hunter", type = "string" },
                new MemoryPositionData { offset = 0x1D3C, name = "HISCORE_CHAMP_SCORE", description = "Greatest Vampire Hunter", type = "bcd", length = 5 },
            }
        };

        public string[] testErrors => null;
    }
}