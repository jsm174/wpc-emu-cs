namespace WPCEmu.Db
{
    public class WorldCupSoccer : IDb
    {
        public string name => "WPC-S: World Cup Soccer";
        public string version => "LX-2";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "wcs_l2", "wcs_d2", "wcs_l3c", "wcs_la2", "wcs_p2", "wcs_p5", "wcs_p3", "wcs_p6" },
            gameName = "World Cup Soccer",
            id = "wcs"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "WCUP_LX2.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "12", name = "MAG GOALIE BUT" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "L FLIPPER LANE" },
            new SwitchMapping { id = "16", name = "STRIKER 3 (HIGH)" },
            new SwitchMapping { id = "17", name = "R FLIPPER LANE" },
            new SwitchMapping { id = "18", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY EXTRA BALL" },
            new SwitchMapping { id = "25", name = "FREE KICK TARGET" },
            new SwitchMapping { id = "26", name = "KICKBACK UPPER" },
            new SwitchMapping { id = "27", name = "SPINNER" },
            new SwitchMapping { id = "28", name = "LIGHT KICKBACK" },

            new SwitchMapping { id = "31", name = "TROUGH 1 (RIGHT)" },
            new SwitchMapping { id = "32", name = "TROUGH 2" },
            new SwitchMapping { id = "33", name = "TROUGH 3" },
            new SwitchMapping { id = "34", name = "TROUGH 4" },
            new SwitchMapping { id = "35", name = "TROUGH 5 (LEFT)" },
            new SwitchMapping { id = "36", name = "TROUGH STACK" },
            new SwitchMapping { id = "37", name = "LIGHT MAG GOALIE" },
            new SwitchMapping { id = "38", name = "BALLSHOOTER" },

            new SwitchMapping { id = "41", name = "GOAL TROUGH" },
            new SwitchMapping { id = "42", name = "GOAL POPPER OPTO" },
            new SwitchMapping { id = "43", name = "GOALIE IS LEFT" },
            new SwitchMapping { id = "44", name = "GOALIE IS RIGHT" },
            new SwitchMapping { id = "45", name = "TV BALL POPPER" },
            new SwitchMapping { id = "47", name = "TRAVEL LANE ROLO" },
            new SwitchMapping { id = "48", name = "GOALIE TARGET" },

            new SwitchMapping { id = "51", name = "SKILL SHOT FRONT" },
            new SwitchMapping { id = "52", name = "SKILL SHOT CENT" },
            new SwitchMapping { id = "53", name = "SKILL SHOT REAR" },
            new SwitchMapping { id = "54", name = "RIGHT EJECT HOLE" },
            new SwitchMapping { id = "55", name = "UPPER EJECT HOLE" },
            new SwitchMapping { id = "56", name = "LEFT EJECT HOLE" },
            new SwitchMapping { id = "57", name = "R LANE HI-UNUSED" },
            new SwitchMapping { id = "58", name = "R LANE LO-UNUSED" },

            new SwitchMapping { id = "61", name = "ROLLOVER 1(HIGH)" },
            new SwitchMapping { id = "62", name = "ROLLOVER 2" },
            new SwitchMapping { id = "63", name = "ROLLOVER 3" },
            new SwitchMapping { id = "64", name = "ROLLOVER 4 (LOW)" },
            new SwitchMapping { id = "65", name = "TACKLE SWITCH" },
            new SwitchMapping { id = "66", name = "STRIKER 1 (LEFT)" },
            new SwitchMapping { id = "67", name = "STRIKER 2 (CENT)" },

            new SwitchMapping { id = "71", name = "L RAMP DIVERTED" },
            new SwitchMapping { id = "72", name = "L RAMP ENTRANCE" },
            new SwitchMapping { id = "74", name = "LEFT RAMP EXIT" },
            new SwitchMapping { id = "75", name = "R RAMP ENTRANCE" },
            new SwitchMapping { id = "76", name = "LOCK MECH LOW" },
            new SwitchMapping { id = "77", name = "LOCK MECH HIGH" },
            new SwitchMapping { id = "78", name = "RIGHT RAMP EXIT" },

            new SwitchMapping { id = "81", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "82", name = "UPPER JET BUMPER" },
            new SwitchMapping { id = "83", name = "LOWER JET BUMPER" },
            new SwitchMapping { id = "84", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "85", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "86", name = "KICKBACK" },
            new SwitchMapping { id = "87", name = "UPPER LEFT LANE" },
            new SwitchMapping { id = "88", name = "UPPER RIGHT LANE" }
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

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-wcs.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#4F4EB2",
            "#59C5CC",
            "#EDE34C",
            "#D13A2A"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "41", "42", "43", "44", "45", "51", "52", "53"
                "36", "41", "42", "43", "44", "45", "51", "52", "53",
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

                new MemoryPositionData { offset = 0x1730, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1737, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x173E, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1745, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

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

                new MemoryPositionData { offset = 0x1CED, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CF1, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CF6, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CFA, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CFF, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D03, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D08, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D0C, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D13, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D17, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}