namespace WPCEmu.Db
{
    public class WhoDunnit : IDb
    {
        public string name => "WPC-S: WHO Dunnit";
        public string version => "1.2";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "wd_03r", "wd_048r", "wd_10r", "wd_10g", "wd_10f", "wd_11", "wd_12", "wd_12g" },
            gameName = "WHO Dunnit",
            id = "wd"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "whod1_2.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "3 BANK POS 2" },
            new SwitchMapping { id = "12", name = "SLOT INDEX LEFT" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "SHOOTER LANE" },
            new SwitchMapping { id = "16", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT INLANE" },
            new SwitchMapping { id = "18", name = "RIGHT LOOP" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY IN BUTTON" },
            new SwitchMapping { id = "25", name = "SLOT INDEX CNTR" },
            new SwitchMapping { id = "26", name = "LEFT INLANE" },
            new SwitchMapping { id = "27", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "28", name = "LEFT LOOP" },

            new SwitchMapping { id = "31", name = "TROUGH JAM" },
            new SwitchMapping { id = "32", name = "TROUGH 1" },
            new SwitchMapping { id = "33", name = "TROUGH 2" },
            new SwitchMapping { id = "34", name = "TROUGH 3" },
            new SwitchMapping { id = "35", name = "TROUGH 4" },
            new SwitchMapping { id = "36", name = "ENTER RAMP" },
            new SwitchMapping { id = "37", name = "MADE RAMP LEFT" },

            new SwitchMapping { id = "41", name = "TOP LEFT HOLE" },
            new SwitchMapping { id = "42", name = "POST JETS" },
            new SwitchMapping { id = "43", name = "BCK RIGHT POPPER" },
            new SwitchMapping { id = "44", name = "LOW RIGHT POPPER" },
            new SwitchMapping { id = "47", name = "EXTRA RIGHT HOLE" },
            new SwitchMapping { id = "48", name = "SLOT INDEX RIGHT" },

            new SwitchMapping { id = "51", name = "LOCK UP 1" },
            new SwitchMapping { id = "52", name = "TOP 4 BANK" },
            new SwitchMapping { id = "53", name = "2ND 4 BANK" },
            new SwitchMapping { id = "54", name = "3RD 4 BANK" },
            new SwitchMapping { id = "55", name = "BOT 4 BANK" },
            new SwitchMapping { id = "56", name = "MYSTERY TARGET" },
            new SwitchMapping { id = "57", name = "LOW RT LOCK 2" },
            new SwitchMapping { id = "58", name = "RED" },

            new SwitchMapping { id = "61", name = "LEFT SLING" },
            new SwitchMapping { id = "62", name = "RIGHT SLING" },
            new SwitchMapping { id = "63", name = "LEFT JET" },
            new SwitchMapping { id = "64", name = "BOTTOM JET" },
            new SwitchMapping { id = "65", name = "RIGHT JET" },
            new SwitchMapping { id = "66", name = "LEFT 3 BANK" },
            new SwitchMapping { id = "67", name = "CENTER 3 BANK" },
            new SwitchMapping { id = "68", name = "RIGHT 3 BANK" },

            new SwitchMapping { id = "71", name = "TOP 2 BANK" },
            new SwitchMapping { id = "72", name = "BOT 2 BANK" },
            new SwitchMapping { id = "73", name = "3 BANKS POS UP" },
            new SwitchMapping { id = "74", name = "UP DN RAMP" },
            new SwitchMapping { id = "75", name = "SCOOP CENTER" },
            new SwitchMapping { id = "76", name = "SCOOP RIGHT" },
            new SwitchMapping { id = "77", name = "SCOOP LEFT" },
            new SwitchMapping { id = "78", name = "BLACK" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "SPINNER" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-wd.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#ECCA47",
            "#CC3725",
            "#452965",
            "#7CCFDF"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "41", "42", "43", "44", "47", "48", "51", "52", "53"
                "31", "36", "37", "41", "42", "43", "44", "47", "48", "51", "52", "53"
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
                new MemoryPositionData { offset = 0x86, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x43E, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x5B6, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1CD9, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CDD, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CE2, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CE6, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CEB, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CEF, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CEB, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CF7, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CFF, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D03, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}