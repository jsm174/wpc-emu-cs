using System;
using System.IO;
using WPCEmu.Boards.Elements;
using WPCEmu.Rom;

namespace WPCEmu.Db
{
    class Example
    {
        static string[] DOTS = new string[]
        {
            " ",
            "░",
            "▒",
            "▓",
            "▓"
        };

        static void Start(Emulator wpcSystem)
        {
            while (true)
            {
                wpcSystem.executeCycle(34482, 16);
                var state = wpcSystem.getState();

                var display = (OutputDmdDisplay.State)state.asic?.display;

                var dmd = "";

                for (var y = 0; y < 32; y++)
                {
                    for (var x = 0; x < 128; x++)
                    {
                        var pixel = y * 128 + x;
                        var value = display.dmdShadedBuffer[pixel];

                        dmd += DOTS[value];
                    }

                    dmd += "\n";
                }

                Console.SetCursorPosition(0, 0);
                Console.WriteLine(dmd);
                Console.WriteLine(state.cpuState.tickCount);
            }
        }

        static void Main(string[] args)
        {
            System.Diagnostics.Trace.Listeners.RemoveAt(0);

            var u06 = File.ReadAllBytes("/Users/jmillard/mm_109b.bin");

            var romObject = new RomBinary
            {
                u06 = u06
            };

            var metaData = new RomMetaData
            {
                skipWpcRomCheck = false,
                features = new string[]
                {
                    "securityPic",
                    "wpc95"
                },
            };

            var wpcSystem = Emulator.initVMwithRom(romObject, metaData);

            wpcSystem.reset();
            wpcSystem.start();

            Start(wpcSystem);
        }
    }
}
