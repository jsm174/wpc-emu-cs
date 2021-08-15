namespace WPCEmu.Db
{
    public class Harly : IDb
    {
        public string name => "WPC-ALPHA: Harley Davidson";
        public string version => "L-3";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "hd_l1", "hd_d1", "hd_l2", "hd_d2", "hd_l3", "hd_d3" },
            gameName = "Harley-Davidson",
            id = "hd"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "HARLY_L3.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "OUTHOLE" },
            new SwitchMapping { id = "16", name = "TROUGH 1" },
            new SwitchMapping { id = "17", name = "TROUGH 2" },
            new SwitchMapping { id = "18", name = "TROUGH 3" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "FRONT DOOR" },
            new SwitchMapping { id = "23", name = "TOKEN DISPENSER" },
            new SwitchMapping { id = "25", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "26", name = "RIGHT JET BUMPER" },
            new SwitchMapping { id = "27", name = "LOWER JET BUMPER" },
            new SwitchMapping { id = "28", name = "LOWER RIGHT EJECT" },

            new SwitchMapping { id = "31", name = "B IN BIKE" },
            new SwitchMapping { id = "32", name = "I IN BIKE" },
            new SwitchMapping { id = "33", name = "K IN BIKE" },
            new SwitchMapping { id = "34", name = "E IN BIKE" },
            new SwitchMapping { id = "35", name = "TOP RIGHT EJECT" },
            new SwitchMapping { id = "36", name = "TOP LEFT EJECT" },
            new SwitchMapping { id = "37", name = "LEFT SLING" },
            new SwitchMapping { id = "38", name = "RIGHT SLING" },

            new SwitchMapping { id = "41", name = "U TOP LANE" },
            new SwitchMapping { id = "42", name = "S TOP LANE" },
            new SwitchMapping { id = "43", name = "A TOP LANE" },

            new SwitchMapping { id = "51", name = "H IN HARLEY" },
            new SwitchMapping { id = "52", name = "A IN HARLEY" },
            new SwitchMapping { id = "53", name = "R IN HARLEY" },
            new SwitchMapping { id = "54", name = "L IN HARLEY" },
            new SwitchMapping { id = "55", name = "E IN HARLEY" },
            new SwitchMapping { id = "56", name = "Y IN HARLEY" },
            new SwitchMapping { id = "57", name = "1ST D IN DAVIDSON" },
            new SwitchMapping { id = "58", name = "A IN DAVIDSON" },

            new SwitchMapping { id = "61", name = "V IN DAVIDSON" },
            new SwitchMapping { id = "62", name = "I IN DAVIDSON" },
            new SwitchMapping { id = "63", name = "2ND D IN DAVIDSON" },
            new SwitchMapping { id = "64", name = "S IN DAVIDSON" },
            new SwitchMapping { id = "65", name = "O IN DAVIDSON" },
            new SwitchMapping { id = "66", name = "N IN DAVIDSON" },
            new SwitchMapping { id = "67", name = "RIGHT SPINNER" },
            new SwitchMapping { id = "68", name = "LEFT SPINNER" },

            new SwitchMapping { id = "71", name = "LEFT LOOP" },
            new SwitchMapping { id = "72", name = "RIGHT LOOP" },
            new SwitchMapping { id = "73", name = "LEFT DRAIN" },
            new SwitchMapping { id = "74", name = "RIGHT DRAIN" },
            new SwitchMapping { id = "75", name = "SHOOTER LANE" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-hd.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcAlphanumeric"
        };

        public string[] cabinetColors => new string[]
        {
            "#44A1DF",
            "#D4914A"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "16", "17", "18",
                "22"
            },
            initialAction = new InitialAction[]
            {
                new InitialAction
                {
                    delayMs = 1500,
                    source = "cabinetInput",
                    value = 16
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            knownValues = new MemoryPositionData[]
            {
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
                new MemoryPositionData { offset = 0x18FB, name = "STAT_4_PLAYER_GAME", description = "Counts finished games", type = "uint8", length = 3 }
            }
        };

        public string[] testErrors => null;
    }
}