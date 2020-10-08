namespace WPCEmu.Db
{
    public class WorldCupSoccerFreewpc : IDb
    {
        public string name => "WPC-S: World Cup Soccer (FreeWPC)";
        public string version => "0.62";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "wcs_f10", "wcs_f50", "wcs_f62" },
            gameName = "World Cup Soccer (FreeWPC 0.62)",
            id = "f62"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "wcs_f62.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "12", name = "MAG GOALIE BUT" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "L FLIPPER LANE" },
            new SwitchMapping { id = "16", name = "STRIKER 3 (HIGH)" },
            new SwitchMapping { id = "17", name = "R FLIPPER LANE" },
            new SwitchMapping { id = "18", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY EXTRA BALL" },
            new SwitchMapping { id = "25", name = "FREE KICK TARGET" },
            new SwitchMapping { id = "26", name = "KICKBACK UPPER" },
            new SwitchMapping { id = "27", name = "SPINNER" },
            new SwitchMapping { id = "28", name = "LIGHT KICKBACK" },

            new SwitchMapping { id = "31", name = "TROUGH 1 (RIGHT)" },
            new SwitchMapping { id = "32", name = "TROUGH 2" },
            new SwitchMapping { id = "33", name = "TROUGH 3" },
            new SwitchMapping { id = "34", name = "TROUGH 4" },
            new SwitchMapping { id = "35", name = "TROUGH 5 (LEFT)" },
            new SwitchMapping { id = "36", name = "TROUGH STACK" },
            new SwitchMapping { id = "37", name = "LIGHT MAG GOALIE" },
            new SwitchMapping { id = "38", name = "BALLSHOOTER" },

            new SwitchMapping { id = "41", name = "GOAL TROUGH" },
            new SwitchMapping { id = "42", name = "GOAL POPPER OPTO" },
            new SwitchMapping { id = "43", name = "GOALIE IS LEFT" },
            new SwitchMapping { id = "44", name = "GOALIE IS RIGHT" },
            new SwitchMapping { id = "45", name = "TV BALL POPPER" },
            new SwitchMapping { id = "47", name = "TRAVEL LANE ROLO" },
            new SwitchMapping { id = "48", name = "GOALIE TARGET" },

            new SwitchMapping { id = "51", name = "SKILL SHOT FRONT" },
            new SwitchMapping { id = "52", name = "SKILL SHOT CENT" },
            new SwitchMapping { id = "53", name = "SKILL SHOT REAR" },
            new SwitchMapping { id = "54", name = "RIGHT EJECT HOLE" },
            new SwitchMapping { id = "55", name = "UPPER EJECT HOLE" },
            new SwitchMapping { id = "56", name = "LEFT EJECT HOLE" },
            new SwitchMapping { id = "57", name = "R LANE HI-UNUSED" },
            new SwitchMapping { id = "58", name = "R LANE LO-UNUSED" },

            new SwitchMapping { id = "61", name = "ROLLOVER 1(HIGH)" },
            new SwitchMapping { id = "62", name = "ROLLOVER 2" },
            new SwitchMapping { id = "63", name = "ROLLOVER 3" },
            new SwitchMapping { id = "64", name = "ROLLOVER 4 (LOW)" },
            new SwitchMapping { id = "65", name = "TACKLE SWITCH" },
            new SwitchMapping { id = "66", name = "STRIKER 1 (LEFT)" },
            new SwitchMapping { id = "67", name = "STRIKER 2 (CENT)" },

            new SwitchMapping { id = "71", name = "L RAMP DIVERTED" },
            new SwitchMapping { id = "72", name = "L RAMP ENTRANCE" },
            new SwitchMapping { id = "74", name = "LEFT RAMP EXIT" },
            new SwitchMapping { id = "75", name = "R RAMP ENTRANCE" },
            new SwitchMapping { id = "76", name = "LOCK MECH LOW" },
            new SwitchMapping { id = "77", name = "LOCK MECH HIGH" },
            new SwitchMapping { id = "78", name = "RIGHT RAMP EXIT" },

            new SwitchMapping { id = "81", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "82", name = "UPPER JET BUMPER" },
            new SwitchMapping { id = "83", name = "LOWER JET BUMPER" },
            new SwitchMapping { id = "84", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "85", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "86", name = "KICKBACK" },
            new SwitchMapping { id = "87", name = "UPPER LEFT LANE" },
            new SwitchMapping { id = "88", name = "UPPER RIGHT LANE" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "RIGHT SPINNER" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUT" },
            new FliptronicsMapping { id = "F7", name = "LEFT SPINNER" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUT" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-wcs.jpg"
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#4F4EB2",
            "#59C5CC",
            "#EDE34C",
            "#D13A2A"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "41", "42", "43", "44", "45", "51", "52", "53"
                "36", "41", "42", "43", "44", "45", "51", "52", "53",
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