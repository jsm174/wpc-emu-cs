namespace WPCEmu.Db
{
    public class Corvette : IDb
    {
        public string name => "WPC-S: Corvette";
        public string version => "LX-1";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "corv_px4", "corv_px5", "corv_la1", "corv_lx1", "corv_dx1", "corv_lx2", "corv_21" },
            gameName = "Corvette",
            id = "corv",
            vpdbId = "corvette"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "u6-lx1.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "PLUNGER" },
            new SwitchMapping { id = "16", name = "L RETURN LANE" },
            new SwitchMapping { id = "17", name = "R RETURN LANE" },
            new SwitchMapping { id = "18", name = "SPINNER" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "1ST GEAR (OPT)" },
            new SwitchMapping { id = "26", name = "2ND GEAR (OPT)" },
            new SwitchMapping { id = "27", name = "3RD GEAR (OPT)" },
            new SwitchMapping { id = "28", name = "4TH GEAR (OPT)" },

            new SwitchMapping { id = "31", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "35", name = "ROUTE 66 ENTRY" },
            new SwitchMapping { id = "36", name = "PIT STOP POPPER" },
            new SwitchMapping { id = "37", name = "TROUGH EJECT" },
            new SwitchMapping { id = "38", name = "INNER LOOP ENTRY" },

            new SwitchMapping { id = "41", name = "ZR1 BOTTOM ENTRY" },
            new SwitchMapping { id = "42", name = "ZR1 TOP ENTRY" },
            new SwitchMapping { id = "43", name = "SKID PAD ENTRY" },
            new SwitchMapping { id = "44", name = "SKID PAD EXIT" },
            new SwitchMapping { id = "45", name = "ROUTE 66 EXIT" },
            new SwitchMapping { id = "46", name = "L STANDUP 3" },
            new SwitchMapping { id = "47", name = "L STANDUP 2" },
            new SwitchMapping { id = "48", name = "L STANDUP 1" },

            new SwitchMapping { id = "51", name = "L RACE START" },
            new SwitchMapping { id = "52", name = "R RACE START" },
            new SwitchMapping { id = "55", name = "L RACE ENCODER" },
            new SwitchMapping { id = "56", name = "R RACE ENCODER" },
            new SwitchMapping { id = "57", name = "ROUTE 66 KICKOUT" },
            new SwitchMapping { id = "58", name = "SKID RTE66 EXIT" },

            new SwitchMapping { id = "61", name = "L SLINGSHOT" },
            new SwitchMapping { id = "62", name = "R SLINGSHOT" },
            new SwitchMapping { id = "63", name = "LEFT JET" },
            new SwitchMapping { id = "64", name = "BOTTOM JET" },
            new SwitchMapping { id = "65", name = "RIGHT JET" },
            new SwitchMapping { id = "66", name = "L ROLLOVER" },
            new SwitchMapping { id = "67", name = "M ROLLOVER" },
            new SwitchMapping { id = "68", name = "R ROLLOVER" },

            new SwitchMapping { id = "71", name = "ZR1 FULL LEFT" },
            new SwitchMapping { id = "72", name = "ZR1 FULL RIGHT" },
            new SwitchMapping { id = "75", name = "ZR1 EXIT" },
            new SwitchMapping { id = "76", name = "ZR1 LOCK BALL 1" },
            new SwitchMapping { id = "77", name = "ZR1 LOCK BALL 2" },
            new SwitchMapping { id = "78", name = "ZR1 LOCK BALL 3" },

            new SwitchMapping { id = "81", name = "MILLION STANDUP" },
            new SwitchMapping { id = "82", name = "SKID PAD STANDUP" },
            new SwitchMapping { id = "83", name = "R STANDUP" },
            new SwitchMapping { id = "84", name = "R RUBBER" },
            new SwitchMapping { id = "86", name = "JET RUBBER" },
            new SwitchMapping { id = "87", name = "L OUTER LOOP" },
            new SwitchMapping { id = "88", name = "R OUTER LOOP" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-corv.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#DBBD5C",
            "#C53A36",
            "#9A3F75",
            "#8490C3"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "41", "42", "43", "51", "52", "55", "56", "71", "72"
                "35", "36", "37", "41", "42", "43", "51", "52", "55", "56", "71", "72",
                "F2", "F4", "F6", "F8"
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

                new MemoryPositionData { offset = 0x49D, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x1731, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1737, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x173D, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1743, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1D33, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D37, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D3C, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D40, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D45, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D49, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D4E, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D52, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D59, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D5D, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}