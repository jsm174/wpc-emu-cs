namespace WPCEmu.Db
{
    public class TheFlintstones : IDb
    {
        public string name => "WPC-S: The Flintstones";
        public string version => "LX-5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "fs_lx2", "fs_dx2", "fs_sp2", "fs_sp2d", "fs_lx4", "fs_dx4", "fs_lx5", "fs_dx5" },
            gameName = "Flintstones, The",
            id = "fs"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "flin_lx5.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "LAUNCH BUTTON" },
            new SwitchMapping { id = "12", name = "TICKET DISP" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RT. SHOOTER LANE" },
            new SwitchMapping { id = "16", name = "LFT. BOWLING TGT" },
            new SwitchMapping { id = "17", name = "CNT. BOWLING TGT" },
            new SwitchMapping { id = "18", name = "RGT. BOWLING TGT" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY IN" },
            new SwitchMapping { id = "25", name = "MACHINE EXIT" },
            new SwitchMapping { id = "26", name = "UP LFT HALF TGT" },
            new SwitchMapping { id = "27", name = "LEFT LANE EXIT" },
            new SwitchMapping { id = "28", name = "LFT LOOP ENTER" },

            new SwitchMapping { id = "31", name = "TROUGH JAM" },
            new SwitchMapping { id = "32", name = "TROUGH 1" },
            new SwitchMapping { id = "33", name = "TROUGH 2" },
            new SwitchMapping { id = "34", name = "TROUGH 3" },
            new SwitchMapping { id = "35", name = "TROUGH 4" },
            new SwitchMapping { id = "36", name = "TOP POPPER" },
            new SwitchMapping { id = "37", name = "RGHT RMP ENTER" },
            new SwitchMapping { id = "38", name = "LFT RMP ENTER" },

            new SwitchMapping { id = "41", name = "BED<R>OCK" },
            new SwitchMapping { id = "42", name = "BEDR<O>CK" },
            new SwitchMapping { id = "43", name = "BEDRO<C>K" },
            new SwitchMapping { id = "44", name = "BEDROC<K>" },
            new SwitchMapping { id = "45", name = "BE<D>ROCK" },
            new SwitchMapping { id = "46", name = "B<E>DROCK" },
            new SwitchMapping { id = "47", name = "<B>EDROCK" },
            new SwitchMapping { id = "48", name = "CENTER LANE EXIT" },

            new SwitchMapping { id = "51", name = "LFT RGHT TGT 3" },
            new SwitchMapping { id = "52", name = "LFT RGHT TGT 2" },
            new SwitchMapping { id = "53", name = "LFT RGHT TGT 1" },
            new SwitchMapping { id = "54", name = "LEFT HALF TGT" },
            new SwitchMapping { id = "55", name = "RIGHT HALF TGT" },
            new SwitchMapping { id = "56", name = "DICTABIRD TARGET" },

            new SwitchMapping { id = "61", name = "LEFT SLING" },
            new SwitchMapping { id = "62", name = "RIGHT SLING" },
            new SwitchMapping { id = "63", name = "LEFT BUMPER" },
            new SwitchMapping { id = "64", name = "RIGHT BUMPER" },
            new SwitchMapping { id = "65", name = "BOTM BUMPER" },
            new SwitchMapping { id = "66", name = "<D>IG" },
            new SwitchMapping { id = "67", name = "D<I>G" },
            new SwitchMapping { id = "68", name = "DI<G>" },

            new SwitchMapping { id = "71", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "72", name = "LFT RET LANE" },
            new SwitchMapping { id = "73", name = "RGT RET LANE" },
            new SwitchMapping { id = "74", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "75", name = "RIGHT LANE EXIT" },
            new SwitchMapping { id = "76", name = "MACHINE ENTER" },
            new SwitchMapping { id = "77", name = "RIGHT RAMP EXIT" },
            new SwitchMapping { id = "78", name = "LEFT RAMP EXIT" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-flintstones.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#F2A73C",
            "#FBE14C",
            "#4BAAD6",
            "#D83282",
            "#A1A73A",
            "#A8572E"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "41", "42", "43"
                "31", "41", "42", "43",
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
                new MemoryPositionData { offset = 0x80, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x43A, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

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

                new MemoryPositionData { offset = 0x1C61, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C64, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C69, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C6C, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C71, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C74, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C79, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1C7C, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1C83, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1C86, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}