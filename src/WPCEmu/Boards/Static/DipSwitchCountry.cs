namespace WPCEmu.Boards.Static
{
    public static class DipSwitchCountry
    {
        /*
        define w15 - w18
          FRENCH: 48 aka 0011 0000b
          GERMAN: 112 aka 0111 0000b
          EUROPE: 208 aka 1101 0000b
          USA: 240 aka 1111 0000b
        */

        public const byte FRENCH = 48;
        public const byte GERMAN = 112;
        public const byte EUROPE = 208;
        public const byte USA = 0;
        public const byte USA2 = 240;
    }
}