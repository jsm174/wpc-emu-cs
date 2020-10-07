namespace WPCEmu.Db
{
    public class CactusCanyon : IDb
    {
        public string name => "WPC-95: Cactus Canyon";
        public string version => "1.3";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "cc_10", "cc_12", "cc_13", "cc_13k", "cc_104" },
            gameName = "Cactus Canyon",
            id = "cc"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "cc_g11.1_3"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "MINE ENTRANCE" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "R RETURN" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "26", name = "L RETURN" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "R STANDUP (BOT)" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "L LOOP BOTTOM" },
            new SwitchMapping { id = "37", name = "RT LOOP BOTTOM" },

            new SwitchMapping { id = "41", name = "MINE POPPER" },
            new SwitchMapping { id = "42", name = "SALOON POPPER" },
            new SwitchMapping { id = "44", name = "R STANDUP (TOP)" },
            new SwitchMapping { id = "46", name = "BEER MUG SWITCH" },
            new SwitchMapping { id = "47", name = "L BONUS X LANE" },
            new SwitchMapping { id = "48", name = "JET EXIT" },

            new SwitchMapping { id = "51", name = "L SLINGSHOT" },
            new SwitchMapping { id = "52", name = "R SLINGSHOT" },
            new SwitchMapping { id = "53", name = "LEFT JET" },
            new SwitchMapping { id = "54", name = "RIGHT JET" },
            new SwitchMapping { id = "55", name = "BOTTOM JET" },
            new SwitchMapping { id = "56", name = "RIGHT LOOP TOP" },
            new SwitchMapping { id = "57", name = "R BONUS X LANE" },
            new SwitchMapping { id = "58", name = "LEFT LOOP TOP" },

            new SwitchMapping { id = "61", name = "DROP #1 (L)" },
            new SwitchMapping { id = "62", name = "DROP #2 (LC)" },
            new SwitchMapping { id = "63", name = "DROP #3 (RC)" },
            new SwitchMapping { id = "64", name = "DROP #4 (R)" },
            new SwitchMapping { id = "65", name = "R RAMP MAKE" },
            new SwitchMapping { id = "66", name = "R RAMP ENTER" },
            new SwitchMapping { id = "67", name = "SKILL BOWL" },
            new SwitchMapping { id = "68", name = "BOT R RAMP" },

            new SwitchMapping { id = "71", name = "TRAIN ENCODER" },
            new SwitchMapping { id = "72", name = "TRAIN HOME" },
            new SwitchMapping { id = "73", name = "SALOON GATE" },
            new SwitchMapping { id = "75", name = "SALOON BART TOY" },
            new SwitchMapping { id = "77", name = "MINE HOME" },
            new SwitchMapping { id = "78", name = "MINE ENCODER" },

            new SwitchMapping { id = "82", name = "C RAMP ENTER" },
            new SwitchMapping { id = "83", name = "L RAMP MAKE" },
            new SwitchMapping { id = "84", name = "C RAMP MAKE" },
            new SwitchMapping { id = "85", name = "L RAMP ENTER" },
            new SwitchMapping { id = "86", name = "L STANDUP (TOP)" },
            new SwitchMapping { id = "87", name = "L STANDUP (BOT)" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-cc.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 56, y = 287, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 72, y = 277, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 90, y = 273, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 110, y = 277, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 125, y = 287, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 90, y = 285, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 20, y = 10, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 30, y = 10, color = "WHITE" } },

                new Lamp[] { new Lamp { x = 127, y = 264, color = "WHITE" } }, // 21
                new Lamp[] { new Lamp { x = 115, y = 257, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 120, y = 246, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 103, y = 253, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 121, y = 114, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 121, y = 133, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 72, y = 155, color = "RED" } },
                new Lamp[] { new Lamp { x = 75, y = 174, color = "GREEN" } },

                new Lamp[] { new Lamp { x = 116, y = 178, color = "YELLOW" } }, // 31
                new Lamp[] { new Lamp { x = 53, y = 207, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 37, y = 242, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 136, y = 234, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 144, y = 221, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 149, y = 208, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 156, y = 194, color = "RED" } },
                new Lamp[] { new Lamp { x = 162, y = 176, color = "WHITE" } },

                new Lamp[] { new Lamp { x = 153, y = 117, color = "WHITE" } }, // 41
                new Lamp[] { new Lamp { x = 149, y = 135, color = "RED" } },
                new Lamp[] { new Lamp { x = 140, y = 144, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 139, y = 151, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 137, y = 157, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 27, y = 194, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 38, y = 335, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 12, y = 317, color = "RED" } },

                new Lamp[] { new Lamp { x = 143, y = 188, color = "YELLOW" } }, // 51
                new Lamp[] { new Lamp { x = 149, y = 245, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 154, y = 229, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 95, y = 190, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 95, y = 177, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 95, y = 163, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 95, y = 149, color = "RED" } },
                new Lamp[] { new Lamp { x = 95, y = 125, color = "WHITE" } },

                new Lamp[] { new Lamp { x = 75, y = 241, color = "LBLUE" } }, // 61
                new Lamp[] { new Lamp { x = 72, y = 227, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 68, y = 213, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 65, y = 201, color = "RED" } },
                new Lamp[] { new Lamp { x = 58, y = 177, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 154, y = 295, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 169, y = 317, color = "RED" } },
                new Lamp[] { new Lamp { x = 143, y = 337, color = "WHITE" } },

                new Lamp[] { new Lamp { x = 62, y = 307, color = "YELLOW" } }, // 71
                new Lamp[] { new Lamp { x = 118, y = 307, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 108, y = 335, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 28, y = 178, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 34, y = 200, color = "RED" } },
                new Lamp[] { new Lamp { x = 30, y = 210, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 43, y = 218, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 48, y = 230, color = "LPURPLE" } },

                new Lamp[] { new Lamp { x = 91, y = 313, color = "YELLOW" } }, // 81
                new Lamp[] { new Lamp { x = 91, y = 373, color = "RED" } },
                new Lamp[] { new Lamp { x = 73, y = 335, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 81, y = 208, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 12, y = 394, color = "YELLOW" } }
            }
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#EACD52",
            "#A73A32",
            "#718F49",
            "#4D779C"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "41", "42"
                "31", "41", "42",
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

                new MemoryPositionData { offset = 0x455, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x584, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A7, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AE, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B5, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1CD9, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CDC, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CE1, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CE4, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CE9, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CEC, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CF1, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1CF4, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1CFB, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1CFE, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}