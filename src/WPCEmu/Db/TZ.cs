namespace WPCEmu.Db
{
    public class TwilightZone : IDb
    {
        public string name => "WPC-Fliptronics: Twilight Zone";
        public string version => "H-8";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "tz_pa1", "tz_pa2", "tz_p3", "tz_p3d", "tz_p4", "tz_p5", "tz_l1", "tz_d1", "tz_l2", "tz_d2", "tz_l3", "tz_d3", "tz_ifpa", "tz_ifpa2", "tz_l4", "tz_d4", "tz_h7", "tz_i7", "tz_h8", "tz_i8", "tz_92", "tz_93", "tz_94h", "tz_94ch" },
            gameName = "Twilight Zone",
            id = "tz"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "tz_h8.u6"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT INLANE" },
            new SwitchMapping { id = "12", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "16", name = "CENTER TROUGH" },
            new SwitchMapping { id = "17", name = "LEFT TROUGH" },
            new SwitchMapping { id = "18", name = "OUTHOLE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "FAR L TROUGH" },
            new SwitchMapping { id = "26", name = "TROUGH PROXIMITY" },
            new SwitchMapping { id = "27", name = "BALL SHOOTER" },
            new SwitchMapping { id = "28", name = "ROCKET KICKER" },

            new SwitchMapping { id = "31", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "32", name = "RIGHT JET BUMPER" },
            new SwitchMapping { id = "33", name = "LOWER JET BUMPER" },
            new SwitchMapping { id = "34", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "35", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "36", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "37", name = "LEFT INLANE 1" },
            new SwitchMapping { id = "38", name = "LEFT INLANE 2" },

            new SwitchMapping { id = "41", name = "DEAD END" },
            new SwitchMapping { id = "42", name = "THE CAMERA" },
            new SwitchMapping { id = "43", name = "PLAYER PIANO" },
            new SwitchMapping { id = "44", name = "MINI PF ENTER" },
            new SwitchMapping { id = "45", name = "MINI PF LEFT (2)" },
            new SwitchMapping { id = "46", name = "MINI PF RGHT (2)" },
            new SwitchMapping { id = "47", name = "CLOCK MILLIONS" },
            new SwitchMapping { id = "48", name = "LOW LEFT 5 MIL" },

            new SwitchMapping { id = "51", name = "GUM POPPER LANE" },
            new SwitchMapping { id = "52", name = "HITCH-HIKER" },
            new SwitchMapping { id = "53", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "54", name = "LEFT RAMP" },
            new SwitchMapping { id = "55", name = "GUMBALL GENEVA" },
            new SwitchMapping { id = "56", name = "GUMBALL EXIT" },
            new SwitchMapping { id = "57", name = "SLOT PROXIMITY" },
            new SwitchMapping { id = "58", name = "SLOT KICKOUT" },

            new SwitchMapping { id = "61", name = "LOWER SKILL" },
            new SwitchMapping { id = "62", name = "CENTER SKILL" },
            new SwitchMapping { id = "63", name = "UPPER SKILL" },
            new SwitchMapping { id = "64", name = "U RIGHT 5 MIL" },
            new SwitchMapping { id = "65", name = "POWER PAYLOFF (2)" },
            new SwitchMapping { id = "66", name = "MID R 5 MIL 1" },
            new SwitchMapping { id = "67", name = "MID R 5 MIL 2" },
            new SwitchMapping { id = "68", name = "LOW RIGHT 5 MIL" },

            new SwitchMapping { id = "72", name = "AUTO-FIRE KICKER" },
            new SwitchMapping { id = "73", name = "RIGHT RAMP" },
            new SwitchMapping { id = "74", name = "GUMBALL POPPER" },
            new SwitchMapping { id = "75", name = "MINI PF TOP" },
            new SwitchMapping { id = "76", name = "MINI PF EXIT" },
            new SwitchMapping { id = "77", name = "MID LEFT 5 MIL" },
            new SwitchMapping { id = "78", name = "U LEFT 5 MIL" },

            new SwitchMapping { id = "81", name = "RIGHT MAGNET" },
            new SwitchMapping { id = "83", name = "LEFT MAGNET" },
            new SwitchMapping { id = "84", name = "LOCK CENTER" },
            new SwitchMapping { id = "85", name = "LOCK UPPER" },
            new SwitchMapping { id = "87", name = "GUMBALL ENTER" },
            new SwitchMapping { id = "88", name = "LOCK LOWER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-tz.jpg",
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics",
        };

        public string[] cabinetColors => new string[]
        {
            "#FCEB4F",
            "#CB322C",
            "#47A5DF",
            "#C27133"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "15", "16", "17",
                "22",
                //OPTO SWITCHES:
                "71", "72", "73", "74", "75", "76", "81", "82", "83", "84", "85", "86", "87"
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

                new MemoryPositionData { offset = 0x431, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x16A1, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A8, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AF, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B6, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1D49, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D4D, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D52, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D56, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D5B, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D5F, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D30, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D68, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D6F, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D73, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => new string[]
        {
            "CLOCK IS BROKEN"
        };
    }
}