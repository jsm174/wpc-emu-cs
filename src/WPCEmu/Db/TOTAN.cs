namespace WPCEmu.Db
{
    public class TalesOfTheArabianNights : IDb
    {
        public string name => "WPC-95: Tales of the Arabian Nights";
        public string version => "1.4";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "totan_04", "totan_12", "totan_13", "totan_14", "totan_15c" },
            gameName = "Tales of the Arabian Nights",
            id = "totan"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "an_g11.1_4"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "HAREM PASSAGE" },
            new SwitchMapping { id = "12", name = "VANISH TUNNEL" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RAMP ENTER" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT INLANE" },
            new SwitchMapping { id = "18", name = "BALL SHOOTER" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "GENIE STANDUP" },
            new SwitchMapping { id = "25", name = "BAZAAR EJECT" },
            new SwitchMapping { id = "26", name = "LEFT INLANE" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "LEFT WIRE MAKE" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "LEFT CAGE OPTO" },
            new SwitchMapping { id = "37", name = "RIGHT CAGE OPTO" },
            new SwitchMapping { id = "38", name = "LEFT EJECT" },

            new SwitchMapping { id = "41", name = "RAMP MADE LEFT" },
            new SwitchMapping { id = "42", name = "GENIE TARGET" },
            new SwitchMapping { id = "43", name = "LEFT LOOP" },
            new SwitchMapping { id = "44", name = "INNER LOOP LEFT" },
            new SwitchMapping { id = "45", name = "INNER LOOP RGT" },
            new SwitchMapping { id = "46", name = "MINI STANDUPS" },
            new SwitchMapping { id = "47", name = "RAMP MADE RGT" },
            new SwitchMapping { id = "48", name = "RGT CAPTIVE BALL" },

            new SwitchMapping { id = "51", name = "LEFT SLING" },
            new SwitchMapping { id = "52", name = "RIGHT SLING" },
            new SwitchMapping { id = "53", name = "LEFT JET" },
            new SwitchMapping { id = "54", name = "RIGHT JET" },
            new SwitchMapping { id = "55", name = "MIDDLE JET" },
            new SwitchMapping { id = "56", name = "LAMP SPIN CCW" },
            new SwitchMapping { id = "57", name = "LAMP SPIN CW" },
            new SwitchMapping { id = "58", name = "LFT CAPTIVE BALL" },

            new SwitchMapping { id = "61", name = "LEFT STANDUPS" },
            new SwitchMapping { id = "62", name = "RIGHT STANDUPS" },
            new SwitchMapping { id = "63", name = "TOP SKILL" },
            new SwitchMapping { id = "64", name = "MIDDLE SKILL" },
            new SwitchMapping { id = "65", name = "BOTTOM SKILL" },
            new SwitchMapping { id = "66", name = "LOCK 1 (BOT)" },
            new SwitchMapping { id = "67", name = "LOCK 2" },
            new SwitchMapping { id = "68", name = "LOCK 3 (TOP)" }
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

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-totan.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#E9B840",
            "#285E8F",
            "#DB3125",
            "#BC9248"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37",
                "31", "36", "37",
                "F2", "F4", "F6", "F8"
            },
            initialAction = new InitialAction[]
            {
                new InitialAction
                {
                    delayMs = 1000,
                    source = "cabinetInput",
                    value = 16
                },
                new InitialAction
                {
                    description = "enable free play",
                    delayMs = 3000,
                    source = "writeMemory",
                    offset = 0x1C76,
                    value = 0x01
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x7A, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x43E, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x58E, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16A7, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16AE, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16B5, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

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

                new MemoryPositionData { offset = 0x1BF8, name = "GAME_BALL_TOTAL", description = "Balls per game", type = "uint8" },
                new MemoryPositionData { offset = 0x1C76, name = "STAT_FREEPLAY", description = "0: not free, 1: free", type = "uint8" },

                new MemoryPositionData { offset = 0x1D5F, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D62, name = "HISCORE_1_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1D68, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D6B, name = "HISCORE_2_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1D71, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D74, name = "HISCORE_3_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1D7A, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D7D, name = "HISCORE_4_SCORE", type = "bcd", length = 6 },

                new MemoryPositionData { offset = 0x1D85, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D88, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 6 }
            }
        };

        public string[] testErrors => null;
    }
}