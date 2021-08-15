using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class Terminator2 : IDb
    {
        public string name => "WPC-DMD: Terminator 2";
        public string version => "L-8";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "t2_l2", "t2_d2", "t2_l3", "t2_d3", "t2_l4", "t2_d4", "t2_l6", "t2_d6", "t2_l8", "t2_d8", "t2_l81", "t2_l82", "t2_p2f", "t2_p2g" },
            gameName = "Terminator 2: Judgement Day",
            id = "t2"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "t2_l8.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "TROUGH LEFT" },
            new SwitchMapping { id = "16", name = "TROUGH CENTER" },
            new SwitchMapping { id = "17", name = "TROUGH RIGHT" },
            new SwitchMapping { id = "18", name = "OUTHOLE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "LEFT OUT LANE" },
            new SwitchMapping { id = "26", name = "LEFT RET. LANE" },
            new SwitchMapping { id = "27", name = "RIGHT RET. LANE" },
            new SwitchMapping { id = "28", name = "RIGHT OUT LANE" },

            new SwitchMapping { id = "31", name = "GUN LOADED" },
            new SwitchMapping { id = "32", name = "GUN MARK" },
            new SwitchMapping { id = "33", name = "GUN HOME" },
            new SwitchMapping { id = "34", name = "GRIP TRIGGER" },
            new SwitchMapping { id = "36", name = "STAND MID LEFT" },
            new SwitchMapping { id = "37", name = "STAND MID CENTER" },
            new SwitchMapping { id = "38", name = "STAND MID RIGHT" },

            new SwitchMapping { id = "41", name = "LEFT JET" },
            new SwitchMapping { id = "42", name = "RIGHT JET" },
            new SwitchMapping { id = "43", name = "BOTTOM JET" },
            new SwitchMapping { id = "44", name = "LEFT SLING" },
            new SwitchMapping { id = "45", name = "RIGHT SLING" },
            new SwitchMapping { id = "46", name = "STAND RIGHT TOP" },
            new SwitchMapping { id = "47", name = "STAND RIGHT MID" },
            new SwitchMapping { id = "48", name = "STAND RIGHT BOT" },

            new SwitchMapping { id = "51", name = "LEFT LOCK" },
            new SwitchMapping { id = "53", name = "LO ESCAPE ROUTE" },
            new SwitchMapping { id = "54", name = "HI ESCAPE ROUTE" },
            new SwitchMapping { id = "55", name = "TOP LOCK" },
            new SwitchMapping { id = "56", name = "TOP LANE LEFT" },
            new SwitchMapping { id = "57", name = "TOP LANE CENTER" },
            new SwitchMapping { id = "58", name = "TOP LANE RIGHT" },

            new SwitchMapping { id = "61", name = "LEFT RAMP ENTRY" },
            new SwitchMapping { id = "62", name = "LEFT RAMP MADE" },
            new SwitchMapping { id = "63", name = "RIGHT RAMP ENTRY" },
            new SwitchMapping { id = "64", name = "RIGHT RAMP MADE" },
            new SwitchMapping { id = "65", name = "LO CHASE LOOP" },
            new SwitchMapping { id = "66", name = "HI CHASE LOOP" },

            new SwitchMapping { id = "71", name = "TARGET 1 HI" },
            new SwitchMapping { id = "72", name = "TARGET 2" },
            new SwitchMapping { id = "73", name = "TARGET 3" },
            new SwitchMapping { id = "74", name = "TARGET 4" },
            new SwitchMapping { id = "75", name = "TARGET 5 LOW" },
            new SwitchMapping { id = "76", name = "BALL POPPER" },
            new SwitchMapping { id = "77", name = "DROP TARGET" },
            new SwitchMapping { id = "78", name = "SHOOTER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-t2.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 61, y = 309, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 74, y = 303, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 89, y = 301, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 102, y = 303, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 115, y = 309, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 88, y = 353, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 89, y = 283, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } }, //#17 NOT USED

                new Lamp[] { new Lamp { x = 18, y = 310, color = "ORANGE" } }, //#21
                new Lamp[] { new Lamp { x = 18, y = 291, color = "RED" }, new Lamp { x = 160, y = 291, color = "RED" } },
                new Lamp[] { new Lamp { x = 30, y = 279, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 147, y = 279, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 77, y = 160, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 74, y = 141, color = "GREEN" } }, //#26
                new Lamp[] { new Lamp { x = 71, y = 121, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 68, y = 100, color = "LPURPLE" } },

                new Lamp[] { new Lamp { x = 53, y = 146, color = "YELLOW" } }, //#31
                new Lamp[] { new Lamp { x = 55, y = 156, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 58, y = 165, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 60, y = 174, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 62, y = 183, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 89, y = 154, color = "RED" } },
                new Lamp[] { new Lamp { x = 98, y = 160, color = "RED" } },
                new Lamp[] { new Lamp { x = 105, y = 167, color = "RED" } },

                new Lamp[] { new Lamp { x = 34, y = 184, color = "GREEN" } }, //#41
                new Lamp[] { new Lamp { x = 40, y = 204, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 47, y = 219, color = "RED" } },
                new Lamp[] { new Lamp { x = 50, y = 233, color = "RED" } },
                new Lamp[] { new Lamp { x = 55, y = 246, color = "RED" } },
                new Lamp[] { new Lamp { x = 59, y = 260, color = "RED" } },
                new Lamp[] { new Lamp { x = 62, y = 272, color = "RED" } },
                new Lamp[] { new Lamp { x = 67, y = 286, color = "RED" } },

                new Lamp[] { new Lamp { x = 84, y = 268, color = "RED" }, new Lamp { x = 94, y = 268, color = "RED" } }, //#51
                new Lamp[] { new Lamp { x = 53, y = 64, color = "RED" }, new Lamp { x = 69, y = 64, color = "RED" } },
                new Lamp[] { new Lamp { x = 136, y = 220, color = "RED" } },
                new Lamp[] { new Lamp { x = 133, y = 234, color = "RED" } },
                new Lamp[] { new Lamp { x = 126, y = 247, color = "RED" } },
                new Lamp[] { new Lamp { x = 120, y = 260, color = "RED" } },
                new Lamp[] { new Lamp { x = 115, y = 272, color = "RED" } },
                new Lamp[] { new Lamp { x = 111, y = 286, color = "RED" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 87, y = 326 },
                new Flashlamp { id = "18", x = 143, y = 327 },
                new Flashlamp { id = "19", x = 35, y = 327 },
                new Flashlamp { id = "20", x = 28, y = 161 },
                new Flashlamp { id = "21", x = 179, y = 228 },
                new Flashlamp { id = "22", x = 155, y = 131 },
                new Flashlamp { id = "23", x = 28, y = 60 },
                new Flashlamp { id = "25", x = 13, y = 144 },
                new Flashlamp { id = "25", x = 13, y = 160 },
                new Flashlamp { id = "26", x = 37, y = 44 },
                new Flashlamp { id = "26", x = 46, y = 69 },
                new Flashlamp { id = "27", x = 77, y = 65 },
                new Flashlamp { id = "27", x = 80, y = 55 },
                new Flashlamp { id = "28", x = 63, y = 71 }
            }
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDmd",
        };

        public string[] cabinetColors => new string[]
        {
            "#3F96D6",
            "#BC3727",
            "#A3CEEA"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "15", "16", "17", "22",
                //OPTO Switches: "23"
                "23"
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
              new ChecksumData { dataStartOffset = 0x1C61, dataEndOffset = 0x1C80, checksumOffset = 0x1C81, checksum = "16bit", name = "HI_SCORE" },
              new ChecksumData { dataStartOffset = 0x1C83, dataEndOffset = 0x1C8A, checksumOffset = 0x1C8B, checksum = "16bit", name = "CHAMPION" },
              new ChecksumData { dataStartOffset = 0x1B20, dataEndOffset = 0x1BF8, checksumOffset = 0x1BF9, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x40F, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x410, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x481, name = "GAME_CURRENT_SCREEN", description = "0x00: attract mode, 0x01: game play/system menu, 0x80: tilt warning, 0xF1: credits view", type = "uint8" },

                new MemoryPositionData { offset = 0x172F, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1735, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x173B, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1741, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

                new MemoryPositionData { offset = 0x1794, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

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

                //new MemoryPositionData { offset = 0x1913, name = "STAT_LEFT_DRAIN", type = "uint8", length = 3 },
                //new MemoryPositionData { offset = 0x1919, name = "STAT_RIGHT_DRAIN", type = "uint8", length = 3 },
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
                //new MemoryPositionData { offset = 0x1C80, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}