using System;
using System.IO;
using WPCEmu.Boards.Elements;
using System.Threading.Tasks;
using System.Threading;

namespace WPCEmu.Example
{
    class Example
    {
        const int TICKS_PER_SECOND = 2000000;
        const int TICKS_PER_STEP = 16;
        const int INITIAL_FRAMERATE = 50;

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
            int ticksPerCall = TICKS_PER_SECOND / INITIAL_FRAMERATE;
            int intervalTimingMs = 1000 / INITIAL_FRAMERATE;

            Task.Run(() =>
            {
                new Timer(_ => wpcSystem.setCabinetInput(16), null, 1500, Timeout.Infinite);

                while (true)
                {
                    wpcSystem.executeCycle(ticksPerCall, TICKS_PER_STEP);

                    DumpDMD(wpcSystem);

                    Thread.Sleep(intervalTimingMs);
                }
            });

            while (true)
            {
                Thread.Sleep(intervalTimingMs);
            }
        }

        static void DumpDMD(Emulator wpcSystem)
        {
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

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    throw new ArgumentException("USAGE: Example <rom file>");
                }

                var u06 = File.ReadAllBytes(args[0]);

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

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}