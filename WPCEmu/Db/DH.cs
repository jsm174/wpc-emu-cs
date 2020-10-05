namespace WPCEmu.Db
{
    public class DirtyHarry : IDb
    {
        public string name => "WPC-S: Dirty Harry";
        public string version => "LX-2";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "dh_lx2", "dh_dx2", "dh_lf2" },
            gameName = "Dirty Harry",
            id = "dh"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "harr_lx2.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "GUN HANDLE TRIGGER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "SHOOTER LANE" },
            new SwitchMapping { id = "16", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT INLANE" },
            new SwitchMapping { id = "18", name = "STANDUP 8" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "EX BALL BUTTON" },
            new SwitchMapping { id = "25", name = "LEFT INLANE" },
            new SwitchMapping { id = "26", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "27", name = "STANDUP 1" },
            new SwitchMapping { id = "28", name = "STANDUP 2" },

            new SwitchMapping { id = "31", name = "TROUGH JAM" },
            new SwitchMapping { id = "32", name = "TROUGH 1" },
            new SwitchMapping { id = "33", name = "TROUGH 2" },
            new SwitchMapping { id = "34", name = "TROUGH 3" },
            new SwitchMapping { id = "35", name = "TROUGH 4" },
            new SwitchMapping { id = "38", name = "RIGHT RAMP MAKE" },

            new SwitchMapping { id = "41", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "42", name = "RIGHT LOOP" },
            new SwitchMapping { id = "43", name = "LEFT RAMP MAKE" },
            new SwitchMapping { id = "44", name = "GUN CHAMBER" },
            new SwitchMapping { id = "45", name = "GUN POPPER" },
            new SwitchMapping { id = "46", name = "TOP R POPPER" },
            new SwitchMapping { id = "47", name = "LEFT POPPER" },

            new SwitchMapping { id = "51", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "53", name = "DROP TARGET DOWN" },
            new SwitchMapping { id = "54", name = "STANDUP 6" },
            new SwitchMapping { id = "55", name = "STANDUP 7" },
            new SwitchMapping { id = "56", name = "STANDUP 5" },
            new SwitchMapping { id = "57", name = "STANDUP 4" },
            new SwitchMapping { id = "58", name = "STANDUP 3" },

            new SwitchMapping { id = "61", name = "LEFT SLING" },
            new SwitchMapping { id = "62", name = "RIGHT SLING" },
            new SwitchMapping { id = "63", name = "LEFT JET" },
            new SwitchMapping { id = "64", name = "MIDDLE JET" },
            new SwitchMapping { id = "65", name = "RIGHT JET" },
            new SwitchMapping { id = "66", name = "LEFT ROLLOVER" },
            new SwitchMapping { id = "67", name = "MIDDLE ROLLOVER" },
            new SwitchMapping { id = "68", name = "RIGHT ROLLOVER" },

            new SwitchMapping { id = "71", name = "LEFT LOOP" },
            new SwitchMapping { id = "73", name = "TOP L POPPER" },
            new SwitchMapping { id = "76", name = "GUN POSITION" },
            new SwitchMapping { id = "77", name = "GUN LOOKUP" },

            new SwitchMapping { id = "88", name = "TEST SWITCH" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to imag
            image = "playfield-dh.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#4363AF",
            "#C1616C",
            "#CF8765",
            "#CC984E"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: 31, 32, 33, 34, 35, 38, 41, 42, 43, 44, 45, 46, 47,
                "31", "38", "41", "42", "43", "44", "45", "46", "47",
                "F2", "F4",
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

                new MemoryPositionData { offset = 0x43D, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1CE3, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CE7, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CEC, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CF0, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D09, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D0D, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 },
            }
        };

        public string[] testErrors => null;
    }
}