namespace WPCEmu.Db
{
    public class DrDude : IDb
    {
        public string name => "WPC-ALPHA: Dr. Dude";
        public string version => "P-7";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "dd_p7" },
            gameName = "Dr. Dude",
            id = "dd"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "dude_u6.p7"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "PLUMB TILT" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "RIGHT COIN SWITCH" },
            new SwitchMapping { id = "15", name = "CENTER COIN SWITCH" },
            new SwitchMapping { id = "16", name = "LEFT COIN SWITCH" },
            new SwitchMapping { id = "17", name = "SLAM TILT" },
            new SwitchMapping { id = "18", name = "HIGH SCORE RESET" },

            new SwitchMapping { id = "21", name = "SHOOTER LANE" },
            new SwitchMapping { id = "22", name = "OUTHOLE" },
            new SwitchMapping { id = "23", name = "TROUGH 1 BALL" },
            new SwitchMapping { id = "24", name = "TROUGH 2 BALLS" },
            new SwitchMapping { id = "25", name = "TROUGH 3 BALLS" },
            new SwitchMapping { id = "26", name = "HEART TARGET" },
            new SwitchMapping { id = "27", name = "ENTER LEFT RAMP" },
            new SwitchMapping { id = "28", name = "SCORE LEFT RAMP" },

            new SwitchMapping { id = "31", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "32", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "33", name = "RIGHT RETURN" },
            new SwitchMapping { id = "34", name = "LEFT RETURN" },
            new SwitchMapping { id = "35", name = "RIGHT DROP 1 (TOP)" },
            new SwitchMapping { id = "36", name = "RIGHT DROP 2" },
            new SwitchMapping { id = "37", name = "RIGHT DROP 3" },
            new SwitchMapping { id = "38", name = "RIGHT DROP 4 (BOTTOM)" },

            new SwitchMapping { id = "41", name = "REFLE(X)" },
            new SwitchMapping { id = "42", name = "REFL(E)X" },
            new SwitchMapping { id = "43", name = "REF(L)EX" },
            new SwitchMapping { id = "44", name = "RE(F)LEX" },
            new SwitchMapping { id = "45", name = "R(E)FLEX" },
            new SwitchMapping { id = "46", name = "(R)EFLEX" },
            new SwitchMapping { id = "47", name = "BIG SHOT" },
            new SwitchMapping { id = "48", name = "MIDDLE RIGHT POPPER" },

            new SwitchMapping { id = "51", name = "MIXER GAB TOP" },
            new SwitchMapping { id = "52", name = "MIXER GAB MIDDLE" },
            new SwitchMapping { id = "53", name = "MIXER GAB BOTTOM" },
            new SwitchMapping { id = "54", name = "MIXER HEART LEFT" },
            new SwitchMapping { id = "55", name = "MIXER HEART MIDDLE" },
            new SwitchMapping { id = "56", name = "MIXER HEART RIGHT" },
            new SwitchMapping { id = "57", name = "TOP LEFT 10 PTS" },

            new SwitchMapping { id = "61", name = "MIXER MAG TOP" },
            new SwitchMapping { id = "62", name = "MIXER MAG MIDDLE" },
            new SwitchMapping { id = "63", name = "MIXER MAG BOTTOM" },
            new SwitchMapping { id = "66", name = "MIDDLE MIDDLE 10 PTS" },
            new SwitchMapping { id = "67", name = "MIDDLE BOTTOM 10 PTS" },
            new SwitchMapping { id = "68", name = "MIDDLE TOP 10 PTS" },

            new SwitchMapping { id = "71", name = "1 TEST TARGET" },
            new SwitchMapping { id = "72", name = "MAGNET TARGET" },
            new SwitchMapping { id = "73", name = "TOP LEFT POPPER" },
            new SwitchMapping { id = "74", name = "LEFT JUMPER BUMPER" },
            new SwitchMapping { id = "75", name = "RIGHT JUMPER BUMPER" },
            new SwitchMapping { id = "76", name = "BOTTOM JUMPER BUMPER" },
            new SwitchMapping { id = "77", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "78", name = "RIGHT SLINGSHOT" },

            new SwitchMapping { id = "81", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "82", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "83", name = "RIGHT LOOP" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-dd.jpg"
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "wpcAlphanumeric"
        };

        public string[] cabinetColors => new string[]
        {
            "#F5F455",
            "#DE7638",
            "#B3ACD7"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "23", "24", "25"
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