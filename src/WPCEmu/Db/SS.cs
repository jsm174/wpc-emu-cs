namespace WPCEmu.Db
{
    public class ScaredStiff : IDb
    {
        public string name => "WPC-95: Scared Stiff";
        public string version => "1.5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "ss_01", "ss_01b", "ss_03", "ss_12", "ss_14", "ss_15" },
            gameName = "Scared Stiff",
            id = "ss"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "SS_G11.1_5"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "12", name = "WHEEL INDEX" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RT FLIPPER LANE" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "25", name = "EXTRA BALL LANE" },
            new SwitchMapping { id = "26", name = "LFT FLIPPER LANE" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "SINGLE STANDUP" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "RIGHT POPPER" },
            new SwitchMapping { id = "37", name = "LEFT KICKOUT" },
            new SwitchMapping { id = "38", name = "CRATE ENTERANCE" },

            new SwitchMapping { id = "41", name = "COFFIN LEFT" },
            new SwitchMapping { id = "42", name = "COFFIN CENTER" },
            new SwitchMapping { id = "43", name = "COFFIN RIGHT" },
            new SwitchMapping { id = "44", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "45", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "46", name = "LEFT RAMP MADE" },
            new SwitchMapping { id = "47", name = "RIGHT RAMP MADE" },
            new SwitchMapping { id = "48", name = "COFFIN ENTERANCE" },

            new SwitchMapping { id = "51", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "52", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "53", name = "UPPER JET" },
            new SwitchMapping { id = "54", name = "CENTER JET" },
            new SwitchMapping { id = "55", name = "LOWER JET" },
            new SwitchMapping { id = "56", name = "UPPER SLINGSHOT" },
            new SwitchMapping { id = "57", name = "CRATE SENSOR" },
            new SwitchMapping { id = "58", name = "LEFT LOOP" },

            new SwitchMapping { id = "61", name = "THREE BANK UPPER" },
            new SwitchMapping { id = "62", name = "THREE BANK MID" },
            new SwitchMapping { id = "63", name = "THREE BANK LOWER" },
            new SwitchMapping { id = "64", name = "LEFT LEAPER" },
            new SwitchMapping { id = "65", name = "CENTER LEAPER" },
            new SwitchMapping { id = "66", name = "RIGHT LEAPER" },
            new SwitchMapping { id = "67", name = "RT RAMP 10 POINT" },
            new SwitchMapping { id = "68", name = "RIGHT ROOP" },

            new SwitchMapping { id = "71", name = "LEFT SKULL LANE" },
            new SwitchMapping { id = "72", name = "CNTR SKULL LANE" },
            new SwitchMapping { id = "73", name = "RIGHT SKULL LANE" },
            new SwitchMapping { id = "74", name = "SECRET PASSAGE" }
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
            image = "playfield-ss.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#8CA5AE",
            "#71AD5A",
            "#C74641"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "38", "41", "42", "43", "44", "45", "46", "47", "48",
                "31", "36", "37", "38", "41", "42", "43", "44", "45", "46", "47", "48",
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
                    offset = 0x1C7C,
                    value = 0x01
                },
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            checksum = new ChecksumData[]
            {
                new ChecksumData { dataStartOffset = 0x1BFE, dataEndOffset = 0x1CEE, checksumOffset = 0x1CEF, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x43D, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x589, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

                new MemoryPositionData { offset = 0x169F, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16A6, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16AD, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16B4, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

                new MemoryPositionData { offset = 0x170D, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1BFE, name = "GAME_BALL_TOTAL", description = "Balls per game", type = "uint8" },
                new MemoryPositionData { offset = 0x1C7C, name = "STAT_FREEPLAY", description = "0: not free, 1: free", type = "uint8" },

                new MemoryPositionData { offset = 0x1D57, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D5B, name = "HISCORE_1_SCORE", type = "bcd", length = 4 },
                new MemoryPositionData { offset = 0x1D5F, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D63, name = "HISCORE_2_SCORE", type = "bcd", length = 4 },
                new MemoryPositionData { offset = 0x1D67, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D6B, name = "HISCORE_3_SCORE", type = "bcd", length = 4 },
                new MemoryPositionData { offset = 0x1D6F, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D73, name = "HISCORE_4_SCORE", type = "bcd", length = 4 },
                new MemoryPositionData { offset = 0x1D79, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D7D, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 4 },
                //TEX 0x1FDC

                new MemoryPositionData { offset = 0x1D89, name = "GAME_CREDITS_FULL", description = "0-10 credits", type = "uint8" },
                new MemoryPositionData { offset = 0x1D8A, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}