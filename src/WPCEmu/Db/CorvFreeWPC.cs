namespace WPCEmu.Db
{
    public class CorvetteFreewpc : IDb
    {
        public string name => "WPC-S: Corvette (FreeWPC)";
        public string version => "0.61";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "corv_f61" },
            gameName = "Corvette (FreeWPC 0.61)",
            id = "f61"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "corf0_61.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "PLUNGER" },
            new SwitchMapping { id = "16", name = "L RETURN LANE" },
            new SwitchMapping { id = "17", name = "R RETURN LANE" },
            new SwitchMapping { id = "18", name = "SPINNER" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "1ST GEAR (OPT)" },
            new SwitchMapping { id = "26", name = "2ND GEAR (OPT)" },
            new SwitchMapping { id = "27", name = "3RD GEAR (OPT)" },
            new SwitchMapping { id = "28", name = "4TH GEAR (OPT)" },

            new SwitchMapping { id = "31", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "35", name = "ROUTE 66 ENTRY" },
            new SwitchMapping { id = "36", name = "PIT STOP POPPER" },
            new SwitchMapping { id = "37", name = "TROUGH EJECT" },
            new SwitchMapping { id = "38", name = "INNER LOOP ENTRY" },

            new SwitchMapping { id = "41", name = "ZR1 BOTTOM ENTRY" },
            new SwitchMapping { id = "42", name = "ZR1 TOP ENTRY" },
            new SwitchMapping { id = "43", name = "SKID PAD ENTRY" },
            new SwitchMapping { id = "44", name = "SKID PAD EXIT" },
            new SwitchMapping { id = "45", name = "ROUTE 66 EXIT" },
            new SwitchMapping { id = "46", name = "L STANDUP 3" },
            new SwitchMapping { id = "47", name = "L STANDUP 2" },
            new SwitchMapping { id = "48", name = "L STANDUP 1" },

            new SwitchMapping { id = "51", name = "L RACE START" },
            new SwitchMapping { id = "52", name = "R RACE START" },
            new SwitchMapping { id = "55", name = "L RACE ENCODER" },
            new SwitchMapping { id = "56", name = "R RACE ENCODER" },
            new SwitchMapping { id = "57", name = "ROUTE 66 KICKOUT" },
            new SwitchMapping { id = "58", name = "SKID RTE66 EXIT" },

            new SwitchMapping { id = "61", name = "L SLINGSHOT" },
            new SwitchMapping { id = "62", name = "R SLINGSHOT" },
            new SwitchMapping { id = "63", name = "LEFT JET" },
            new SwitchMapping { id = "64", name = "BOTTOM JET" },
            new SwitchMapping { id = "65", name = "RIGHT JET" },
            new SwitchMapping { id = "66", name = "L ROLLOVER" },
            new SwitchMapping { id = "67", name = "M ROLLOVER" },
            new SwitchMapping { id = "68", name = "R ROLLOVER" },

            new SwitchMapping { id = "71", name = "ZR1 FULL LEFT" },
            new SwitchMapping { id = "72", name = "ZR1 FULL RIGHT" },
            new SwitchMapping { id = "75", name = "ZR1 EXIT" },
            new SwitchMapping { id = "76", name = "ZR1 LOCK BALL 1" },
            new SwitchMapping { id = "77", name = "ZR1 LOCK BALL 2" },
            new SwitchMapping { id = "78", name = "ZR1 LOCK BALL 3" },

            new SwitchMapping { id = "81", name = "MILLION STANDUP" },
            new SwitchMapping { id = "82", name = "SKID PAD STANDUP" },
            new SwitchMapping { id = "83", name = "R STANDUP" },
            new SwitchMapping { id = "84", name = "R RUBBER" },
            new SwitchMapping { id = "86", name = "JET RUBBER" },
            new SwitchMapping { id = "87", name = "L OUTER LOOP" },
            new SwitchMapping { id = "88", name = "R OUTER LOOP" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-corv.jpg"
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "securityPic",
            "wpcSecure"
        };

        public string[] cabinetColors => new string[]
        {
            "#DBBD5C",
            "#C53A36",
            "#9A3F75",
            "#8490C3"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "41", "42", "43", "51", "52", "55", "56", "71", "72"
                "35", "36", "37", "41", "42", "43", "51", "52", "55", "56", "71", "72"
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