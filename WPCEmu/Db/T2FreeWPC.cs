namespace WPCEmu.Db
{
    public class Terminator2Freewpc : IDb
    {
        public string name => "WPC-DMD: Terminator 2 (FreeWPC)";
        public string version => "0.32";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "t2_f19", "t2_f20", "t2_f32" },
            gameName = "Terminator 2: Judgement Day (FreeWPC)",
            id = "t2F"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "ft20_32.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "RIGHT FLIPPER" },
            new SwitchMapping { id = "12", name = "LEFT FLIPPER" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "TROUGH LEFT" },
            new SwitchMapping { id = "16", name = "TROUGH CENTER" },
            new SwitchMapping { id = "17", name = "TROUGH RIGHT" },
            new SwitchMapping { id = "18", name = "OUTHOLE" },
            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKED OPTQ" },
            new SwitchMapping { id = "25", name = "LEFT OUT LANE" },
            new SwitchMapping { id = "26", name = "LEFT RET. LANE" },
            new SwitchMapping { id = "27", name = "RIGHT RET. LANE" },
            new SwitchMapping { id = "28", name = "RIGHT OUT LANE" },
            new SwitchMapping { id = "31", name = "GUN LOADED" },
            new SwitchMapping { id = "32", name = "GUN MARK" },
            new SwitchMapping { id = "33", name = "GUN HOME" },
            new SwitchMapping { id = "34", name = "GRIP TRIGGER" },
            new SwitchMapping { id = "36", name = "STAND MID LEFT" },
            new SwitchMapping { id = "37", name = "STAND MID CENTER" },
            new SwitchMapping { id = "38", name = "STAND MID RIGHT" },
            new SwitchMapping { id = "41", name = "LEFT JET" },
            new SwitchMapping { id = "42", name = "RIGHT JET" },
            new SwitchMapping { id = "43", name = "BOTTOM JET" },
            new SwitchMapping { id = "44", name = "LEFT SLING" },
            new SwitchMapping { id = "45", name = "RIGHT SLING" },
            new SwitchMapping { id = "46", name = "STAND RIGHT TOP" },
            new SwitchMapping { id = "47", name = "STAND RIGHT MID" },
            new SwitchMapping { id = "48", name = "STAND RIGHT BOT" },
            new SwitchMapping { id = "51", name = "LEFT LOCK" },
            new SwitchMapping { id = "53", name = "LO ESCAPE ROUTE" },
            new SwitchMapping { id = "54", name = "HI ESCAPE ROUTE" },
            new SwitchMapping { id = "55", name = "TOP LOCK" },
            new SwitchMapping { id = "56", name = "TOP LANE LEFT" },
            new SwitchMapping { id = "57", name = "TOP LANE CENTER" },
            new SwitchMapping { id = "58", name = "TOP LANE RIGHT" },
            new SwitchMapping { id = "61", name = "LEFT RAMP ENTRY" },
            new SwitchMapping { id = "62", name = "LEFT RAMP MADE" },
            new SwitchMapping { id = "63", name = "RIGHT RAMP ENTRY" },
            new SwitchMapping { id = "64", name = "RIGHT RAMP MADE" },
            new SwitchMapping { id = "65", name = "LO CHASE LOOP" },
            new SwitchMapping { id = "66", name = "HI CHASE LOOP" },
            new SwitchMapping { id = "71", name = "TARGET 1 HI" },
            new SwitchMapping { id = "72", name = "TARGET 2" },
            new SwitchMapping { id = "73", name = "TARGET 3" },
            new SwitchMapping { id = "74", name = "TARGET 4" },
            new SwitchMapping { id = "75", name = "TARGET 5 LOW" },
            new SwitchMapping { id = "76", name = "BALL POPPER" },
            new SwitchMapping { id = "77", name = "DROP TARGET" },
            new SwitchMapping { id = "78", name = "SHOOTER" }
        };

        public FliptronicsMapping[] fliptronicsMappings => null;

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-t2.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 61, y = 309, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 74, y = 303, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 89, y = 301, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 102, y = 303, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 115, y = 309, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 88, y = 353, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 89, y = 283, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } }, //#17 NOT USED

                new Lamp[] { new Lamp { x = 18, y = 310, color = "ORANGE" } }, //#21
                new Lamp[] { new Lamp { x = 18, y = 291, color = "RED" }, new Lamp { x = 160, y = 291, color = "RED" } },
                new Lamp[] { new Lamp { x = 30, y = 279, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 147, y = 279, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 77, y = 160, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 74, y = 141, color = "GREEN" } }, //#26
                new Lamp[] { new Lamp { x = 71, y = 121, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 68, y = 100, color = "LPURPLE" } },

                new Lamp[] { new Lamp { x = 53, y = 146, color = "YELLOW" } }, //#31
                new Lamp[] { new Lamp { x = 55, y = 156, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 58, y = 165, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 60, y = 174, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 62, y = 183, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 89, y = 154, color = "RED" } },
                new Lamp[] { new Lamp { x = 98, y = 160, color = "RED" } },
                new Lamp[] { new Lamp { x = 105, y = 167, color = "RED" } },

                new Lamp[] { new Lamp { x = 34, y = 184, color = "GREEN" } }, //#41
                new Lamp[] { new Lamp { x = 40, y = 204, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 47, y = 219, color = "RED" } },
                new Lamp[] { new Lamp { x = 50, y = 233, color = "RED" } },
                new Lamp[] { new Lamp { x = 55, y = 246, color = "RED" } },
                new Lamp[] { new Lamp { x = 59, y = 260, color = "RED" } },
                new Lamp[] { new Lamp { x = 62, y = 272, color = "RED" } },
                new Lamp[] { new Lamp { x = 67, y = 286, color = "RED" } },

                new Lamp[] { new Lamp { x = 84, y = 268, color = "RED" }, new Lamp { x = 94, y = 268, color = "RED" } }, //#51
                new Lamp[] { new Lamp { x = 53, y = 64, color = "RED" }, new Lamp { x = 69, y = 64, color = "RED" } },
                new Lamp[] { new Lamp { x = 136, y = 220, color = "RED" } },
                new Lamp[] { new Lamp { x = 133, y = 234, color = "RED" } },
                new Lamp[] { new Lamp { x = 126, y = 247, color = "RED" } },
                new Lamp[] { new Lamp { x = 120, y = 260, color = "RED" } },
                new Lamp[] { new Lamp { x = 115, y = 272, color = "RED" } },
                new Lamp[] { new Lamp { x = 111, y = 286, color = "RED" } },

                new Lamp[] { new Lamp { x = 66, y = 196, color = "ORANGE" } }, //#61
                new Lamp[] { new Lamp { x = 68, y = 209, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 71, y = 221, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 74, y = 233, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 77, y = 244, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 30, y = 237, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 39, y = 255, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 50, y = 130, color = "RED" } },

                new Lamp[] { new Lamp { x = 125, y = 198, color = "ORANGE" } }, //#71
                new Lamp[] { new Lamp { x = 120, y = 210, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 115, y = 221, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 111, y = 233, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 107, y = 244, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 146, y = 241, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 146, y = 250, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 146, y = 259, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 151, y = 189, color = "RED" } }, //#81
                new Lamp[] { new Lamp { x = 129, y = 187, color = "RED" } },
                new Lamp[] { new Lamp { x = 144, y = 205, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 41, y = 379, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 64, y = 80, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 98, y = 29, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 118, y = 33, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 139, y = 38, color = "GREEN" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 87, y = 326, },
                new Flashlamp { id = "18", x = 143, y = 327, },
                new Flashlamp { id = "19", x = 35, y = 327, },
                new Flashlamp { id = "20", x = 28, y = 161, },
                new Flashlamp { id = "21", x = 179, y = 228, },
                new Flashlamp { id = "22", x = 155, y = 131, },
                new Flashlamp { id = "23", x = 28, y = 60, },
                new Flashlamp { id = "25", x = 13, y = 144, }, new Flashlamp { id = "25", x = 13, y = 160, },
                new Flashlamp { id = "26", x = 37, y = 44, }, new Flashlamp { id = "26", x = 46, y = 69, },
                new Flashlamp { id = "27", x = 77, y = 65, }, new Flashlamp { id = "27", x = 80, y = 55, },
                new Flashlamp { id = "28", x = 63, y = 71, }
            }
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "wpcDmd"
        };

        public string[] cabinetColors => new string[]
        {
            "#3F96D6",
            "#BC3727",
            "#A3CEEA"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                //OPTO Switches: 23
                "15", "16", "17", "23"
            }
        };

        public MemoryPosition? memoryPosition => null;

        public string[] testErrors => null;
    }
}