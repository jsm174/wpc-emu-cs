namespace WPCEmu.Db
{
    public class PopeyeSavesTheEarth : IDb
    {
        public string name => "WPC-DCS: Popeye Saves the Earth";
        public string version => "LX-5";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "pop_pa3", "pop_pa4", "pop_la4", "pop_lx4", "pop_lx5", "pop_dx5" },
            gameName = "Popeye Saves the Earth",
            id = "pop",
            vpdbId = "popeye"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "peye_lx5.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "LEFT LANE" },
            new SwitchMapping { id = "12", name = "BUY IN" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "RIGHT LANE" },
            new SwitchMapping { id = "16", name = "LEFT JET" },
            new SwitchMapping { id = "17", name = "RIGHT JET" },
            new SwitchMapping { id = "18", name = "CENTER JET" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "BALL LAUNCH" },
            new SwitchMapping { id = "25", name = "LEFT LOOP" },
            new SwitchMapping { id = "26", name = "POPEYE \"E1\"" },
            new SwitchMapping { id = "27", name = "POPEYE \"Y\"" },
            new SwitchMapping { id = "28", name = "POPEYE \"E2\"" },

            new SwitchMapping { id = "31", name = "LEFT POPPER" },
            new SwitchMapping { id = "32", name = "RIGHT POPPER" },
            new SwitchMapping { id = "33", name = "RIGHT LOOP OPTO" },
            new SwitchMapping { id = "34", name = "RAMP ENTRANCE" },
            new SwitchMapping { id = "35", name = "RAMP COMPLETION" },
            new SwitchMapping { id = "36", name = "ESCALATOR POPPER" },
            new SwitchMapping { id = "37", name = "WHEEL EXIT" },
            new SwitchMapping { id = "38", name = "HAG STANDUP" },

            new SwitchMapping { id = "41", name = "TWO BANK" },
            new SwitchMapping { id = "42", name = "CENTER LANE" },
            new SwitchMapping { id = "43", name = "LOCKUP UPPER" },
            new SwitchMapping { id = "44", name = "LOCKUP CENTER" },
            new SwitchMapping { id = "45", name = "LOCKUP LOWER" },
            new SwitchMapping { id = "46", name = "WHEEL OPTO 1" },
            new SwitchMapping { id = "47", name = "WHEEL OPTO 2" },
            new SwitchMapping { id = "48", name = "WHEEL OPTO 3" },

            new SwitchMapping { id = "51", name = "RIGHT TROUGH" },
            new SwitchMapping { id = "52", name = "TROUGH 2ND" },
            new SwitchMapping { id = "53", name = "TROUGH 3RD" },
            new SwitchMapping { id = "54", name = "TROUGH 4TH" },
            new SwitchMapping { id = "55", name = "TROUGH 5TH" },
            new SwitchMapping { id = "56", name = "LEFT TROUGH" },
            new SwitchMapping { id = "57", name = "TROUGH JAM" },
            new SwitchMapping { id = "58", name = "\"SEA\" STANDUP" },

            new SwitchMapping { id = "61", name = "LEFT CHEEK" },
            new SwitchMapping { id = "62", name = "RIGHT CHEEK" },
            new SwitchMapping { id = "63", name = "ESCALATOR EXIT" },
            new SwitchMapping { id = "64", name = "ANIMAL DOLPHIN" },
            new SwitchMapping { id = "65", name = "ANIMAL EAGLE" },
            new SwitchMapping { id = "66", name = "ANIMAL LEOPARD" },
            new SwitchMapping { id = "67", name = "ANIMAL PANDA" },
            new SwitchMapping { id = "68", name = "ANIMAL RHINO" },

            new SwitchMapping { id = "71", name = "POPEYE \"P1\"" },
            new SwitchMapping { id = "72", name = "POPEYE \"O\"" },
            new SwitchMapping { id = "73", name = "POPEYE \"P2\"" },
            new SwitchMapping { id = "74", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "75", name = "LEFT FLIP LANE" },
            new SwitchMapping { id = "76", name = "LEFT SLINGSHOT" },
            new SwitchMapping { id = "77", name = "RIGHT SLINGSHOT" },
            new SwitchMapping { id = "78", name = "RIGHT FLIP LANE" },

            new SwitchMapping { id = "81", name = "UP EXIT TO WHEEL" },
            new SwitchMapping { id = "82", name = "UPPER RAMP LEFT" },
            new SwitchMapping { id = "83", name = "UPPER RAMP RIGHT" },
            new SwitchMapping { id = "84", name = "ANIMAL JACKPOT" },
            new SwitchMapping { id = "85", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "86", name = "SHOOTER LANE" },
            new SwitchMapping { id = "87", name = "LOCKUP KICKER" },
            new SwitchMapping { id = "88", name = "UPPER SHOT EXIT" }
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
            image = "playfield-pste.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDcs"
        };

        public string[] cabinetColors => new string[]
        {
            "#3582D3",
            "#EB612D",
            "#E93B26",
            "#EDBD71",
            "#E63A25",
            "#FADE4C",
            "#4CA75F"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                //OPTO SWITCHES: "31", "32", "33", "34", "35", "36", "37", "46", "47", "48", "51", "52", "53", "54", "55", "56", "57", "81", "82", "83"
                "22",
                "31", "32", "33", "34", "35", "36", "37", "46", "47", "48", "57", "81", "82", "83"

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