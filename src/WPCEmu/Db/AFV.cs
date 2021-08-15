using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class AddamsFamilyValues : IDb
    {
        public string name => "WPC-DCS: Addams Family Values (Redemption game)";
        public string version => "L-4";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "afv_l4", "afv_d4" },
            gameName = "Addams Family Values",
            id = "afv"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "afv_u6.l4"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "COIN DROP" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "16", name = "TICKET NOTCH" },
            new SwitchMapping { id = "17", name = "TICKET LOW" },
            new SwitchMapping { id = "18", name = "TICKET TEST" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "28", name = "WHEEL INDEX" },

            new SwitchMapping { id = "31", name = "BOTTOM SLOT 16" },
            new SwitchMapping { id = "32", name = "BOTTOM SLOT 15" },
            new SwitchMapping { id = "33", name = "BOTTOM SLOT 14" },
            new SwitchMapping { id = "34", name = "BOTTOM SLOT 13" },
            new SwitchMapping { id = "35", name = "BOTTOM SLOT 12" },
            new SwitchMapping { id = "36", name = "BOTTOM SLOT 11" },
            new SwitchMapping { id = "37", name = "BOTTOM SLOT 10" },
            new SwitchMapping { id = "38", name = "BOTTOM SLOT 9" },

            new SwitchMapping { id = "41", name = "BOTTOM SLOT 8" },
            new SwitchMapping { id = "42", name = "BOTTOM SLOT 7" },
            new SwitchMapping { id = "43", name = "BOTTOM SLOT 6" },
            new SwitchMapping { id = "44", name = "BOTTOM SLOT 5" },
            new SwitchMapping { id = "45", name = "BOTTOM SLOT 4" },
            new SwitchMapping { id = "46", name = "BOTTOM SLOT 3" },
            new SwitchMapping { id = "47", name = "BOTTOM SLOT 2" },
            new SwitchMapping { id = "48", name = "BOTTOM SLOT 1" },

            new SwitchMapping { id = "51", name = "GOBBLE RIGHT" },
            new SwitchMapping { id = "52", name = "GOBBLE CENTER" },
            new SwitchMapping { id = "53", name = "GOBBLE LEFT" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => null;
       
        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#D6CB52",
            "#A32D27",
            "#3B85B7",
            "#C79E3F"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "38", "41", "42", "43", "44", "45", "46", "47", "48"
                "22",
                "31", "32", "33", "34", "35", "36", "37", "38", "41", "42", "43", "44", "45", "46", "47", "48"
            }
        };

        public MemoryPosition? memoryPosition => null;

        public string[] testErrors => null;
    }
}