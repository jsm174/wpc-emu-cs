﻿using WPCEmu.Boards;

namespace WPCEmu.Db
{
    public class AttackFromMarsFreewpc : IDb
    {
        public string name => "WPC-95: Attack from Mars (FreeWPC)";
        public string version => "0.32";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "afm_f10", "afm_f20", "afm_f32" },
            gameName = "Attack from Mars (FreeWPC)",
            id = "afmF"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "afm_1_13.bin"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "LAUNCH BUTTON" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "RIGHT RETURN" },
            new SwitchMapping { id = "18", name = "SHOOTER LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "26", name = "LEFT RETURN" },
            new SwitchMapping { id = "27", name = "RIGHT OUTLANE" },

            new SwitchMapping { id = "31", name = "TROUGH EJECT" },
            new SwitchMapping { id = "32", name = "TROUGH BALL 1" },
            new SwitchMapping { id = "33", name = "TROUGH BALL 2" },
            new SwitchMapping { id = "34", name = "TROUGH BALL 3" },
            new SwitchMapping { id = "35", name = "TROUGH BALL 4" },
            new SwitchMapping { id = "36", name = "LEFT POPPER" },
            new SwitchMapping { id = "37", name = "RIGHT POPPER" },
            new SwitchMapping { id = "38", name = "LEFT TOP LANE" },

            new SwitchMapping { id = "41", name = "MARTI\"A\"N TARGET" },
            new SwitchMapping { id = "42", name = "MARTIA\"N\" TARGET" },
            new SwitchMapping { id = "43", name = "MAR\"T\"IAN TARGET" },
            new SwitchMapping { id = "44", name = "MART\"I\"AN TARGET" },
            new SwitchMapping { id = "45", name = "L MOTOR BANK" },
            new SwitchMapping { id = "46", name = "C MOTOR BANK" },
            new SwitchMapping { id = "47", name = "R MOTOR BANK" },
            new SwitchMapping { id = "48", name = "RIGHT TOP LANE" },

            new SwitchMapping { id = "51", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "52", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "53", name = "LEFT JET" },
            new SwitchMapping { id = "54", name = "BOTTOM JET" },
            new SwitchMapping { id = "55", name = "RIGHT JET" },
            new SwitchMapping { id = "56", name = "\"M\"ARTIAN TARGET" },
            new SwitchMapping { id = "57", name = "M\"A\"RTIAN TARGET" },
            new SwitchMapping { id = "58", name = "MA\"R\"TIAN TARGET" },

            new SwitchMapping { id = "61", name = "L RAMP ENTER" },
            new SwitchMapping { id = "62", name = "C RAMP ENTER" },
            new SwitchMapping { id = "63", name = "R RAMP ENTER" },
            new SwitchMapping { id = "64", name = "L RAMP EXIT" },
            new SwitchMapping { id = "65", name = "R RAMP EXIT" },
            new SwitchMapping { id = "66", name = "MOTOR BANK DOWN" },
            new SwitchMapping { id = "67", name = "MOTOR BANK UP" },

            new SwitchMapping { id = "71", name = "RIGHT LOOP HI" },
            new SwitchMapping { id = "72", name = "RIGHT LOOP LO" },
            new SwitchMapping { id = "73", name = "LEFT LOOP HI" },
            new SwitchMapping { id = "74", name = "LEFT LOOP LO" },
            new SwitchMapping { id = "75", name = "L SAUCER TGT" },
            new SwitchMapping { id = "76", name = "R SAUCER TGT" },
            new SwitchMapping { id = "77", name = "DROP TARGET" },
            new SwitchMapping { id = "78", name = "CENTER TROUGH" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-afm.jpg",
            lamps = new Lamp[][]
            {
                new Lamp[] { new Lamp { x = 71, y = 318, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 61, y = 326, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 81, y = 309, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 99, y = 309, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 91, y = 330, color = "RED" } },
                new Lamp[] { new Lamp { x = 110, y = 318, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 120, y = 326, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 102, y = 92, color = "RED" } },

                new Lamp[] { new Lamp { x = 61, y = 241, color = "RED" } }, // 21
                new Lamp[] { new Lamp { x = 58, y = 224, color = "RED" } },
                new Lamp[] { new Lamp { x = 53, y = 207, color = "RED" } },
                new Lamp[] { new Lamp { x = 49, y = 191, color = "RED" } },
                new Lamp[] { new Lamp { x = 44, y = 172, color = "RED" } },
                new Lamp[] { new Lamp { x = 71, y = 188, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 68, y = 175, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 65, y = 159, color = "RED" } },

                new Lamp[] { new Lamp { x = 126, y = 248, color = "RED" } }, // 31
                new Lamp[] { new Lamp { x = 131, y = 232, color = "RED" } },
                new Lamp[] { new Lamp { x = 135, y = 215, color = "RED" } },
                new Lamp[] { new Lamp { x = 142, y = 199, color = "RED" } },
                new Lamp[] { new Lamp { x = 145, y = 182, color = "RED" } },
                new Lamp[] { new Lamp { x = 125, y = 181, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 122, y = 193, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 128, y = 169, color = "ORANGE" } },

                new Lamp[] { new Lamp { x = 175, y = 159, color = "RED" } }, // 41
                new Lamp[] { new Lamp { x = 61, y = 143, color = "RED" } },
                new Lamp[] { new Lamp { x = 125, y = 23, color = "RED" } },
                new Lamp[] { new Lamp { x = 145, y = 22, color = "RED" } },
                new Lamp[] { new Lamp { x = 89, y = 136, color = "RED" } },
                new Lamp[] { new Lamp { x = 99, y = 136, color = "RED" } },
                new Lamp[] { new Lamp { x = 109, y = 136, color = "RED" } },
                new Lamp[] { new Lamp { x = 78, y = 147, color = "GREEN" } },

                new Lamp[] { new Lamp { x = 99, y = 260, color = "RED" } }, // 51
                new Lamp[] { new Lamp { x = 99, y = 244, color = "RED" } },
                new Lamp[] { new Lamp { x = 99, y = 227, color = "RED" } },
                new Lamp[] { new Lamp { x = 76, y = 215, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 74, y = 203, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 99, y = 212, color = "RED" } },
                new Lamp[] { new Lamp { x = 99, y = 196, color = "RED" } },
                new Lamp[] { new Lamp { x = 99, y = 179, color = "RED" } },

                new Lamp[] { new Lamp { x = 151, y = 252, color = "YELLOW" } }, // 61
                new Lamp[] { new Lamp { x = 150, y = 240, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 155, y = 219, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 159, y = 206, color = "ORANGE" } },
                new Lamp[] { new Lamp { x = 163, y = 192, color = "RED" } },
                new Lamp[] { new Lamp { x = 168, y = 176, color = "RED" } },
                new Lamp[] { new Lamp { x = 131, y = 156, color = "RED" } },
                new Lamp[] { new Lamp { x = 122, y = 147, color = "GREEN" } },

                new Lamp[] { new Lamp { x = 32, y = 208, color = "YELLOW" } }, // 71
                new Lamp[] { new Lamp { x = 28, y = 194, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 24, y = 180, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 21, y = 164, color = "RED" } },
                new Lamp[] { new Lamp { x = 16, y = 148, color = "RED" } },
                new Lamp[] { new Lamp { x = 32, y = 264, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 34, y = 253, color = "GREEN" } },
                new Lamp[] { new Lamp { x = 37, y = 242, color = "GREEN" } },

                new Lamp[] { new Lamp { x = 91, y = 380, color = "RED" } }, // 81
                new Lamp[] { new Lamp { x = 8, y = 304, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 24, y = 296, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 159, y = 296, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 173, y = 304, color = "YELLOW" } },
                new Lamp[] { new Lamp { x = 180, y = 390, color = "RED" } },
                new Lamp[] { new Lamp { x = 0, y = 0, color = "BLACK" } },
                new Lamp[] { new Lamp { x = 10, y = 390, color = "RED" } }
            },
            flashlamps = new Flashlamp[] {
                new Flashlamp { id = "17", x = 184, y = 40 },
                new Flashlamp { id = "18", x = 157, y = 79 },
                new Flashlamp { id = "19", x = 188, y = 188 },
                new Flashlamp { id = "20", x = 174, y = 251 },
                new Flashlamp { id = "21", x = 99, y = 160 },
                new Flashlamp { id = "22", x = 150, y = 63 },
                new Flashlamp { id = "23", x = 102, y = 93 },
                new Flashlamp { id = "25", x = 14, y = 28 },
                new Flashlamp { id = "26", x = 74, y = 21 },
                new Flashlamp { id = "27", x = 20, y = 120 },
                new Flashlamp { id = "28", x = 237, y = 153 }
            }
        };

        public bool skipWpcRomCheck => false;

        public string[] features => new string[]
        {
            "securityPic",
            "wpc95"
        };

        public string[] cabinetColors => new string[]
        {
            "#FBF853",
            "#E63228",
            "#CFED4E",
            "#4DA23C"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37"
                "31", "36", "37", "67"
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