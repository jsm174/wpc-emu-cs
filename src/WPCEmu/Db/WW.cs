namespace WPCEmu.Db
{
    public class WhiteWater : IDb
    {
        public string name => "WPC-Fliptronics: White Water";
        public string version => "L-5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "ww_p1", "ww_p2", "ww_p8", "ww_p9", "ww_l2", "ww_d2", "ww_l3", "ww_d3", "ww_l4", "ww_d4", "ww_l5", "ww_d5", "ww_lh5", "ww_lh6", "ww_lh6c" },
            gameName = "White Water",
            id = "ww",
            vpdbId = "whitewater"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "wwatr_l5.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "OUTHOLE" },
            new SwitchMapping { id = "16", name = "LEFT JET" },
            new SwitchMapping { id = "17", name = "RIGHT JET" },
            new SwitchMapping { id = "18", name = "CENTER JET" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKET OPTO" },
            new SwitchMapping { id = "25", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "26", name = "LEFT FLIP LANE" },
            new SwitchMapping { id = "27", name = "RIGHT FLIP LANE" },
            new SwitchMapping { id = "28", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "31", name = "RIVER \"R2\"" },
            new SwitchMapping { id = "32", name = "RIVER \"E\"" },
            new SwitchMapping { id = "33", name = "RIVER \"V\"" },
            new SwitchMapping { id = "34", name = "RIVER \"I\"" },
            new SwitchMapping { id = "35", name = "RIVER \"R1\"" },
            new SwitchMapping { id = "36", name = "THREE BANK TOP" },
            new SwitchMapping { id = "37", name = "THREE BANK CNTR" },
            new SwitchMapping { id = "38", name = "THREE BANK LOWER" },

            new SwitchMapping { id = "41", name = "LIGHT LOCK LEFT" },
            new SwitchMapping { id = "42", name = "LIGHT LOCK RIGHT" },
            new SwitchMapping { id = "43", name = "LEFT LOOP" },
            new SwitchMapping { id = "44", name = "RIGHT LOOP" },
            new SwitchMapping { id = "45", name = "SECRET PASSAGE" },
            new SwitchMapping { id = "46", name = "LFT RAMP ENTER" },
            new SwitchMapping { id = "47", name = "RAPIDS ENTER" },
            new SwitchMapping { id = "48", name = "CANYON ENTRANCE" },

            new SwitchMapping { id = "51", name = "LEFT SLING" },
            new SwitchMapping { id = "52", name = "RIGHT SLING" },
            new SwitchMapping { id = "53", name = "BALLSHOOTER" },
            new SwitchMapping { id = "54", name = "LOWER JET ARENA" },
            new SwitchMapping { id = "55", name = "RIGHT JET ARENA" },
            new SwitchMapping { id = "56", name = "EXTRA BALL" },
            new SwitchMapping { id = "57", name = "CANYON MAIN" },
            new SwitchMapping { id = "58", name = "BIGFOOT CAVE" },

            new SwitchMapping { id = "61", name = "WHIRLPOOL POPPER" },
            new SwitchMapping { id = "62", name = "WHIRLPOOL EXIT" },
            new SwitchMapping { id = "63", name = "LOCKUP RIGHT" },
            new SwitchMapping { id = "64", name = "LOCKUP CENTER" },
            new SwitchMapping { id = "65", name = "LOCKUP LEFT" },
            new SwitchMapping { id = "66", name = "LEFT RAMP MAIN" },
            new SwitchMapping { id = "68", name = "DISAS DROP ENTER" },

            new SwitchMapping { id = "71", name = "RAPIDS RAMP MAIN" },
            new SwitchMapping { id = "73", name = "HOT FOOT UPPER" },
            new SwitchMapping { id = "74", name = "HOT FOOT LOWER" },
            new SwitchMapping { id = "75", name = "DISAS DROP MAIN" },
            new SwitchMapping { id = "76", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "77", name = "CENTER TROUGH" },
            new SwitchMapping { id = "78", name = "LEFT TROUGH" },

            new SwitchMapping { id = "86", name = "BIGFOOT OPTO 1" },
            new SwitchMapping { id = "87", name = "BIGFOOT OPTO 2" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-ww.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 12, y = 316, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 12, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 30, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 154, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 171, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 63, y = 76, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 51, y = 274, color = "RED" } },

                new Lamp[] { new Lamp { x = 35, y = 202, color = "LBLUE" } }, //21
                new Lamp[] { new Lamp { x = 35, y = 214, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 36, y = 224, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 36, y = 236, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 37, y = 246, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 73, y = 110, color = "RED" } },
                new Lamp[] { new Lamp { x = 75, y = 126, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 76, y = 133, color = "LBLUE" } },

                new Lamp[] { new Lamp { x = 66, y = 216, color = "YELLOW" } }, //31
                new Lamp[] { new Lamp { x = 87, y = 192, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 80, y = 196, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 57, y = 181, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 52, y = 161, color = "RED" } },
                new Lamp[] { new Lamp { x = 63, y = 150, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 69, y = 322, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 63, y = 310, color = "LBLUE" } },

                new Lamp[] { new Lamp { x = 109, y = 175, color = "YELLOW" } }, //41
                new Lamp[] { new Lamp { x = 112, y = 185, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 77, y = 143, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 107, y = 165, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 101, y = 150, color = "RED" } },
                new Lamp[] { new Lamp { x = 90, y = 147, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 59, y = 297, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 55, y = 285, color = "ORANGE" } },

                new Lamp[] { new Lamp { x = 178, y = 172, color = "RED" } }, //51
                new Lamp[] { new Lamp { x = 14, y = 120, color = "RED" } },
                new Lamp[] { new Lamp { x = 91, y = 91, color = "RED" } },
                new Lamp[] { new Lamp { x = 100, y = 83, color = "RED" } },
                new Lamp[] { new Lamp { x = 78, y = 50, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 127, y = 259, color = "RED" } },
                new Lamp[] { new Lamp { x = 130, y = 243, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 87, y = 310, color = "YELLOW" } }, //61
                new Lamp[] { new Lamp { x = 118, y = 292, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 105, y = 270, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 81, y = 282, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 89, y = 257, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 94, y = 239, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 111, y = 231, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 117, y = 239, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } }, //71
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 123, y = 107, color = "RED" } },
                new Lamp[] { new Lamp { x = 133, y = 100, color = "RED" } },

                new Lamp[] { new Lamp { x = 144, y = 240, color = "YELLOW" } }, //81
                new Lamp[] { new Lamp { x = 147, y = 222, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 163, y = 215, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 163, y = 195, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 20, y = 395, color = "YELLOW" } }

            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 150, y = 77 },
                new Flashlamp { id = "19", x = 23, y = 23 },
                new Flashlamp { id = "20", x = 39, y = 37 },
                new Flashlamp { id = "21", x = 10, y = 170 },
                new Flashlamp { id = "22", x = 10, y = 238 },
                new Flashlamp { id = "23", x = 134, y = 153 }
            }
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#3662F1",
            "#CFBB3E",
            "#8EC0E0",
            "#C7B3B7"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "86", "87",
                "76", "77", "78",
                "86", "87"
            },
            initialAction = new InitialAction[]
            {
                new InitialAction
                {
                    delayMs = 1500,
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
                new ChecksumData { dataStartOffset = 0x1C61, dataEndOffset = 0x1C80, checksumOffset = 0x1C81, checksum = "16bit", name = "HI_SCORE" },
                new ChecksumData { dataStartOffset = 0x1C83, dataEndOffset = 0x1C8A, checksumOffset = 0x1C8B, checksum = "16bit", name = "CHAMPION" },
                new ChecksumData { dataStartOffset = 0x1B20, dataEndOffset = 0x1BF8, checksumOffset = 0x1BF9, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x86, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x40F, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x410, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x48B, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x1730, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1736, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x173C, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1742, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x17A5, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

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
                new MemoryPositionData { offset = 0x1C64, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C69, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C6C, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C71, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C74, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C79, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C7C, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C83, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1C86, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x1C93, name = "GAME_CREDITS_FULL", description = "0-10 credits", type = "uint8" },
                new MemoryPositionData { offset = 0x1C94, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}