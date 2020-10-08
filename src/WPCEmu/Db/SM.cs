namespace WPCEmu.Db
{
    public class StrikeMaster : IDb
    {
        public string name => "WPC-DMD: Strike Master Shuffle Alley (Redemption game)";
        public string version => "L-4";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "strik_l4", "strik_d4" },
            gameName = "Strike Master",
            id = "strik"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "STRIK_L4.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "GAME SELECT" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKET OPTO" },
            new SwitchMapping { id = "27", name = "LOW TICKET SENSE" },
            new SwitchMapping { id = "28", name = "MAN TICKET DISP" },

            new SwitchMapping { id = "31", name = "HIGH SCORE RESET" },
            new SwitchMapping { id = "33", name = "PIN SWITCH H" },
            new SwitchMapping { id = "34", name = "PIN SWITCH AA" },
            new SwitchMapping { id = "35", name = "PIN SWITCH G" },
            new SwitchMapping { id = "36", name = "PIN SWITCH S" },
            new SwitchMapping { id = "37", name = "PIN SWITCH R" },
            new SwitchMapping { id = "38", name = "PIN SWITCH Q" },

            new SwitchMapping { id = "41", name = "PIN SWITCH P" },
            new SwitchMapping { id = "42", name = "PIN SWITCH O" },
            new SwitchMapping { id = "43", name = "PIN SWITCH N" },
            new SwitchMapping { id = "44", name = "PIN SWITCH M" },
            new SwitchMapping { id = "45", name = "PIN SWITCH W" },
            new SwitchMapping { id = "46", name = "PIN SWITCH V" },
            new SwitchMapping { id = "47", name = "PIN SWITCH U" },
            new SwitchMapping { id = "48", name = "PIN SWITCH T" },

            new SwitchMapping { id = "51", name = "PIN SWITCH Z" },
            new SwitchMapping { id = "52", name = "PIN SWITCH Y" },
            new SwitchMapping { id = "53", name = "PIN SWITCH X" },
            new SwitchMapping { id = "54", name = "BACK ROW" },
            new SwitchMapping { id = "55", name = "PIN SWITCH K" },
            new SwitchMapping { id = "56", name = "PIN SWITCH L" },

            new SwitchMapping { id = "61", name = "PIN SWITCH F" },
            new SwitchMapping { id = "62", name = "PIN SWITCH E" },
            new SwitchMapping { id = "63", name = "PIN SWITCH B" },
            new SwitchMapping { id = "64", name = "PIN SWITCH A" },
            new SwitchMapping { id = "65", name = "PIN SWITCH D" },
            new SwitchMapping { id = "66", name = "PIN SWITCH C" },
            new SwitchMapping { id = "67", name = "PIN SWITCH J" },
            new SwitchMapping { id = "68", name = "PIN SWITCH I" }
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
            "#EABC52",
            "#8D2A1E",
            "#61708D",
            "#E98B4A"
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

        public string[] testErrors => null;
    }
}