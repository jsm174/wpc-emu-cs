namespace WPCEmu.Db
{
    public class GilligansIsland : IDb
    {
        public string name => "WPC-DMD: Gilligan's Island";
        public string version => "L-9";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "gi_l3", "gi_d3", "gi_l4", "gi_d4", "gi_l6", "gi_d6", "gi_l9", "gi_d9" },
            gameName = "Gilligan's Island",
            id = "gi"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "gilli_l9.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "16", name = "TROUGH LEFT" },
            new SwitchMapping { id = "17", name = "TROUGH RIGHT" },
            new SwitchMapping { id = "18", name = "OUTHOLE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "LEFT OUT LANE" },
            new SwitchMapping { id = "26", name = "LEFT RETURN LN" },
            new SwitchMapping { id = "27", name = "RIGHT RETURN LN" },
            new SwitchMapping { id = "28", name = "RIGHT OUT LANE" },

            new SwitchMapping { id = "31", name = "PAYOFF MID LEFT" },
            new SwitchMapping { id = "32", name = "RIGHT 10PT" },
            new SwitchMapping { id = "33", name = "LEFT LOCK" },
            new SwitchMapping { id = "34", name = "LEFT STAND UP" },
            new SwitchMapping { id = "36", name = "LEFT BANK LEFT" },
            new SwitchMapping { id = "37", name = "LEFT BANK MIDDLE" },
            new SwitchMapping { id = "38", name = "LEFT BANK RIGHT" },

            new SwitchMapping { id = "41", name = "LEFT JET" },
            new SwitchMapping { id = "42", name = "RIGHT JET" },
            new SwitchMapping { id = "43", name = "BOTTOM JET" },
            new SwitchMapping { id = "44", name = "LEFT SLING" },
            new SwitchMapping { id = "45", name = "RIGHT SLING" },
            new SwitchMapping { id = "46", name = "RIGHT BANK LEFT" },
            new SwitchMapping { id = "47", name = "RIGHT BANK MID" },
            new SwitchMapping { id = "48", name = "RIGHT BANK RIGHT" },

            new SwitchMapping { id = "51", name = "LAGOON - N" },
            new SwitchMapping { id = "52", name = "LAGOON - O" },
            new SwitchMapping { id = "53", name = "LAGOON - O" },
            new SwitchMapping { id = "54", name = "LAGOON - G" },
            new SwitchMapping { id = "55", name = "LAGOON - A" },
            new SwitchMapping { id = "56", name = "LAGOON - L" },
            new SwitchMapping { id = "57", name = "RAMP_SUP" },
            new SwitchMapping { id = "58", name = "JET 10PTS" },

            new SwitchMapping { id = "61", name = "ISLAND ENTRANCE" },
            new SwitchMapping { id = "62", name = "RAMP STATUS" },
            new SwitchMapping { id = "63", name = "LEFT LOOP" },
            new SwitchMapping { id = "64", name = "RIGHT LOOP" },
            new SwitchMapping { id = "65", name = "S_TURN" },
            new SwitchMapping { id = "66", name = "BALL POPPER" },
            new SwitchMapping { id = "67", name = "TOP EJECT" },
            new SwitchMapping { id = "68", name = "TOP RIGHT" },

            new SwitchMapping { id = "71", name = "PAYOFF TOP LEFT" },
            new SwitchMapping { id = "72", name = "PAYOFF TOP RITE" },
            new SwitchMapping { id = "73", name = "PAYOFF BOT RITE" },
            new SwitchMapping { id = "74", name = "PAYOFF BOT LEFT" },
            new SwitchMapping { id = "75", name = "LOCK LANE" },
            new SwitchMapping { id = "76", name = "WHEEL LOCK" },
            new SwitchMapping { id = "77", name = "WHEEL OPTO" },
            new SwitchMapping { id = "78", name = "SHOOTER" },

            new SwitchMapping { id = "83", name = "TOP LEFT LOOP" },
            new SwitchMapping { id = "84", name = "TOP RIGHT LOOP" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-gilligan.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDmd"
        };

        public string[] cabinetColors => new string[]
        {
            "#EA4D27",
            "#1E3B9E",
            "#F5B942",
            "#4591B6",
            "#BAC54F"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "16", "17", "22",
                //OPTO SWITCHES: "77"
                "77"
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

        public string[] testErrors => new string[]
        {
            "Island ERROR"
        };
    }
}