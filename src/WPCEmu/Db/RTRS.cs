namespace WPCEmu.Db
{
    public class RedTedsRoadShow : IDb
    {
        public string name => "WPC-S: Red & Ted's Road Show";
        public string version => "LX-5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "rs_l6", "rs_lx2p3", "rs_lx2", "rs_dx2", "rs_lx3", "rs_dx3", "rs_la4", "rs_da4", "rs_lx4", "rs_dx4", "rs_la5", "rs_da5", "rs_lx5", "rs_dx5" },
            gameName = "Red & Ted's Road Show",
            id = "rs"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "u6_lx5.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "\"TED\"S MOUTH" },
            new SwitchMapping { id = "12", name = "DOZER DOWN" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "DOZER UP" },
            new SwitchMapping { id = "16", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT INLANE 2" },
            new SwitchMapping { id = "18", name = "RIGHT INLANE 1" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "\"RED\"S MOUTH" },
            new SwitchMapping { id = "26", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "27", name = "LEFT INLANE" },
            new SwitchMapping { id = "28", name = "B ZONE 3 BANK" },

            new SwitchMapping { id = "31", name = "SKILL SHOT LOWER" },
            new SwitchMapping { id = "32", name = "SKILL SHOT UPPER" },
            new SwitchMapping { id = "33", name = "RIGHT SHOOTER" },
            new SwitchMapping { id = "34", name = "RADIO 3 BANK" },
            new SwitchMapping { id = "35", name = "\"RED\" STANDUP U" },
            new SwitchMapping { id = "36", name = "\"RED\" STAND L" },
            new SwitchMapping { id = "37", name = "HIT \"RED\"" },
            new SwitchMapping { id = "38", name = "RIGHT LOOP EXIT" },

            new SwitchMapping { id = "41", name = "TROUGH JAM" },
            new SwitchMapping { id = "42", name = "TROUGH 1" },
            new SwitchMapping { id = "43", name = "TROUGH 2" },
            new SwitchMapping { id = "44", name = "TROUGH 3" },
            new SwitchMapping { id = "45", name = "TROUGH 4" },
            new SwitchMapping { id = "46", name = "RIGHT LOOP ENTER" },
            new SwitchMapping { id = "47", name = "HIT BULLDOZER" },
            new SwitchMapping { id = "48", name = "HIT \"TED\"" },

            new SwitchMapping { id = "51", name = "SPINNER" },
            new SwitchMapping { id = "52", name = "LOCKUP 1" },
            new SwitchMapping { id = "53", name = "LOCKUP 2" },
            new SwitchMapping { id = "54", name = "LOCK KICKOUT" },
            new SwitchMapping { id = "55", name = "R RAMP EXIT LEFT" },
            new SwitchMapping { id = "56", name = "LEFT RAMP EXIT" },
            new SwitchMapping { id = "57", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "58", name = "LEFT SHOOTER" },

            new SwitchMapping { id = "61", name = "LEFT SLING" },
            new SwitchMapping { id = "62", name = "RIGHT SLING" },
            new SwitchMapping { id = "63", name = "LEFT JET" },
            new SwitchMapping { id = "64", name = "TOP JET" },
            new SwitchMapping { id = "65", name = "RIGHT JET" },

            new SwitchMapping { id = "71", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "72", name = "R RAMP EXIT CEN" },
            new SwitchMapping { id = "73", name = "F ROCKS 5X BLAST" },
            new SwitchMapping { id = "74", name = "F ROCKS RAD RIOT" },
            new SwitchMapping { id = "75", name = "F ROCKS EX BALL" },
            new SwitchMapping { id = "76", name = "F ROCKS TOP" },
            new SwitchMapping { id = "77", name = "UNDER BLAST ZONE" },
            new SwitchMapping { id = "78", name = "START CITY" },

            new SwitchMapping { id = "81", name = "WHITE STANDUP" },
            new SwitchMapping { id = "82", name = "RED STANDUP" },
            new SwitchMapping { id = "83", name = "YELLOW STANDUP" },
            new SwitchMapping { id = "84", name = "ORANGE STANDUP" },
            new SwitchMapping { id = "85", name = "MID L FLIP TOP" },
            new SwitchMapping { id = "86", name = "MID L FLIP BOT" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F7", name = "ML FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-rtrs.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#4182C2",
            "#DD713E",
            "#E2D355",
            "#E53925",
            "#6EEA74"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "41", "42", "43", "44", "45",
                "41",
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

                new MemoryPositionData { offset = 0x1C61, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C64, name = "HISCORE_1_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C69, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C6D, name = "HISCORE_2_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C71, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C76, name = "HISCORE_3_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C79, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C7F, name = "HISCORE_4_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C87, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1C8B, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}