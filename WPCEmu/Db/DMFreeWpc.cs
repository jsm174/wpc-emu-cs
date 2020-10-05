namespace WPCEmu.Db
{
    public class DemolitionManFreewpc : IDb
    {
        public string name => "WPC-DCS: Demolition Man (FreeWPC)";
        public string version => "1.01";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "dm_dt099", "dm_dt101" },
            gameName = "Demolition Man (FreeWPC)",
            id = "dmF"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "dm_dt101.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "BALL LAUNCH" },
            new SwitchMapping { id = "12", name = "L HANDLE BUTTON" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "16", name = "LEFT INLANE" },
            new SwitchMapping { id = "17", name = "RIGHT INLANE" },
            new SwitchMapping { id = "18", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "CLAW RIGHT" },
            new SwitchMapping { id = "26", name = "CLAW LEFT" },
            new SwitchMapping { id = "27", name = "SHOOTER LANE" },

            new SwitchMapping { id = "31", name = "TROUGH 1 (RIGHT)" },
            new SwitchMapping { id = "32", name = "TROUGH 2" },
            new SwitchMapping { id = "33", name = "TROUGH 3" },
            new SwitchMapping { id = "34", name = "TROUGH 4" },
            new SwitchMapping { id = "35", name = "TROUGH 5 (LEFT)" },
            new SwitchMapping { id = "36", name = "TROUGH JAM" },
            new SwitchMapping { id = "38", name = "STANDUP 5" },

            new SwitchMapping { id = "41", name = "LEFT SLING" },
            new SwitchMapping { id = "42", name = "RIGHT SLING" },
            new SwitchMapping { id = "43", name = "LEFT JET" },
            new SwitchMapping { id = "44", name = "TOP SLING" },
            new SwitchMapping { id = "45", name = "LEFT JET" },
            new SwitchMapping { id = "46", name = "R RAMP ENTERPOST" },
            new SwitchMapping { id = "47", name = "R RAMP EXIT" },
            new SwitchMapping { id = "48", name = "RIGHT LOOP" },

            new SwitchMapping { id = "51", name = "L RAMP ENTER" },
            new SwitchMapping { id = "52", name = "L RAMP EXIT" },
            new SwitchMapping { id = "53", name = "CENTER RAMP" },
            new SwitchMapping { id = "54", name = "UPPER REBOUND" },
            new SwitchMapping { id = "55", name = "LEFT LOOP" },
            new SwitchMapping { id = "56", name = "STANDUP 2" },
            new SwitchMapping { id = "57", name = "STANDUP 3" },
            new SwitchMapping { id = "58", name = "STANDUP 4" },

            new SwitchMapping { id = "61", name = "SIDE RAMP ENTER" },
            new SwitchMapping { id = "62", name = "SIDE RAMP EXIT" },
            new SwitchMapping { id = "63", name = "(M)TL ROLLOVER" },
            new SwitchMapping { id = "64", name = "M(T)L ROLLOVER" },
            new SwitchMapping { id = "65", name = "MT(L) ROLLOVER" },
            new SwitchMapping { id = "66", name = "EJECT" },
            new SwitchMapping { id = "67", name = "ELEVATOR INDEX" },

            new SwitchMapping { id = "71", name = "CAR CRASH 1" },
            new SwitchMapping { id = "72", name = "CAR CRASH 2" },
            new SwitchMapping { id = "73", name = "TOP POPPER" },
            new SwitchMapping { id = "74", name = "ELEVATOR HOLD" },
            new SwitchMapping { id = "76", name = "BOTTOM POPPER" },
            new SwitchMapping { id = "77", name = "EYEBALL STANDUP" },
            new SwitchMapping { id = "78", name = "STANDUP 1" },

            new SwitchMapping { id = "81", name = "CLAW \"CAPT SIM\"" },
            new SwitchMapping { id = "82", name = "CLAW \"SUP JETS\"" },
            new SwitchMapping { id = "83", name = "CLAW \"PR BREAK\"" },
            new SwitchMapping { id = "84", name = "CLAW \"FREEZE\"" },
            new SwitchMapping { id = "85", name = "CLAW \"ACMAG\"" },
            new SwitchMapping { id = "86", name = "UL FLIPPER GATE" },
            new SwitchMapping { id = "87", name = "CAR CR STANDUP" },
            new SwitchMapping { id = "88", name = "LOWER REBOUND" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "RIGHT SPINNER" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to imag
            image = "playfield-dm.jpg"
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "wpcDcs"
        };

        public string[] cabinetColors => new string[]
        {
            "#72BAF6",
            "#F4D7AD",
            "#E8BE42",
            "#BBBDBE"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: 25, 26, 31, 32, 33, 34, 35, 36, 67, 71, 72, 73, 74, 76
                "25", "26", "36", "67", "71", "72", "73", "74", "76",
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

        public MemoryPosition? memoryPosition => null;

        public string[] testErrors => null;
    }
}