using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class BlackRose : IDb
    {
        public string name => "WPC-Fliptronics: Black Rose";
        public string version => "L-3";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "br_p17", "br_p18", "br_l1", "br_d1", "br_l3", "br_d3", "br_l4", "br_d4" },
            gameName = "Black Rose",
            id = "br"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "u6-l3.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "OUTHOLE" },
            new SwitchMapping { id = "16", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "17", name = "CENTER TROUGH" },
            new SwitchMapping { id = "18", name = "LEFT TROUGH" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "SHOOTER" },
            new SwitchMapping { id = "26", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "27", name = "LEFT RETURN LANE" },
            new SwitchMapping { id = "28", name = "LEFT SLINGSHOT" },

            new SwitchMapping { id = "31", name = "BOT STANDUPS BOT" },
            new SwitchMapping { id = "32", name = "BOT STANDUPS MID" },
            new SwitchMapping { id = "33", name = "BOT STANDUPS TOP" },
            new SwitchMapping { id = "34", name = "FIRE BUTTON" },
            new SwitchMapping { id = "35", name = "CANNON KICKER" },
            new SwitchMapping { id = "36", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "37", name = "IGHT RETURN LANE" },
            new SwitchMapping { id = "38", name = "RIGHT SLINGSHOT" },

            new SwitchMapping { id = "41", name = "MID STANDUPS TOP" },
            new SwitchMapping { id = "42", name = "MID STANDUPS MID" },
            new SwitchMapping { id = "43", name = "MID STANDUPS BOT" },
            new SwitchMapping { id = "44", name = "L RAMP ENTER" },
            new SwitchMapping { id = "45", name = "TOP LEFT LOOP" },
            new SwitchMapping { id = "46", name = "LEFT JET" },
            new SwitchMapping { id = "47", name = "RIGHT JET" },
            new SwitchMapping { id = "48", name = "BOTTOM JET" },

            new SwitchMapping { id = "51", name = "TOP STANDUPS BOT" },
            new SwitchMapping { id = "52", name = "TOP STANDUPS MID" },
            new SwitchMapping { id = "53", name = "TOP STANDUPS TOP" },
            new SwitchMapping { id = "54", name = "RAMP DOWN" },
            new SwitchMapping { id = "55", name = "BALL POPPER" },
            new SwitchMapping { id = "56", name = "R RAMP MADE" },
            new SwitchMapping { id = "57", name = "JETS EXIT" },
            new SwitchMapping { id = "58", name = "JETS ENTER" },

            new SwitchMapping { id = "61", name = "SUBWAY TOP" },
            new SwitchMapping { id = "62", name = "BACKBOARD RAMP" },
            new SwitchMapping { id = "63", name = "LOCKUP 1" },
            new SwitchMapping { id = "64", name = "LOCKUP 2" },
            new SwitchMapping { id = "65", name = "R SINGLE STANDUP" },
            new SwitchMapping { id = "66", name = "SUBWAY BOTTOM" },

            new SwitchMapping { id = "71", name = "LOCKUP ENTER" },
            new SwitchMapping { id = "72", name = "MIDDLE RAMP" },
            new SwitchMapping { id = "76", name = "R RAMP ENTER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-br.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#FAE24D",
            "#E53D28",
            "#3582D4",
            "#C2B38B"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                "16", "17", "18"
            },
            initialAction = new InitialAction[]
            {
                new InitialAction
                {
                    delayMs = 1500,
                    source = "cabinetInput",
                    value = 16
                }
            }
        };

        public MemoryPosition? memoryPosition => new MemoryPosition
        {
            knownValues = new MemoryPositionData[]
            {
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x48B, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x1730, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1736, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x173C, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1742, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1C6D, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C70, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C75, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C78, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C7D, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C80, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C85, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C88, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C8F, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1C92, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}