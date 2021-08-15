namespace WPCEmu.Db
{
    public class NoFear : IDb
    {
        public string name => "WPC-S: No Fear";
        public string version => "2.3X";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "nf_10", "nf_101", "nf_11x", "nf_20", "nf_22", "nf_23", "nf_23f", "nf_23x" },
            gameName = "No Fear",
            id = "nf"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "nofe2_3x.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "BALL LAUNCH" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "SHOOTER LANE" },
            new SwitchMapping { id = "16", name = "SPINNER" },
            new SwitchMapping { id = "17", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "18", name = "RIGHT RETURN" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY EXTRA BALL" },
            new SwitchMapping { id = "25", name = "KICKBACK" },
            new SwitchMapping { id = "26", name = "LEFT RETURN" },
            new SwitchMapping { id = "27", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "28", name = "RIGHT SLINGSHOT" },

            new SwitchMapping { id = "31", name = "TROUGH STACK" },
            new SwitchMapping { id = "32", name = "TROUGH 1 (RIGHT)" },
            new SwitchMapping { id = "33", name = "TROUGH 2" },
            new SwitchMapping { id = "34", name = "TROUGH 3" },
            new SwitchMapping { id = "35", name = "TROUGH 4" },
            new SwitchMapping { id = "37", name = "CENTER TR ENTR" },
            new SwitchMapping { id = "38", name = "LEFT TR ENTER" },

            new SwitchMapping { id = "41", name = "RIGHT POPPER 1" },
            new SwitchMapping { id = "42", name = "RIGHT POPPER 2" },
            new SwitchMapping { id = "46", name = "LEFT MAGNET" },
            new SwitchMapping { id = "47", name = "CENTER MAGNET" },
            new SwitchMapping { id = "48", name = "RIGHT MAGNET" },

            new SwitchMapping { id = "51", name = "DROP TARGET" },
            new SwitchMapping { id = "54", name = "LEFT WIREFORM" },
            new SwitchMapping { id = "55", name = "INNER LOOP" },
            new SwitchMapping { id = "56", name = "LIGHT KB BOTTOM" },
            new SwitchMapping { id = "57", name = "LIGHT KB TOP" },
            new SwitchMapping { id = "58", name = "RIGHT LOOP" },

            new SwitchMapping { id = "61", name = "EJECT HOLE" },
            new SwitchMapping { id = "62", name = "LEFT LOOP" },
            new SwitchMapping { id = "63", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "64", name = "LEFT RMP MIDDLE" },
            new SwitchMapping { id = "66", name = "RIGHT RMP ENTER" },
            new SwitchMapping { id = "67", name = "RIGHT RAMP EXIT" },

            new SwitchMapping { id = "71", name = "RIGHT BANK TOP" },
            new SwitchMapping { id = "72", name = "RIGHT BANK MID" },
            new SwitchMapping { id = "73", name = "RIGHT BANK BOT" },
            new SwitchMapping { id = "74", name = "L TROLL UP" },
            new SwitchMapping { id = "75", name = "R TROLL UP" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-nf.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#CD3833",
            "#F3C748",
            "#E39444"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES "31", "32", "33", "34", "35", "37", "38", "41", "42", "46", "47", "48",
                "31", "37", "38", "41", "42", "46", "47", "48"
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
                    offset = 0x1C16,
                    value = 0x01
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            checksum = new ChecksumData[]
            {
                new ChecksumData { dataStartOffset = 0x1CED, dataEndOffset = 0x1D10, checksumOffset = 0x1D11, checksum = "16bit", name = "HI_SCORE" },
                new ChecksumData { dataStartOffset = 0x1D13, dataEndOffset = 0x1D1B, checksumOffset = 0x1D1C, checksum = "16bit", name = "CHAMPION" },
                new ChecksumData { dataStartOffset = 0x1B98, dataEndOffset = 0x1C84, checksumOffset = 0x1C85, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x7A, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x3B2, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x3B3, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },

                new MemoryPositionData { offset = 0x43E, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x16A1, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16A8, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16AF, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16B6, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

                new MemoryPositionData { offset = 0x1709, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1913, name = "STAT_LEFT_DRAIN", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x1919, name = "STAT_RIGHT_DRAIN", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x19FD, name = "STAT_LEFT_FLIPPER_TRIG", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x1A03, name = "STAT_RIGHT_FLIPPER_TRIG", type = "uint8", length = 3 },

                new MemoryPositionData { offset = 0x1B98, name = "GAME_BALL_TOTAL", description = "Balls per game", type = "uint8" },
                new MemoryPositionData { offset = 0x1C16, name = "STAT_FREEPLAY", description = "0: not free, 1: free", type = "uint8" },

                new MemoryPositionData { offset = 0x1CED, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CF1, name = "HISCORE_1_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1CF6, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CFA, name = "HISCORE_2_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1CFF, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D03, name = "HISCORE_3_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1D08, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D0C, name = "HISCORE_4_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1D13, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D17, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x1D24, name = "GAME_CREDITS_FULL", description = "0-10 credits", type = "uint8" },
                new MemoryPositionData { offset = 0x1D25, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}