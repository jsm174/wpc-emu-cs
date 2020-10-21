namespace WPCEmu.Db
{
    public class JohnnyMnemonic : IDb
    {
        public string name => "WPC-S: Johnny Mnemonic";
        public string version => "1.2 R";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "jm_05r", "jm_12r", "jm_12b" },
            gameName = "Johnny Mnemonic",
            id = "jm"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "john1_2r.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
           //TODO
            new SwitchMapping { id = "11", name = "BALL LAUNCH" },
            new SwitchMapping { id = "12", name = "X HAND HOME" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "16", name = "LEFT FLIP. LANE" },
            new SwitchMapping { id = "17", name = "RIGHT FLIP. LANE" },
            new SwitchMapping { id = "18", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY IN BUTTON" },
            new SwitchMapping { id = "25", name = "L SLINGSHOT" },
            new SwitchMapping { id = "26", name = "R SLINGSHOT" },
            new SwitchMapping { id = "27", name = "L STANDUP" },
            new SwitchMapping { id = "28", name = "R STANDUP" },

            new SwitchMapping { id = "31", name = "TROUGH JAM" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "POPPER BALL 1" },
            new SwitchMapping { id = "37", name = "Y HAND HOME" },
            new SwitchMapping { id = "38", name = "R RUBBER" },

            new SwitchMapping { id = "41", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "42", name = "LEFT RAMP MADE" },
            new SwitchMapping { id = "43", name = "DROP TARGET" },
            new SwitchMapping { id = "44", name = "LEFT JET" },
            new SwitchMapping { id = "45", name = "BOTTOM JET" },
            new SwitchMapping { id = "46", name = "RIGHT JET" },
            new SwitchMapping { id = "47", name = "CRAZ BOB\'S" },
            new SwitchMapping { id = "48", name = "SPINNER" },

            new SwitchMapping { id = "51", name = "CYBER MATRIX 11" },
            new SwitchMapping { id = "52", name = "CYBER MATRIX 21" },
            new SwitchMapping { id = "53", name = "CYBER MATRIX 31" },
            new SwitchMapping { id = "54", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "55", name = "RIGHT RAMP MADE" },
            new SwitchMapping { id = "56", name = "LEFT LOOP" },
            new SwitchMapping { id = "57", name = "RIGHT LOOP" },
            new SwitchMapping { id = "58", name = "INNER LOOP ENTRY" },

            new SwitchMapping { id = "61", name = "CYBER MATRIX 12" },
            new SwitchMapping { id = "62", name = "CYBER MATRIX 22" },
            new SwitchMapping { id = "63", name = "CYBER MATRIX 32" },
            new SwitchMapping { id = "64", name = "LEFT JET LANE" },
            new SwitchMapping { id = "65", name = "MIDDLE JET LANE" },
            new SwitchMapping { id = "66", name = "RIGHT JET LANE" },
            new SwitchMapping { id = "67", name = "R HAND CONTROL" },
            new SwitchMapping { id = "68", name = "L HAND CONTROL" },

            new SwitchMapping { id = "71", name = "CYBER MATRIX 13" },
            new SwitchMapping { id = "72", name = "CYBER MATRIX 23" },
            new SwitchMapping { id = "73", name = "CYBER MATRIX 33" },
            new SwitchMapping { id = "74", name = "X HAND ENCODER A" },
            new SwitchMapping { id = "75", name = "X HAND ENCODER B" },
            new SwitchMapping { id = "76", name = "Y HAND ENCODER B" },
            new SwitchMapping { id = "77", name = "Y HAND ENCODER A" },
            new SwitchMapping { id = "78", name = "SHOOTER LANE" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "BALL IN HAND" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-jm.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#C82C23",
            "#F1D1A7",
            "#F1C251",
            "#B5C0C6",
            "#001689"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "12", "22", "43",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "74", "75", "76", "77"
                "31", "36", "76"

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
                    offset = 0x1B9E,
                    value = 0x01
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            checksum = new ChecksumData[]
            {
                new ChecksumData { dataStartOffset = 0x1C61, dataEndOffset = 0x1C84, checksumOffset = 0x1C85, checksum = "16bit", name = "HI_SCORE" },
                new ChecksumData { dataStartOffset = 0x1C87, dataEndOffset = 0x1C8F, checksumOffset = 0x1C90, checksum = "16bit", name = "CHAMPION" },
                new ChecksumData { dataStartOffset = 0x1B20, dataEndOffset = 0x1BF8, checksumOffset = 0x1BF9, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x3B2, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x3B3, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },

                new MemoryPositionData { offset = 0x43E, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16A7, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16AE, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16B5, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

                new MemoryPositionData { offset = 0x1711, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1B20, name = "GAME_BALL_TOTAL", description = "Balls per game", type = "uint8" },
                new MemoryPositionData { offset = 0x1B9E, name = "STAT_FREEPLAY", description = "0: not free, 1: free", type = "uint8" },

                new MemoryPositionData { offset = 0x1C61, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C64, name = "HISCORE_1_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C6A, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C6D, name = "HISCORE_2_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C73, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C76, name = "HISCORE_3_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C7C, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C7F, name = "HISCORE_4_SCORE", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1C87, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1C8A, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 6 },

                new MemoryPositionData { offset = 0x1C98, name = "GAME_CREDITS_FULL", description = "0-10 credits", type = "uint8" },
                new MemoryPositionData { offset = 0x1C99, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}