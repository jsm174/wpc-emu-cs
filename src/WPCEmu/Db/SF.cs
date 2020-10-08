namespace WPCEmu.Db
{
    public class SlugFest : IDb
    {
        public string name => "WPC-DMD: SlugFest (Redemption game)";
        public string version => "L-1";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "sf_l1", "sf_d1" },
            gameName = "SlugFest",
            id = "sf"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "sf_u6.l1"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "DISPENSER PRICE" },
            new SwitchMapping { id = "25", name = "STEAL BASE/RUN" },
            new SwitchMapping { id = "26", name = "BAT SWITCH" },
            new SwitchMapping { id = "27", name = "DISPENSER LOW" },
            new SwitchMapping { id = "28", name = "DISPENSER UNJAM" },

            new SwitchMapping { id = "31", name = "PINCH HIT" },
            new SwitchMapping { id = "32", name = "FAST BALL PITCH" },
            new SwitchMapping { id = "33", name = "CHANGEUP PITCH" },
            new SwitchMapping { id = "34", name = "CURVE PITCH" },
            new SwitchMapping { id = "35", name = "SCREW BALL PITCH" },
            new SwitchMapping { id = "36", name = "THROW OUT RUNNER" },
            new SwitchMapping { id = "37", name = "START PLAYER 1" },
            new SwitchMapping { id = "38", name = "START PLAYER 2" },

            new SwitchMapping { id = "41", name = "TARGET PANEL-L" },
            new SwitchMapping { id = "42", name = "TARGET PANEL-2L" },
            new SwitchMapping { id = "43", name = "TARGET PANEL-3L" },
            new SwitchMapping { id = "44", name = "TARGET PANEL-M" },
            new SwitchMapping { id = "45", name = "TARGET PANEL-3R" },
            new SwitchMapping { id = "46", name = "TARGET PANEL-2R" },
            new SwitchMapping { id = "47", name = "TARGET PANEL-R" },

            new SwitchMapping { id = "51", name = "BLACK-ROW TROUGH" },
            new SwitchMapping { id = "52", name = "STRIKE TROUGH" },
            new SwitchMapping { id = "54", name = "PITCH OPTO" },
            new SwitchMapping { id = "55", name = "RAMP OPTO" },
            new SwitchMapping { id = "56", name = "PLAYFIELD TILT1" },
            new SwitchMapping { id = "57", name = "PLAYFIELD TILT2" },

            new SwitchMapping { id = "61", name = "L BLEACHER" },
            new SwitchMapping { id = "62", name = "M BLEACHER" },
            new SwitchMapping { id = "63", name = "R BLEACHER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => null;

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDmd"
        };

        public string[] cabinetColors => new string[]
        {
            "#232C94",
            "#992C1B",
            "#DAD446"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                // OPTO SWITCHES: "54", "55",
                "54", "55",
                "61", "62", "63"
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
            "Ramp Position Error",
            "Fast Pitch Error"
        };
    }
}