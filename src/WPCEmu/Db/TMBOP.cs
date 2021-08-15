namespace WPCEmu.Db
{
    public class TheMachineBrideOfPinbot : IDb
    {
        public string name => "WPC-ALPHA: The Machine: Bride of Pin·bot";
        public string version => "L-7";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "bop_l2", "bop_d2", "bop_l3", "bop_d3", "bop_l4", "bop_d4", "bop_l5", "bop_d5", "bop_l6", "bop_d6", "bop_l7", "bop_d7", "bop_l8", "bop_d8" },
            gameName = "Machine: Bride of Pinbot, The",
            id = "bop",
            vpdbId = "tmbop"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "tmbopl_7.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "16", name = "LEFT FLIPPER LANE" },
            new SwitchMapping { id = "17", name = "RIGHT FLIPPER LANE" },
            new SwitchMapping { id = "18", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKET OPTO" },
            new SwitchMapping { id = "25", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "26", name = "CENTER TROUGH" },
            new SwitchMapping { id = "27", name = "LEFT TROUGH" },
            new SwitchMapping { id = "28", name = "LEFT STANDUP" },

            new SwitchMapping { id = "31", name = "SKILL SHOT 50K" },
            new SwitchMapping { id = "32", name = "SKILL SHOT 75K" },
            new SwitchMapping { id = "33", name = "SKILL SHOT 100K" },
            new SwitchMapping { id = "34", name = "SKILL SHOT 200K" },
            new SwitchMapping { id = "35", name = "SKILL SHOT 25K" },
            new SwitchMapping { id = "36", name = "RIGHT TOP STANDUP" },
            new SwitchMapping { id = "37", name = "RIGHT BOTTOM STANDUP" },
            new SwitchMapping { id = "38", name = "OUTHOLE" },

            new SwitchMapping { id = "41", name = "RIGHT RAMP MADE" },
            new SwitchMapping { id = "43", name = "LEFT LOOP" },
            new SwitchMapping { id = "44", name = "RIGHT LOOP TOP" },
            new SwitchMapping { id = "45", name = "RIGHT LOOP BOTTOM" },
            new SwitchMapping { id = "46", name = "UNDER PLAYFIELD KICKBACK" },
            new SwitchMapping { id = "47", name = "ENTER HEAD" },

            new SwitchMapping { id = "51", name = "SPINNER" },
            new SwitchMapping { id = "52", name = "SHOOTER" },
            new SwitchMapping { id = "53", name = "UPPER RIGHT JET BUMPER" },
            new SwitchMapping { id = "54", name = "UPPER LEFT JET BUMPER" },
            new SwitchMapping { id = "55", name = "LOWER JET BUMPER" },
            new SwitchMapping { id = "56", name = "JET BUMPER SLING" },
            new SwitchMapping { id = "57", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "58", name = "RIGHT SLINGSHOT" },

            new SwitchMapping { id = "63", name = "HEAD LEFT EYE" },
            new SwitchMapping { id = "64", name = "HEAD RIGHT EYE" },
            new SwitchMapping { id = "65", name = "HEAD MOUTH" },
            new SwitchMapping { id = "67", name = "FACE POSITION" },

            new SwitchMapping { id = "71", name = "WIREFORM TOP" },
            new SwitchMapping { id = "72", name = "WIREFORM BOTTOM" },
            new SwitchMapping { id = "73", name = "ENTER MINI PLAYFIELD" },
            new SwitchMapping { id = "74", name = "MINI EXIT LEFT" },
            new SwitchMapping { id = "75", name = "MINI EXIT RIGHT" },
            new SwitchMapping { id = "76", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "77", name = "RIGHT RAMP ENTER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-bop.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcAlphanumeric"
        };

        public string[] cabinetColors => new string[]
        {
            "#F6D64A",
            "#CF4330",
            "#D2D2D1"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                //OPTO "23"
                "22", "23",
                "25", "26", "27"
            },
            initialAction = new InitialAction[]
            {
                new InitialAction
                {
                    delayMs = 1500,
                    source = "cabinetInput",
                    value = 16
                }
            }
        };

        public MemoryPosition? memoryPosition => null;

        public string[] testErrors => new string[]
        {
            "HEAD MOTOR AND/OR SWITCH ERROR"
        };
    }
}