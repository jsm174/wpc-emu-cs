namespace WPCEmu.Db
{
    public class WhiteWaterFreewpc : IDb
    {
        public string name => "WPC-Fliptronics: White Water \"Bigfoot\" (FreeWPC)";
        public string version => "0.1";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "ww_bfr01", "ww_bfr01b", "ww_bfr01c", "ww_bfr01d" },
            gameName = "White Water (FreeWPC)",
            id = "wwF"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "wwatr_l5.freewpc.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "OUTHOLE" },
            new SwitchMapping { id = "16", name = "LEFT JET" },
            new SwitchMapping { id = "17", name = "RIGHT JET" },
            new SwitchMapping { id = "18", name = "CENTER JET" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKET OPTO" },
            new SwitchMapping { id = "25", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "26", name = "LEFT FLIP LANE" },
            new SwitchMapping { id = "27", name = "RIGHT FLIP LANE" },
            new SwitchMapping { id = "28", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "31", name = "RIVER \"R2\"" },
            new SwitchMapping { id = "32", name = "RIVER \"E\"" },
            new SwitchMapping { id = "33", name = "RIVER \"V\"" },
            new SwitchMapping { id = "34", name = "RIVER \"I\"" },
            new SwitchMapping { id = "35", name = "RIVER \"R1\"" },
            new SwitchMapping { id = "36", name = "THREE BANK TOP" },
            new SwitchMapping { id = "37", name = "THREE BANK CNTR" },
            new SwitchMapping { id = "38", name = "THREE BANK LOWER" },

            new SwitchMapping { id = "41", name = "LIGHT LOCK LEFT" },
            new SwitchMapping { id = "42", name = "LIGHT LOCK RIGHT" },
            new SwitchMapping { id = "43", name = "LEFT LOOP" },
            new SwitchMapping { id = "44", name = "RIGHT LOOP" },
            new SwitchMapping { id = "45", name = "SECRET PASSAGE" },
            new SwitchMapping { id = "46", name = "LFT RAMP ENTER" },
            new SwitchMapping { id = "47", name = "RAPIDS ENTER" },
            new SwitchMapping { id = "48", name = "CANYON ENTRANCE" },

            new SwitchMapping { id = "51", name = "LEFT SLING" },
            new SwitchMapping { id = "52", name = "RIGHT SLING" },
            new SwitchMapping { id = "53", name = "BALLSHOOTER" },
            new SwitchMapping { id = "54", name = "LOWER JET ARENA" },
            new SwitchMapping { id = "55", name = "RIGHT JET ARENA" },
            new SwitchMapping { id = "56", name = "EXTRA BALL" },
            new SwitchMapping { id = "57", name = "CANYON MAIN" },
            new SwitchMapping { id = "58", name = "BIGFOOT CAVE" },

            new SwitchMapping { id = "61", name = "WHIRLPOOL POPPER" },
            new SwitchMapping { id = "62", name = "WHIRLPOOL EXIT" },
            new SwitchMapping { id = "63", name = "LOCKUP RIGHT" },
            new SwitchMapping { id = "64", name = "LOCKUP CENTER" },
            new SwitchMapping { id = "65", name = "LOCKUP LEFT" },
            new SwitchMapping { id = "66", name = "LEFT RAMP MAIN" },
            new SwitchMapping { id = "68", name = "DISAS DROP ENTER" },

            new SwitchMapping { id = "71", name = "RAPIDS RAMP MAIN" },
            new SwitchMapping { id = "73", name = "HOT FOOT UPPER" },
            new SwitchMapping { id = "74", name = "HOT FOOT LOWER" },
            new SwitchMapping { id = "75", name = "DISAS DROP MAIN" },
            new SwitchMapping { id = "76", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "77", name = "CENTER TROUGH" },
            new SwitchMapping { id = "78", name = "LEFT TROUGH" },

            new SwitchMapping { id = "86", name = "BIGFOOT OPTO 1" },
            new SwitchMapping { id = "87", name = "BIGFOOT OPTO 2" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-ww.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 12, y = 316, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 12, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 30, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 154, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 171, y = 292, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 63, y = 76, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 51, y = 274, color = "RED" } },

                new Lamp[] { new Lamp { x = 35, y = 202, color = "LBLUE" } }, //21
                new Lamp[] { new Lamp { x = 35, y = 214, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 36, y = 224, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 36, y = 236, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 37, y = 246, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 73, y = 110, color = "RED" } },
                new Lamp[] { new Lamp { x = 75, y = 126, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 76, y = 133, color = "LBLUE" } },

                new Lamp[] { new Lamp { x = 66, y = 216, color = "YELLOW" } }, //31
                new Lamp[] { new Lamp { x = 87, y = 192, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 80, y = 196, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 57, y = 181, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 52, y = 161, color = "RED" } },
                new Lamp[] { new Lamp { x = 63, y = 150, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 69, y = 322, color = "WHITE" } },
                new Lamp[] { new Lamp { x = 63, y = 310, color = "LBLUE" } },

                new Lamp[] { new Lamp { x = 109, y = 175, color = "YELLOW" } }, //41
                new Lamp[] { new Lamp { x = 112, y = 185, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 77, y = 143, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 107, y = 165, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 101, y = 150, color = "RED" } },
                new Lamp[] { new Lamp { x = 90, y = 147, color = "LBLUE" } },
                new Lamp[] { new Lamp { x = 59, y = 297, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 55, y = 285, color = "ORANGE" } },

                new Lamp[] { new Lamp { x = 178, y = 172, color = "RED" } }, //51
                new Lamp[] { new Lamp { x = 14, y = 120, color = "RED" } },
                new Lamp[] { new Lamp { x = 91, y = 91, color = "RED" } },
                new Lamp[] { new Lamp { x = 100, y = 83, color = "RED" } },
                new Lamp[] { new Lamp { x = 78, y = 50, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 127, y = 259, color = "RED" } },
                new Lamp[] { new Lamp { x = 130, y = 243, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 87, y = 310, color = "YELLOW" } }, //61
                new Lamp[] { new Lamp { x = 118, y = 292, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 105, y = 270, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 81, y = 282, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 89, y = 257, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 94, y = 239, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 111, y = 231, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 117, y = 239, color = "YELLOW" } },

                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } }, //71
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 123, y = 107, color = "RED" } },
                new Lamp[] { new Lamp { x = 133, y = 100, color = "RED" } },

                new Lamp[] { new Lamp { x = 144, y = 240, color = "YELLOW" } }, //81
                new Lamp[] { new Lamp { x = 147, y = 222, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 163, y = 215, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 163, y = 195, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 20, y = 395, color = "YELLOW" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 150, y = 77 },
                new Flashlamp { id = "19", x = 23, y = 23 },
                new Flashlamp { id = "20", x = 39, y = 37 },
                new Flashlamp { id = "21", x = 10, y = 170 },
                new Flashlamp { id = "22", x = 10, y = 238 },
                new Flashlamp { id = "23", x = 134, y = 153 }
            }
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "wpcFliptronics"
        };

        public string[] cabinetColors => new string[]
        {
            "#0632C1",
            "#B252E7",
            "#ECCD47",
            "#7EC1EB",
            "#E63D26",
            "#79A332"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "86", "87",
                "76", "77", "78",
                "86", "87"
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