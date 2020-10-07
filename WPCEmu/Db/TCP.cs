﻿namespace WPCEmu.Db
{
    public class TheChampionPub : IDb
    {
        public string name => "WPC-95: The Champion Pub";
        public string version => "1.6";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "cp_15", "cp_16" },
            gameName = "Champion Pub, The",
            id = "cp"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "CP_G11.1_6"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "MADE RAMP" },
            new SwitchMapping { id = "12", name = "HEAVY BAG" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LOCK UP 1" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT RETURN" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BALL LAUNCH" },
            new SwitchMapping { id = "25", name = "THREE BANK MID" },
            new SwitchMapping { id = "26", name = "LEFT RETURN" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "28", name = "POPPER" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "LEFT JAB MADE" },
            new SwitchMapping { id = "37", name = "CORNER EJECT" },
            new SwitchMapping { id = "38", name = "RIGHT JAB MADE" },

            new SwitchMapping { id = "41", name = "BOXER POLE CNTR" },
            new SwitchMapping { id = "42", name = "BEHND LEFT SCOOP" },
            new SwitchMapping { id = "43", name = "BEHND RGHT SCOOP" },
            new SwitchMapping { id = "44", name = "ENTER RAMP" },
            new SwitchMapping { id = "45", name = "JUMP ROPE" },
            new SwitchMapping { id = "46", name = "BAG POLE CENTER" },
            new SwitchMapping { id = "47", name = "BOXER POLE RIGHT" },
            new SwitchMapping { id = "48", name = "BOXER POLE LEFT" },

            new SwitchMapping { id = "51", name = "LEFT SLING" },
            new SwitchMapping { id = "52", name = "RIGHT SLING" },
            new SwitchMapping { id = "53", name = "THREE BANK BOTTO" },
            new SwitchMapping { id = "54", name = "THREE BANK TOP" },
            new SwitchMapping { id = "55", name = "LEFT HALF GUY" },
            new SwitchMapping { id = "56", name = "RGHT HALD GUY" },
            new SwitchMapping { id = "57", name = "LOCK UP 2" },
            new SwitchMapping { id = "58", name = "LOCK UP 3" },

            new SwitchMapping { id = "61", name = "LEFT SCOOP UP" },
            new SwitchMapping { id = "62", name = "RIGHT SCOOP UP" },
            new SwitchMapping { id = "63", name = "POWER SHOT" },
            new SwitchMapping { id = "64", name = "ROPE CAM" },
            new SwitchMapping { id = "65", name = "SPEED BAG" },
            new SwitchMapping { id = "66", name = "BOXER GUT 1" },
            new SwitchMapping { id = "67", name = "BOXER GUT 2" },
            new SwitchMapping { id = "68", name = "BOXER HEAD" },

            new SwitchMapping { id = "71", name = "EXIT ROPE" },
            new SwitchMapping { id = "72", name = "ENTER SPEED BAG" },
            new SwitchMapping { id = "73", name = "REMOVED" },
            new SwitchMapping { id = "74", name = "ENTER LOCKUP" },
            new SwitchMapping { id = "75", name = "SWITCH 75" },
            new SwitchMapping { id = "76", name = "TOP OF RAMP" },
            new SwitchMapping { id = "77", name = "SWITCH 77" },
            new SwitchMapping { id = "78", name = "ENTER ROPE" }
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
            image = "playfield-tcp.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#E76031",
            "#ECCA9E",
            "#497A9B",
            "#CC2D1E"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "38", "41", "42", "43", "44", "45", "46", "47", "48", "64"
                "31", "36", "38", "41", "42", "43", "44", "45", "46", "47", "48", "64",
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

                new MemoryPositionData { offset = 0x440, name = "GAME_CURRENT_SCREEN", description = "0: attract mode, 0x80: tilt warning, 0x89: shows tournament enable screen, 0xF1: coin door open/add more credits, 0xF4: switch scanning", type = "uint8" },
                new MemoryPositionData { offset = 0x56F, name = "GAME_ATTRACTMODE_SEQ", description = "Game specific sequence of attract mode, could be used to skip some screens", type = "uint8" },

                new MemoryPositionData { offset = 0x16A1, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16A8, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16AF, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x16B6, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 5 },

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

                new MemoryPositionData { offset = 0x1D15, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D19, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D1E, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D22, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D27, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D2B, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D30, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D34, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D3B, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D3F, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => new string[]
        {
             "R. FLIPPER E.O.S. IS STUCK CLOSED",
             "L. FLIPPER E.O.S. IS STUCK CLOSED"
        };
    }
}