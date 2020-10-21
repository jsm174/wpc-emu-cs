namespace WPCEmu.Db
{
    public class TwilightZoneFreewpc : IDb
    {
        public string name => "WPC-Fliptronics: Twilight Zone (FreeWPC)";
        public string version => "1.0";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "tz_f100" },
            gameName = "Twilight Zone (FreeWPC 1.00)",
            id = "tz"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "ftz1_00.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT INLANE" },
            new SwitchMapping { id = "12", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "16", name = "CENTER TROUGH" },
            new SwitchMapping { id = "17", name = "LEFT TROUGH" },
            new SwitchMapping { id = "18", name = "OUTHOLE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BUY-IN BUTTON" },
            new SwitchMapping { id = "25", name = "FAR L TROUGH" },
            new SwitchMapping { id = "26", name = "TROUGH PROXIMITY" },
            new SwitchMapping { id = "27", name = "BALL SHOOTER" },
            new SwitchMapping { id = "28", name = "ROCKET KICKER" },

            new SwitchMapping { id = "31", name = "LEFT JET BUMPER" },
            new SwitchMapping { id = "32", name = "RIGHT JET BUMPER" },
            new SwitchMapping { id = "33", name = "LOWER JET BUMPER" },
            new SwitchMapping { id = "34", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "35", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "36", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "37", name = "LEFT INLANE 1" },
            new SwitchMapping { id = "38", name = "LEFT INLANE 2" },

            new SwitchMapping { id = "41", name = "DEAD END" },
            new SwitchMapping { id = "42", name = "THE CAMERA" },
            new SwitchMapping { id = "43", name = "PLAYER PIANO" },
            new SwitchMapping { id = "44", name = "MINI PF ENTER" },
            new SwitchMapping { id = "45", name = "MINI PF LEFT (2)" },
            new SwitchMapping { id = "46", name = "MINI PF RGHT (2)" },
            new SwitchMapping { id = "47", name = "CLOCK MILLIONS" },
            new SwitchMapping { id = "48", name = "LOW LEFT 5 MIL" },

            new SwitchMapping { id = "51", name = "GUM POPPER LANE" },
            new SwitchMapping { id = "52", name = "HITCH-HIKER" },
            new SwitchMapping { id = "53", name = "LEFT RAMP ENTER" },
            new SwitchMapping { id = "54", name = "LEFT RAMP" },
            new SwitchMapping { id = "55", name = "GUMBALL GENEVA" },
            new SwitchMapping { id = "56", name = "GUMBALL EXIT" },
            new SwitchMapping { id = "57", name = "SLOT PROXIMITY" },
            new SwitchMapping { id = "58", name = "SLOT KICKOUT" },

            new SwitchMapping { id = "61", name = "LOWER SKILL" },
            new SwitchMapping { id = "62", name = "CENTER SKILL" },
            new SwitchMapping { id = "63", name = "UPPER SKILL" },
            new SwitchMapping { id = "64", name = "U RIGHT 5 MIL" },
            new SwitchMapping { id = "65", name = "POWER PAYLOFF (2)" },
            new SwitchMapping { id = "66", name = "MID R 5 MIL 1" },
            new SwitchMapping { id = "67", name = "MID R 5 MIL 2" },
            new SwitchMapping { id = "68", name = "LOW RIGHT 5 MIL" },

            new SwitchMapping { id = "72", name = "AUTO-FIRE KICKER" },
            new SwitchMapping { id = "73", name = "RIGHT RAMP" },
            new SwitchMapping { id = "74", name = "GUMBALL POPPER" },
            new SwitchMapping { id = "75", name = "MINI PF TOP" },
            new SwitchMapping { id = "76", name = "MINI PF EXIT" },
            new SwitchMapping { id = "77", name = "MID LEFT 5 MIL" },
            new SwitchMapping { id = "78", name = "U LEFT 5 MIL" },

            new SwitchMapping { id = "81", name = "RIGHT MAGNET" },
            new SwitchMapping { id = "83", name = "LEFT MAGNET" },
            new SwitchMapping { id = "84", name = "LOCK CENTER" },
            new SwitchMapping { id = "85", name = "LOCK UPPER" },
            new SwitchMapping { id = "87", name = "GUMBALL ENTER" },
            new SwitchMapping { id = "88", name = "LOCK LOWER" }
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
            image = "playfield-tz.jpg"
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#FCEB4F",
            "#CB322C",
            "#47A5DF",
            "#C27133"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22"
                //OPTO SWITCHES:
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