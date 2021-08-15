using System;
using System.Collections.Generic;
using System.Linq;

namespace WPCEmu.Tracer
{
    class Disasm
    {
        public struct Instruction
        {
            public string mnemo;
            public string _params;
            public byte bytes;
        }

        public struct DS
        {
            public byte bytes;
            public byte mode;
            public string mnemo;
        }

        /*
        ILLEGAL 0
        DIRECT 1
        INHERENT 2
        BRANCH_REL_16 3
        IMMEDIAT_8 4
        BRANCH_REL_8 5
        INDEXED 6
        EXTENDED 7
        IMMEDIAT_16 8
        PSHS 10
        PSHU 11
        EXG, TFR 20
        */

        static readonly DS[] ds = new DS[] {
            //0
            new DS { bytes = 2, mode = 1, mnemo = "NEG" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 1, mnemo = "COM" },
            new DS { bytes = 2, mode = 1, mnemo = "LSR" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 1, mnemo = "ROR" },
            new DS { bytes = 2, mode = 1, mnemo = "ASR" },
            new DS { bytes = 2, mode = 1, mnemo = "LSL" },
            new DS { bytes = 2, mode = 1, mnemo = "ROL" },
            new DS { bytes = 2, mode = 1, mnemo = "DEC" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 1, mnemo = "INC" },
            new DS { bytes = 2, mode = 1, mnemo = "TST" },
            new DS { bytes = 2, mode = 1, mnemo = "JMP" },
            new DS { bytes = 2, mode = 1, mnemo = "CLR" },
            new DS { bytes = 1, mode = 0, mnemo = "Prefix" },
            new DS { bytes = 1, mode = 0, mnemo = "Prefix" },
            new DS { bytes = 1, mode = 2, mnemo = "NOP" },
            new DS { bytes = 1, mode = 2, mnemo = "SYNC" },

            //20
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 3, mode = 3, mnemo = "LBRA" },
            new DS { bytes = 3, mode = 3, mnemo = "LBSR" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "DAA" },
            new DS { bytes = 2, mode = 4, mnemo = "ORCC" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 4, mnemo = "ANDCC" },
            new DS { bytes = 1, mode = 2, mnemo = "SEX" },
            new DS { bytes = 2, mode = 20, mnemo = "EXG" },
            new DS { bytes = 2, mode = 20, mnemo = "TFR" },
            new DS { bytes = 2, mode = 5, mnemo = "BRA" },
            new DS { bytes = 2, mode = 5, mnemo = "BRN" },
            new DS { bytes = 2, mode = 5, mnemo = "BHI" },
            new DS { bytes = 2, mode = 5, mnemo = "BLS" },
            new DS { bytes = 2, mode = 5, mnemo = "BCC" },
            new DS { bytes = 2, mode = 5, mnemo = "BCS" },
            new DS { bytes = 2, mode = 5, mnemo = "BNE" },
            new DS { bytes = 2, mode = 5, mnemo = "BEQ" },

            //40
            new DS { bytes = 2, mode = 5, mnemo = "BVC" },
            new DS { bytes = 2, mode = 5, mnemo = "BVS" },
            new DS { bytes = 2, mode = 5, mnemo = "BPL" },
            new DS { bytes = 2, mode = 5, mnemo = "BMI" },
            new DS { bytes = 2, mode = 5, mnemo = "BGE" },
            new DS { bytes = 2, mode = 5, mnemo = "BLT" },
            new DS { bytes = 2, mode = 5, mnemo = "BGT" },
            new DS { bytes = 2, mode = 5, mnemo = "BLE" },
            new DS { bytes = 2, mode = 6, mnemo = "LEAX" },
            new DS { bytes = 2, mode = 6, mnemo = "LEAY" },
            new DS { bytes = 2, mode = 6, mnemo = "LEAS" },
            new DS { bytes = 2, mode = 6, mnemo = "LEAU" },
            new DS { bytes = 2, mode = 10, mnemo = "PSHS" },
            new DS { bytes = 2, mode = 10, mnemo = "PULS" },
            new DS { bytes = 2, mode = 11, mnemo = "PSHU" },
            new DS { bytes = 2, mode = 11, mnemo = "PULU" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "RTS" },
            new DS { bytes = 1, mode = 2, mnemo = "ABX" },
            new DS { bytes = 1, mode = 2, mnemo = "RTI" },

            //60
            new DS { bytes = 2, mode = 2, mnemo = "CWAI" },
            new DS { bytes = 1, mode = 2, mnemo = "MUL" },
            new DS { bytes = 1, mode = 2, mnemo = "RESET" },
            new DS { bytes = 1, mode = 2, mnemo = "SWI1" },
            new DS { bytes = 1, mode = 2, mnemo = "NEGA" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "COMA" },
            new DS { bytes = 1, mode = 2, mnemo = "LSRA" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "RORA" },
            new DS { bytes = 1, mode = 2, mnemo = "ASRA" },
            new DS { bytes = 1, mode = 2, mnemo = "ASLA" },
            new DS { bytes = 1, mode = 2, mnemo = "ROLA" },
            new DS { bytes = 1, mode = 2, mnemo = "DECA" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "INCA" },
            new DS { bytes = 1, mode = 2, mnemo = "TSTA" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "CLRA" },

            //80
            new DS { bytes = 1, mode = 2, mnemo = "NEGB" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "COMB" },
            new DS { bytes = 1, mode = 2, mnemo = "LSRB" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "RORB" },
            new DS { bytes = 1, mode = 2, mnemo = "ASRB" },
            new DS { bytes = 1, mode = 2, mnemo = "ASLB" },
            new DS { bytes = 1, mode = 2, mnemo = "ROLB" },
            new DS { bytes = 1, mode = 2, mnemo = "DECB" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "INCB" },
            new DS { bytes = 1, mode = 2, mnemo = "TSTB" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 2, mnemo = "CLRB" },
            new DS { bytes = 2, mode = 6, mnemo = "NEG" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 6, mnemo = "COM" },

            //100
            new DS { bytes = 2, mode = 6, mnemo = "LSR" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 6, mnemo = "ROR" },
            new DS { bytes = 2, mode = 6, mnemo = "ASR" },
            new DS { bytes = 2, mode = 6, mnemo = "LSL" },
            new DS { bytes = 2, mode = 6, mnemo = "ROL" },
            new DS { bytes = 2, mode = 6, mnemo = "DEC" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 6, mnemo = "INC" },
            new DS { bytes = 2, mode = 6, mnemo = "TST" },
            new DS { bytes = 2, mode = 6, mnemo = "JMP" },
            new DS { bytes = 2, mode = 6, mnemo = "CLR" },
            new DS { bytes = 3, mode = 7, mnemo = "NEG" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 3, mode = 7, mnemo = "COM" },
            new DS { bytes = 3, mode = 7, mnemo = "LSR" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 3, mode = 7, mnemo = "ROR" },
            new DS { bytes = 3, mode = 7, mnemo = "ASR" },
            new DS { bytes = 3, mode = 7, mnemo = "LSL" },

            //120
            new DS { bytes = 3, mode = 7, mnemo = "ROL" },
            new DS { bytes = 3, mode = 7, mnemo = "DEC" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 3, mode = 7, mnemo = "INC" },
            new DS { bytes = 3, mode = 7, mnemo = "TST" },
            new DS { bytes = 3, mode = 7, mnemo = "JMP" },
            new DS { bytes = 3, mode = 7, mnemo = "CLR" },
            new DS { bytes = 2, mode = 4, mnemo = "SUBA" },
            new DS { bytes = 2, mode = 4, mnemo = "CMPA" },
            new DS { bytes = 2, mode = 4, mnemo = "SBCA" },
            new DS { bytes = 3, mode = 8, mnemo = "SUBD" },
            new DS { bytes = 2, mode = 4, mnemo = "ANDA" },
            new DS { bytes = 2, mode = 4, mnemo = "BITA" },
            new DS { bytes = 2, mode = 4, mnemo = "LDA" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 4, mnemo = "EORA" },
            new DS { bytes = 2, mode = 4, mnemo = "ADCA" },
            new DS { bytes = 2, mode = 4, mnemo = "ORA" },
            new DS { bytes = 2, mode = 4, mnemo = "ADDA" },
            new DS { bytes = 3, mode = 8, mnemo = "CMPX" },
            new DS { bytes = 2, mode = 5, mnemo = "BSR" },

            //140
            new DS { bytes = 3, mode = 8, mnemo = "LDX" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 1, mnemo = "SUBA" },
            new DS { bytes = 2, mode = 1, mnemo = "CMPA" },
            new DS { bytes = 2, mode = 1, mnemo = "SBCA" },
            new DS { bytes = 2, mode = 1, mnemo = "SUBd" },
            new DS { bytes = 2, mode = 1, mnemo = "ANDA" },
            new DS { bytes = 2, mode = 1, mnemo = "BITA" },
            new DS { bytes = 2, mode = 1, mnemo = "LDA" },
            new DS { bytes = 2, mode = 1, mnemo = "STA" },
            new DS { bytes = 2, mode = 1, mnemo = "EORA" },
            new DS { bytes = 2, mode = 1, mnemo = "ADCA" },
            new DS { bytes = 2, mode = 1, mnemo = "ORA" },
            new DS { bytes = 2, mode = 1, mnemo = "ADDA" },
            new DS { bytes = 2, mode = 1, mnemo = "CMPX" },
            new DS { bytes = 2, mode = 1, mnemo = "JSR" },
            new DS { bytes = 2, mode = 1, mnemo = "LDX" },
            new DS { bytes = 2, mode = 1, mnemo = "STX" },
            new DS { bytes = 2, mode = 6, mnemo = "SUBA" },
            new DS { bytes = 2, mode = 6, mnemo = "CMPA" },
            new DS { bytes = 2, mode = 6, mnemo = "SBCA" },

            //160
            new DS { bytes = 2, mode = 6, mnemo = "SUBD" },
            new DS { bytes = 2, mode = 6, mnemo = "ANDA" },
            new DS { bytes = 2, mode = 6, mnemo = "BITA" },
            new DS { bytes = 2, mode = 6, mnemo = "LDA" },
            new DS { bytes = 2, mode = 6, mnemo = "STA" },
            new DS { bytes = 2, mode = 6, mnemo = "EORA" },
            new DS { bytes = 2, mode = 6, mnemo = "ADCA" },
            new DS { bytes = 2, mode = 6, mnemo = "ORA" },
            new DS { bytes = 2, mode = 6, mnemo = "ADDA" },
            new DS { bytes = 2, mode = 6, mnemo = "CMPX" },
            new DS { bytes = 2, mode = 6, mnemo = "JSR" },
            new DS { bytes = 2, mode = 6, mnemo = "LDX" },
            new DS { bytes = 2, mode = 6, mnemo = "STX" },
            new DS { bytes = 3, mode = 7, mnemo = "SUBA" },
            new DS { bytes = 3, mode = 7, mnemo = "CMPA" },
            new DS { bytes = 3, mode = 7, mnemo = "SBCA" },
            new DS { bytes = 3, mode = 7, mnemo = "SUBD" },
            new DS { bytes = 3, mode = 7, mnemo = "ANDA" },
            new DS { bytes = 3, mode = 7, mnemo = "BITA" },
            new DS { bytes = 3, mode = 7, mnemo = "LDA" },
            new DS { bytes = 3, mode = 7, mnemo = "STA" },

            //180
            new DS { bytes = 3, mode = 7, mnemo = "EORA" },
            new DS { bytes = 3, mode = 7, mnemo = "ADCA" },
            new DS { bytes = 3, mode = 7, mnemo = "ORA" },
            new DS { bytes = 3, mode = 7, mnemo = "ADDA" },
            new DS { bytes = 3, mode = 7, mnemo = "CMPX" },
            new DS { bytes = 3, mode = 7, mnemo = "JSR" },
            new DS { bytes = 3, mode = 7, mnemo = "LDX" },
            new DS { bytes = 3, mode = 7, mnemo = "STX" },
            new DS { bytes = 2, mode = 4, mnemo = "SUBB" },
            new DS { bytes = 2, mode = 4, mnemo = "CMPB" },
            new DS { bytes = 2, mode = 4, mnemo = "SBCB" },
            new DS { bytes = 3, mode = 8, mnemo = "ADDD" },
            new DS { bytes = 2, mode = 4, mnemo = "ANDB" },
            new DS { bytes = 2, mode = 4, mnemo = "BITB" },
            new DS { bytes = 2, mode = 4, mnemo = "LDB" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 4, mnemo = "EORB" },
            new DS { bytes = 2, mode = 4, mnemo = "ADCB" },
            new DS { bytes = 2, mode = 4, mnemo = "ORB" },
            new DS { bytes = 2, mode = 4, mnemo = "ADDB" },
            new DS { bytes = 3, mode = 8, mnemo = "LDD" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 3, mode = 8, mnemo = "LDU" },
            new DS { bytes = 1, mode = 0, mnemo = "???" },
            new DS { bytes = 2, mode = 1, mnemo = "SUBB" },
            new DS { bytes = 2, mode = 1, mnemo = "CMPB" },
            new DS { bytes = 2, mode = 1, mnemo = "SBCB" },
            new DS { bytes = 2, mode = 1, mnemo = "ADDD" },
            new DS { bytes = 2, mode = 1, mnemo = "ANDB" },
            new DS { bytes = 2, mode = 1, mnemo = "BITB" },
            new DS { bytes = 2, mode = 1, mnemo = "LDB" },
            new DS { bytes = 2, mode = 1, mnemo = "STB" },
            new DS { bytes = 2, mode = 1, mnemo = "EORB" },
            new DS { bytes = 2, mode = 1, mnemo = "ADCB" },
            new DS { bytes = 2, mode = 1, mnemo = "ORB " },
            new DS { bytes = 2, mode = 1, mnemo = "ADDB" },
            new DS { bytes = 2, mode = 1, mnemo = "LDD " },
            new DS { bytes = 2, mode = 1, mnemo = "STD " },
            new DS { bytes = 2, mode = 1, mnemo = "LDU " },
            new DS { bytes = 2, mode = 1, mnemo = "STU " },
            new DS { bytes = 2, mode = 6, mnemo = "SUBB" },
            new DS { bytes = 2, mode = 6, mnemo = "CMPB" },
            new DS { bytes = 2, mode = 6, mnemo = "SBCB" },
            new DS { bytes = 2, mode = 6, mnemo = "ADDD" },
            new DS { bytes = 2, mode = 6, mnemo = "ANDB" },
            new DS { bytes = 2, mode = 6, mnemo = "BITB" },
            new DS { bytes = 2, mode = 6, mnemo = "LDB" },
            new DS { bytes = 2, mode = 6, mnemo = "STB" },
            new DS { bytes = 2, mode = 6, mnemo = "EORB" },
            new DS { bytes = 2, mode = 6, mnemo = "ADCB" },
            new DS { bytes = 2, mode = 6, mnemo = "ORB" },
            new DS { bytes = 2, mode = 6, mnemo = "ADDB" },
            new DS { bytes = 2, mode = 6, mnemo = "LDD" },
            new DS { bytes = 2, mode = 6, mnemo = "STD" },
            new DS { bytes = 2, mode = 6, mnemo = "LDU" },
            new DS { bytes = 2, mode = 6, mnemo = "STU" },
            new DS { bytes = 3, mode = 7, mnemo = "SUBB" },
            new DS { bytes = 3, mode = 7, mnemo = "CMPB" },
            new DS { bytes = 3, mode = 7, mnemo = "SBCB" },
            new DS { bytes = 3, mode = 7, mnemo = "ADDD" },
            new DS { bytes = 3, mode = 7, mnemo = "ANDB" },
            new DS { bytes = 3, mode = 7, mnemo = "BITB" },
            new DS { bytes = 3, mode = 7, mnemo = "LDB" },
            new DS { bytes = 3, mode = 7, mnemo = "STB" },
            new DS { bytes = 3, mode = 7, mnemo = "EORB" },
            new DS { bytes = 3, mode = 7, mnemo = "ADCB" },
            new DS { bytes = 3, mode = 7, mnemo = "ORB" },
            new DS { bytes = 3, mode = 7, mnemo = "ADDB" },
            new DS { bytes = 3, mode = 7, mnemo = "LDD" },
            new DS { bytes = 3, mode = 7, mnemo = "STD" },
            new DS { bytes = 3, mode = 7, mnemo = "LDU" },
            new DS { bytes = 3, mode = 7, mnemo = "STU" }
        };

        static readonly Dictionary<byte, DS> ds11 = new Dictionary<byte, DS>()
        {
            [0x3F] = new DS { bytes = 2, mode = 2, mnemo = "SWI3" },
            [0x83] = new DS { bytes = 4, mode = 8, mnemo = "CMPU" },
            [0x8C] = new DS { bytes = 4, mode = 8, mnemo = "CMPS" },
            [0x93] = new DS { bytes = 3, mode = 1, mnemo = "CMPU" },
            [0x9C] = new DS { bytes = 3, mode = 1, mnemo = "CMPS" },
            [0xA3] = new DS { bytes = 3, mode = 6, mnemo = "CMPU" },
            [0xAC] = new DS { bytes = 3, mode = 6, mnemo = "CMPS" },
            [0xB3] = new DS { bytes = 4, mode = 7, mnemo = "CMPU" },
            [0xBC] = new DS { bytes = 4, mode = 7, mnemo = "CMPS" },
        };

        // format: bytes, mode, mnemo
        // see http://techheap.packetizer.com/processors/6809/6809Instructions.html
        static readonly Dictionary<byte, DS> ds10 = new Dictionary<byte, DS>()
        {
            [0x21] = new DS { bytes = 4, mode = 3, mnemo = "LBRN" },
            [0x22] = new DS { bytes = 4, mode = 3, mnemo = "LBHI" },
            [0x23] = new DS { bytes = 4, mode = 3, mnemo = "LBLS" },
            [0x24] = new DS { bytes = 4, mode = 3, mnemo = "LBCC" },
            [0x25] = new DS { bytes = 4, mode = 3, mnemo = "LBCS" },
            [0x26] = new DS { bytes = 4, mode = 3, mnemo = "LBNE" },
            [0x27] = new DS { bytes = 4, mode = 3, mnemo = "LBEQ" },
            [0x28] = new DS { bytes = 4, mode = 3, mnemo = "LBVC" },
            [0x29] = new DS { bytes = 4, mode = 3, mnemo = "LBVS" },
            [0x2a] = new DS { bytes = 4, mode = 3, mnemo = "LBPL" },
            [0x2b] = new DS { bytes = 4, mode = 3, mnemo = "LBMI" },
            [0x2c] = new DS { bytes = 4, mode = 3, mnemo = "LBGE" },
            [0x2d] = new DS { bytes = 4, mode = 3, mnemo = "LBLT" },
            [0x2e] = new DS { bytes = 4, mode = 3, mnemo = "LBGT" },
            [0x2f] = new DS { bytes = 4, mode = 3, mnemo = "LBLE" },
            [0x3F] = new DS { bytes = 2, mode = 2, mnemo = "SWI2" },
            [0x83] = new DS { bytes = 4, mode = 8, mnemo = "CMPD" },
            [0x8C] = new DS { bytes = 4, mode = 8, mnemo = "CMPY" },
            [0x8E] = new DS { bytes = 4, mode = 8, mnemo = "LDY" },
            [0x93] = new DS { bytes = 3, mode = 1, mnemo = "CMPD" },
            [0x9C] = new DS { bytes = 3, mode = 1, mnemo = "CMPY" },
            [0x9E] = new DS { bytes = 3, mode = 1, mnemo = "LDY" },
            [0x9F] = new DS { bytes = 3, mode = 1, mnemo = "STY" },
            [0xA3] = new DS { bytes = 3, mode = 6, mnemo = "CMPD" },
            [0xAC] = new DS { bytes = 3, mode = 6, mnemo = "CMPY" },
            [0xAE] = new DS { bytes = 3, mode = 6, mnemo = "LDY" },
            [0xAF] = new DS { bytes = 3, mode = 6, mnemo = "STY" },
            [0xB3] = new DS { bytes = 4, mode = 7, mnemo = "CMPD" },
            [0xBC] = new DS { bytes = 4, mode = 7, mnemo = "CMPY" },
            [0xBE] = new DS { bytes = 4, mode = 7, mnemo = "LDY" },
            [0xBF] = new DS { bytes = 4, mode = 7, mnemo = "STY" },
            [0xCE] = new DS { bytes = 4, mode = 8, mnemo = "LDS" },
            [0xDE] = new DS { bytes = 3, mode = 1, mnemo = "LDS" },
            [0xDF] = new DS { bytes = 3, mode = 1, mnemo = "STS" },
            [0xEE] = new DS { bytes = 3, mode = 6, mnemo = "LDS" },
            [0xEF] = new DS { bytes = 3, mode = 6, mnemo = "STS" },
            [0xFE] = new DS { bytes = 4, mode = 7, mnemo = "LDS" },
            [0xFF] = new DS { bytes = 4, mode = 7, mnemo = "STS" },
        };

        /*
        ILLEGAL 0
        DIRECT 1
        INHERENT 2
        BRANCH_REL_16 3
        IMMEDIAT_8 4
        BRANCH_REL_8 5
        INDEXED 6
        EXTENDED 7
        IMMEDIAT_16 8
        */

        static string toHexN(ushort n, int d) {
          //var s = n.toString(16);
          //while (s.length<d) {
          //  s = "0" + s;
          //}
          string hexString = "$" + n.ToString("X" + d);
          return hexString;
        }

        static string toHex2(ushort n) {
            return toHexN((ushort) (n & 0xff), 2);
        }

        static string toHex4(ushort n) {
            return toHexN(n,4);
        }

        public static Instruction disasm(byte i, byte a, byte b, byte c, byte d, ushort pc)
        {
            string[] rx, ro;
            int j;
            var sx = ds[i];
            if (i == 0x10)
            {
                sx = ds10[a];
                //if (sx == undefined)
                //{
                //    return ["???", 2];
                //}
                i = a; a = b; b = c; c = d;
            }
            if (i == 0x11)
            {
                sx = ds11[a];
                //if (sx == undefined)
                //{
                //    return ["???", 2];
                //}
                i = a; a = b; b = c; c = d;
            }
            var bytes = sx.bytes;
            var mode = sx.mode;
            var mnemo = sx.mnemo;
            var _params = "";

            switch (mode)
            {
                case 0: //invalid
                    break;
                case 1: //direct page
                    _params = toHex2(a);
                    break;
                case 2: // inherent
                    break;
                case 3: //brel16
                    _params = toHex4((ushort) ((a * 256 + b) < 32768 ?
                                        // "+bytes": update current OP size too!
                                        (a * 256 + b + pc + bytes) :
                                        (a * 256 + b + pc + bytes - 65536)));
                    break;
                case 4: //imm8
                    _params = "#" + toHex2(a);
                    break;
                case 5: //brel8
                    _params = toHex4((ushort) ((a) < 128 ? (a + pc + 2) : (a + pc - 254)));
                    break;
                case 6: //indexed, postbyte etc.
                    var pb = a;
                    var ixr = new string[] { "X", "Y", "U", "S" }[(pb & 0x60) >> 5];
                    if ((pb & 0x80) == 0)
                    {
                        //direct5
                        var disp = (sbyte) (pb & 0x1f);
                        if (disp > 15) disp = (sbyte)(disp - 32);
                        if (disp >= 0)
                        {
                            _params = toHexN((ushort)disp, 1) + ',' + ixr;
                        }
                        else
                        {
                            // make sure we display negative hex values as mame
                            _params = "-$" + Math.Abs(disp) + "," + ixr;
                        }
                        break;
                    }
                    var ind = (byte) (pb & 0x10);
                    var mod = (byte) (pb & 0x0f);
                    var ofs8 = (b > 127) ? (b - 256) : b;
                    var ofs16 = ((b * 256 + c) > 32767) ? ((b * 256 + c) - 65536) : (b * 256 + c);
                    if (ind == 0)
                    {
                        switch (mod)
                        {
                            case 0: _params = "," + ixr + "+"; break;
                            case 1: _params = "," + ixr + "++"; break;
                            case 2: _params = ",-" + ixr; break;
                            case 3: _params = ",--" + ixr; break;
                            case 4: _params = "," + ixr; break;
                            case 5: _params = "B," + ixr; break;
                            case 6: _params = "A," + ixr; break;
                            case 7: _params = "???"; break;
                            case 8:
                                _params = toHex2((ushort)ofs8) + "," + ixr;
                                bytes++;
                                break;
                            case 9:
                                _params = toHex4((ushort)ofs16) + "," + ixr;
                                bytes += 2;
                                break;
                            case 10: _params = "???"; break;
                            case 11: _params = "D," + ixr; break;
                            case 12: _params = ofs8 + ",PC"; bytes++; break;
                            case 13: _params = ofs16 + ",PC"; bytes += 2; break;
                            case 14: _params = "???"; break;
                            case 15:
                                _params = toHex4((ushort)(b * 256 + c));
                                bytes += 2;
                                break;
                        }
                    }
                    else
                    {
                        switch (mod)
                        {
                            case 0: _params = "???"; break;
                            case 1: _params = "[," + ixr + "++]"; break;
                            case 2: _params = "???"; break;
                            case 3: _params = "[,--" + ixr + "]"; break;
                            case 4: _params = "[," + ixr + "]"; break;
                            case 5: _params = "[B," + ixr + "]"; break;
                            case 6: _params = "[A," + ixr + "]"; break;
                            case 7: _params = "???"; break;
                            case 8:
                                _params = "[" + toHex2((ushort)ofs8) + "," + ixr + "]";
                                bytes++;
                                break;
                            case 9:
                                _params = "[" + toHex4((ushort)ofs16) + "," + ixr + "]";
                                bytes += 2;
                                break;
                            case 10: _params = "???"; break;
                            case 11: _params = "[D," + ixr + "]"; break;
                            case 12: _params = "[" + ofs8 + ",PC]"; bytes++; break;
                            case 13: _params = "[" + ofs16 + ",PC]"; bytes += 2; break;
                            case 14: _params = "???"; break;
                            case 15: _params = "[" + toHex4((ushort)(b * 256 + c)) + "]"; bytes += 2; break;
                        }
                    }

                    break;
                case 7: //extended
                    _params = toHex4((ushort)(a * 256 + b));
                    break;
                case 8: //imm16
                    _params = "#" + toHex4((ushort)(a * 256 + b));
                    break;

                case 10: //pshs, puls
                    rx = new string[] { "PC", "U", "Y", "X", "DP", "B", "A", "CC" };
                    ro = new string[] { };
                    for (j = 0; j < 8; j++)
                    {
                        if ((a & 1) != 0) { ro = ro.Concat(new[] { rx[7 - j] }).ToArray(); }
                        a >>= 1;
                    }
                    _params = string.Join(",", ro);
                    break;
                case 11: //pshs, puls
                    rx = new string[] { "PC", "S", "Y", "X", "DP", "B", "A", "CC" };
                    ro = new string[] { };
                    for (j = 0; j < 8; j++)
                    {
                        if ((a & 1) != 0) { ro = ro.Concat(new[] { rx[7 - j] }).ToArray(); }
                        a >>= 1;
                    }
                    _params = string.Join(",", ro);
                    break;
                case 20: //TFR etc
                    rx = new string[] { "D", "X", "Y", "U", "S", "PC", "?", "?", "A", "B", "CC", "DP", "?", "?", "?", "?" };
                    _params = rx[a >> 4] + "," + rx[a & 0x0f];
                    break;
            }

            return new Instruction
            {
                mnemo = mnemo,
                _params = _params,
                bytes = bytes,
            };
        }
    }
}
