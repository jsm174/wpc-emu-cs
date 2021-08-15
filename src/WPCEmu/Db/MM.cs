using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class MedievalMadness : IDb
    {
        public string name => "WPC-95: Medieval Madness";
        public string version => "L-8";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "mm_05", "mm_10", "mm_10u", "mm_109", "mm_109b", "mm_109c" },
            gameName = "Medieval Madness",
            id = "mm"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "mm_109b.bin"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "LAUNCH BUTTON" },
            new SwitchMapping { id = "12", name = "CATAPULT TARGET" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "L TROLL TARGET" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT RETURN" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "25", name = "R TROLL TARGET" },
            new SwitchMapping { id = "26", name = "LEFT RETURN" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "RIGHT EJECT" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "LEFT POPPER" },
            new SwitchMapping { id = "37", name = "CASTLE GATE" },
            new SwitchMapping { id = "38", name = "CATAPULT" },

            new SwitchMapping { id = "41", name = "MOAT ENTER" },
            new SwitchMapping { id = "44", name = "CASTLE LOCK" },
            new SwitchMapping { id = "45", name = "L TROLL (U/PLDF)" },
            new SwitchMapping { id = "46", name = "R TROLL (U/PLDF)" },
            new SwitchMapping { id = "47", name = "LEFT TOP LANE" },
            new SwitchMapping { id = "48", name = "RIGHT TOP LANE" },

            new SwitchMapping { id = "51", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "52", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "53", name = "LEFT JET" },
            new SwitchMapping { id = "54", name = "BOTTOM JET" },
            new SwitchMapping { id = "55", name = "RIGHT JET" },
            new SwitchMapping { id = "56", name = "DRAWBRIDGE UP" },
            new SwitchMapping { id = "57", name = "DRAWBRIDGE DOWN" },
            new SwitchMapping { id = "58", name = "TOWER EXIT" },

            new SwitchMapping { id = "61", name = "L RAMP ENTER" },
            new SwitchMapping { id = "62", name = "L RAMP EXIT" },
            new SwitchMapping { id = "63", name = "R RAMP ENTER" },
            new SwitchMapping { id = "64", name = "R RAMP EXIT" },
            new SwitchMapping { id = "65", name = "LEFT LOOP LO" },
            new SwitchMapping { id = "66", name = "LEFT LOOP HI" },
            new SwitchMapping { id = "67", name = "RIGHT LOOP LO" },
            new SwitchMapping { id = "68", name = "RIGHT LOOP HI" },

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
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => new SolenoidMapping[]
        {
            new SolenoidMapping { id = "01", name = "AUTO PLUNGER" },
            new SolenoidMapping { id = "02", name = "TROUGH EJECT" },
            new SolenoidMapping { id = "03", name = "LEFT POPPER" },
            new SolenoidMapping { id = "04", name = "CASTLE" },
            new SolenoidMapping { id = "05", name = "CASTLE GATE POWER" },
            new SolenoidMapping { id = "06", name = "CASTLE GATE HOLD" },
            new SolenoidMapping { id = "07", name = "KNOCKER" },
            new SolenoidMapping { id = "08", name = "CATAPULT" },
            new SolenoidMapping { id = "09", name = "RIGHT EJECT" },
            new SolenoidMapping { id = "10", name = "LEFT SLINGSHOT" },
            new SolenoidMapping { id = "11", name = "RIGHT SLINGSHOT" },
            new SolenoidMapping { id = "12", name = "LEFT JET BUMPER" },
            new SolenoidMapping { id = "13", name = "BOTTOM JET BUMPER" },
            new SolenoidMapping { id = "14", name = "RIGHT JET BUMPER" },
            new SolenoidMapping { id = "15", name = "TOWER DIVERTER PWR" },
            new SolenoidMapping { id = "16", name = "TOWER DIVERTER HOLD" },
            new SolenoidMapping { id = "17", name = "LEFT SIDE LO" },
            new SolenoidMapping { id = "18", name = "LEFT RAMP FLASHERS" },
            new SolenoidMapping { id = "19", name = "LEFT SIDE HIGH FLSHRS" },
            new SolenoidMapping { id = "20", name = "RIGHT SIDE HIGH FLSHRS" },
            new SolenoidMapping { id = "21", name = "RIGHT RAMP FLASHERS" },
            new SolenoidMapping { id = "22", name = "CASTLE RIGHT SIDE FLSHRS" },
            new SolenoidMapping { id = "23", name = "RIGHT SIDE LOW FLSHRS" },
            new SolenoidMapping { id = "24", name = "MOAT FLASHERS" },
            new SolenoidMapping { id = "25", name = "CASTLE LEFT SIDE FLSHRS" },
            new SolenoidMapping { id = "26", name = "TOWER LOCK POST" },
            new SolenoidMapping { id = "27", name = "RIGHT GATE" },
            new SolenoidMapping { id = "28", name = "LEFT GATE" },
            new SolenoidMapping { id = "29", name = "LOWER RIGHT FLIPPER POWER" },
            new SolenoidMapping { id = "30", name = "LOWER RIGHT FLIPPER HOLD" },
            new SolenoidMapping { id = "31", name = "LOWER LEFT FLIPPER POWER" },
            new SolenoidMapping { id = "32", name = "LOWER LEFT FLIPPER HOLD" },
            new SolenoidMapping { id = "33", name = "LEFT TROLL POWER" },
            new SolenoidMapping { id = "34", name = "LEFT TROLL HOLD" },
            new SolenoidMapping { id = "35", name = "RIGHT TROLL POWER" },
            new SolenoidMapping { id = "36", name = "RIGHT TROLL HOLD" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-mm.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 149, y = 245, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 150, y = 256, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 151, y = 268, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 127, y = 213, color = "RED" } },
                new Lamp[] { new Lamp { x = 122, y = 229, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 116, y = 243, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 113, y = 255, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 110, y = 266, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 152, y = 206, color = "RED" } }, // 21
                new Lamp[] { new Lamp { x = 146, y = 221, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 140, y = 235, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 134, y = 248, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 90, y = 194, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 88, y = 208, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 85, y = 222, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 82, y = 236, color = "LBLUE" } },

                new Lamp[] { new Lamp { x = 117, y = 148, color = "YELLOW" } }, // 31
                new Lamp[] { new Lamp { x = 113, y = 160, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 112, y = 171, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 105, y = 208, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 102, y = 220, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 100, y = 233, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 97, y = 247, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 95, y = 260, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 27, y = 178, color = "RED" } }, // 41
                new Lamp[] { new Lamp { x = 33, y = 194, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 38, y = 209, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 44, y = 222, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 32, y = 236, color = "RED" } },
                new Lamp[] { new Lamp { x = 38, y = 249, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 45, y = 263, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 51, y = 277, color = "WHITE" } },

                new Lamp[] { new Lamp { x = 87, y = 127, color = "RED" } }, // 51
                new Lamp[] { new Lamp { x = 91, y = 148, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 90, y = 166, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 91, y = 179, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 143, y = 37, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 158, y = 37, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 78, y = 151, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 104, y = 159, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 111, y = 307, color = "WHITE" } }, // 61
                new Lamp[] { new Lamp { x = 90, y = 301, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 69, y = 307, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 54, y = 194, color = "RED" } },
                new Lamp[] { new Lamp { x = 59, y = 209, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 63, y = 224, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 66, y = 236, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 70, y = 248, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 108, y = 234, color = "WHITE" } }, // 71
                new Lamp[] { new Lamp { x = 90, y = 320, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 72, y = 324, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 90, y = 341, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 66, y = 146, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 68, y = 158, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 70, y = 170, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 180, y = 110, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 168, y = 302, color = "RED" } }, // 81
                new Lamp[] { new Lamp { x = 154, y = 293, color = "RED" } },
                new Lamp[] { new Lamp { x = 26, y = 293, color = "RED" } },
                new Lamp[] { new Lamp { x = 12, y = 302, color = "RED" } },
                new Lamp[] { new Lamp { x = 64, y = 134, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 90, y = 376, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 163, y = 394, color = "RED" } },
                new Lamp[] { new Lamp { x = 19, y = 394, color = "YELLOW" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 12, y = 193 },
                new Flashlamp { id = "18", x = 48, y = 144 },
                new Flashlamp { id = "19", x = 20, y = 18 },
                new Flashlamp { id = "20", x = 191, y = 55 },
                new Flashlamp { id = "21", x = 144, y = 155 },
                new Flashlamp { id = "22", x = 117, y = 40 },
                new Flashlamp { id = "23", x = 183, y = 235 },
                new Flashlamp { id = "24", x = 111, y = 111 },
                new Flashlamp { id = "25", x = 36, y = 66 }
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
            "#F4E784",
            "#48618C",
            "#C73434",
            "#D26B3D"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22", "56",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "41",
                "31", "36", "37", "41"
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
                    offset = 0x1C12,
                    value = 0x01
                },
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            checksum = new ChecksumData[]
            {
                new ChecksumData { dataStartOffset = 0x1D29, dataEndOffset = 0x1D48, checksumOffset = 0x1D49, checksum = "16bit", name = "HI_SCORE" },
                new ChecksumData { dataStartOffset = 0x1D4B, dataEndOffset = 0x1D52, checksumOffset = 0x1D53, checksum = "16bit", name = "CHAMPION" },
                new ChecksumData { dataStartOffset = 0x1B92, dataEndOffset = 0x1CB2, checksumOffset = 0x1CB3, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },
                new MemoryPositionData { offset = 0x326, name = "TEXT", description = "random visible text", type = "string" },
                new MemoryPositionData { offset = 0x3B2, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x3B3, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },

                new MemoryPositionData { offset = 0x440, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x56F, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A6, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AC, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B2, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x170D, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1B92, name = "GAME_BALL_TOTAL", description = "Balls per game", type = "uint8" },
                new MemoryPositionData { offset = 0x1C12, name = "STAT_FREEPLAY", description = "0: not free, 1: free", type = "uint8" },

                new MemoryPositionData { offset = 0x1D29, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D2C, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D31, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D34, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D39, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D3C, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D41, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D44, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D4B, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D4E, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x1D5B, name = "GAME_CREDITS_FULL", description = "0-10 credits", type = "uint8" },
                new MemoryPositionData { offset = 0x1D5C, name = "GAME_CREDITS_HALF", description = "0: no half credits", type = "uint8" }
            }
        };

        public string[] testErrors => null;

        //TODO
        //attract mode screen
    }
}

/*
# BALL STATE TOM

INITIAL STATE
- TROUGH 1, TROUGH 2, TROUGH 3 and TROUGH 4 are off (OPTO)
- TROUGH EJECT in on (OPTO)

BALL IN SHOOTER LANE
- TROUGH 1, TROUGH 2, and TROUGH 3 are off (OPTO)
- TROUGH EJECT and THROUGH 4 are on (OPTO)
- SHOOTER LANE is on

BALL IN GAME
- TROUGH 1, TROUGH 2, and TROUGH 3 are off (OPTO)
- TROUGH EJECT and THROUGH 4 are on (OPTO)
- SHOOTER LANE is off

BALL DRAIN
- TROUGH 1, TROUGH 2, TROUGH 3 AND THROUGH 4 are off (OPTO)
- TROUGH EJECT is on (OPTO)
*/
