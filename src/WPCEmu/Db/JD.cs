namespace WPCEmu.Db
{
    public class JudgeDredd : IDb
    {
        public string name => "WPC-DCS: Judge Dredd";
        public string version => "L-7";

        public Pinmame? pinmame => new Pinmame
        { 
            knownNames = new string[] { "jd_l1", "jd_d1", "jd_l4", "jd_d4", "jd_l5", "jd_d5", "jd_l6", "jd_d6", "jd_l7", "jd_d7" },
            gameName = "Judge Dredd",
            id = "jd"
        };

        public RomFile? rom => new RomFile
        {
            u06 = "jdrd_l7.rom"
        };

        public SwitchMapping[] switchMapping => new SwitchMapping[]
        {
            new SwitchMapping { id = "11", name = "L FIRE BUTTON" },
            new SwitchMapping { id = "12", name = "R FIRE BUTTON" },
            new SwitchMapping { id = "13", name = "START BUTTON" },
            new SwitchMapping { id = "14", name = "PLUMB BOB TILT" },
            new SwitchMapping { id = "15", name = "LFT SHOOT LANE" },
            new SwitchMapping { id = "16", name = "LEFT OUTLANE" },
            new SwitchMapping { id = "17", name = "LFT RET LANE" },
            new SwitchMapping { id = "18", name = "3 BANK TGTS" },

            new SwitchMapping { id = "21", name = "SLAM TILT" },
            new SwitchMapping { id = "22", name = "COIN DOOR CLOSED" },
            new SwitchMapping { id = "23", name = "TICKET OPTQ" },
            new SwitchMapping { id = "25", name = "TOP R POST" },
            new SwitchMapping { id = "26", name = "CAP BALL 1" },
            new SwitchMapping { id = "27", name = "LOW LFT TARGET" },

            new SwitchMapping { id = "31", name = "BUY IN" },
            new SwitchMapping { id = "33", name = "TOP CNTR RO" },
            new SwitchMapping { id = "34", name = "INSIDE R RET" },
            new SwitchMapping { id = "35", name = "SMALL LOOP CNTR" },
            new SwitchMapping { id = "36", name = "LEFT SCR POST" },
            new SwitchMapping { id = "37", name = "SUBWAY ENTER" },
            new SwitchMapping { id = "38", name = "SUBWAY 2" },

            new SwitchMapping { id = "41", name = "R BALL SHOOTER" },
            new SwitchMapping { id = "42", name = "RIGHT OUTLANE" },
            new SwitchMapping { id = "43", name = "OUTSIDE R RET" },
            new SwitchMapping { id = "44", name = "SUPER GAME" },

            new SwitchMapping { id = "51", name = "LEFT SLING" },
            new SwitchMapping { id = "52", name = "RIGHT SLING" },
            new SwitchMapping { id = "53", name = "CAP BALL 2" },
            new SwitchMapping { id = "54", name = "<J>UDGE" },
            new SwitchMapping { id = "55", name = "J<U>DGE" },
            new SwitchMapping { id = "56", name = "JU<D>GE" },
            new SwitchMapping { id = "57", name = "JUD<G>E" },
            new SwitchMapping { id = "58", name = "JUDG<E>" },

            new SwitchMapping { id = "61", name = "GLOBE POS 1" },
            new SwitchMapping { id = "62", name = "GLOBE EXIT" },
            new SwitchMapping { id = "63", name = "LFT RMP TO LOCK" },
            new SwitchMapping { id = "64", name = "LFT RMP EXIT" },
            new SwitchMapping { id = "66", name = "CNTR RMP EXIT" },
            new SwitchMapping { id = "67", name = "LEFT RMP ENTER" },
            new SwitchMapping { id = "68", name = "CAP BALL 3" },

            new SwitchMapping { id = "71", name = "ARM FAR RIGHT" },
            new SwitchMapping { id = "72", name = "TOP RGHT OPTO" },
            new SwitchMapping { id = "73", name = "LEFT POPPER" },
            new SwitchMapping { id = "74", name = "RIGHT POPPER" },
            new SwitchMapping { id = "75", name = "TOP R RMP EXIT" },
            new SwitchMapping { id = "76", name = "RIGHT RAMP EXIT" },
            new SwitchMapping { id = "77", name = "GLOBE POS 2" },

            new SwitchMapping { id = "81", name = "TROUGH 6" },
            new SwitchMapping { id = "82", name = "TROUGH 5" },
            new SwitchMapping { id = "83", name = "TROUGH 4" },
            new SwitchMapping { id = "84", name = "TROUGH 3" },
            new SwitchMapping { id = "85", name = "TROUGH 2" },
            new SwitchMapping { id = "86", name = "TROUGH 1" },
            new SwitchMapping { id = "87", name = "TOP TROUGH" }
        };

        public FliptronicsMapping[] fliptronicsMappings => new FliptronicsMapping[]
        {
            new FliptronicsMapping { id = "F1", name = "R FLIPPER EOS" },
            new FliptronicsMapping { id = "F2", name = "R FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F3", name = "L FLIPPER EOS" },
            new FliptronicsMapping { id = "F4", name = "L FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F6", name = "UR FLIPPER BUTTON" },
            new FliptronicsMapping { id = "F7", name = "UL FLIPPER EOS" },
            new FliptronicsMapping { id = "F8", name = "UL FLIPPER BUTTON" }
        };

        public SolenoidMapping[] solenoidMapping => null;

        public Playfield? playfield => new Playfield
        {
            //size must be 200x400, lamp positions according to image
            image = "playfield-jd.jpg"
        };

        public bool skipWpcRomCheck => true;

        public string[] features => new string[]
        {
            "wpcDcs"
        };

        public string[] cabinetColors => new string[]
        {
            "#4F7DB4",
            "#D9CF7B",
            "#4EA575",
            "#CE432D",
            "#C77036"
        };

        public Initialise? initialise => new Initialise
        {
            closedSwitches = new string[]
            {
                //OPTO SWITCHES: "54", "55", "56", "57", "58", "61", "62", "63", "64", "66", "67", "71", "72", "73", "74", "75", "76", "77", "81", "82", "83", "84", "85", "86", "87"
                "22",
                "54", "55", "56", "57", "58", "61", "62", "63", "64", "66", "67", "71", "72", "73", "74", "75", "76", "87"
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