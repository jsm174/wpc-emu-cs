namespace WPCEmu.Db
{
    public class HighSpeed2TheGetaway : IDb
    {
        public string name => "WPC-Fliptronics: High Speed II, The Getaway";
        public string version => "L-5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "gw_pb", "gw_pc", "gw_pd", "gw_p7", "gw_p8", "gw_l1", "gw_d1", "gw_l2", "gw_d2", "gw_l3", "gw_d3", "gw_l5", "gw_d5", "gw_l5c" },
            gameName = "Getaway: High Speed II, The",
            id = "gw",
            vpdbId = "hs2"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "GETAW_L5.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT FREEWAY BOT" },
            new SwitchMapping { id = "16", name = "LEFT FREEWAY TOP" },
            new SwitchMapping { id = "17", name = "FREEWAY BOT" },
            new SwitchMapping { id = "18", name = "FREEWAY TOP" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "LEFT OUT LANE" },
            new SwitchMapping { id = "26", name = "LEFT RET LANE" },
            new SwitchMapping { id = "27", name = "RIGHT RET LANE" },
            new SwitchMapping { id = "28", name = "RIGHT OUT LANE" },

            new SwitchMapping { id = "31", name = "LEFT SLING" },
            new SwitchMapping { id = "32", name = "RIGHT SLING" },
            new SwitchMapping { id = "33", name = "GEAR SHIFTER LO" },
            new SwitchMapping { id = "34", name = "GEAR SHIFTER HI" },
            new SwitchMapping { id = "36", name = "TOP RED" },
            new SwitchMapping { id = "37", name = "MIDDLE RED" },
            new SwitchMapping { id = "38", name = "BOTTOM RED" },

            new SwitchMapping { id = "41", name = "TOP YELLOW" },
            new SwitchMapping { id = "42", name = "MID YELLOW" },
            new SwitchMapping { id = "43", name = "BOT YELLOW" },
            new SwitchMapping { id = "44", name = "R BANK BOT" },
            new SwitchMapping { id = "45", name = "R BANK MID" },
            new SwitchMapping { id = "46", name = "R BANK TOP" },

            new SwitchMapping { id = "51", name = "TOP GREEN" },
            new SwitchMapping { id = "52", name = "MIDDLE GREEN" },
            new SwitchMapping { id = "53", name = "BOTTOM GREEN" },
            new SwitchMapping { id = "54", name = "RAMP DOWN" },
            new SwitchMapping { id = "55", name = "OUTHOLE" },
            new SwitchMapping { id = "56", name = "LEFT TROUGH" },
            new SwitchMapping { id = "57", name = "CENTER TROUGH" },
            new SwitchMapping { id = "58", name = "RIGHT TROUGH" },

            new SwitchMapping { id = "61", name = "TOP JET" },
            new SwitchMapping { id = "62", name = "LEFT JET" },
            new SwitchMapping { id = "63", name = "BOTTOM JET" },
            new SwitchMapping { id = "65", name = "MADE UP/DWN RAMP" },
            new SwitchMapping { id = "67", name = "MADE L RAMP" },

            new SwitchMapping { id = "71", name = "TOP LOOP" },
            new SwitchMapping { id = "72", name = "MID LOOP" },
            new SwitchMapping { id = "73", name = "BOT LOOP" },
            new SwitchMapping { id = "74", name = "TOP LOCK" },
            new SwitchMapping { id = "75", name = "MID LOCK" },
            new SwitchMapping { id = "76", name = "BOT LOCK" },
            new SwitchMapping { id = "77", name = "EJECT HOLE" },
            new SwitchMapping { id = "78", name = "SHOOTER" },

            new SwitchMapping { id = "81", name = "OPTO 1" },
            new SwitchMapping { id = "82", name = "OPTO 2" },
            new SwitchMapping { id = "83", name = "OPTO 3" },
            new SwitchMapping { id = "84", name = "ENTER LEFT RAMP" },
            new SwitchMapping { id = "85", name = "OPTO MADE LOOP" },
            new SwitchMapping { id = "86", name = "L BANK BOT" },
            new SwitchMapping { id = "87", name = "L BANK MID" },
            new SwitchMapping { id = "88", name = "L BANK TOP" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-getaway.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#C24B3D",
            "#939494",
            "#D14537"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                "33", "55", "56", "57", "58",
                //OPTO SWITCH
                "81", "82", "83", "84", "85"

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