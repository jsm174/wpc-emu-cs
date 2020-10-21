namespace WPCEmu.Db
{
    public class TheatreOfMagic : IDb
    {
        public string name => "WPC-S: Theatre of Magic";
        public string version => "1.3X";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "tom_06", "tom_061", "tom_10f", "tom_101f", "tom_12", "tom_121", "tom_13", "tom_13f", "tom_14h", "tom_14hb", "tom_15c" },
            gameName = "Theatre of Magic",
            id = "tom"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "tom1_3x.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY IN" },
            new SwitchMapping { id = "25", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "26", name = "LEFT RET LANE" },
            new SwitchMapping { id = "27", name = "RIGHT RET LANE" },
            new SwitchMapping { id = "28", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "31", name = "TROUGH JAM" },
            new SwitchMapping { id = "32", name = "TROUGH 1" },
            new SwitchMapping { id = "33", name = "TROUGH 2" },
            new SwitchMapping { id = "34", name = "TROUGH 3" },
            new SwitchMapping { id = "35", name = "TROUGH 4" },
            new SwitchMapping { id = "36", name = "SUBWAY OPTO" },
            new SwitchMapping { id = "37", name = "SPINNER" },
            new SwitchMapping { id = "38", name = "RGT LOWER TGT" },

            new SwitchMapping { id = "41", name = "LOCK 1" },
            new SwitchMapping { id = "42", name = "LOCK 2" },
            new SwitchMapping { id = "43", name = "LOCK 3" },
            new SwitchMapping { id = "44", name = "POPPER" },
            new SwitchMapping { id = "45", name = "LFT DRAIN EDDY" },
            new SwitchMapping { id = "47", name = "SUBWAY MICRO" },
            new SwitchMapping { id = "48", name = "RGT DRAIN EDDY" },

            new SwitchMapping { id = "51", name = "L BANK TGT" },
            new SwitchMapping { id = "52", name = "CAP BALL REST" },
            new SwitchMapping { id = "53", name = "R LANE ENTER" },
            new SwitchMapping { id = "54", name = "LEFT LANE ENTER" },
            new SwitchMapping { id = "55", name = "CUBE POS 4" },
            new SwitchMapping { id = "56", name = "CUBE POS 1" },
            new SwitchMapping { id = "57", name = "CUBE POS 2" },
            new SwitchMapping { id = "58", name = "CUBE POS 3" },

            new SwitchMapping { id = "61", name = "LEFT SLING" },
            new SwitchMapping { id = "62", name = "RIGTH SLING" },
            new SwitchMapping { id = "63", name = "BOTTOM JET" },
            new SwitchMapping { id = "64", name = "MIDDLE JET" },
            new SwitchMapping { id = "65", name = "TOP JET" },
            new SwitchMapping { id = "66", name = "TOP LANE 1" },
            new SwitchMapping { id = "67", name = "TOP LANE 2" },

            new SwitchMapping { id = "71", name = "CNTR RAMP EXIT" },
            new SwitchMapping { id = "73", name = "R RAMP EXIT" },
            new SwitchMapping { id = "74", name = "R RAMP EXIT 2" },
            new SwitchMapping { id = "75", name = "CNTR RMP ENTER" },
            new SwitchMapping { id = "76", name = "R RAMP ENTER" },
            new SwitchMapping { id = "77", name = "CAP BALL TOP" },
            new SwitchMapping { id = "78", name = "LOOP LEFT" },

            new SwitchMapping { id = "81", name = "LOOP RIGHT" },
            new SwitchMapping { id = "82", name = "CNTR RMP TGTS" },
            new SwitchMapping { id = "83", name = "VANISH LOCK 1" },
            new SwitchMapping { id = "84", name = "VANISH LOCK 2" },
            new SwitchMapping { id = "85", name = "TRUNK EDDY" },
            new SwitchMapping { id = "86", name = "R LANE EXIT" },
            new SwitchMapping { id = "87", name = "LEFT LANE EXIT" }
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
            image = "playfield-tom.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 51, y = 257, color = "RED" } },
                new Lamp[] { new Lamp { x = 63, y = 254, color = "RED" } },
                new Lamp[] { new Lamp { x = 77, y = 252, color = "RED" } },
                new Lamp[] { new Lamp { x = 90, y = 252, color = "RED" } },
                new Lamp[] { new Lamp { x = 103, y = 252, color = "RED" } },
                new Lamp[] { new Lamp { x = 117, y = 256, color = "RED" } },
                new Lamp[] { new Lamp { x = 131, y = 257, color = "RED" } },
                new Lamp[] { new Lamp { x = 70, y = 235, color = "LBLUE" } },

                new Lamp[] { new Lamp { x = 133, y = 216, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 129, y = 226, color = "RED" } },
                new Lamp[] { new Lamp { x = 126, y = 235, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 123, y = 243, color = "RED" } },
                new Lamp[] { new Lamp { x = 150, y = 225, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 140, y = 248, color = "ORANGE" }, new Lamp { x = 36, y = 215, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 75, y = 222, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 81, y = 136, color = "RED" } }, // 28

                new Lamp[] { new Lamp { x = 91, y = 219, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 112, y = 235, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 101, y = 147, color = "RED" } },
                new Lamp[] { new Lamp { x = 100, y = 158, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 99, y = 169, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 104, y = 222, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 121, y = 17, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 141, y = 20, color = "YELLOW" } }, // 38

                new Lamp[] { new Lamp { x = 16, y = 160, color = "RED" } },
                new Lamp[] { new Lamp { x = 24, y = 179, color = "RED" } },
                new Lamp[] { new Lamp { x = 30, y = 196, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 75, y = 203, color = "RED" } },
                new Lamp[] { new Lamp { x = 128, y = 145, color = "WHITE" }, new Lamp { x = 41, y = 131, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 37, y = 176, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 46, y = 148, color = "RED" } },
                new Lamp[] { new Lamp { x = 62, y = 138, color = "RED" } }, //  48

                new Lamp[] { new Lamp { x = 71, y = 132, color = "RED" } },
                new Lamp[] { new Lamp { x = 74, y = 190, color = "RED" } },
                new Lamp[] { new Lamp { x = 74, y = 180, color = "RED" } },
                new Lamp[] { new Lamp { x = 51, y = 170, color = "WHITE" }, new Lamp { x = 126, y = 166, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 73, y = 168, color = "RED" } },
                new Lamp[] { new Lamp { x = 71, y = 144, color = "RED" } },
                new Lamp[] { new Lamp { x = 72, y = 155, color = "RED" } },
                new Lamp[] { new Lamp { x = 44, y = 196, color = "RED" } }, // 58

                new Lamp[] { new Lamp { x = 76, y = 279, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 76, y = 292, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 90, y = 262, color = "RED" } },
                new Lamp[] { new Lamp { x = 76, y = 305, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 79, y = 318, color = "RED" } },
                new Lamp[] { new Lamp { x = 105, y = 279, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 105, y = 292, color = "RED" } },
                new Lamp[] { new Lamp { x = 105, y = 305, color = "GREEN" } }, // 68

                new Lamp[] { new Lamp { x = 102, y = 318, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 129, y = 123, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 76, y = 337, color = "RED" } },
                new Lamp[] { new Lamp { x = 85, y = 341, color = "RED" } },
                new Lamp[] { new Lamp { x = 97, y = 341, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 106, y = 337, color = "RED" } },
                new Lamp[] { new Lamp { x = 163, y = 200, color = "RED" } },
                new Lamp[] { new Lamp { x = 34, y = 241, color = "ORANGE" } }, // 78

                new Lamp[] { new Lamp { x = 16, y = 334, color = "RED" }, new Lamp { x = 165, y = 337, color = "RED" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 63, y = 73, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 90, y = 360, color = "RED" } },
                new Lamp[] { new Lamp { x = 160, y = 395, color = "RED" } },
                new Lamp[] { new Lamp { x = 40, y = 395, color = "YELLOW" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "20", x = 22, y = 319 },
                new Flashlamp { id = "20", x = 160, y = 323 },
                new Flashlamp { id = "23", x = 24, y = 148 },
                new Flashlamp { id = "24", x = 144, y = 140 },
                new Flashlamp { id = "25", x = 17, y = 105 },
                new Flashlamp { id = "26", x = 24, y = 148 },
                new Flashlamp { id = "27", x = 153, y = 64 },
                new Flashlamp { id = "28", x = 111, y = 90 }
            }
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#BD8F40",
            "#CD3C35",
            "#7088B9",
            "#9C6A3F"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "55", "56", "57", "58"
                "31", "36", "55", "56", "57", "58"
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
                new ChecksumData { dataStartOffset = 0x1C83, dataEndOffset = 0x1D8A, checksumOffset = 0x1D8B, checksum = "16bit", name = "CHAMPION" },
                new ChecksumData { dataStartOffset = 0x1B20, dataEndOffset = 0x1BF8, checksumOffset = 0x1BF9, checksum = "16bit", name = "ADJUSTMENT" }
            },
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x86, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                //new MemoryPositionData { offset = 0x326, name = "TEXT", description = "random visible text", type = "string" },
                new MemoryPositionData { offset = 0x3AF, name = "GAME_PLAYER_CURRENT", description = "if pinball starts, current player is set to 1, maximal 4", type = "uint8" },
                new MemoryPositionData { offset = 0x3B0, name = "GAME_BALL_CURRENT", description = "if pinball starts, current ball is set to 1, maximal 4", type = "uint8" },

                new MemoryPositionData { offset = 0x43B, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x629, name = "GAME_PLAYER_TOTAL", description = "1-4 players", type = "uint8" },

                new MemoryPositionData { offset = 0x16A0, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A6, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AC, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B2, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

/*
# BALL STATE TOM

INITIAL STATE
- TROUGH 1, TROUGH 2, TROUGH 3 and TROUGH 4 are off (OPTO)
- TROUGH JAM in on (OPTO)

BALL IN SHOOTER LANE
- TROUGH 1, TROUGH 2, and TROUGH 3 are off (OPTO)
- TROUGH JAM and THROUGH 4 are on (OPTO)
- RIGHT RETURN LANE is on

BALL IN GAME
- TROUGH 1, TROUGH 2, and TROUGH 3 are off (OPTO)
- TROUGH JAM and THROUGH 4 are on (OPTO)
- SHOOTER LANE is off

BALL DRAIN
- TROUGH 1, TROUGH 2, TROUGH 3 and TROUGH 4 are off (OPTO)
- TROUGH JAM is on (OPTO)
*/