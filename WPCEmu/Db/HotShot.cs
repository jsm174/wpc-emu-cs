namespace WPCEmu.Db
{
    public class HotShotBasketball : IDb
    {
        public string name => "WPC-DMD: Hot Shot Basketball (Redemption game)";
        public string version => "P-8";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "hshot_p8", "hshot_p9" },
            gameName = "Hot Shot Basketball",
            id = "hshot"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "hshot_p8.u6"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "15", name = "SERVE BALL LEFT" },
            new SwitchMapping { id = "16", name = "SERVE BALL RIGHT" },
            new SwitchMapping { id = "17", name = "SELECT GAME" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "26", name = "BALL IN SHOOTER" },
            new SwitchMapping { id = "27", name = "DISPENSER LOW" },
            new SwitchMapping { id = "28", name = "DISPENSER UNJAM" },

            new SwitchMapping { id = "31", name = "BASKET" },
            new SwitchMapping { id = "32", name = "BASKET MTR TOP" },
            new SwitchMapping { id = "33", name = "BASKET MTR BOT" }
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
            "#2153C6",
            "#A22C2C",
            "#ED9643",
            "#F5DC60"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22"
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
            "ERROR-BASKET NOT CALIBRATED",
            "CHK. BASKET MOTOR AND SW32/SW33"
        };
    }
}