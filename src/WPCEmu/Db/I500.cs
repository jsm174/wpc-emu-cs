namespace WPCEmu.Db
{
    public class Indianapolis500 : IDb
    {
        public string name => "WPC-S: Indianapolis 500";
        public string version => "1.1R";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "i500_11r", "i500_11b", "i500_10r" },
            gameName = "Indianapolis 500",
            id = "i500"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "indy1_1r.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "BALL LAUNCH" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "16", name = "LEFT FLIP LANE" },
            new SwitchMapping { id = "17", name = "RIGHT FLIP LANE" },
            new SwitchMapping { id = "18", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "SHOOTER LANE" },
            new SwitchMapping { id = "26", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "27", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "28", name = "THREE BANK UPPER" },

            new SwitchMapping { id = "31", name = "HREE BANK CENTER" },
            new SwitchMapping { id = "32", name = "THREE BANK LOWER" },
            new SwitchMapping { id = "34", name = "RT FLIP WRENCH" },
            new SwitchMapping { id = "35", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "36", name = "LEFT RAMP MADE" },
            new SwitchMapping { id = "37", name = "LEFT LOOP" },
            new SwitchMapping { id = "38", name = "RIGHT LOOP" },

            new SwitchMapping { id = "41", name = "TOP TROUGH" },
            new SwitchMapping { id = "42", name = "TROUGH 1 (RT)" },
            new SwitchMapping { id = "43", name = "TROUGH 2" },
            new SwitchMapping { id = "44", name = "TROUGH 3" },
            new SwitchMapping { id = "45", name = "TROUGH 4 (LFT)" },
            new SwitchMapping { id = "46", name = "LFT RAMP STANDUP" },
            new SwitchMapping { id = "47", name = "TURBO WRENCH" },
            new SwitchMapping { id = "48", name = "JET BUMPER WRENCH" },

            new SwitchMapping { id = "51", name = "LEFT LANE" },
            new SwitchMapping { id = "52", name = "CENTER LANE" },
            new SwitchMapping { id = "53", name = "RIGHT LANE" },
            new SwitchMapping { id = "54", name = "TEN POINT" },
            new SwitchMapping { id = "55", name = "LEFT RAMP WRENCH" },
            new SwitchMapping { id = "56", name = "LEFT LIGHT-UP" },
            new SwitchMapping { id = "57", name = "CENTER LIGHT-UP" },
            new SwitchMapping { id = "58", name = "RIGHT LIGHT-UP" },

            new SwitchMapping { id = "61", name = "UPPER POPPER" },
            new SwitchMapping { id = "62", name = "TURBO POPPER" },
            new SwitchMapping { id = "63", name = "TURBO BALL SENSE" },
            new SwitchMapping { id = "64", name = "UPPER EJECT" },
            new SwitchMapping { id = "65", name = "LOWER KICKER" },
            new SwitchMapping { id = "66", name = "TURBO INDEX" },

            new SwitchMapping { id = "72", name = "LEFT JET" },
            new SwitchMapping { id = "73", name = "RIGHT JET" },
            new SwitchMapping { id = "74", name = "CENTER JET" },
            new SwitchMapping { id = "75", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "76", name = "RIGHT RAMP MADE" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-i500.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#E83A24",
            "#F4BE43",
            "#2E73B9",
            "#4CA65F"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "41", "42", "43", "44", "45", "56", "57", "58", "61", "62", "63"
                "41", "56", "57", "58", "61", "62", "63"

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

                new MemoryPositionData { offset = 0x441, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1D25, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D28, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D2D, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D30, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D35, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D38, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D3D, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D40, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D85, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D89, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}