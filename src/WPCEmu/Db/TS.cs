namespace WPCEmu.Db
{
    public class TheShadow : IDb
    {
        public string name => "WPC-S: The Shadow";
        public string version => "LX-5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "ts_pa1", "ts_pa2", "ts_la2", "ts_da2", "ts_la4", "ts_da4", "ts_lx4", "ts_dx4", "ts_lx5", "ts_dx5", "ts_la6", "ts_da6", "ts_lh6", "ts_lh6p", "ts_dh6", "ts_lf6", "ts_df6", "ts_lm6", "ts_dm6" },
            gameName = "Shadow, The",
            id = "ts"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "shad_x5.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "GUN TRIGGER" },
            new SwitchMapping { id = "12", name = "R PHURBA CONTROL" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "16", name = "RGHT RETURN LANE" },
            new SwitchMapping { id = "17", name = "LEFT RETURN LANE" },
            new SwitchMapping { id = "18", name = "LEFT OUTLANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "(M)ONGOL TARGET" },
            new SwitchMapping { id = "26", name = "M(O)NGOL TARGET" },
            new SwitchMapping { id = "27", name = "MONGO(L) TARGET" },
            new SwitchMapping { id = "28", name = "MONG(O)L TARGET" },

            new SwitchMapping { id = "31", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "32", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "33", name = "INNER SANCTUM" },
            new SwitchMapping { id = "34", name = "L PHURBA CONTROL" },
            new SwitchMapping { id = "35", name = "LEFT RUBBER" },
            new SwitchMapping { id = "36", name = "MINI KICKER" },
            new SwitchMapping { id = "37", name = "MINI LIMIT LEFT" },
            new SwitchMapping { id = "38", name = "MINI LIMIT RIGHT" },

            new SwitchMapping { id = "41", name = "TROUGH 1" },
            new SwitchMapping { id = "42", name = "TROUGH 2" },
            new SwitchMapping { id = "43", name = "TROUGH 3" },
            new SwitchMapping { id = "44", name = "TROUGH 4" },
            new SwitchMapping { id = "45", name = "TROUGH 5" },
            new SwitchMapping { id = "46", name = "TOP TROUGH" },
            new SwitchMapping { id = "47", name = "INNER LOOP ENTER" },
            new SwitchMapping { id = "48", name = "SHOOTER" },

            new SwitchMapping { id = "51", name = "WALL TARGET DOWN" },
            new SwitchMapping { id = "52", name = "MO(N)GOL TARGET" },
            new SwitchMapping { id = "53", name = "MON(G)OL TARGET" },
            new SwitchMapping { id = "54", name = "LEFT LOOP ENTER" },
            new SwitchMapping { id = "55", name = "BATTLE DROP DOWN" },
            new SwitchMapping { id = "56", name = "CENTER STANDUP" },
            new SwitchMapping { id = "57", name = "RIGHT LOOP ENTER" },
            new SwitchMapping { id = "58", name = "MINI EXIT TUBE" },

            new SwitchMapping { id = "61", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "62", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "63", name = "LOCKUP RIGHT" },
            new SwitchMapping { id = "64", name = "LOCKUP MIDDLE" },
            new SwitchMapping { id = "65", name = "LOCKUP LEFT" },
            new SwitchMapping { id = "66", name = "LEFT EJECT" },
            new SwitchMapping { id = "67", name = "RIGHT EJECT" },
            new SwitchMapping { id = "68", name = "POPPER" },

            new SwitchMapping { id = "71", name = "MINI L STANDUP 1" },
            new SwitchMapping { id = "72", name = "MINI L STANDUP 2" },
            new SwitchMapping { id = "73", name = "MINI L STANDUP 3" },
            new SwitchMapping { id = "74", name = "MINI L STANDUP 4" },
            new SwitchMapping { id = "75", name = "LFT RMP LFT MADE" },
            new SwitchMapping { id = "76", name = "LFT RMP RGT MADE" },
            new SwitchMapping { id = "77", name = "RGT RMP LFT MADE" },
            new SwitchMapping { id = "78", name = "RGT RMP RGT MADE" },

            new SwitchMapping { id = "81", name = "MINI R STANDUP 4" },
            new SwitchMapping { id = "82", name = "MINI R STANDUP 3" },
            new SwitchMapping { id = "84", name = "MINI R STANDUP 1" },
            new SwitchMapping { id = "85", name = "MINI DROP LEFT" },
            new SwitchMapping { id = "86", name = "MINI DRP MID LFT" },
            new SwitchMapping { id = "87", name = "MINI DRP MID RGT" },
            new SwitchMapping { id = "88", name = "MINI DROP RIGHT" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-ts.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#1D36B5",
            "#EA382D",
            "#57B034",
            "#8695F8"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "36", "37", "38", "41", "42", "43", "44", "45", "46", "47", "85", "86", "87", "88",
                "31", "32", "33", "36", "37", "38", "46", "47", "85", "86", "87", "88",
                "F2", "F4", "F6"
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

                new MemoryPositionData { offset = 0x441, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A6, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AC, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B2, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1D1E, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D21, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D26, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D29, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D2E, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D31, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D36, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D39, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D40, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D43, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}