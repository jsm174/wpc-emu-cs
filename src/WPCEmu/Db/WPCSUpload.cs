namespace WPCEmu.Db
{
    public class UploadWpcS : IDb
    {
        public string name => "UPLOAD: WPC-S Emulation";
        public string version => "Unknown";

        public Pinmame? pinmame => null;

        public RomFile? rom => new RomFile
        {
            u06 = "UPLOAD"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "SWITCH 15" },
            new SwitchMapping { id = "16", name = "SWITCH 16" },
            new SwitchMapping { id = "17", name = "SWITCH 17" },
            new SwitchMapping { id = "18", name = "SWITCH 18" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "SWITCH 25" },
            new SwitchMapping { id = "26", name = "SWITCH 26" },
            new SwitchMapping { id = "27", name = "SWITCH 27" },
            new SwitchMapping { id = "28", name = "SWITCH 28" },

            new SwitchMapping { id = "31", name = "SWITCH 31" },
            new SwitchMapping { id = "32", name = "SWITCH 32" },
            new SwitchMapping { id = "33", name = "SWITCH 33" },
            new SwitchMapping { id = "34", name = "SWITCH 34" },
            new SwitchMapping { id = "35", name = "SWITCH 35" },
            new SwitchMapping { id = "36", name = "SWITCH 36" },
            new SwitchMapping { id = "37", name = "SWITCH 37" },
            new SwitchMapping { id = "38", name = "SWITCH 38" },

            new SwitchMapping { id = "41", name = "SWITCH 41" },
            new SwitchMapping { id = "42", name = "SWITCH 42" },
            new SwitchMapping { id = "43", name = "SWITCH 43" },
            new SwitchMapping { id = "44", name = "SWITCH 44" },
            new SwitchMapping { id = "45", name = "SWITCH 45" },
            new SwitchMapping { id = "46", name = "SWITCH 46" },
            new SwitchMapping { id = "47", name = "SWITCH 47" },
            new SwitchMapping { id = "48", name = "SWITCH 48" },

            new SwitchMapping { id = "51", name = "SWITCH 51" },
            new SwitchMapping { id = "52", name = "SWITCH 52" },
            new SwitchMapping { id = "53", name = "SWITCH 53" },
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

            new SwitchMapping { id = "71", name = "SWITCH 71" },
            new SwitchMapping { id = "72", name = "SWITCH 72" },
            new SwitchMapping { id = "73", name = "SWITCH 73" },
            new SwitchMapping { id = "74", name = "SWITCH 74" },
            new SwitchMapping { id = "75", name = "SWITCH 75" },
            new SwitchMapping { id = "76", name = "SWITCH 76" },
            new SwitchMapping { id = "77", name = "SWITCH 77" },
            new SwitchMapping { id = "78", name = "SWITCH 78" },

            new SwitchMapping { id = "81", name = "SWITCH 81" },
            new SwitchMapping { id = "82", name = "SWITCH 82" },
            new SwitchMapping { id = "83", name = "SWITCH 83" },
            new SwitchMapping { id = "84", name = "SWITCH 84" },
            new SwitchMapping { id = "85", name = "SWITCH 85" },
            new SwitchMapping { id = "86", name = "SWITCH 86" },
            new SwitchMapping { id = "87", name = "SWITCH 87" },
            new SwitchMapping { id = "88", name = "SWITCH 88" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => null;

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "securityPic"
        };

        public string[] cabinetColors => null;

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22"
            }
        };

        public MemoryPosition? memoryPosition => null;

        public string[] testErrors => null;
    }
}