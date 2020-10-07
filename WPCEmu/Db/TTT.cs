namespace WPCEmu.Db
{
    public class TicketTacToe : IDb
    {
        public string name => "WPC-95: Ticket Tac Toe (Redemption game)";
        public string version => "1.0";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "ttt_10" },
            gameName = "Ticket Tac Toe",
            id = "ttt"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "TIKT1_0.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "16", name = "RIGHT SLING" },
            new SwitchMapping { id = "17", name = "RIGHT POST" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "26", name = "LEFT POST" },
            new SwitchMapping { id = "27", name = "LEFT SLING" },

            new SwitchMapping { id = "31", name = "HOLE \"9\"" },
            new SwitchMapping { id = "32", name = "HOLE \"8\"" },
            new SwitchMapping { id = "33", name = "HOLE \"7\"" },
            new SwitchMapping { id = "34", name = "HOLE \"6\"" },
            new SwitchMapping { id = "35", name = "HOLE \"5\"" },
            new SwitchMapping { id = "36", name = "KICKER OPTO" },

            new SwitchMapping { id = "41", name = "HOLE \"4\"" },
            new SwitchMapping { id = "42", name = "HOLE \"3\"" },
            new SwitchMapping { id = "43", name = "HOLE \"2\"" },
            new SwitchMapping { id = "44", name = "HOLE \"1\"" },

            new SwitchMapping { id = "51", name = "TICKET OPTO" },
            new SwitchMapping { id = "52", name = "TICKETS LOW" },
            new SwitchMapping { id = "53", name = "TICKET TEST" },
            new SwitchMapping { id = "54", name = "SWITCH 54" },
            new SwitchMapping { id = "55", name = "SWITCH 55" },
            new SwitchMapping { id = "56", name = "SWITCH 56" },
            new SwitchMapping { id = "57", name = "SWITCH 57" },
            new SwitchMapping { id = "58", name = "SWITCH 58" },

            new SwitchMapping { id = "61", name = "SWITCH 61" },
            new SwitchMapping { id = "62", name = "SWITCH 62" },
            new SwitchMapping { id = "63", name = "SWITCH 63" },
            new SwitchMapping { id = "64", name = "SWITCH 64" },
            new SwitchMapping { id = "65", name = "SWITCH 65" },
            new SwitchMapping { id = "66", name = "SWITCH 66" },
            new SwitchMapping { id = "67", name = "SWITCH 67" },
            new SwitchMapping { id = "68", name = "SWITCH 68" },

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
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => null;

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#EEB455",
            "#C43B3A",
            "#F4DD61",
            "#9C8AB6",
            "#D59999",
            "#3A6FBB"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "41", "42", "43", "44", "51"
                "31", "32", "33", "34", "35", "41", "42", "43", "44",
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