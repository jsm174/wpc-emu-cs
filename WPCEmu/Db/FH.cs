namespace WPCEmu.Db
{
    public class Funhouse : IDb
    {
        public string name => "WPC-ALPHA: Funhouse";
        public string version => "L-9";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "fh_l2", "fh_l3", "fh_d3", "fh_l4", "fh_d4", "fh_l5", "fh_d5", "fh_l9", "fh_d9", "fh_l9b", "fh_d9b", "fh_905h", "fh_906h" },
            gameName = "Funhouse",
            id = "fh"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "funh_l9.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "STEPS LIGHTS FRENZY" },
            new SwitchMapping { id = "16", name = "UPPER RAMP SWITCH" },
            new SwitchMapping { id = "17", name = "S-T-E-P \"S\"" },
            new SwitchMapping { id = "18", name = "UPPER LEFT JET BUMPER" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "FRONT DOOR" },
            new SwitchMapping { id = "25", name = "LOCK MECH RIGHT" },
            new SwitchMapping { id = "26", name = "STEPS LIGHTS EXTRA BALL" },
            new SwitchMapping { id = "27", name = "LOCK MECH CENTER" },
            new SwitchMapping { id = "28", name = "LOCK MECH LEFT" },

            new SwitchMapping { id = "31", name = "S-T-E-P \"P\"" },
            new SwitchMapping { id = "32", name = "TOP SUPERDOG STANDUP TARGET" },
            new SwitchMapping { id = "33", name = "UPPER LEFT GANGWAY ROLLUNDER" },
            new SwitchMapping { id = "34", name = "BOTTOM SUPERDOG STANDUP TARGET" },
            new SwitchMapping { id = "35", name = "STEPS TRACK LOWER" },
            new SwitchMapping { id = "36", name = "STEPS 500,000" },
            new SwitchMapping { id = "37", name = "CENTER SUPERDOG STANDUP TARGET" },
            new SwitchMapping { id = "38", name = "STEPS TRACK UPPER" },

            new SwitchMapping { id = "41", name = "LEFT SLINGSHOT (KICKER)" },
            new SwitchMapping { id = "42", name = "LEFT FLIPPER RETURN LANE" },
            new SwitchMapping { id = "43", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "44", name = "WIND TUNNEL HOLE" },
            new SwitchMapping { id = "45", name = "TRAP DOOR" },
            new SwitchMapping { id = "46", name = "RUDYS HIDEOUT KICKBIG" },
            new SwitchMapping { id = "47", name = "LEFT BALL SHOOTER" },
            new SwitchMapping { id = "48", name = "RAMP EXIT TRACK" },

            new SwitchMapping { id = "51", name = "DUMMY JAW" },
            new SwitchMapping { id = "52", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "53", name = "RIGHT SLINGSHOOT (KICKER)" },
            new SwitchMapping { id = "54", name = "S-T-E-P \"T\"" },
            new SwitchMapping { id = "55", name = "STEPS SUPERDOG" },
            new SwitchMapping { id = "56", name = "RAMPS ENTRANCE" },
            new SwitchMapping { id = "57", name = "JET BUMPER LANE" },
            new SwitchMapping { id = "58", name = "TUNNEL KICKOUT" },

            new SwitchMapping { id = "61", name = "RT INSIDE FLIPPER RETURN LANE" },
            new SwitchMapping { id = "62", name = "RIGHT BALL SHOOTER" },
            new SwitchMapping { id = "63", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "64", name = "S-T-E-P \"E\"" },
            new SwitchMapping { id = "65", name = "DUMMY EJECT HOLE" },
            new SwitchMapping { id = "66", name = "UPPER RIGHT GANGWAY LANE" },
            new SwitchMapping { id = "67", name = "LOWER RIGHT DROP HOLE" },
            new SwitchMapping { id = "68", name = "LOWER JET BUMPER" },

            new SwitchMapping { id = "71", name = "RT OUTSIDE FLIPPER RETURN LANE" },
            new SwitchMapping { id = "72", name = "LEFT TROUGH" },
            new SwitchMapping { id = "73", name = "OUTHOLE" },
            new SwitchMapping { id = "74", name = "CENTER TROUGH" },
            new SwitchMapping { id = "75", name = "UPPER RIGHT LOOP SWITCH" },
            new SwitchMapping { id = "76", name = "TRAP DOOR CLOSED" },
            new SwitchMapping { id = "77", name = "UPPER RIGHT JET BUMPER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-fh.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcAlphanumeric"
        };

        public string[] cabinetColors => new string[]
        {
            "#1F3A9D",
            "#86A7D6",
            "#D43126",
            "#F9E650"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                //OPTO "51", "55"
                "22",
                "51", "55",
                "63", "72", "74"
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

        public string[] testErrors => null;
    }
}