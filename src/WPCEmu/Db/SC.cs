namespace WPCEmu.Db
{
    public class SafeCracker : IDb
    {
        public string name => "WPC-95: Safe Cracker";
        public string version => "1.8";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "sc_091", "sc_10", "sc_14", "sc_17", "sc_17n", "sc_18", "sc_18n", "sc_18s11", "sc_18n11", "sc_18s2", "sc_18ns2" },
            gameName = "Safe Cracker",
            id = "sc"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "safe_18g.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "TP TROUGH (ROOF)" },
            new SwitchMapping { id = "12", name = "TP TROUGH (MOVE)" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RIGHT ORBIT" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "18", name = "BALLSHOOTER" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "25", name = "UR FLIP ROLLOVER" },
            new SwitchMapping { id = "26", name = "LEFT RETURN" },
            new SwitchMapping { id = "27", name = "RIGHT RETURN" },
            new SwitchMapping { id = "28", name = "LEFT ORBIT" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "LOOKUP 1 FRONT" },
            new SwitchMapping { id = "37", name = "LOOKUP 2 REAR" },

            new SwitchMapping { id = "41", name = "KICKBACK" },
            new SwitchMapping { id = "42", name = "LEFT BIG KICK" },
            new SwitchMapping { id = "43", name = "TOKN CHUTE EXIT" },
            new SwitchMapping { id = "44", name = "LEFT JET" },
            new SwitchMapping { id = "45", name = "RIGHT JET" },
            new SwitchMapping { id = "46", name = "TOP JET" },
            new SwitchMapping { id = "47", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "48", name = "RIGHT SLINGSHOT" },

            new SwitchMapping { id = "51", name = "(A)LARM STANDUP" },
            new SwitchMapping { id = "52", name = "A(L)ARM STANDUP" },
            new SwitchMapping { id = "53", name = "AL(A)RM STANDUP" },
            new SwitchMapping { id = "54", name = "ALA(R)M STANDUP" },
            new SwitchMapping { id = "55", name = "ALAR(M) STANDUP" },
            new SwitchMapping { id = "56", name = "MOVNG TARGET C" },
            new SwitchMapping { id = "57", name = "MOVNG TARGET B" },
            new SwitchMapping { id = "58", name = "MOVNG TARGET A" },

            new SwitchMapping { id = "61", name = "TL 3BANK TOP" },
            new SwitchMapping { id = "62", name = "TL 3BANK MIDDLE" },
            new SwitchMapping { id = "63", name = "TL 3BANK BOTTOM" },
            new SwitchMapping { id = "64", name = "TR 3BANK BOTTOM" },
            new SwitchMapping { id = "65", name = "TR 3BANK MIDDLE" },
            new SwitchMapping { id = "66", name = "TR 3BANK TOP" },
            new SwitchMapping { id = "67", name = "TOP LEFT LANE" },
            new SwitchMapping { id = "68", name = "TOP POPPER" },

            new SwitchMapping { id = "71", name = "BL 3BANK TOP" },
            new SwitchMapping { id = "72", name = "BL 3BANK MIDDLE" },
            new SwitchMapping { id = "73", name = "BL 3BANK BOTTOM" },
            new SwitchMapping { id = "74", name = "BR 3BANK BOTTOM" },
            new SwitchMapping { id = "75", name = "BR 3BANK MIDDLE" },
            new SwitchMapping { id = "76", name = "BR 3BANK TOP" },
            new SwitchMapping { id = "77", name = "BANK KICKOUT" },
            new SwitchMapping { id = "78", name = "TOP RIGHT LANE" },

            new SwitchMapping { id = "81", name = "LEFT TOKEN LVL" },
            new SwitchMapping { id = "82", name = "RIGHT TOKEN LVL" },
            new SwitchMapping { id = "83", name = "RAMP ENTRANCE" },
            new SwitchMapping { id = "84", name = "RAMP MADE" },
            new SwitchMapping { id = "85", name = "WHEEL CHANNEL A" },
            new SwitchMapping { id = "86", name = "WHEEL CHANNEL B" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F8", name = "TOKEN COIN SLOT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-sc.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#294FEF",
            "#F6F25B",
            "#D8362C",
            "#DC8B33"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "41", "42", "43", "61", "62", "63", "64", "65", "66", "71", "72", "73", "74", "75", "76", "85", "86"
                "31", "36", "37", "41", "42", "43", "61", "62", "63", "64", "65", "66", "71", "72", "73", "74", "75", "76", "85", "86"
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

                new MemoryPositionData { offset = 0x479, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x5BD, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1D29, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D2C, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D31, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D34, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D39, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D3C, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D41, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D44, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D4B, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D4E, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}