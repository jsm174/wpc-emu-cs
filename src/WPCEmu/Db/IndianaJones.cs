using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class IndianaJonesThePinballAdventure : IDb
    {
        public string name => "WPC-DCS: Indiana Jones, The Pinball Adventure";
        public string version => "L-7";

        public Pinmame? pinmame => new Pinmame
        {
            knownNames = new string[] { "ij_p2", "ij_l3", "ij_d3", "ij_l4", "ij_d4", "ij_l5", "ij_d5", "ij_l6", "ij_d6", "ij_l7", "ij_d7", "ij_lg7", "ij_dg7", "ij_h1", "ij_i1" },
            gameName = "Indiana Jones: The Pinball Adventure",
            id = "ij"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "ijone_l7.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "SINGLE DROP" },
            new SwitchMapping { id = "12", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "16", name = "LEFT RETURN LANE" },
            new SwitchMapping { id = "17", name = "RGHT RETURN LANE" },
            new SwitchMapping { id = "18", name = "RGHT OUTLANE TOP" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "(I)NDY LANE" },
            new SwitchMapping { id = "26", name = "I(N)DY LANE" },
            new SwitchMapping { id = "27", name = "IN(D)Y LANE" },
            new SwitchMapping { id = "28", name = "IND(Y) LANE" },

            new SwitchMapping { id = "31", name = "LEFT EJECT" },
            new SwitchMapping { id = "32", name = "EXIT IDOL" },
            new SwitchMapping { id = "33", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "34", name = "GUN TRIGGER" },
            new SwitchMapping { id = "35", name = "LEFT JET" },
            new SwitchMapping { id = "36", name = "RIGHT JET" },
            new SwitchMapping { id = "37", name = "BOTTOM JET" },
            new SwitchMapping { id = "38", name = "CENTER STANDUP" },

            new SwitchMapping { id = "41", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "42", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "43", name = "TOP IDOL ENTER" },
            new SwitchMapping { id = "44", name = "RIGHT POPPER" },
            new SwitchMapping { id = "45", name = "CENTER ENTER" },
            new SwitchMapping { id = "46", name = "TOP POST" },
            new SwitchMapping { id = "47", name = "SUBWAY LOOPKUP" },
            new SwitchMapping { id = "48", name = "RIGHT SLINGSHOT" },

            new SwitchMapping { id = "51", name = "ADVENT(U)RE TRGT" },
            new SwitchMapping { id = "52", name = "ADVENTU(R)E TRGT" },
            new SwitchMapping { id = "53", name = "ADVENTUR(E) TRGT" },
            new SwitchMapping { id = "54", name = "LEFT LOOP TOP" },
            new SwitchMapping { id = "55", name = "LEFT LOOP BOTTOM" },
            new SwitchMapping { id = "56", name = "RIGHT LOOP TOP" },
            new SwitchMapping { id = "57", name = "RIGHT LOOP BOT." },
            new SwitchMapping { id = "58", name = "RGHT OUTLANE BOT" },

            new SwitchMapping { id = "61", name = "(A)DVENTURE TRGT" },
            new SwitchMapping { id = "62", name = "A(D)VENTURE TRGT" },
            new SwitchMapping { id = "63", name = "AD(V)ENTURE TRGT" },
            new SwitchMapping { id = "64", name = "CAPTVE VALL BACK" },
            new SwitchMapping { id = "65", name = "MINI TOP LEFT" },
            new SwitchMapping { id = "66", name = "MINI MID TOP LFT" },
            new SwitchMapping { id = "67", name = "MINI MID BOT LFT" },
            new SwitchMapping { id = "68", name = "MINI COTTOM LEFT" },

            new SwitchMapping { id = "71", name = "CAPTVE BALL FRNT" },
            new SwitchMapping { id = "72", name = "MINI TOP HOLE" },
            new SwitchMapping { id = "73", name = "MINI BOTTOM HOLE" },
            new SwitchMapping { id = "74", name = "RIGHT RAMP MADE" },
            new SwitchMapping { id = "75", name = "MINI TOP RIGHT" },
            new SwitchMapping { id = "76", name = "MINI MID TOP RGT" },
            new SwitchMapping { id = "77", name = "MINI MID BOT RGT" },
            new SwitchMapping { id = "78", name = "MINI BOTTOM RGT" },

            new SwitchMapping { id = "81", name = "TROUGH 6" },
            new SwitchMapping { id = "82", name = "TROUGH 5" },
            new SwitchMapping { id = "83", name = "TROUGH 4" },
            new SwitchMapping { id = "84", name = "TROUGH 3" },
            new SwitchMapping { id = "85", name = "TROUGH 2" },
            new SwitchMapping { id = "86", name = "TROUGH 1" },
            new SwitchMapping { id = "87", name = "TOP TROUGH" },
            new SwitchMapping { id = "88", name = "SHOOTER" }

            //TODO 91 - 95
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "DROP ADV(E)NTURE" },
            new FliptronicsMapping { id = "F6", name = "DROP ADVE(N)TURE" },
            new FliptronicsMapping { id = "F7", name = "DROP ADVEN(T)URE" },
            new FliptronicsMapping { id = "F8", name = "LEFT RAMP MADE" }
        };

        public SolenoidMapping[] solenoidMapping => new SolenoidMapping[]
        {
            new SolenoidMapping { id = "01", name = "Ball Popper" },
            new SolenoidMapping { id = "02", name = "Ball Launch" },
            new SolenoidMapping { id = "03", name = "Totem Drop Up" },
            new SolenoidMapping { id = "04", name = "Ball Release" },
            new SolenoidMapping { id = "05", name = "Center Drop Bank" },
            new SolenoidMapping { id = "06", name = "Idol Release" },
            new SolenoidMapping { id = "07", name = "Knocker" },
            new SolenoidMapping { id = "08", name = "Left Elect" },
            new SolenoidMapping { id = "09", name = "Left Jet Bumper" },
            new SolenoidMapping { id = "10", name = "Right Jet Bumper" },
            new SolenoidMapping { id = "11", name = "Bumpeur Bas" },
            new SolenoidMapping { id = "12", name = "Left Slingshot" },
            new SolenoidMapping { id = "13", name = "Right Slingshot" },
            new SolenoidMapping { id = "14", name = "Left Control Gate" },
            new SolenoidMapping { id = "15", name = "Right Control Gate" },
            new SolenoidMapping { id = "16", name = "Totem Drop Down" },
            new SolenoidMapping { id = "17", name = "Eternal Life" },
            new SolenoidMapping { id = "18", name = "Light Jackpot" },
            new SolenoidMapping { id = "19", name = "Super Jackpot" },
            new SolenoidMapping { id = "20", name = "Jackpot" },
            new SolenoidMapping { id = "21", name = "Path Of Adventure" },
            new SolenoidMapping { id = "22", name = "Mini Motor Left" },
            new SolenoidMapping { id = "23", name = "Mini Motor Right" },
            new SolenoidMapping { id = "24", name = "Plane Gun LEDS" },
            new SolenoidMapping { id = "25", name = "Dogfight Hurry Up" },
            new SolenoidMapping { id = "26", name = "Right Ramp" },
            new SolenoidMapping { id = "27", name = "Left Ramp" },
            new SolenoidMapping { id = "28", name = "Subway Release" },
            new SolenoidMapping { id = "29", name = "Lower Right Flipper Power" },
            new SolenoidMapping { id = "30", name = "Lower Right Flipper Hold" },
            new SolenoidMapping { id = "31", name = "Lower Left Flipper Power" },
            new SolenoidMapping { id = "32", name = "Lower Left Flipper Hold" },
            new SolenoidMapping { id = "33", name = "Diverter Power" },
            new SolenoidMapping { id = "34", name = "Diverter Hold" },
            new SolenoidMapping { id = "35", name = "Top Lockup Power" },
            new SolenoidMapping { id = "36", name = "Top Lockup Hold" },
            new SolenoidMapping { id = "37", name = "Left Side Flasher" },
            new SolenoidMapping { id = "38", name = "Right Side Flasher" },
            new SolenoidMapping { id = "39", name = "Special Flasher" },
            new SolenoidMapping { id = "40", name = "Totem Multiball" },
            new SolenoidMapping { id = "41", name = "Jackpot Multiplier Flasher" },
            new SolenoidMapping { id = "42", name = "Wheel Motor" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-ij.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDcs"
        };

        public string[] cabinetColors => new string[]
        {
            "#EC5629",
            "#EDE24C",
            "#4399B9",
            "#DF916D"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22", 
                //OPTO SWITCHES: "81", "82", "83", "84", "85", "86",
                "41", "42", "43", "44", "45", "47", "71", "72", "73"
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
                new MemoryPositionData { offset = 0x86, name = "GAME_RUNNING", description = "0: not running, 1: running", type = "uint8" },

                new MemoryPositionData { offset = 0x43A, name = "GAME_CURRENT_SCREEN", description = "0x00: attract mode, 0x01: game play/system menu, 0x80: tilt warning, 0xF1: credits view", type = "uint8" },

                new MemoryPositionData { offset = 0x1730, name = "GAME_SCORE_P1", description = "Player 1 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1737, name = "GAME_SCORE_P2", description = "Player 2 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x173E, name = "GAME_SCORE_P3", description = "Player 3 Score", type = "bcd", length = 6 },
                new MemoryPositionData { offset = 0x1745, name = "GAME_SCORE_P4", description = "Player 4 Score", type = "bcd", length = 6 },

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

                new MemoryPositionData { offset = 0x1D78, name = "HISCORE_1_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D7B, name = "HISCORE_1_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D80, name = "HISCORE_2_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D83, name = "HISCORE_2_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D88, name = "HISCORE_3_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D8B, name = "HISCORE_3_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D90, name = "HISCORE_4_NAME", type = "string" },
                new MemoryPositionData { offset = 0x1D93, name = "HISCORE_4_SCORE", type = "bcd", length = 5 },
                new MemoryPositionData { offset = 0x1D9A, name = "HISCORE_CHAMP_NAME", description = "Grand Champion", type = "string" },
                new MemoryPositionData { offset = 0x1D9D, name = "HISCORE_CHAMP_SCORE", description = "Grand Champion", type = "bcd", length = 5 }
            }
        };

        public string[] testErrors => new string[]
        {
            "ERR. MINI PFD. BAD, CHK. SWITCHES/MTR",
            "ERROR IDOL BAD, CHK. SWITCHES/MTR",
            "ERR. DROP BNK BAD, CHK. SWITCH/COIL",
            "ER. SNGLE DRP. BAD, CHK. SWITCH/COIL",
            "CHECK SWITCH 94, MINI PFD. RIGHT",
            "CHECK SWITCH 95, MINI PFD. LEFT"
        };
    }
}