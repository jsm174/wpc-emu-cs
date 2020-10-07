namespace WPCEmu.Db
{
    public class Hurricane : IDb
    {
        public string name => "WPC-DMD: Hurricane";
        public string version => "L-2";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "hurr_l2", "hurr_d2" },
            gameName = "Hurricane",
            id = "hurr",
            vpdbId = "hurricane"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "hurcnl_2.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "CREDIT BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "OUTHOLE" },
            new SwitchMapping { id = "16", name = "TROUGH 1" },
            new SwitchMapping { id = "17", name = "TROUGH 2" },
            new SwitchMapping { id = "18", name = "TROUGH 3" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "RIGHT SLING" },
            new SwitchMapping { id = "26", name = "RIGHT RETURN" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "BALL SHOOTER" },

            new SwitchMapping { id = "31", name = "FERRIS WHEEL" },
            new SwitchMapping { id = "33", name = "L DROP 1" },
            new SwitchMapping { id = "34", name = "L DROP 2" },
            new SwitchMapping { id = "35", name = "L DROP 3" },
            new SwitchMapping { id = "36", name = "LEFT SLING" },
            new SwitchMapping { id = "37", name = "LEFT RETURN" },
            new SwitchMapping { id = "38", name = "LEFT OUTLANE" },

            new SwitchMapping { id = "42", name = "RIGHT STANDUP 1" },
            new SwitchMapping { id = "43", name = "RIGHT STANDUP 2" },
            new SwitchMapping { id = "44", name = "RIGHT STANDUP 3" },
            new SwitchMapping { id = "45", name = "RIGHT STANDUP 4" },

            new SwitchMapping { id = "51", name = "LEFT JET" },
            new SwitchMapping { id = "52", name = "RIGHT JET" },
            new SwitchMapping { id = "53", name = "BOTTOM JET" },
            new SwitchMapping { id = "55", name = "DUNK THE DUMMY" },
            new SwitchMapping { id = "56", name = "LEFT JUGGLER" },
            new SwitchMapping { id = "57", name = "RIGHT JUGGLER" },

            new SwitchMapping { id = "61", name = "HURRICANE ENTRY" },
            new SwitchMapping { id = "62", name = "HURRICANE EXIT" },
            new SwitchMapping { id = "63", name = "COMET ENTRY" },
            new SwitchMapping { id = "64", name = "COMET EXIT" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => new SolenoidMapping[]
        {
            new SolenoidMapping { id = "1", name = "BACKBOX MOTOR" },
            new SolenoidMapping { id = "2", name = "LEFT BANK" },
            new SolenoidMapping { id = "4", name = "LEFT JUGGLER" },
            new SolenoidMapping { id = "5", name = "RIGHT JUGGLER" },
            new SolenoidMapping { id = "6", name = "FERRIS WHEELS" },
            new SolenoidMapping { id = "7", name = "KNOCKER" },
            new SolenoidMapping { id = "9", name = "OUTHOLE" },
            new SolenoidMapping { id = "10", name = "BALL RELEASE" },
            new SolenoidMapping { id = "11", name = "LEFT SLING" },
            new SolenoidMapping { id = "12", name = "RIGHT SLING" },
            new SolenoidMapping { id = "13", name = "LEFT JET" },
            new SolenoidMapping { id = "14", name = "RIGHT JET" },
            new SolenoidMapping { id = "15", name = "BOTTOM JET" },
            new SolenoidMapping { id = "15", name = "BOTTOM JET" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-hurricane.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 82, y = 312, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 98, y = 312, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 74, y = 325, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 91, y = 322, color = "RED" } },
                new Lamp[] { new Lamp { x = 107, y = 325, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 91, y = 338, color = "RED" } }, // CLOWN MOUTH
                new Lamp[] { new Lamp { x = 10, y = 288, color = "ORANGE" } }, //LEFT OUTLANE
                new Lamp[] { new Lamp { x = 26, y = 288, color = "ORANGE" } }, // LEFT RETURN LANE

                new Lamp[] { new Lamp { x = 77, y = 295, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 67, y = 285, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 59, y = 277, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 54, y = 269, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 45, y = 261, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 32, y = 220, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 35, y = 208, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 37, y = 197, color = "GREEN" } },

                new Lamp[] { new Lamp { x = 67, y = 265, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 75, y = 272, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 85, y = 276, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 96, y = 276, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 106, y = 272, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 113, y = 265, color = "WHITE" } }, //#36,PALACE E
                new Lamp[] { new Lamp { x = 170, y = 287, color = "ORANGE" } }, //RIGHT OUTLANE
                new Lamp[] { new Lamp { x = 155, y = 287, color = "ORANGE" } },

                new Lamp[] { new Lamp { x = 121, y = 244, color = "RED" } }, //#41, SPECIAL
                new Lamp[] { new Lamp { x = 124, y = 232, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 127, y = 221, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 131, y = 209, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 134, y = 197, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 154, y = 108, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 160, y = 99, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 167, y = 108, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 56, y = 167, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 55, y = 150, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 56, y = 133, color = "WHITE" } }, //MYSTERY
                new Lamp[] { new Lamp { x = 62, y = 110, color = "YELLOW" } }, //JACKPOT
                new Lamp[] { new Lamp { x = 90, y = 371, color = "ORANGE" } }, //#55, PLAY IT AGAIN
                new Lamp[] { new Lamp { x = 31, y = 165, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 60, y = 45, color = "YELLOW" } }, //#57, FERRIS WHEEL - UNKNOWN
                new Lamp[] { new Lamp { x = 87, y = 184, color = "WHITE" } },

                new Lamp[] { new Lamp { x = 79, y = 154, color = "GREEN" } }, //#61
                new Lamp[] { new Lamp { x = 92, y = 153, color = "RED" } },
                new Lamp[] { new Lamp { x = 77, y = 141, color = "ORANGE" } }, //#63
                new Lamp[] { new Lamp { x = 91, y = 140, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 86, y = 92, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 127, y = 80, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 116, y = 120, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 109, y = 188, color = "LBLUE" } },

                new Lamp[] { new Lamp { x = 91, y = 247, color = "YELLOW" } }, //#71
                new Lamp[] { new Lamp { x = 93, y = 233, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 95, y = 217, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 80, y = 214, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 153, y = 231, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 151, y = 219, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 149, y = 207, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 147, y = 195, color = "GREEN" } },

                new Lamp[] { new Lamp { x = 164, y = 57, color = "WHITE" } }, // #81
                new Lamp[] { new Lamp { x = 164, y = 30, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 189, y = 28, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 185, y = 59, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 155, y = 359, color = "WHITE" }, new Lamp { x = 31, y = 359, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 11, y = 389, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 39, y = 298, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 139, y = 298, color = "YELLOW" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 188, y = 211 }, new Flashlamp { id = "17", x = 188, y = 247 },
                new Flashlamp { id = "18", x = 166, y = 14 }, new Flashlamp { id = "18", x = 189, y = 28 },
                new Flashlamp { id = "19", x = 95, y = 216 },
                new Flashlamp { id = "20", x = 88, y = 185 },
                new Flashlamp { id = "21", x = 62, y = 111 },
                new Flashlamp { id = "22", x = 19, y = 119 },
                new Flashlamp { id = "23", x = 13, y = 12 }, new Flashlamp { id = "23", x = 35, y = 48 },
                new Flashlamp { id = "24", x = 36, y = 344 },
                new Flashlamp { id = "25", x = 138, y = 344 },
                new Flashlamp { id = "26", x = 95, y = 112 },
                new Flashlamp { id = "27", x = 116, y = 151 },
                new Flashlamp { id = "28", x = 9, y = 241 }
            }
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDmd"
        };

        public string[] cabinetColors => new string[]
        {
            "#B7341B",
            "#DA7E2E",
            "#FAE34C",
            "#ECBAB6"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "16", "17", "18", "22"
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
                    offset = 0x1B9C,
                    value = 0x01
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            checksum = new ChecksumData[]
            {
                new ChecksumData { dataStartOffset = 0x1C4D, dataEndOffset = 0x1C6C, checksumOffset = 0x1C6D, checksum = "16bit", name = "HI_SCORE" },
                new ChecksumData { dataStartOffset = 0x1C6F, dataEndOffset = 0x1C76, checksumOffset = 0x1C77, checksum = "16bit", name = "CHAMPION" },
                new ChecksumData { dataStartOffset = 0x1B20, dataEndOffset = 0x1BE4, checksumOffset = 0x1BE5, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x86, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                //new MemoryPositionData { offset = 0x323, name = "TEXT", description = "random visible text", type = "string" },
                new MemoryPositionData { offset = 0x3AC, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x3AD, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },

                new MemoryPositionData { offset = 0x420, name = "GAME_CURRENT_SCREEN", description = "0x00: attract mode, 0x01: game play/system menu, 0x80: tilt warning, 0xF1: credits view", type = "uint8" },
                new MemoryPositionData { offset = 0x494, name = "LANGUAGE", type = "uint8" },

                new MemoryPositionData { offset = 0x172F, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1735, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x173B, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1741, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

                new MemoryPositionData { offset = 0x178C, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

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
                new MemoryPositionData { offset = 0x19DF, name = "STAT_LEFT_FLIPPER_TRIG", type = "uint8", length = 3 },
                new MemoryPositionData { offset = 0x19E5, name = "STAT_RIGHT_FLIPPER_TRIG", type = "uint8", length = 3 },

                //0x1B7C Pricing Option
                new MemoryPositionData { offset = 0x1B20, name = "GAME_BALL_TOTAL", description = "Balls per game", type = "uint8" },
                new MemoryPositionData { offset = 0x1B9C, name = "STAT_FREEPLAY", description = "0: not free, 1: free", type = "uint8" },

                new MemoryPositionData { offset = 0x1C4D, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C50, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C55, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C58, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C5D, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C60, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C65, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C68, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C6F, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1C72, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x1C7F, name = "GAME_CREDITS_FULL", description = "0-10 credits", type = "uint8" },
                new MemoryPositionData { offset = 0x1C80, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;
    }
}

/*
# BALL STATE HURRICANE

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
