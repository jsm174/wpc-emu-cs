using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class TheAddamsFamilySpecial : IDb
    {
        public string name => "WPC-Fliptronics: The Addams Family Special Collectors Edition";
        public string version => "LA-3";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "tafg_h3", "tafg_i3", "tafg_lx3", "tafg_dx3", "tafg_la2", "tafg_da2", "tafg_la3", "tafg_da3" },
            gameName = "Addams Family Special Collectors Edition, The",
            id = "ta"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "U6-LA3.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "BUY IN" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT TROUGH" },
            new SwitchMapping { id = "16", name = "CENTER TROUGH" },
            new SwitchMapping { id = "17", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "18", name = "OUTHOLE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "RIGHT FLIP LANE" },
            new SwitchMapping { id = "26", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "27", name = "BALL SHOOTER" },

            new SwitchMapping { id = "31", name = "UPPER LEFT JET" },
            new SwitchMapping { id = "32", name = "UPPER RIGHT JET" },
            new SwitchMapping { id = "33", name = "CENTER LEFT JET" },
            new SwitchMapping { id = "34", name = "CENTER RIGHT JET" },
            new SwitchMapping { id = "35", name = "LOWER JET" },
            new SwitchMapping { id = "36", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "37", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "38", name = "UPPER LEFT LOOP" },

            new SwitchMapping { id = "41", name = "GRAVE \"G\"" },
            new SwitchMapping { id = "42", name = "GRAVE \"R\"" },
            new SwitchMapping { id = "43", name = "CHAIR KICKOUT" },
            new SwitchMapping { id = "44", name = "COUSIN IT" },
            new SwitchMapping { id = "45", name = "LOWER SWAMP MIL" },
            new SwitchMapping { id = "47", name = "CENTER SWAMP MIL" },
            new SwitchMapping { id = "48", name = "UPPER SWAMP MIL" },

            new SwitchMapping { id = "51", name = "SHOOTER LANE" },
            new SwitchMapping { id = "53", name = "BOOKCASE OPTO 1" },
            new SwitchMapping { id = "54", name = "BOOKCASE OPTO 2" },
            new SwitchMapping { id = "55", name = "BOOKCASE OPTO 3" },
            new SwitchMapping { id = "56", name = "BOOKCASE OPTO 4" },
            new SwitchMapping { id = "57", name = "BUMPER LANE OPTO" },
            new SwitchMapping { id = "58", name = "RIGHT RAMP EXIT" },

            new SwitchMapping { id = "61", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "62", name = "TRAIN WRECK" },
            new SwitchMapping { id = "63", name = "THING EJECT LANE" },
            new SwitchMapping { id = "64", name = "RIGHT RAMP ENTER" },
            new SwitchMapping { id = "65", name = "RIGHT RAMP TOP" },
            new SwitchMapping { id = "66", name = "LEFT RAMP TOP" },
            new SwitchMapping { id = "67", name = "UPPER RIGHT LOOP" },
            new SwitchMapping { id = "68", name = "VAULT" },

            new SwitchMapping { id = "71", name = "SWAMP LOCK UPPER" },
            new SwitchMapping { id = "72", name = "SWAMP LOCK CENTR" },
            new SwitchMapping { id = "73", name = "SWAMP LOCK LOWER" },
            new SwitchMapping { id = "74", name = "LOCKUP KICKOUT" },
            new SwitchMapping { id = "75", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "76", name = "LFT FLIP LANE 2" },
            new SwitchMapping { id = "77", name = "THING KICKOUT" },
            new SwitchMapping { id = "78", name = "LFT FLIP LANE 1" },

            new SwitchMapping { id = "81", name = "BOOKCASE OPEN" },
            new SwitchMapping { id = "82", name = "BOOKCASE CLOSED" },
            new SwitchMapping { id = "84", name = "THING DOWN OPTO" },
            new SwitchMapping { id = "85", name = "THING UP OPTO" },
            new SwitchMapping { id = "86", name = "GRAVE \"A\"" },
            new SwitchMapping { id = "87", name = "THING EJECT HOLE" }
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

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-addams.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#4670A9",
            "#EAC757",
            "#9B9DA3",
            "#50306A"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "15", "16", "17",
                "22",
                //OPTO SWITCHES
                "53", "54", "55", "56", "57", "84", "85"
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