using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WPCEmu.Example
{
    class Tracer
    {
        struct OutputSlice
        {
            public ushort pc;
            public byte i1;
            public byte i2;
            public byte i3;
            public byte i4;
            public byte i5;
            public byte cc;
            public byte a;
            public byte b;
            public ushort x;
            public ushort y;
            public ushort s;
            public ushort u;
        }

        const uint MAXSTEPS = 0xFFF000;
        const int MAX_LOOPS = 64;

        static int traceLoops = 0;
        static ushort[] lastPC = Enumerable.Repeat((ushort)0xFF, MAX_LOOPS).ToArray();

        static List<OutputSlice> outputSlice = new List<OutputSlice>();

        static void Trace(Emulator wpcSystem)
        {
            uint steps = 0;
            while (steps++ < MAXSTEPS)
            {
                wpcSystem.executeCycle(1, 1);
                var cpu = wpcSystem.cpuBoard.cpu;

                var pc = cpu.regPC;

                var i1 = cpu.memoryReadFunction(pc);
                var i2 = cpu.memoryReadFunction((ushort) (pc + 1));
                var i3 = cpu.memoryReadFunction((ushort) (pc + 2));
                var i4 = cpu.memoryReadFunction((ushort) (pc + 3));
                var i5 = cpu.memoryReadFunction((ushort) (pc + 4));

                outputSlice.Add(new OutputSlice
                {
                    pc = pc,
                    i1 = i1,
                    i2 = i2,
                    i3 = i3,
                    i4 = i4,
                    i5 = i5,
                    cc = cpu.regCC,
                    a = cpu.regA,
                    b = cpu.regB,
                    x = cpu.regX,
                    y = cpu.regY,
                    s = cpu.regS,
                    u = cpu.regU
                });

                //if (steps % (MAX_LOOPS * 100) == 0)
                {
                    flushTraces();
                    initTraceLoops();
                }
            }
        }

        static void initTraceLoops()
        {
            outputSlice.Clear();
        }

        static void flushTraces()
        {
            foreach (var line in outputSlice) {
                var pc = line.pc;
                var count = 0;
                /* check for trace_loops - ripped from mame */
                for (var i = 0; i < MAX_LOOPS; i++)
                {
                    if (lastPC[i] == pc)
                    {
                        count++;
                    }
                }

                if (count > 1)
                {
                    traceLoops++;
                }
                else
                {
                    if (traceLoops > 0)
                    {
                        Console.WriteLine("\n   (loops for " + traceLoops + " instructions)\n");
                        traceLoops = 0;
                    }
                    var instr = Disasm.disasm(line.i1, line.i2, line.i3, line.i4, line.i5, pc);
                    printInstruction(pc, instr, line);

                    for (var i = 1; i < MAX_LOOPS; i++)
                    {
                        lastPC[i - 1] = lastPC[i];
                    }
                    lastPC[MAX_LOOPS - 1] = pc;
                }
            }
        }

        static string formatRegister(ushort value, int padLength)
        {
            return value.ToString("X" + padLength) + " ";
        }

        static void printInstruction(ushort pc, Disasm.Instruction instr, OutputSlice line)
        {
            string CC = "CC=" + formatRegister(line.cc, 2);
            string A = "A=" + formatRegister(line.a, 4);
            string B = "B=" + formatRegister(line.b, 4);
            string X = "X=" + formatRegister(line.x, 4);
            string Y = "Y=" + formatRegister(line.y, 4);
            string S = "S=" + formatRegister(line.s, 4);
            string U = "U=" + formatRegister(line.u, 4);
            string REGS = CC + A + B + X + Y + S + U;

            if (instr._params.Length > 0)
            {
                Console.WriteLine(REGS + pc.ToString("X4") + ": " + instr.mnemo.PadRight(6) + instr._params);
                Debug.Print(REGS + pc.ToString("X4") + ": " + instr.mnemo.PadRight(6) + instr._params);
            }
            else
            {
                Console.WriteLine(REGS + pc.ToString("X4") + ": " + instr.mnemo);
                Debug.Print(REGS + pc.ToString("X4") + ": " + instr.mnemo);
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

            Trace(wpcSystem);
        }
    }
}
