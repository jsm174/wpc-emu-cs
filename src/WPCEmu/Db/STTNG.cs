namespace WPCEmu.Db
{
    public class StarTrekTheNextGeneration : IDb
    {
        public string name => "WPC-DCS: Star Trek, The Next Generation";
        public string version => "LX-7";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "sttng_p4", "sttng_p5", "sttng_p6", "sttng_p8", "sttng_l1", "sttng_d1", "sttng_l2", "sttng_d2", "sttng_l3", "sttng_l7", "sttng_d7", "sttng_l7c", "sttng_x7", "sttng_dx", "sttng_s7", "sttng_ds", "sttng_g7", "sttng_h7" },
            gameName = "Star Trek: The Next Generation",
            id = "sttng"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "TREK_LX7.ROM"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "BUY IN BUTTON" },
            new SwitchMapping { id = "12", name = "CONTROL GRIP" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LEFT OUT LANE" },
            new SwitchMapping { id = "16", name = "LEFT RET LANE" },
            new SwitchMapping { id = "17", name = "RIGHT RET LANE" },
            new SwitchMapping { id = "18", name = "RIGHT OUT LANE" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "MADE MID RAMP" },
            new SwitchMapping { id = "25", name = "ENTER RIGHT RAMP" },
            new SwitchMapping { id = "26", name = "LEFT 45 TARGET" },
            new SwitchMapping { id = "27", name = "CENTER 45 TARGET" },
            new SwitchMapping { id = "28", name = "RIGHT 45 TARGET" },

            new SwitchMapping { id = "31", name = "BORG LOCK" },
            new SwitchMapping { id = "32", name = "UNDER LGUN SW2" },
            new SwitchMapping { id = "33", name = "UNDER RGUN SW2" },
            new SwitchMapping { id = "34", name = "RGHT GUN SHOOTER" },
            new SwitchMapping { id = "35", name = "UNDER LLOCK SW2" },
            new SwitchMapping { id = "36", name = "UNDER LGUN SW1" },
            new SwitchMapping { id = "37", name = "UNDER RGUN SW1" },
            new SwitchMapping { id = "38", name = "LEFT GUN SHOOTER" },

            new SwitchMapping { id = "41", name = "UNDER LLOCK SW1" },
            new SwitchMapping { id = "42", name = "UNDER LLOCK SW3" },
            new SwitchMapping { id = "43", name = "UNDER LLOCK SW4" },
            new SwitchMapping { id = "44", name = "LEFT OUTER LOOP" },
            new SwitchMapping { id = "45", name = "UNDER TOP HOLE" },
            new SwitchMapping { id = "46", name = "UNDER LEFT HOLE" },
            new SwitchMapping { id = "47", name = "UNDER BORG HOLE" },
            new SwitchMapping { id = "48", name = "BORG ENTRY" },

            new SwitchMapping { id = "51", name = "LBANK TOP" },
            new SwitchMapping { id = "52", name = "LBANK MIDDLE" },
            new SwitchMapping { id = "53", name = "LBANK BOTTOM" },
            new SwitchMapping { id = "54", name = "RBANK TOP" },
            new SwitchMapping { id = "55", name = "RBANK MIDDLE" },
            new SwitchMapping { id = "56", name = "RBANK BOTTOM" },
            new SwitchMapping { id = "57", name = "TOP DROP TARGET" },
            new SwitchMapping { id = "58", name = "RIGHT OUTER LOOP" },

            new SwitchMapping { id = "61", name = "TROUGH RL 6" },
            new SwitchMapping { id = "62", name = "TROUGH RL 5" },
            new SwitchMapping { id = "63", name = "TROUGH RL 4" },
            new SwitchMapping { id = "64", name = "TROUGH RL 3" },
            new SwitchMapping { id = "65", name = "TROUGH RL 2" },
            new SwitchMapping { id = "66", name = "TROUGH RL 1" },
            new SwitchMapping { id = "67", name = "TROUGH UP" },
            new SwitchMapping { id = "68", name = "SHOOTER" },

            new SwitchMapping { id = "71", name = "LEFT JET" },
            new SwitchMapping { id = "72", name = "RIGHT JET" },
            new SwitchMapping { id = "73", name = "BOTTOM JET" },
            new SwitchMapping { id = "74", name = "RIGHT SLING" },
            new SwitchMapping { id = "75", name = "LEFT SLING" },
            new SwitchMapping { id = "76", name = "TOP LANE LEFT" },
            new SwitchMapping { id = "77", name = "TOP LANE CENTER" },
            new SwitchMapping { id = "78", name = "TOP LANE RIGHT" },

            new SwitchMapping { id = "81", name = "TIME" },
            new SwitchMapping { id = "82", name = "RIFT" },
            new SwitchMapping { id = "83", name = "MADE LEFT RAMP" },
            new SwitchMapping { id = "84", name = "Q" },
            new SwitchMapping { id = "85", name = "LEFT 2X SHUTTLE" },
            new SwitchMapping { id = "86", name = "RIGHT 2X SHUTTLE" },
            new SwitchMapping { id = "87", name = "MADE RIGHT RAMP" },
            new SwitchMapping { id = "88", name = "ENTER LEFT RAMP" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F5", name = "UR FLIPPER EOS" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "SPINNER" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-sttng.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDcs"
        };

        public string[] cabinetColors => new string[]
        {
            "#C32F1E",
            "#3C85CF",
            "#F0DD5F",
            "#C95170",
            "#AA311E"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                "22",
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "38", "41", "42", "43", "44", "45", "46", "47", "48"
                //               "61", "62", "63", "64", "65", "66", "67"
                "31", "32", "33", "35", "36", "37", "41", "42", "43", "44", "45", "46", "47", "48", "67"
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