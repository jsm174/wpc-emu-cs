namespace WPCEmu.Db
{
    public class MonsterBash : IDb
    {
        public string name => "WPC-95: Monster Bash";
        public string version => "1.06b";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "mb_05", "mb_10", "mb_106", "mb_106b" },
            gameName = "Monster Bash",
            id = "mb"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "mb_106b.bin"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "LAUNCH BUTTON" },
            new SwitchMapping { id = "12", name = "DRAC STANDUP TOP" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "DRAC STANDUP BOT" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT RETURN" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TOMB TREASURE" },
            new SwitchMapping { id = "25", name = "DRACULA TARGET" },
            new SwitchMapping { id = "26", name = "LEFT RETURN" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "LEFT EJECT" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "RIGHT POPPER" },

            new SwitchMapping { id = "42", name = "L FLIP OPTO" },
            new SwitchMapping { id = "43", name = "R FLIP OPTO" },
            new SwitchMapping { id = "44", name = "L BLUE TGT" },
            new SwitchMapping { id = "45", name = "C BLUE TGT" },
            new SwitchMapping { id = "46", name = "R BLUE TGT" },
            new SwitchMapping { id = "47", name = "L FLIP PROX" },
            new SwitchMapping { id = "48", name = "R FLIP PROX" },

            new SwitchMapping { id = "51", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "52", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "53", name = "LEFT JET" },
            new SwitchMapping { id = "54", name = "RIGHT JET" },
            new SwitchMapping { id = "55", name = "BOTTOM JET" },
            new SwitchMapping { id = "56", name = "LEFT TOP LANE" },
            new SwitchMapping { id = "57", name = "MIDDLE TOP LANE" },
            new SwitchMapping { id = "58", name = "RIGHT TOP LANE" },

            new SwitchMapping { id = "61", name = "LEFT LOOP LO" },
            new SwitchMapping { id = "62", name = "LEFT LOOP HI" },
            new SwitchMapping { id = "63", name = "RIGHT LOOP LO" },
            new SwitchMapping { id = "64", name = "RIGHT LOOP HI" },
            new SwitchMapping { id = "65", name = "CENTER LOOP" },
            new SwitchMapping { id = "66", name = "L RAMP ENTER" },
            new SwitchMapping { id = "67", name = "L RAMP EXIT" },
            new SwitchMapping { id = "68", name = "C RAMP ENTER" },

            new SwitchMapping { id = "71", name = "R RAMP ENTER" },
            new SwitchMapping { id = "72", name = "R RAMP EXIT" },
            new SwitchMapping { id = "73", name = "R RAMP LOCK" },
            new SwitchMapping { id = "74", name = "DRAC POSITION 5" },
            new SwitchMapping { id = "75", name = "DRAC POSITION 4" },
            new SwitchMapping { id = "76", name = "DRAC POSITION 3" },
            new SwitchMapping { id = "77", name = "DRAC POSITION 2" },
            new SwitchMapping { id = "78", name = "DRAC POSITION 1" },

            new SwitchMapping { id = "81", name = "UP/DN BANK UP" },
            new SwitchMapping { id = "82", name = "UP/DN BANK DOWN" },
            new SwitchMapping { id = "83", name = "FRANK TABLE DOWN" },
            new SwitchMapping { id = "84", name = "FRANK TABLE UP" },
            new SwitchMapping { id = "85", name = "L UP/DN BANK TGT" },
            new SwitchMapping { id = "86", name = "R UP/DN BANK TGT" },
            new SwitchMapping { id = "87", name = "FRANK HIT" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F7", name = "CENTER SPINNER" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => new SolenoidMapping[]
        {
            new SolenoidMapping { id = "01", name = "AUTO PLUNGER" },
            new SolenoidMapping { id = "02", name = "BRIDE POST" },
            new SolenoidMapping { id = "03", name = "MUMMY COFFIN" },
            new SolenoidMapping { id = "04", name = "NOT USED" },
            new SolenoidMapping { id = "05", name = "LEFT GATE" },
            new SolenoidMapping { id = "06", name = "RIGHT GATE" },
            new SolenoidMapping { id = "07", name = "NOT USED" },
            new SolenoidMapping { id = "08", name = "RAMP LOCK POST" },
            new SolenoidMapping { id = "09", name = "TROUGH EJECT" },
            new SolenoidMapping { id = "10", name = "LEFT SLINGSHOT" },
            new SolenoidMapping { id = "11", name = "RIGHT SLINGSHOT" },
            new SolenoidMapping { id = "12", name = "LEFT JET BUMPER" },
            new SolenoidMapping { id = "13", name = "RIGHT JET BUMPER" },
            new SolenoidMapping { id = "14", name = "BOTTOM JET BUMPER" },
            new SolenoidMapping { id = "15", name = "LEFT EJECT" },
            new SolenoidMapping { id = "16", name = "RIGHT POPPER" },
            new SolenoidMapping { id = "17", name = "WOLFMAN FLASHERS" },
            new SolenoidMapping { id = "18", name = "BRIDE FLASHERS" },
            new SolenoidMapping { id = "19", name = "FRANKENSTEIN FLASHERS" },
            new SolenoidMapping { id = "20", name = "DRACULA COFFIN FLASHERS" },
            new SolenoidMapping { id = "21", name = "CREATURE FLASHERS" },
            new SolenoidMapping { id = "22", name = "JETS/MUMMY FLASHERS" },
            new SolenoidMapping { id = "23", name = "RIGHT POPPER FLASHER" },
            new SolenoidMapping { id = "24", name = "FRANK ARROW FLASHER" },
            new SolenoidMapping { id = "25", name = "MONSTERS OF ROCK FLSHR" },
            new SolenoidMapping { id = "26", name = "WOLFMAN LOOP FLASHERS" },
            new SolenoidMapping { id = "27", name = "FRANKENSTEIN MOTOR" },
            new SolenoidMapping { id = "28", name = "UP/DOWN BANK MOTOR" },
            new SolenoidMapping { id = "29", name = "LOWER RIGHT FLIPPER POWER" },
            new SolenoidMapping { id = "30", name = "LOWER RIGHT FLIPPER HOLD" },
            new SolenoidMapping { id = "31", name = "LOWER LEFT FLIPPER POWER" },
            new SolenoidMapping { id = "32", name = "LOWER LEFT FLIPPER HOLD" },
            new SolenoidMapping { id = "37", name = "DRACULA MOTOR FORWARD" },
            new SolenoidMapping { id = "38", name = "DRACULA MOTOR BACKWARD" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-mb.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#0F238D",
            "#54B85D",
            "#EBE460",
            "#CB348C",
            "#4FB4E0",
            "#C4382C"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22", "81",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "74", "75", "76", "77", "78"
                "31", "36", "42", "43", "74", "75", "76", "77", "78",
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

                new MemoryPositionData { offset = 0x458, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x583, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1C93, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C96, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C9B, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C9E, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CA3, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CA6, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CAB, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CAE, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CB5, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1CB8, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}