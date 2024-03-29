namespace WPCEmu.Db
{
    public class DrWho : IDb
    {
        public string name => "WPC-Fliptronics: Dr. Who";
        public string version => "L-1";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "dw_p5", "dw_p6", "dw_l1", "dw_d1", "dw_l2", "dw_d2" },
            gameName = "Dr. Who",
            id = "dw"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "drwho_l2.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT SLING" },
            new SwitchMapping { id = "16", name = "RIGHT SLING" },
            new SwitchMapping { id = "17", name = "SHOOTER LANE" },
            new SwitchMapping { id = "18", name = "EXIT JETS" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKET OPTQ" },
            new SwitchMapping { id = "25", name = "TROUGH 1 BALL" },
            new SwitchMapping { id = "26", name = "TROUGH 2 BALLS" },
            new SwitchMapping { id = "27", name = "TROUGH 3 BALLS" },
            new SwitchMapping { id = "28", name = "OUTHOLE" },

            new SwitchMapping { id = "31", name = "OPTO POPPER" },
            new SwitchMapping { id = "32", name = "MINI HOME OPTO" },
            new SwitchMapping { id = "33", name = "ENTER TRAMP OPTO" },
            new SwitchMapping { id = "34", name = "LAUNCH BALL" },
            new SwitchMapping { id = "35", name = "SCORE TOP RAMP" },
            new SwitchMapping { id = "36", name = "ENTER BOT RAMP" },
            new SwitchMapping { id = "37", name = "SCORE BOT RAMP" },
            new SwitchMapping { id = "38", name = "MINI DOOR MID" },

            new SwitchMapping { id = "41", name = "<E>S-C-A-P-E" },
            new SwitchMapping { id = "42", name = "E<S>C-A-P-E" },
            new SwitchMapping { id = "43", name = "E-S<C>A-P-E" },
            new SwitchMapping { id = "44", name = "E-S-C<A>P-E" },
            new SwitchMapping { id = "45", name = "E-S-C-A<P>E" },
            new SwitchMapping { id = "46", name = "E-S-C-A-P<E>" },
            new SwitchMapping { id = "47", name = "HANGON SCORE" },
            new SwitchMapping { id = "48", name = "SELECT DOCTOR" },

            new SwitchMapping { id = "51", name = "<R>E-P-A-I-R" },
            new SwitchMapping { id = "52", name = "R<E>P-A-I-R" },
            new SwitchMapping { id = "53", name = "R-E<P>A-I-R" },
            new SwitchMapping { id = "54", name = "R-E-P<A>I-R" },
            new SwitchMapping { id = "55", name = "R-E-P-A<I>R" },
            new SwitchMapping { id = "56", name = "R-E-P-A-I<R>" },
            new SwitchMapping { id = "57", name = "TRAP DOOR DOWN" },
            new SwitchMapping { id = "58", name = "ACTIVAT TRANSMAT" },

            new SwitchMapping { id = "61", name = "LEFT JET" },
            new SwitchMapping { id = "62", name = "RIGHT JET" },
            new SwitchMapping { id = "63", name = "BOTTOM JET" },
            new SwitchMapping { id = "64", name = "LEFT DRAIN" },
            new SwitchMapping { id = "65", name = "LEFT RETURN" },
            new SwitchMapping { id = "66", name = "RIGHT RETURN" },
            new SwitchMapping { id = "67", name = "RIGHT DRAIN" },
            new SwitchMapping { id = "68", name = "MINI DOOR LEFT" },

            new SwitchMapping { id = "71", name = "MINIOPTO5BANK R1" },
            new SwitchMapping { id = "72", name = "MINIOPTO5BANK R2" },
            new SwitchMapping { id = "73", name = "MINIOPTO5BANK M" },
            new SwitchMapping { id = "74", name = "MINIOPTO5BANK L2" },
            new SwitchMapping { id = "75", name = "MINIOPTO5BANK L1" },
            new SwitchMapping { id = "76", name = "MINI L OPTOEJECT" },
            new SwitchMapping { id = "77", name = "MINI R OPTOEJECT" },
            new SwitchMapping { id = "78", name = "MINI LITES LOCK" },

            new SwitchMapping { id = "82", name = "PLAYFIELD GLASS" },
            new SwitchMapping { id = "88", name = "MINI DOOR RIGHT" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-dw.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#F2E24D",
            "#D52F2D",
            "#4480E3",
            "#E2672C"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22", "25", "26", "27",
                "82",
                //OPTO SWITCHES
                "31", "32", "33", "71", "72", "73", "74", "75", "76", "77"
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
                new MemoryPositionData { offset = 0x88, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },
                //new MemoryPositionData { offset = 0x42B, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },

                new MemoryPositionData { offset = 0x1680, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1686, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x168C, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1692, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

                new MemoryPositionData { offset = 0x0A0A, name = "BONUS_MULTIP_P1", description = "Player 1 Score", type = "uint8" },
                new MemoryPositionData { offset = 0x0A0B, name = "BONUS_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1D17, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D1A, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D1F, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D22, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D27, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D2A, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D2F, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D32, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D39, name = "HISCORE_CHAMP_NAME", description = "Greatest Time Lord", type = "string" },
                new MemoryPositionData { offset = 0x1D3C, name = "HISCORE_CHAMP_SCORE", description = "Greatest Time Lord", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => null;
    }
}