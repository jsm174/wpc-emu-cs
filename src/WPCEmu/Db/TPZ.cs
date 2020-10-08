namespace WPCEmu.Db
{
    public class ThePartyZone : IDb
    {
        public string name => "WPC-DMD: The Party Zone";
        public string version => "L-2";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "pz_l1", "pz_d1", "pz_l2", "pz_d2", "pz_l3", "pz_d3", "pz_f4", "pz_f5" },
            gameName = "Party Zone",
            id = "pz"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "PZ_U6.L2"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "16", name = "HA - 1" },
            new SwitchMapping { id = "17", name = "HA - 2" },
            new SwitchMapping { id = "18", name = "HA - 3" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "26", name = "BOP - B" },
            new SwitchMapping { id = "27", name = "BOP - O" },
            new SwitchMapping { id = "28", name = "BOP - P" },

            new SwitchMapping { id = "31", name = "BACK RAMP SWITCH" },
            new SwitchMapping { id = "34", name = "EDM QUAL - 1" },
            new SwitchMapping { id = "35", name = "EDM QUAL - 2" },
            new SwitchMapping { id = "36", name = "EDM QUAL - 3" },
            new SwitchMapping { id = "37", name = "EDM QUAL - 4" },
            new SwitchMapping { id = "38", name = "EDM QUAL - 5" },

            new SwitchMapping { id = "41", name = "BACK BALL POPPER" },
            new SwitchMapping { id = "42", name = "RT BALL POPPER" },
            new SwitchMapping { id = "43", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "44", name = "RIGHT JET BUMPER" },
            new SwitchMapping { id = "45", name = "BOTTOM JET BUMP" },

            new SwitchMapping { id = "51", name = "HEAD OPTO - 1" },
            new SwitchMapping { id = "52", name = "HEAD OPTO - 2" },
            new SwitchMapping { id = "53", name = "HEAD OPTO - 3" },
            new SwitchMapping { id = "54", name = "LEFT RETURN LANE" },
            new SwitchMapping { id = "55", name = "LEFT DRAIN" },
            new SwitchMapping { id = "56", name = "END ZONE TARGET" },
            new SwitchMapping { id = "57", name = "RT RETURN LANE" },
            new SwitchMapping { id = "58", name = "RIGHT DRAIN" },

            new SwitchMapping { id = "61", name = "SHOOTER LANE" },
            new SwitchMapping { id = "62", name = "LANE TO TOP" },
            new SwitchMapping { id = "63", name = "OUT OF CONTROL" },
            new SwitchMapping { id = "64", name = "TOP REBOUND" },
            new SwitchMapping { id = "65", name = "SKILL SHOT" },
            new SwitchMapping { id = "66", name = "REQUEST" },
            new SwitchMapping { id = "67", name = "DJ EJECT" },
            new SwitchMapping { id = "68", name = "TIME" },

            new SwitchMapping { id = "71", name = "COTTAGE ENTRANCE" },
            new SwitchMapping { id = "72", name = "ENTER LEFT RAMP" },
            new SwitchMapping { id = "73", name = "LEFT SLING" },
            new SwitchMapping { id = "74", name = "RIGHT SLING" },
            new SwitchMapping { id = "75", name = "OUTHOLE" },
            new SwitchMapping { id = "76", name = "TROUGH 1" },
            new SwitchMapping { id = "77", name = "TROUGH 2" },
            new SwitchMapping { id = "78", name = "TROUGH 3" },

            new SwitchMapping { id = "81", name = "WAY" },
            new SwitchMapping { id = "82", name = "OUT" },
            new SwitchMapping { id = "83", name = "OF" },
            new SwitchMapping { id = "84", name = "CONTROL" },
            new SwitchMapping { id = "85", name = "2ND COTTAGE SW" }
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
            "#F9D649",
            "#E95F35",
            "#4B1F95",
            "#75F94C",
            "#EE7783"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                // OPTO SWITCHES: "51", "52", "53"
                // TODO: HEAD does not work
                "52", "53",
                "76", "77", "78"

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
            "HEAD MALFUNCTION, USE HEAD TEST"
        };
    }
}