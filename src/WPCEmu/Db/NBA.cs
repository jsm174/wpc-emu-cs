namespace WPCEmu.Db
{
    public class NBAFastbreak : IDb
    {
        public string name => "WPC-95: NBA Fastbreak";
        public string version => "3.1";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "nbaf_11s", "nbaf_11", "nbaf_11a", "nbaf_115", "nbaf_21", "nbaf_22", "nbaf_23", "nbaf_31", "nbaf_31a" },
            gameName = "NBA Fastbreak",
            id = "nbaf"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "fb_g11.3_1"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "BALL LAUNCHER" },
            new SwitchMapping { id = "12", name = "BACKBOX BASKET" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "SHOOTER LANE" },
            new SwitchMapping { id = "16", name = "LT RETURN LANE" },
            new SwitchMapping { id = "17", name = "RT RETURN LANE" },
            new SwitchMapping { id = "18", name = "L R STANDUP" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "RIGHT JET" },
            new SwitchMapping { id = "25", name = "EJECT HOLE" },
            new SwitchMapping { id = "26", name = "LEFT OUT LANE" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "U R STANDUP" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "CENTER RAMP OPTO" },
            new SwitchMapping { id = "37", name = "R LOOP ENT OPTO" },
            new SwitchMapping { id = "38", name = "RIGHT LOOP EXIT" },

            new SwitchMapping { id = "41", name = "STANDUP \"3\"" },
            new SwitchMapping { id = "42", name = "STANDUP \"P\"" },
            new SwitchMapping { id = "43", name = "STANDUP \"T\"" },
            new SwitchMapping { id = "44", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "45", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "46", name = "LEFT RAMP MADE" },
            new SwitchMapping { id = "47", name = "LEFT LOOP ENTER" },
            new SwitchMapping { id = "48", name = "LEFT LOOP MADE" },

            new SwitchMapping { id = "51", name = "DEFENDER POS 4" },
            new SwitchMapping { id = "52", name = "DEFENDER POS 3" },
            new SwitchMapping { id = "53", name = "DEFEND LOCK POS" },
            new SwitchMapping { id = "54", name = "DEFENDER POS 2" },
            new SwitchMapping { id = "55", name = "DEFENDER POS 1" },
            new SwitchMapping { id = "56", name = "JETS BALL DRAIN" },
            new SwitchMapping { id = "57", name = "L SLINGSHOT" },
            new SwitchMapping { id = "58", name = "R SLINGSHOT" },

            new SwitchMapping { id = "61", name = "LEFT JET" },
            new SwitchMapping { id = "62", name = "MIDDLE JET" },
            new SwitchMapping { id = "63", name = "L LOOP RAMP EXIT" },
            new SwitchMapping { id = "64", name = "RIGHT RAMP MADE" },
            new SwitchMapping { id = "65", name = "\"IN THE PAINT\" 4" },
            new SwitchMapping { id = "66", name = "\"IN THE PAINT\" 3" },
            new SwitchMapping { id = "67", name = "\"IN THE PAINT\" 2" },
            new SwitchMapping { id = "68", name = "\"IN THE PAINT\" 1" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "BASKED MADE" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "BASKED HOLD" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-nba.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#2B1772",
            "#E73F27",
            "#F8DD4C",
            "#67DCEE",
            "#AB6341"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "51", "52", "53", "54", "55"
                "31", "36", "37", "51", "52", "53", "54", "55"

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

                new MemoryPositionData { offset = 0x43D, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x5AC, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1C87, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1C8A, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}