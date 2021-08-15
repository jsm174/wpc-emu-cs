namespace WPCEmu.Db
{
    public class JackBot : IDb
    {
        public string name => "WPC-S: Jack·Bot";
        public string version => "1.0R";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "jb_10r", "jb_101r", "jb_10b", "jb_101b" },
            gameName = "Jack*Bot",
            id = "jb"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "jack1_0r.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "L LEFT 10 POINT" },
            new SwitchMapping { id = "12", name = "U LEFT 10 POINT" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RAMP IS DOWN" },
            new SwitchMapping { id = "16", name = "HIGH DROP TARGET" },
            new SwitchMapping { id = "17", name = "CENT DROP TARGET" },
            new SwitchMapping { id = "18", name = "LOW DROP TARGET" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY EXTRA BALL" },
            new SwitchMapping { id = "25", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "26", name = "L FLIPPER LANE" },
            new SwitchMapping { id = "27", name = "R FLIPPER LANE" },
            new SwitchMapping { id = "28", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "31", name = "TROUGH JAM" },
            new SwitchMapping { id = "32", name = "TROUGH 1 (RIGHT)" },
            new SwitchMapping { id = "33", name = "TROUGH 2" },
            new SwitchMapping { id = "34", name = "TROUGH 3" },
            new SwitchMapping { id = "35", name = "TROUGH 4 (LEFT)" },
            new SwitchMapping { id = "36", name = "RAMP EXIT" },
            new SwitchMapping { id = "37", name = "RAMP ENTRANCE" },
            new SwitchMapping { id = "38", name = "TARG UNDER RAMP" },

            new SwitchMapping { id = "41", name = "VISOR 1 (LEFT)" },
            new SwitchMapping { id = "42", name = "VISOR 2" },
            new SwitchMapping { id = "43", name = "VISOR 3" },
            new SwitchMapping { id = "44", name = "VISOR 4" },
            new SwitchMapping { id = "45", name = "VISOR 5 (RIGHT)" },
            new SwitchMapping { id = "46", name = "GAME SAUCER" },
            new SwitchMapping { id = "47", name = "LEFT EJECT HOLE" },
            new SwitchMapping { id = "48", name = "RIGHT EJECT HOLE" },

            new SwitchMapping { id = "51", name = "5-BANK 1 (UPPER)" },
            new SwitchMapping { id = "52", name = "5-BANK TARGET 2" },
            new SwitchMapping { id = "53", name = "5-BANK TARGET 3" },
            new SwitchMapping { id = "54", name = "5-BANK TARGET 4" },
            new SwitchMapping { id = "55", name = "5-BANK 5 (LOWER)" },
            new SwitchMapping { id = "56", name = "VORTEX UPPER" },
            new SwitchMapping { id = "57", name = "VORTEX CENTER" },
            new SwitchMapping { id = "58", name = "VORTEX LOWER" },

            new SwitchMapping { id = "61", name = "UPPER JET BUMPER" },
            new SwitchMapping { id = "62", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "63", name = "LOWER JET BUMPER" },
            new SwitchMapping { id = "64", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "65", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "66", name = "RIGHT 10 POINT" },
            new SwitchMapping { id = "67", name = "HIT ME TARGET" },
            new SwitchMapping { id = "68", name = "BALL SHOOTER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "VISOR IS CLOSED" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "VISOR IS OPEN" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-jackbot.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#F9DF4C",
            "#E53F28",
            "#888888",
            "#DA6C2A"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22", "31",
                //OPTO SWITCHES: "16", "17", "18", "41", "42", "43", "44", "45"
                "16", "17", "18"
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
                new MemoryPositionData { offset = 0x7A, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x43E, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x16A1, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A7, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AD, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B3, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1D5F, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D63, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D68, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D6C, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D71, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D75, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D7A, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D7E, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D85, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D89, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}