namespace WPCEmu.Db
{
    public class FishTales : IDb
    {
        public string name => "WPC-Fliptronics: Fish Tales";
        public string version => "L-5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "ft_p4", "ft_p5", "ft_l3", "ft_l4", "ft_l5", "ft_d5", "ft_l5p", "ft_d6" },
            gameName = "Fish Tales",
            id = "ft"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "FSHTL_5.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "CREDIT BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "OUTHOLE" },
            new SwitchMapping { id = "16", name = "TROUGH 1" },
            new SwitchMapping { id = "17", name = "TROUGH 2" },
            new SwitchMapping { id = "18", name = "TROUGH 3" },
            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "26", name = "LEFT RETURN LANE" },
            new SwitchMapping { id = "27", name = "LEFT STANDUP 1" },
            new SwitchMapping { id = "28", name = "LEFT STANDUP 2" },
            new SwitchMapping { id = "31", name = "CAST" },
            new SwitchMapping { id = "32", name = "LEFT BOAT EXIT" },
            new SwitchMapping { id = "33", name = "RIGHT BOAT EXIT" },
            new SwitchMapping { id = "34", name = "SPINNER" },
            new SwitchMapping { id = "35", name = "REEL ENTRY" },
            new SwitchMapping { id = "36", name = "CATAPULT" },
            new SwitchMapping { id = "37", name = "REEL 1 OPTO" },
            new SwitchMapping { id = "38", name = "REEL 2 OPTO" },
            new SwitchMapping { id = "41", name = "CAPTIVE BALL" },
            new SwitchMapping { id = "42", name = "RIGHT BOAT ENTRY" },
            new SwitchMapping { id = "43", name = "LEFT BOAT ENTRY" },
            new SwitchMapping { id = "44", name = "LIE E" },
            new SwitchMapping { id = "45", name = "LIE I" },
            new SwitchMapping { id = "46", name = "LIE L" },
            new SwitchMapping { id = "47", name = "BALL POPPER" },
            new SwitchMapping { id = "48", name = "DROP TARGET" },
            new SwitchMapping { id = "51", name = "LEFT JET" },
            new SwitchMapping { id = "52", name = "CENTER JET" },
            new SwitchMapping { id = "53", name = "RIGHT JET" },
            new SwitchMapping { id = "54", name = "RIGHT STANDUP 1" },
            new SwitchMapping { id = "55", name = "RIGHT STANDUP 2" },
            new SwitchMapping { id = "56", name = "BALL SHOOTER" },
            new SwitchMapping { id = "57", name = "LEFT SLING" },
            new SwitchMapping { id = "58", name = "RIGHT SLING" },
            new SwitchMapping { id = "61", name = "EXTRA BALL" },
            new SwitchMapping { id = "62", name = "TOP RIGHT LOOP" },
            new SwitchMapping { id = "63", name = "TOP EJECT HOLE" },
            new SwitchMapping { id = "64", name = "TOP LEFT LOOP" },
            new SwitchMapping { id = "65", name = "RIGHT RETURN" },
            new SwitchMapping { id = "66", name = "RIGHT OUTLANE" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-ft.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 93, y = 165, color = "WHITE" } }, //11
                new Lamp[] { new Lamp { x = 93, y = 155, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 93, y = 145, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 93, y = 135, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 93, y = 125, color = "RED" } },
                new Lamp[] { new Lamp { x = 102, y = 25, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 128, y = 23, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 151, y = 25, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 66, y = 260, color = "YELLOW" } }, //21
                new Lamp[] { new Lamp { x = 84, y = 268, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 100, y = 268, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 119, y = 260, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 50, y = 172, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 46, y = 148, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 43, y = 123, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 39, y = 102, color = "RED" } },

                new Lamp[] { new Lamp { x = 66, y = 277, color = "YELLOW" } }, //31
                new Lamp[] { new Lamp { x = 84, y = 280, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 100, y = 280, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 119, y = 277, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 110, y = 160, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 110, y = 128, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 77, y = 160, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 77, y = 128, color = "LPURPLE" } },

                new Lamp[] { new Lamp { x = 75, y = 300, color = "GREEN" } }, //41
                new Lamp[] { new Lamp { x = 92, y = 300, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 92, y = 347, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 107, y = 300, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 56, y = 225, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 50, y = 234, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 48, y = 246, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 15, y = 272, color = "RED" }, new Lamp { x = 170, y = 272, color = "RED" } },

                new Lamp[] { new Lamp { x = 80, y = 332, color = "YELLOW" } }, //51
                new Lamp[] { new Lamp { x = 91, y = 329, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 92, y = 313, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 103, y = 332, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 152, y = 238, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 151, y = 226, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 147, y = 212, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 28, y = 272, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 61, y = 205, color = "GREEN" } }, //61
                new Lamp[] { new Lamp { x = 74, y = 209, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 86, y = 214, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 99, y = 214, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 110, y = 209, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 123, y = 205, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 93, y = 196, color = "RED" } },
                new Lamp[] { new Lamp { x = 175, y = 272, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 170, y = 84, color = "YELLOW" } }, //71
                new Lamp[] { new Lamp { x = 152, y = 155, color = "RED" } },
                new Lamp[] { new Lamp { x = 158, y = 138, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 161, y = 128, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 165, y = 117, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 168, y = 172, color = "LPURPLE" } },
                new Lamp[] { new Lamp { x = 175, y = 151, color = "RED" } },
                new Lamp[] { new Lamp { x = 148, y = 121, color = "ORANGE" } },

                new Lamp[] { new Lamp { x = 15, y = 60, color = "YELLOW" } }, //81
                new Lamp[] { new Lamp { x = 15, y = 52, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 15, y = 44, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 15, y = 36, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 15, y = 28, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 189, y = 395, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 33, y = 391, color = "YELLOW" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 38, y = 101 },
                new Flashlamp { id = "18", x = 93, y = 196 },
                new Flashlamp { id = "19", x = 93, y = 125 },
                new Flashlamp { id = "20", x = 93, y = 135 },
                new Flashlamp { id = "21", x = 93, y = 145 },
                new Flashlamp { id = "22", x = 93, y = 155 },
                new Flashlamp { id = "23", x = 93, y = 165 },
                new Flashlamp { id = "25", x = 31, y = 153 },
                new Flashlamp { id = "26", x = 24, y = 92 },
                new Flashlamp { id = "26", x = 62, y = 49 },
                new Flashlamp { id = "27", x = 168, y = 109 },
                new Flashlamp { id = "28", x = 20, y = 180 }
            }
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#EA3223",
            "#54A7E1",
            "#FAEC53",
            "#913D17"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "16", "17", "18",
                "22",
                "38", "48"
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
                new MemoryPositionData { offset = 0x7A, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                //new MemoryPositionData { offset = 0x326, name = "TEXT", description = "random visible text", type = "string" },
                new MemoryPositionData { offset = 0x355, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x356, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },

                new MemoryPositionData { offset = 0x42B, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

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
                new MemoryPositionData { offset = 0x18B9, name = "STAT_PLAYTIME", description = "Minutes playing", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x18C5, name = "STAT_BALLS_PLAYED", type = "uint8", length = 3 },
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
                new MemoryPositionData { offset = 0x1C94, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" },
                //new MemoryPositionData { offset = 0x1C95, description: 'credits checksum 1 (2 * full + half)", type = "uint8" },
                //new MemoryPositionData { offset = 0x1C9B, description: 'credits checksum 2 (0xff - (full + half + checksum1))", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}

/*
# BALL STATE FISHTALES

INITIAL STATE
- TROUGH 1, TROUGH 2 and TROUGH 3 are on (ball on switches)

BALL IN SHOOTER LANE
- TROUGH 2, TROUGH 3 and BALL SHOOTER are on

BALL IN GAME
- TROUGH 2, TROUGH 3

BALL DRAIN
- TROUGH 2, TROUGH 3, OUTHOLE on
- TROUGH 1, TROUGH 2, TROUGH 3 on
*/