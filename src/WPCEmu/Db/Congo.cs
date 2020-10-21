namespace WPCEmu.Db
{
    public class Congo : IDb
    {
        public string name => "WPC-95: Congo";
        public string version => "2.1";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "congo_11", "congo_13", "congo_20", "congo_21" },
            gameName = "Congo",
            id = "congo"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "cg_g11.2_1"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "INNER LEFT LOOP" },
            new SwitchMapping { id = "12", name = "UPPER LOOP" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "JET EXIT" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT RETURN LANE" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "25", name = "RIGHT EJECT RUBBER" },
            new SwitchMapping { id = "26", name = "LEFT RETURN INLANE" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "\"YOU\" STANDUP TARGET" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "VOLCANO STACK" },
            new SwitchMapping { id = "37", name = "MYSTERY EJECT" },
            new SwitchMapping { id = "38", name = "RIGHT EJECT" },

            new SwitchMapping { id = "41", name = "LOCK BALL 1" },
            new SwitchMapping { id = "42", name = "LOCK BALL 2" },
            new SwitchMapping { id = "43", name = "LOCK BALL 3" },
            new SwitchMapping { id = "44", name = "MINE SHAFT" },
            new SwitchMapping { id = "45", name = "LEFT LOOP" },
            new SwitchMapping { id = "46", name = "LEFT BANK TOP" },
            new SwitchMapping { id = "47", name = "LEFT BANK CENTER" },
            new SwitchMapping { id = "48", name = "LEFT BANK BOTTOM" },

            new SwitchMapping { id = "51", name = "TRAVI" },
            new SwitchMapping { id = "52", name = "COM" },
            new SwitchMapping { id = "53", name = "2-WAY POPPER" },
            new SwitchMapping { id = "54", name = "\"WE ARE\" STANDUP TARGET" },
            new SwitchMapping { id = "55", name = "\"WATCHING\" STANDUP TARGET" },
            new SwitchMapping { id = "56", name = "PERIMETER DEFENSE" },
            new SwitchMapping { id = "57", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "58", name = "LEFT RAMP EXIT" },

            new SwitchMapping { id = "61", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "62", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "63", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "64", name = "RIGHT JET BUMPER" },
            new SwitchMapping { id = "65", name = "BOTTOM JET BUMPER" },
            new SwitchMapping { id = "67", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "68", name = "RIGHT RAMP EXIT" },

            new SwitchMapping { id = "71", name = "(A)MY" },
            new SwitchMapping { id = "72", name = "A(M)Y" },
            new SwitchMapping { id = "73", name = "AM(Y)" },
            new SwitchMapping { id = "74", name = "(C)ONGO" },
            new SwitchMapping { id = "75", name = "C(O)NGO" },
            new SwitchMapping { id = "76", name = "CO(N)GO" },
            new SwitchMapping { id = "77", name = "CON(G)O" },
            new SwitchMapping { id = "78", name = "CONG(O)" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "RIGHT SPINNER" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "LEFT SPINNER" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-congo.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#D1DE62",
            "#DBE559",
            "#DE733A",
            "#96A1A4"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "41", "42", "43"
                "31", "41", "42", "43"
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
                new ChecksumData { dataStartOffset = 0x1CE3, dataEndOffset = 0x1D06, checksumOffset = 0x1D07, checksum = "16bit", name = "HI_SCORE" },
                new ChecksumData { dataStartOffset = 0x1D09, dataEndOffset = 0x1D11, checksumOffset = 0x1D12, checksum = "16bit", name = "CHAMPION" },
                new ChecksumData { dataStartOffset = 0x1B98, dataEndOffset = 0x1C7A, checksumOffset = 0x1C7B, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x3B2, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x3B3, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },

                new MemoryPositionData { offset = 0x440, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x579, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16A7, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16AE, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x16B5, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

                //0x1885, 0x188D
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

                new MemoryPositionData { offset = 0x1B98, name = "GAME_BALL_TOTAL", description = "Balls per game", type = "uint8" },
                new MemoryPositionData { offset = 0x1C16, name = "STAT_FREEPLAY", description = "0: not free, 1: free", type = "uint8" },

                new MemoryPositionData { offset = 0x1CE3, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CE7, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CEC, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CF0, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CF5, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CF9, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CFE, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D02, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D09, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D0D, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x1D1A, name = "GAME_CREDITS_FULL", description = "0-10 credits", type = "uint8" },
                new MemoryPositionData { offset = 0x1D1B, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}