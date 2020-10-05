namespace WPCEmu.Db
{
    public class CirqusVoltaire : IDb
    {
        public string name => "WPC-95: Cirqus Voltaire";
        public string version => "1.3";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "cv_10", "cv_11", "cv_13", "cv_14", "cirq_14", "cv_20h", "cv_20hc" },
            gameName = "Cirqus Voltaire",
            id = "cv"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "CV_G11.1_3"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "BACK BOX LUCK" },
            new SwitchMapping { id = "12", name = "WIRE RAM ENTER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT LOOP UPPER" },
            new SwitchMapping { id = "16", name = "TOP EDDY" },
            new SwitchMapping { id = "17", name = "RIGHT INLANE" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "RIGHT LOOP UPPER" },
            new SwitchMapping { id = "25", name = "INNER LOOP LEFT" },
            new SwitchMapping { id = "26", name = "LEFT INLANE" },
            new SwitchMapping { id = "27", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "28", name = "INNER LOOP RIGHT" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "POPPER OPTO" },
            new SwitchMapping { id = "37", name = "WOW TARGET" },
            new SwitchMapping { id = "38", name = "TOP TARGETS" },

            new SwitchMapping { id = "41", name = "LEFT LANE" },
            new SwitchMapping { id = "42", name = "RINGMASTER UP" },
            new SwitchMapping { id = "43", name = "RINGMASTER MID" },
            new SwitchMapping { id = "44", name = "RINGMASTER DOWN" },
            new SwitchMapping { id = "45", name = "LEFT RAMP MADE" },
            new SwitchMapping { id = "46", name = "TROUGH UPPER" },
            new SwitchMapping { id = "47", name = "TROUGH MIDDLE" },
            new SwitchMapping { id = "48", name = "LEFT LOOP ENTER" },

            new SwitchMapping { id = "51", name = "LEFT SLING" },
            new SwitchMapping { id = "52", name = "RIGHT SLING" },
            new SwitchMapping { id = "53", name = "UPPER JET" },
            new SwitchMapping { id = "54", name = "MIDDLE JET" },
            new SwitchMapping { id = "55", name = "LOWER JET" },
            new SwitchMapping { id = "56", name = "SKILL SHOT" },
            new SwitchMapping { id = "57", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "58", name = "RING N,G" },

            new SwitchMapping { id = "61", name = "LIGHT STANDUP" },
            new SwitchMapping { id = "62", name = "LOCK STANDUP" },
            new SwitchMapping { id = "63", name = "RAMP ENTER" },
            new SwitchMapping { id = "64", name = "RAMP MAGNET" },
            new SwitchMapping { id = "65", name = "RAMP MADE" },
            new SwitchMapping { id = "66", name = "RAMP LOCK LOW" },
            new SwitchMapping { id = "67", name = "RAMP LOCK MID" },
            new SwitchMapping { id = "68", name = "RAMP LOCK HIGH" },

            new SwitchMapping { id = "71", name = "LEFT SAUCER" },
            new SwitchMapping { id = "72", name = "RIGHT SAUCER" },
            new SwitchMapping { id = "74", name = "BIG BALL REBOUND" },
            new SwitchMapping { id = "75", name = "VOLT RIGHT" },
            new SwitchMapping { id = "76", name = "VOLT LEFT" },
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "RIGHT SPINNER" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F7", name = "LEFT SPINNER" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to imag
            image = "playfield-cv.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95",
        };

        public string[] cabinetColors => new string[]
        {
            "#A1C14B",
            "#67AA44",
            "#D6623C",
            "#9FD8E4",
            "#B7807C"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: 31, 32, 33, 34, 35, 36,
                "31",
                "F2", "F4", "F6", "F8"
            },
            initialAction = new InitialAction[]
            {
                new InitialAction
                {
                    delayMs = 1500,
                    source = "cabinetInput",
                    value = 16
                },
                new InitialAction
                {
                    delayMs = 10000,
                    source = "switchInput",
                    value = 22,
                },
                new InitialAction
                {
                    delayMs = 2000,
                    source = "switchInput",
                    value = 22,
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x45B, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x595, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A6, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AC, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B2, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x180C, name = "STAT_GAME_ID", type = "string" },
                new MemoryPositionData { offset = 0x1883, name = "STAT_GAMES_STARTED", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x1889, name = "STAT_TOTAL_PLAYS", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x188F, name = "STAT_TOTAL_FREE_PLAYS", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18BF, name = "STAT_MINUTES_ON", description = "Minutes powered on", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18B9, name = "STAT_PLAYTIME", description = "Minutes playing", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18C5, name = "STAT_BALLS_PLAYED", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18CB, name = "STAT_TILT_COUNTER", type = "uint8", length = 5 },
                new MemoryPositionData { offset = 0x18E9, name = "STAT_1_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18EF, name = "STAT_2_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18F5, name = "STAT_3_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18FB, name = "STAT_4_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 },

                new MemoryPositionData { offset = 0x1D83, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D86, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D8B, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D8E, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D93, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D96, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D9B, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D9E, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x174E, name = "HISCORE_CHAMP_NAME", description = "Cannon Ball Champion", type = "string" },
                new MemoryPositionData { offset = 0x1751, name = "HISCORE_CHAMP_SCORE", description = "Cannon Ball Champion", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1DA5, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1DA8, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => new string[]
        {
            "CHECK SWITCH 16 TOP EDDY",
            "CHECK SWITCH 42 RINGMASTER UP",
            "CHECK SWITCH 43 RINGMASTER MID",
            "CHECK SWITCH 44 RINGMASTER DOWN",
            "RINGMASTER ERROR NO MOTION",
        };
    }
}