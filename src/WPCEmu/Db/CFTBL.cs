namespace WPCEmu.Db
{
    public class CreatureFromTheBlackLagoon : IDb
    {
        public string name => "WPC-Fliptronics: Creature from the Black Lagoon";
        public string version => "L-4";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "cftbl_p3", "cftbl_l2", "cftbl_d2", "cftbl_l3", "cftbl_d3", "cftbl_l4", "cftbl_d4", "cftbl_l5c" },
            gameName = "Creature from the Black Lagoon",
            id = "cftbl"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "creat_l4.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "CREDIT BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "TOP LEFT RO" },
            new SwitchMapping { id = "16", name = "LEFT SUBWAY" },
            new SwitchMapping { id = "17", name = "CENTER SUBWAY" },
            new SwitchMapping { id = "18", name = "CNTR SHOT RU" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "<P>AID" },
            new SwitchMapping { id = "26", name = "P<A>ID" },
            new SwitchMapping { id = "27", name = "PA<I>D" },
            new SwitchMapping { id = "28", name = "PAI<D>" },

            new SwitchMapping { id = "33", name = "BOTTOM JET" },
            new SwitchMapping { id = "34", name = "RIGHT POPPER" },
            new SwitchMapping { id = "35", name = "R RAMP ENTER" },
            new SwitchMapping { id = "36", name = "L RAMP ENTER" },
            new SwitchMapping { id = "37", name = "LOW R POPPER" },
            new SwitchMapping { id = "38", name = "RAMP UP DWN" },

            new SwitchMapping { id = "41", name = "COLA" },
            new SwitchMapping { id = "42", name = "HOTDOG" },
            new SwitchMapping { id = "43", name = "POPCORN" },
            new SwitchMapping { id = "44", name = "ICE CREAM" },
            new SwitchMapping { id = "45", name = "LEFT JET" },
            new SwitchMapping { id = "46", name = "RIGHT JET" },
            new SwitchMapping { id = "47", name = "LEFT SLING" },
            new SwitchMapping { id = "48", name = "RIGHT SLING" },

            new SwitchMapping { id = "51", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "52", name = "LEFT RET LANE" },
            new SwitchMapping { id = "53", name = "RIGHT RET LANE" },
            new SwitchMapping { id = "54", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "55", name = "OUTHOLE" },
            new SwitchMapping { id = "56", name = "RGHT TROUGH" },
            new SwitchMapping { id = "57", name = "CNTR TROUGH" },
            new SwitchMapping { id = "58", name = "LFT TROUGH" },

            new SwitchMapping { id = "61", name = "R RAMP EXIT" },
            new SwitchMapping { id = "62", name = "LOW L RAMP EXIT" },
            new SwitchMapping { id = "63", name = "CNTR LANE EXIT" },
            new SwitchMapping { id = "64", name = "UPPER RAMP EXIT" },
            new SwitchMapping { id = "65", name = "BOWL" },
            new SwitchMapping { id = "66", name = "SHOOTER" }
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
            image = "playfield-cftbl.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#76F570",
            "#9C3FAB",
            "#F7DB4E"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "56", "57", "58",
                "22"
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

                new MemoryPositionData { offset = 0x48B, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1CA9, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CAC, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CB1, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CB4, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CB9, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CBC, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CC1, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CC4, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CCB, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1CCE, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => new string[]
        {
            "CLOCK IS BROKEN"
        };
    }
}