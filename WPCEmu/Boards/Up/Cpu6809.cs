/*
  seems to be based on 6809.c by Larry Bank

  TODO, see https://groups.google.com/forum/#!msg/comp.sys.m6809/ct2V1nGIy2c/4xfP-qI91TIJ
  1) NMIs were not being masked before the 1st LDS.
  Fixed -> NMIs are now ignored until the system stack pointer
  is initialized
  "the NMI is not recognized until the first program load of the Hardware Stack Pointer (S)."

  2) CWAI pushed state twice, once when the CWAI first executed
  and again when the interrupt occured (DOH!).
  Fixed -> The second state push has been removed.

  3) SYNC required an enabled interrupt before exiting the sync state.
  Fixed -> It now exits the sync state on the occurance of NMI,
  IRQ, or FIRQ interrupts regardless of whether IRQ or FIRQ are
  masked.  If the interrupt duration is < 3 clocks or if the interrupt
  is masked then continue executing from the next instruction
  else take the interrupt vector.

*/

/*
The MIT License (MIT)

Copyright (c) 2014 Martin Maly, http://retrocip.cz, http://www.uelectronics.info,
twitter: @uelectronics

Copyright (c) 2018 Michael Vogt
twitter: @neophob

Copyright (c) 2020 Jason Millard
twitter: @jsm174

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Diagnostics;

namespace WPCEmu.Boards.Up
{
    public class Cpu6809
    {
        const byte F_CARRY = 1;
        const byte F_OVERFLOW = 2;
        const byte F_ZERO = 4;
        const byte F_NEGATIVE = 8;
        const byte F_IRQMASK = 16;
        const byte F_HALFCARRY = 32;
        const byte F_FIRQMASK = 64;
        const byte F_ENTIRE = 128;

        const ushort vecRESET = 0xFFFE;
        const ushort vecFIRQ = 0xFFF6;
        const ushort vecIRQ = 0xFFF8;
        const ushort vecNMI = 0xFFFC;
        const ushort vecSWI = 0xFFFA;
        const ushort vecSWI2 = 0xFFF4;
        const ushort vecSWI3 = 0xFFF2;

        readonly byte[] cycles = {
          6, 0, 0, 6, 6, 0, 6, 6, 6, 6, 6, 0, 6, 6, 3, 6, /* 00-0F */
          1, 1, 2, 2, 0, 0, 5, 9, 0, 2, 3, 0, 3, 2, 8, 7, /* 10-1F */
          3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, /* 20-2F */
          4, 4, 4, 4, 5, 5, 5, 5, 0, 5, 3, 6, 21, 11, 0, 19,/* 30-3F */
          2, 0, 0, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 0, 2, /* 40-4F */
          2, 0, 0, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 0, 2, /* 50-5F */
          6, 0, 0, 6, 6, 0, 6, 6, 6, 6, 6, 0, 6, 6, 3, 6, /* 60-6F */
          7, 0, 0, 7, 7, 0, 7, 7, 7, 7, 7, 0, 7, 7, 4, 7, /* 70-7F */
          2, 2, 2, 4, 2, 2, 2, 0, 2, 2, 2, 2, 4, 7, 3, 0, /* 80-8F */
          4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 6, 7, 5, 5, /* 90-9F */
          4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 6, 7, 5, 5, /* A0-AF */
          5, 5, 5, 7, 5, 5, 5, 5, 5, 5, 5, 5, 7, 8, 6, 6, /* B0-BF */
          2, 2, 2, 4, 2, 2, 2, 0, 2, 2, 2, 2, 3, 0, 3, 0, /* C0-CF */
          4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, /* D0-DF */
          4, 4, 4, 6, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, /* E0-EF */
          5, 5, 5, 7, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6  /* F0-FF */
        };

        /* Instruction timing for the two-byte opcodes */
        readonly byte[] cycles2 = {
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 00-0F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 10-1F */
          0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, /* 20-2F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, /* 30-3F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 40-4F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 50-5F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 60-6F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 70-7F */
          0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 4, 0, /* 80-8F */
          0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 6, 6, /* 90-9F */
          0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 6, 6, /* A0-AF */
          0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 7, 7, /* B0-BF */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, /* C0-CF */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6, /* D0-DF */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6, /* E0-EF */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7  /* F0-FF */
        };

        /* Negative and zero flags for quicker flag settings */
        readonly byte[] flagsNZ = {
          4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 00-0F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 10-1F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 20-2F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 30-3F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 40-4F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 50-5F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 60-6F */
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /* 70-7F */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, /* 80-8F */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, /* 90-9F */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, /* A0-AF */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, /* B0-BF */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, /* C0-CF */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, /* D0-DF */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, /* E0-EF */
          8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8  /* F0-FF */
        };

        Action<ushort, byte> memoryWriteFunction;
        Func<ushort, byte> memoryReadFunction;
        public Func<byte> fetch;

        public int tickCount;

        bool irqPending;
        bool firqPending;
        public int missedIRQ;
        public int missedFIRQ;

        public int irqCount;
        public int firqCount;
        int nmiCount;

        public byte regA;
        public byte regB;
        public ushort regX;
        public ushort regY;
        public ushort regU;
        public ushort regS;
        public byte regCC;
        public ushort regPC;
        public byte regDP;

        public struct State
        {
            public ushort regPC;
            public ushort regS;
            public ushort regU;
            public byte regA;
            public byte regB;
            public ushort regX;
            public ushort regY;
            public byte regDP;
            public byte regCC;
            public int missedIRQ;
            public int missedFIRQ;
            public int irqCount;
            public int firqCount;
            public int nmiCount;
            public int tickCount;
        }

        public static Cpu6809 GetInstance(Action<ushort, byte> memoryWriteFunction, Func<ushort, byte> memoryReadFunction)
        {
            return new Cpu6809(memoryWriteFunction, memoryReadFunction);
        }

        public Cpu6809(Action<ushort, byte> memoryWriteFunction, Func<ushort, byte> memoryReadFunction)
        {
            Debug.Print("INITIALIZE CPU");

            this.memoryWriteFunction = memoryWriteFunction;
            this.memoryReadFunction = memoryReadFunction;
            fetch = FetchFunction;

            tickCount = 0;

            irqPending = false;
            firqPending = false;
            missedIRQ = 0;
            missedFIRQ = 0;

            irqCount = 0;
            firqCount = 0;
            nmiCount = 0;

            regA = 0;
            regB = 0;
            regX = 0;
            regY = 0;
            regU = 0;
            regS = 0;
            regCC = 0;
            regPC = 0;
            regDP = 0;
        }

        // set overflow flag
        public void setV8(ushort a, ushort b, ushort r)
        {
            regCC |= (byte) (((a ^ b ^ r ^ (r >> 1)) & 0x80) >> 6);
        }

        // set overflow flag
        public void setV16(ulong a, ulong b, ulong r)
        {
            regCC |= (byte) (((a ^ b ^ r ^ (r >> 1)) & 0x8000) >> 14);
        }

        public ushort getD()
        {
            return (ushort)((regA << 8) + regB);
        }

        public void setD(ushort v)
        {
            regA = (byte)((v >> 8) & 0xff);
            regB = (byte)(v & 0xff);
        }

        // push byte to S stack
        public void PUSHB(byte b)
        {
            regS = (ushort)((regS - 1) & 0xFFFF);
            memoryWriteFunction(regS, (byte) (b & 0xFF));
        }

        // push word to S stack
        public void PUSHW(ushort b)
        {
            regS = (ushort)((regS - 1) & 0xFFFF);
            memoryWriteFunction(regS, (byte) (b & 0xFF));
            regS = (ushort)((regS - 1) & 0xFFFF);
            memoryWriteFunction(regS, (byte) ((b >> 8) & 0xFF));
        }

        // push byte to U stack
        public void PUSHBU(byte b)
        {
            regU = (ushort)((regU - 1) & 0xFFFF);
            memoryWriteFunction(regU, (byte) (b & 0xFF));
        }

        // push word to U stack
        public void PUSHWU(ushort b)
        {
            regU = (ushort)((regU - 1) & 0xFFFF);
            memoryWriteFunction(regU, (byte) (b & 0xFF));
            regU = (ushort)((regU - 1) & 0xFFFF);
            memoryWriteFunction(regU, (byte) ((b >> 8) & 0xFF));
        }

        // pull byte from S stack
        byte PULLB()
        {
            return memoryReadFunction(regS++);
        }

        // pull word from S stack
        ushort PULLW()
        {
            return (ushort) ((memoryReadFunction(regS++) << 8) + memoryReadFunction(regS++));
        }

        // pull byte from U stack
        byte PULLBU()
        {
            return memoryReadFunction(regU++);
        }

        // pull word from U stack
        ushort PULLWU()
        {
            return (ushort) ((memoryReadFunction(regU++) << 8) + memoryReadFunction(regU++));
        }

        //Push A, B, CC, DP, D, X, Y, U, or PC onto hardware stack
        void PSHS(byte ucTemp)
        {
            int i = 0;
            if ((ucTemp & 0x80) != 0)
            {
                PUSHW(regPC);
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                PUSHW(regU);
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                PUSHW(regY);
                i += 2;
            }
            if ((ucTemp & 0x10) != 0)
            {
                PUSHW(regX);
                i += 2;
            }
            if ((ucTemp & 0x8) != 0)
            {
                PUSHB(regDP);
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                PUSHB(regB);
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                PUSHB(regA);
                i++;
            }
            if ((ucTemp & 0x1) != 0)
            {
                PUSHB(regCC);
                i++;
            }
            tickCount += i; //timing
        }

        //Push A, B, CC, DP, D, X, Y, S, or PC onto user stack
        void PSHU(byte ucTemp)
        {
            int i = 0;
            if ((ucTemp & 0x80) != 0)
            {
                PUSHWU(regPC);
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                PUSHWU(regS);
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                PUSHWU(regY);
                i += 2;
            }
            if ((ucTemp & 0x10) != 0)
            {
                PUSHWU(regX);
                i += 2;
            }
            if ((ucTemp & 0x8) != 0)
            {
                PUSHBU(regDP);
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                PUSHBU(regB);
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                PUSHBU(regA);
                i++;
            }
            if ((ucTemp & 0x1) != 0)
            {
                PUSHBU(regCC);
                i++;
            }
            tickCount += i; //timing
        }

        //Pull A, B, CC, DP, D, X, Y, U, or PC from hardware stack
        void PULS(byte ucTemp)
        {
            int i = 0;
            if ((ucTemp & 0x1) != 0)
            {
                regCC = PULLB();
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                regA = PULLB();
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                regB = PULLB();
                i++;
            }
            if ((ucTemp & 0x8) != 0)
            {
                regDP = PULLB();
                i++;
            }
            if ((ucTemp & 0x10) != 0)
            {
                regX = PULLW();
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                regY = PULLW();
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                regU = PULLW();
                i += 2;
            }
            if ((ucTemp & 0x80) != 0)
            {
                regPC = PULLW();
                i += 2;
            }
            tickCount += i; //timing
        }

        //Pull A, B, CC, DP, D, X, Y, S, or PC from hardware stack
        void PULU(byte ucTemp)
        {
            int i = 0;
            if ((ucTemp & 0x1) != 0)
            {
                regCC = PULLBU();
                i++;
            }
            if ((ucTemp & 0x2) != 0)
            {
                regA = PULLBU();
                i++;
            }
            if ((ucTemp & 0x4) != 0)
            {
                regB = PULLBU();
                i++;
            }
            if ((ucTemp & 0x8) != 0)
            {
                regDP = PULLBU();
                i++;
            }
            if ((ucTemp & 0x10) != 0)
            {
                regX = PULLWU();
                i += 2;
            }
            if ((ucTemp & 0x20) != 0)
            {
                regY = PULLWU();
                i += 2;
            }
            if ((ucTemp & 0x40) != 0)
            {
                regS = PULLWU();
                i += 2;
            }
            if ((ucTemp & 0x80) != 0)
            {
                regPC = PULLWU();
                i += 2;
            }
            tickCount += i; //timing
        }

        public ushort getPostByteRegister(byte ucPostByte)
        {
            switch (ucPostByte & 0xF)
            {
                case 0x00:
                    return getD();
                case 0x1:
                    return regX;
                case 0x2:
                    return regY;
                case 0x3:
                    return regU;
                case 0x4:
                    return regS;
                case 0x5:
                    return regPC;
                case 0x8:
                    return regA;
                case 0x9:
                    return regB;
                case 0xA:
                    return regCC;
                case 0xB:
                    return regDP;
                default:
                    /* illegal */
                    throw new Exception("getPBR_INVALID_" + ucPostByte);
            }
        }

        public void setPostByteRegister(byte ucPostByte, ushort v)
        {
            /* Get destination register */
            switch (ucPostByte & 0xF)
            {
                case 0x00:
                    setD(v);
                    return;
                case 0x1:
                    regX = v;
                    return;
                case 0x2:
                    regY = v;
                    return;
                case 0x3:
                    regU = v;
                    return;
                case 0x4:
                    regS = v;
                    return;
                case 0x5:
                    regPC = v;
                    return;
                case 0x8:
                    regA = (byte) (v & 0xFF);
                    return;
                case 0x9:
                    regB = (byte) (v & 0xFF);
                    return;
                case 0xA:
                    regCC = (byte) (v & 0xFF);
                    return;
                case 0xB:
                    regDP = (byte) (v & 0xFF);
                    return;
                default:
                    /* illegal */
                    throw new Exception("setPBR_INVALID_" + ucPostByte);
            }
        }

        // Transfer or exchange two registers.
        public void TFREXG(byte ucPostByte, bool bExchange)
        {
            ushort ucTemp = (ushort) (ucPostByte & 0x88);
            if (ucTemp == 0x80 || ucTemp == 0x08)
            {
                throw new Exception("TFREXG_ERROR_MIXING_8_AND_16BIT_REGISTER!");
            }

            ucTemp = getPostByteRegister((byte) (ucPostByte >> 4));
            if (bExchange)
            {
                setPostByteRegister((byte) (ucPostByte >> 4), getPostByteRegister(ucPostByte));
            }
            /* Transfer */
            setPostByteRegister(ucPostByte, ucTemp);
        }

        public byte signed5bit(byte x)
        {
            return (byte) ((x > 0xF) ? x - 0x20 : x);
        }

        public byte signed(byte x)
        {
            return (byte) (x > 0x7F ? x - 0x100 : x);
        }

        public ushort signed16(ushort x)
        {
            return (ushort) ((x > 0x7FFF) ? x - 0x10000 : x);
        }

        byte FetchFunction()
        {
            return memoryReadFunction(regPC++);
        }

        ushort fetch16()
        {
            byte v1 = memoryReadFunction(regPC++);
            byte v2 = memoryReadFunction(regPC++);
            return (ushort) ((v1 << 8) + v2);
        }

        ushort ReadWord(ushort addr)
        {
            byte v1 = memoryReadFunction(addr);
            byte v2 = memoryReadFunction((ushort) ((addr + 1) & 0xFFFF));
            return (ushort) ((v1 << 8) + v2);
        }

        public void WriteWord(ushort addr, ushort v)
        {
            memoryWriteFunction(addr, (byte) ((v >> 8) & 0xff));
            memoryWriteFunction((ushort) ((addr + 1) & 0xFFFF), (byte) (v & 0xff));
        }

        //PURPOSE: Calculate the EA for INDEXING addressing mode.
        //
        // Offset sizes for postbyte
        // ±4-bit (-16 to +15)
        // ±7-bit (-128 to +127)
        // ±15-bit (-32768 to +32767)
        public ushort PostByte()
        {
            const byte INDIRECT_FIELD = 0x10;
            const byte REGISTER_FIELD = 0x60;
            const byte COMPLEXTYPE_FIELD = 0x80;
            const byte ADDRESSINGMODE_FIELD = 0x0F;

            byte postByte = fetch();
            ushort registerField;
            // Isolate register is used for the indexed operation
            // see Table 3-6. Indexed Addressing Postbyte Register
            switch (postByte & REGISTER_FIELD)
            {
                case 0x00:
                    registerField = regX;
                    break;
                case 0x20:
                    registerField = regY;
                    break;
                case 0x40:
                    registerField = regU;
                    break;
                case 0x60:
                    registerField = regS;
                    break;
                default:
                    throw new Exception("INVALID_ADDRESS_PB");
            }

            ushort? xchg = null;
            ushort EA = 0; //Effective Address
            if ((postByte & COMPLEXTYPE_FIELD) != 0)
            {
                // Complex stuff
                switch (postByte & ADDRESSINGMODE_FIELD)
                {
                    case 0x00: // R+
                        EA = registerField;
                        xchg = (ushort) (registerField + 1);
                        tickCount += 2;
                        break;
                    case 0x01: // R++
                        EA = registerField;
                        xchg = (ushort) (registerField + 2);
                        tickCount += 3;
                        break;
                    case 0x02: // -R
                        xchg = (ushort) (registerField - 1);
                        EA = xchg.Value;
                        tickCount += 2;
                        break;
                    case 0x03: // --R
                        xchg = (ushort) (registerField - 2);
                        EA = xchg.Value;
                        tickCount += 3;
                        break;
                    case 0x04: // EA = R + 0 OFFSET
                        EA = registerField;
                        break;
                    case 0x05: // EA = R + REGB OFFSET
                        EA = (ushort) (registerField + (sbyte) signed(regB));
                        tickCount += 1;
                        break;
                    case 0x06: // EA = R + REGA OFFSET
                        EA = (ushort) (registerField + (sbyte) signed(regA));
                        tickCount += 1;
                        break;
                    // case 0x07 is ILLEGAL
                    case 0x08: // EA = R + 7bit OFFSET
                        EA = (ushort) (registerField + (sbyte) signed(fetch()));
                        tickCount += 1;
                        break;
                    case 0x09: // EA = R + 15bit OFFSET
                        EA = (ushort) (registerField + (short) signed16(fetch16()));
                        tickCount += 4;
                        break;
                    // case 0x0A is ILLEGAL
                    case 0x0B: // EA = R + D OFFSET
                        EA = (ushort) (registerField + getD());
                        tickCount += 4;
                        break;
                    case 0x0C:
                        { // EA = PC + 7bit OFFSET
                          // NOTE: fetch increases regPC - so order is important!
                            sbyte tmpByte = (sbyte) signed(fetch());
                            EA = (ushort) (regPC + tmpByte);
                            tickCount += 1;
                            break;
                        }
                    case 0x0D:
                        { // EA = PC + 15bit OFFSET
                          // NOTE: fetch increases regPC - so order is important!
                            short word = (short) signed16(fetch16());
                            EA = (ushort) (regPC + word);
                            tickCount += 5;
                            break;
                        }
                    // case 0xE is ILLEGAL
                    case 0x0F: // EA = ADDRESS
                        EA = fetch16();
                        tickCount += 5;
                        break;
                    default:
                        {
                            byte mode = (byte) (postByte & ADDRESSINGMODE_FIELD);
                            throw new Exception("INVALID_ADDRESS_MODE_" + mode);
                        }
                }

                EA &= 0xFFFF;
                if ((postByte & INDIRECT_FIELD) != 0)
                {
                    /* TODO: Indirect "Increment/Decrement by 1" is not valid
                    const adrmode = postByte & ADDRESSINGMODE_FIELD
                    if (adrmode == 0 || adrmode === 2) {
                      throw new Error('INVALID_INDIRECT_ADDRESSMODE_', adrmode);
                    }
                    */
                    // INDIRECT addressing
                    EA = ReadWord(EA);
                    tickCount += 3;
                }
            }
            else
            {
                // Just a 5 bit signed offset + register, NO INDIRECT ADDRESS MODE
                sbyte sByte = (sbyte) signed5bit((byte) (postByte & 0x1F));
                EA = (ushort) (registerField + sByte);
                tickCount += 1;
            }

            if (xchg != null)
            {
                xchg &= 0xFFFF;
                switch (postByte & REGISTER_FIELD)
                {
                    case 0:
                        regX = (ushort) xchg;
                        break;
                    case 0x20:
                        regY = (ushort) xchg;
                        break;
                    case 0x40:
                        regU = (ushort) xchg;
                        break;
                    case 0x60:
                        regS = (ushort) xchg;
                        break;
                    default:
                        throw new Exception("PB_INVALID_XCHG_VALUE_" + postByte);
                }
            }
            // Return the effective address
            return (ushort) (EA & 0xFFFF);
        }

        public void flagsNZ16(ushort word)
        {
            regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE));
            if (word == 0)
            {
                regCC |= F_ZERO;
            }
            if ((word & 0x8000) != 0)
            {
                regCC |= F_NEGATIVE;
            }
        }

        // ============= Operations

        byte oINC(byte b)
        {
            b = (byte) ((b + 1) & 0xFF);
            regCC = (byte) (regCC & ~(F_ZERO | F_OVERFLOW | F_NEGATIVE));
            regCC |= flagsNZ[b];
            //Docs say:
            //V: Set if the original operand was 01111111
            if (b == 0x80)
            {
                regCC |= F_OVERFLOW;
            }
            return b;
        }

        byte oDEC(byte b)
        {
            b = (byte)((b - 1) & 0xFF);
            regCC = (byte) (regCC & ~(F_ZERO | F_OVERFLOW | F_NEGATIVE));
            regCC |= flagsNZ[b];
            //Docs say:
            //V: Set if the original operand was 10000000
            if (b == 0x7f)
            {
                regCC |= F_OVERFLOW;
            }
            return b;
        }

        byte oSUB(byte b, byte v)
        {
            ushort temp = (ushort) (b - v);
            regCC = (byte) (regCC & ~(F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0x100) != 0)
            {
                regCC |= F_CARRY;
            }
            setV8(b, v, temp);
            temp &= 0xFF;
            regCC |= flagsNZ[temp];
            return (byte) temp;
        }

        ushort oSUB16(ushort b, ushort v)
        {
            ushort temp = (ushort)(b - v);
            regCC = (byte) (regCC & ~(F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0x8000) != 0)
            {
                regCC |= F_NEGATIVE;
            }
            if ((temp & 0x10000) != 0)
            {
                regCC |= F_CARRY;
            }
            setV16(b, v, temp);
            temp &= 0xFFFF;
            if (temp == 0)
            {
                regCC |= F_ZERO;
            }
            return temp;
        }

        byte oADD(byte b, byte v)
        {
            ushort temp = (ushort) (b + v);
            regCC = (byte) (regCC & ~(F_HALFCARRY | F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0x100) != 0)
            {
                regCC |= F_CARRY;
            }
            setV8(b, v, temp);
            if (((temp ^ b ^ v) & 0x10) != 0)
            {
                regCC |= F_HALFCARRY;
            }
            temp &= 0xFF;
            regCC |= flagsNZ[temp];
            return (byte)temp;
        }

        ushort oADD16(ushort b, ushort v)
        {
            ulong temp = (ulong) (b + v);
            regCC = (byte) (regCC & ~(F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0x8000) != 0)
            {
                regCC |= F_NEGATIVE;
            }
            if ((temp & 0x10000) != 0)
            {
                regCC |= F_CARRY;
            }
            setV16(b, v, temp);
            temp &= 0xFFFF;
            if (temp == 0)
            {
                regCC |= F_ZERO;
            }
            return (ushort)temp;
        }

        byte oADC(byte b, byte v)
        {
            ushort temp = (ushort) (b + v + (regCC & F_CARRY));
            regCC = (byte) (regCC & ~(F_HALFCARRY | F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0x100) != 0)
            {
                regCC |= F_CARRY;
            }
            setV8(b, v, temp);
            if (((temp ^ b ^ v) & 0x10) != 0)
            {
                regCC |= F_HALFCARRY;
            }
            temp &= 0xFF;
            regCC |= flagsNZ[temp];
            return (byte)temp;
        }

        byte oSBC(byte b, byte v)
        {
            ushort temp = (ushort) (b - v - (regCC & F_CARRY));
            regCC = (byte) (regCC & ~(F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0x100) != 0)
            {
                regCC |= F_CARRY;
            }
            setV8(b, v, temp);
            temp &= 0xFF;
            regCC |= flagsNZ[temp];
            return (byte) temp;
        }

        public void oCMP(byte b, byte v)
        {
            ushort temp = (ushort) (b - v);
            regCC = (byte) (regCC & ~(F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0x100) != 0)
            {
                regCC |= F_CARRY;
            }
            setV8(b, v, temp);
            temp &= 0xFF;
            regCC |= flagsNZ[temp];
        }

        public void oCMP16(ushort b, ushort v)
        {
            ulong temp = (ulong) (b - v);
            regCC = (byte) (regCC & ~(F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            if ((temp & 0xFFFF) == 0)
            {
                regCC |= F_ZERO;
            }
            if ((temp & 0x8000) != 0)
            {
                regCC |= F_NEGATIVE;
            }
            if ((temp & 0x10000) != 0)
            {
                regCC |= F_CARRY;
            }
            setV16(b, v, temp);
        }

        public byte oNEG(byte b)
        {
            regCC = (byte) (regCC & ~(F_CARRY | F_ZERO | F_OVERFLOW | F_NEGATIVE));
            b = (byte) ((0 - b) & 0xFF);
            if (b == 0x80)
            {
                regCC |= F_OVERFLOW;
            }
            if (b == 0)
            {
                regCC |= F_ZERO;
            }
            if ((b & 0x80) != 0)
            {
                regCC |= F_NEGATIVE | F_CARRY;
            }
            return b;
        }

        byte oLSR(byte b)
        {
            regCC = (byte) (regCC & ~(F_ZERO | F_CARRY | F_NEGATIVE));
            if ((b & F_CARRY) != 0)
            {
                regCC |= F_CARRY;
            }
            b >>= 1;
            if (b == 0)
            {
                regCC |= F_ZERO;
            }
            return b;
        }

        byte oASR(byte b)
        {
            regCC = (byte) (regCC & ~(F_ZERO | F_CARRY | F_NEGATIVE));
            if ((b & 0x01) != 0)
            {
                regCC |= F_CARRY;
            }
            b = (byte) ((b & 0x80) | (b >> 1));
            b &= 0xFF;
            regCC |= flagsNZ[b];
            return b;
        }

        byte oASL(byte b)
        {
            ushort temp = b;
            regCC = (byte) (regCC & ~(F_ZERO | F_CARRY | F_NEGATIVE | F_OVERFLOW));
            if ((b & 0x80) != 0)
            {
                regCC |= F_CARRY;
            }
            b <<= 1;
            if (((b ^ temp) & 0x80) != 0)
            {
                regCC |= F_OVERFLOW;
            }
            b &= 0xFF;
            regCC |= flagsNZ[b];
            return b;
        }

        byte oROL(byte b)
        {
            byte temp = b;
            byte oldCarry = (byte) (regCC & F_CARRY);
            regCC = (byte) (regCC & ~(F_ZERO | F_CARRY | F_NEGATIVE | F_OVERFLOW));
            if ((b & 0x80) != 0)
            {
                regCC |= F_CARRY;
            }
            b = (byte) ((b << 1) | oldCarry);
            if (((b ^ temp) & 0x80) != 0)
            {
                regCC |= F_OVERFLOW;
            }
            b &= 0xFF;
            regCC |= flagsNZ[b];
            return b;
        }

        byte oROR(byte b)
        {
            byte oldCarry = (byte) (regCC & F_CARRY);
            regCC = (byte) (regCC & ~(F_ZERO | F_CARRY | F_NEGATIVE));
            if ((b & 0x01) != 0)
            {
                regCC |= F_CARRY;
            }
            b = (byte) ((b >> 1) | (oldCarry << 7));
            b &= 0xFF;
            regCC |= flagsNZ[b];
            return b;
        }

        byte oEOR(byte b, byte v)
        {
            regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
            b ^= v;
            b &= 0xFF;
            regCC |= flagsNZ[b];
            return b;
        }

        byte oOR(byte b, byte v)
        {
            regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
            b |= v;
            b &= 0xFF;
            regCC |= flagsNZ[b];
            return b;
        }

        byte oAND(byte b, byte v)
        {
            regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
            b &= v;
            b &= 0xFF;
            regCC |= flagsNZ[b];
            return b;
        }

        byte oCOM(byte b)
        {
            regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
            b ^= 0xFF;
            b &= 0xFF;
            regCC |= flagsNZ[b];
            regCC |= F_CARRY;
            return b;
        }

        //----common
        public ushort dpadd()
        {
            //direct page + 8bit index
            return (ushort) ((regDP << 8) + fetch());
        }

        public int step()
        {
            int oldTickCount = tickCount;

            // LATCH IRQ lines, see 6803 diagram "figure3-1.jpg"

            if (firqPending)
            {
                if ((regCC & F_FIRQMASK) == 0)
                {
                    firqPending = false;
                    firqCount++;
                    _executeFirq();
                    return tickCount - oldTickCount;
                }
                missedFIRQ++;
            }

            if (irqPending)
            {
                if ((regCC & F_IRQMASK) == 0)
                {
                    irqPending = false;
                    irqCount++;
                    _executeIrq();
                    return tickCount - oldTickCount;
                }
                missedIRQ++;
            }

            ushort addr = 0;
            byte pb = 0;

            byte opcode = fetch();
            tickCount += cycles[opcode];
            //debug('OP 0x' + opcode.toString(16));
            switch (opcode)
            {
                case 0x00: //NEG DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oNEG(memoryReadFunction(addr))
                    );
                    break;
                case 0x03: //COM DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oCOM(memoryReadFunction(addr))
                    );
                    break;
                case 0x04: //LSR DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oLSR(memoryReadFunction(addr))
                    );
                    break;
                case 0x06: //ROR DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oROR(memoryReadFunction(addr))
                    );
                    break;
                case 0x07: //ASR DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oASR(memoryReadFunction(addr))
                    );
                    break;
                case 0x08: //ASL DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oASL(memoryReadFunction(addr))
                    );
                    break;
                case 0x09: //ROL DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oROL(memoryReadFunction(addr))
                    );
                    break;

                case 0x0a: //DEC DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oDEC(memoryReadFunction(addr))
                    );
                    break;
                case 0x0c: //INC DP
                    addr = dpadd();
                    memoryWriteFunction(
                      addr,
                      oINC(memoryReadFunction(addr))
                    );
                    break;

                case 0x0d: //TST DP
                    addr = dpadd();
                    pb = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[pb];
                    break;

                case 0x0e: //JMP DP
                    addr = dpadd();
                    regPC = addr;
                    break;
                case 0x0f: //CLR DP
                    addr = dpadd();
                    memoryWriteFunction(addr, 0);
                    regCC = (byte) (regCC & ~(F_CARRY | F_NEGATIVE | F_OVERFLOW));
                    regCC |= F_ZERO;
                    break;

                case 0x12: //NOP
                    break;
                case 0x13: //SYNC
                    /*
                    This commands stops the CPU, brings the processor bus to high impedance state and waits for an interrupt.
                    */
                    Debug.Print("SYNC is broken!");
                    break;
                case 0x16: //LBRA relative
                    addr = fetch16();
                    regPC += addr;
                    break;
                case 0x17: //LBSR relative
                    addr = fetch16();
                    PUSHW(regPC);
                    regPC += addr;
                    break;
                case 0x19:
                    {//DAA
                        ushort correctionFactor = 0;
                        byte nhi = (byte) (regA & 0xF0);
                        byte nlo = (byte) (regA & 0x0F);
                        if (nlo > 0x09 || (regCC & F_HALFCARRY) != 0)
                        {
                            correctionFactor |= 0x06;
                        }
                        if (nhi > 0x80 && nlo > 0x09)
                        {
                            correctionFactor |= 0x60;
                        }
                        if (nhi > 0x90 || (regCC & F_CARRY) != 0)
                        {
                            correctionFactor |= 0x60;
                        }
                        addr = (ushort) (correctionFactor + regA);
                        // TODO Check, mame does not clear carry here
                        regCC = (byte) (regCC & ~(F_CARRY | F_NEGATIVE | F_ZERO | F_OVERFLOW));
                        if ((addr & 0x100) != 0)
                        {
                            regCC |= F_CARRY;
                        }
                        regA = (byte) (addr & 0xFF);
                        regCC |= flagsNZ[regA];
                        break;
                    }
                case 0x1a: //ORCC
                    regCC |= fetch();
                    break;
                case 0x1c: //ANDCC
                    regCC = (byte) (regCC & fetch());
                    break;
                case 0x1d: //SEX
                           //TODO should we use signed here?
                    regA = (byte) (((regB & 0x80) != 0) ? 0xff : 0);
                    flagsNZ16(getD());
                    break;
                case 0x1e: //EXG
                    pb = fetch();
                    TFREXG(pb, true);
                    break;
                case 0x1f: //TFR
                    pb = fetch();
                    TFREXG(pb, false);
                    break;

                case 0x20: //BRA
                    addr = signed(fetch());
                    regPC += addr;
                    break;
                case 0x21: //BRN
                    addr = signed(fetch());
                    break;
                case 0x22: //BHI
                    addr = signed(fetch());
                    if ((regCC & (F_CARRY | F_ZERO)) == 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x23: //BLS
                    addr = signed(fetch());
                    if ((regCC & (F_CARRY | F_ZERO)) != 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x24: //BCC
                    addr = signed(fetch());
                    if ((regCC & F_CARRY) == 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x25: //BCS
                    addr = signed(fetch());
                    if ((regCC & F_CARRY) != 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x26: //BNE
                    addr = signed(fetch());
                    if ((regCC & F_ZERO) == 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x27: //BEQ
                    addr = signed(fetch());
                    if ((regCC & F_ZERO) != 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x28: //BVC
                    addr = signed(fetch());
                    if ((regCC & F_OVERFLOW) == 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x29: //BVS
                    addr = signed(fetch());
                    if ((regCC & F_OVERFLOW) != 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x2a: //BPL
                    addr = signed(fetch());
                    if ((regCC & F_NEGATIVE) == 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x2b: //BMI
                    addr = signed(fetch());
                    if ((regCC & F_NEGATIVE) != 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x2c: //BGE
                    addr = signed(fetch());
                    if (((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) == 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x2d: //BLT
                    addr = signed(fetch());
                    if (((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) != 0)
                    {
                        regPC += addr;
                    }
                    break;
                case 0x2e: //BGT
                    addr = signed(fetch());
                    if (
                        (((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) == 0) ||
                        ((regCC & F_ZERO) != 0)
                    )
                    {
                        regPC += addr;
                    }
                    break;
                case 0x2f: //BLE
                    addr = signed(fetch());
                    if (
                       (((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) != 0) ||
                       ((regCC & F_ZERO) != 0)
                    )
                    {
                        regPC += addr;
                    }
                    break;

                case 0x30: //LEAX
                    regX = PostByte();
                    regCC = (byte) (regCC & ~F_ZERO);
                    if (regX == 0)
                    {
                        regCC |= F_ZERO;
                    }
                    break;
                case 0x31: //LEAY
                    regY = PostByte();
                    regCC = (byte) (regCC & ~F_ZERO);
                    if (regY == 0)
                    {
                        regCC |= F_ZERO;
                    }
                    break;
                case 0x32: //LEAS
                    regS = PostByte();
                    break;
                case 0x33: //LEAU
                    regU = PostByte();
                    break;

                case 0x34: //PSHS
                    PSHS(fetch());
                    break;
                case 0x35: //PULS
                    PULS(fetch());
                    break;
                case 0x36: //PSHU
                    PSHU(fetch());
                    break;
                case 0x37: //PULU
                    PULU(fetch());
                    break;
                case 0x39: //RTS
                    regPC = PULLW();
                    break;
                case 0x3a: //ABX
                    regX += regB;
                    break;
                case 0x3b: //RTI
                    regCC = PULLB();
                    Debug.Print("RTI {0} {1}", (byte) (regCC & F_ENTIRE), tickCount);
                    // Check for fast interrupt
                    if ((regCC & F_ENTIRE) != 0)
                    {
                        tickCount += 9;
                        regA = PULLB();
                        regB = PULLB();
                        regDP = PULLB();
                        regX = PULLW();
                        regY = PULLW();
                        regU = PULLW();
                    }
                    regPC = PULLW();
                    break;
                case 0x3c: //CWAI
                    Debug.Print("CWAI is broken!");
                    /*
                     * CWAI stacks the entire machine state on the hardware stack,
                     * then waits for an interrupt; when the interrupt is taken
                     * later, the state is *not* saved again after CWAI.
                     * see mame-6809.c how to proper implement this opcode
                     */
                    regCC = (byte) (regCC & fetch());
                    //TODO - ??? set cwai flag to true, do not exec next interrupt (NMI, FIRQ, IRQ) - but set reset cwai flag afterwards
                    break;
                case 0x3d: //MUL
                    addr = (ushort) (regA * regB);
                    if (addr == 0)
                    {
                        regCC |= F_ZERO;
                    }
                    else
                    {
                        regCC = (byte) (regCC & ~F_ZERO);
                    }
                    if ((addr & 0x80) != 0)
                    {
                        regCC |= F_CARRY;
                    }
                    else
                    {
                        regCC = (byte) (regCC & ~F_CARRY);
                    }
                    setD(addr);
                    break;
                case 0x3f: //SWI
                    Debug.Print("SWI is untested!");
                    regCC |= F_ENTIRE;
                    PUSHW(regPC);
                    PUSHW(regU);
                    PUSHW(regY);
                    PUSHW(regX);
                    PUSHB(regDP);
                    PUSHB(regB);
                    PUSHB(regA);
                    PUSHB(regCC);
                    regCC |= F_IRQMASK | F_FIRQMASK;
                    regPC = ReadWord(vecSWI);
                    break;

                case 0x40:
                    regA = oNEG(regA);
                    break;
                case 0x43:
                    regA = oCOM(regA);
                    break;
                case 0x44:
                    regA = oLSR(regA);
                    break;
                case 0x46:
                    regA = oROR(regA);
                    break;
                case 0x47:
                    regA = oASR(regA);
                    break;
                case 0x48:
                    regA = oASL(regA);
                    break;
                case 0x49:
                    regA = oROL(regA);
                    break;
                case 0x4a:
                    regA = oDEC(regA);
                    break;
                case 0x4c:
                    regA = oINC(regA);
                    break;
                case 0x4d: // tsta
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0x4f: /* CLRA */
                    regA = 0;
                    regCC = (byte) (regCC & ~(F_NEGATIVE | F_OVERFLOW | F_CARRY));
                    regCC |= F_ZERO;
                    break;

                case 0x50: /* NEGB */
                    regB = oNEG(regB);
                    break;
                case 0x53:
                    regB = oCOM(regB);
                    break;
                case 0x54:
                    regB = oLSR(regB);
                    break;
                case 0x56:
                    regB = oROR(regB);
                    break;
                case 0x57:
                    regB = oASR(regB);
                    break;
                case 0x58:
                    regB = oASL(regB);
                    break;
                case 0x59:
                    regB = oROL(regB);
                    break;
                case 0x5a:
                    regB = oDEC(regB);
                    break;
                case 0x5c: // INCB
                    regB = oINC(regB);
                    break;
                case 0x5d: /* TSTB */
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0x5f: //CLRB
                    regB = 0;
                    regCC = (byte) (regCC & ~(F_NEGATIVE | F_OVERFLOW | F_CARRY));
                    regCC |= F_ZERO;
                    break;

                case 0x60: //NEG indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oNEG(memoryReadFunction(addr))
                    );
                    break;
                case 0x63: //COM indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oCOM(memoryReadFunction(addr))
                    );
                    break;
                case 0x64: //LSR indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oLSR(memoryReadFunction(addr))
                    );
                    break;
                case 0x66: //ROR indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oROR(memoryReadFunction(addr))
                    );
                    break;
                case 0x67: //ASR indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oASR(memoryReadFunction(addr))
                    );
                    break;
                case 0x68: //ASL indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oASL(memoryReadFunction(addr))
                    );
                    break;
                case 0x69: //ROL indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oROL(memoryReadFunction(addr))
                    );
                    break;

                case 0x6a: //DEC indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oDEC(memoryReadFunction(addr))
                    );
                    break;
                case 0x6c: //INC indexed
                    addr = PostByte();
                    memoryWriteFunction(
                      addr,
                      oINC(memoryReadFunction(addr))
                    );
                    break;

                case 0x6d: //TST indexed
                    addr = PostByte();
                    pb = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[pb];
                    break;

                case 0x6e: //JMP indexed
                    addr = PostByte();
                    regPC = addr;
                    break;
                case 0x6f: //CLR indexed
                    addr = PostByte();
                    memoryWriteFunction(addr, 0);
                    regCC = (byte) (regCC & ~(F_CARRY | F_NEGATIVE | F_OVERFLOW));
                    regCC |= F_ZERO;
                    break;

                case 0x70: //NEG extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oNEG(memoryReadFunction(addr))
                    );
                    break;
                case 0x73: //COM extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oCOM(memoryReadFunction(addr))
                    );
                    break;
                case 0x74: //LSR extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oLSR(memoryReadFunction(addr))
                    );
                    break;
                case 0x76: //ROR extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oROR(memoryReadFunction(addr))
                    );
                    break;
                case 0x77: //ASR extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oASR(memoryReadFunction(addr))
                    );
                    break;
                case 0x78: //ASL extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oASL(memoryReadFunction(addr))
                    );
                    break;
                case 0x79: //ROL extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oROL(memoryReadFunction(addr))
                    );
                    break;

                case 0x7a: //DEC extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oDEC(memoryReadFunction(addr))
                    );
                    break;
                case 0x7c: //INC extended
                    addr = fetch16();
                    memoryWriteFunction(
                      addr,
                      oINC(memoryReadFunction(addr))
                    );
                    break;

                case 0x7d: //TST extended
                    addr = fetch16();
                    pb = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[pb];
                    break;

                case 0x7e: //JMP extended
                    addr = fetch16();
                    regPC = addr;
                    break;
                case 0x7f: //CLR extended
                    addr = fetch16();
                    memoryWriteFunction(addr, 0);
                    regCC = (byte) (regCC & ~(F_CARRY | F_NEGATIVE | F_OVERFLOW));
                    regCC |= F_ZERO;
                    break;

                case 0x80: //SUBA imm
                    regA = oSUB(regA, fetch());
                    break;
                case 0x81: //CMPA imm
                    oCMP(regA, fetch());
                    break;
                case 0x82: //SBCA imm
                    regA = oSBC(regA, fetch());
                    break;
                case 0x83: //SUBD imm
                    setD(oSUB16(getD(), fetch16()));
                    break;
                case 0x84: //ANDA imm
                    regA = oAND(regA, fetch());
                    break;
                case 0x85: //BITA imm
                    oAND(regA, fetch());
                    break;
                case 0x86: //LDA imm
                    regA = fetch();
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0x88: //EORA imm
                    regA = oEOR(regA, fetch());
                    break;
                case 0x89: //ADCA imm
                    regA = oADC(regA, fetch());
                    break;
                case 0x8a: //ORA imm
                    regA = oOR(regA, fetch());
                    break;
                case 0x8b: //ADDA imm
                    regA = oADD(regA, fetch());
                    break;
                case 0x8c: //CMPX imm
                    oCMP16(regX, fetch16());
                    break;

                case 0x8d: //JSR imm
                    addr = signed(fetch());
                    PUSHW(regPC);
                    regPC += addr;
                    break;
                case 0x8e: //LDX imm
                    regX = fetch16();
                    flagsNZ16(regX);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0x90: //SUBA direct
                    addr = dpadd();
                    regA = oSUB(regA, memoryReadFunction(addr));
                    break;
                case 0x91: //CMPA direct
                    addr = dpadd();
                    oCMP(regA, memoryReadFunction(addr));
                    break;
                case 0x92: //SBCA direct
                    addr = dpadd();
                    regA = oSBC(regA, memoryReadFunction(addr));
                    break;
                case 0x93: //SUBD direct
                    addr = dpadd();
                    setD(oSUB16(getD(), ReadWord(addr)));
                    break;
                case 0x94: //ANDA direct
                    addr = dpadd();
                    regA = oAND(regA, memoryReadFunction(addr));
                    break;
                case 0x95: //BITA direct
                    addr = dpadd();
                    oAND(regA, memoryReadFunction(addr));
                    break;
                case 0x96: //LDA direct
                    addr = dpadd();
                    regA = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0x97: //STA direct
                    addr = dpadd();
                    memoryWriteFunction(addr, regA);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0x98: //EORA direct
                    addr = dpadd();
                    regA = oEOR(regA, memoryReadFunction(addr));
                    break;
                case 0x99: //ADCA direct
                    addr = dpadd();
                    regA = oADC(regA, memoryReadFunction(addr));
                    break;
                case 0x9a: //ORA direct
                    addr = dpadd();
                    regA = oOR(regA, memoryReadFunction(addr));
                    break;
                case 0x9b: //ADDA direct
                    addr = dpadd();
                    regA = oADD(regA, memoryReadFunction(addr));
                    break;
                case 0x9c: //CMPX direct
                    addr = dpadd();
                    oCMP16(regX, ReadWord(addr));
                    break;

                case 0x9d: //JSR direct
                    addr = dpadd();
                    PUSHW(regPC);
                    regPC = addr;
                    break;
                case 0x9e: //LDX direct
                    addr = dpadd();
                    regX = ReadWord(addr);
                    flagsNZ16(regX);
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;
                case 0x9f: //STX direct
                    addr = dpadd();
                    WriteWord(addr, regX);
                    flagsNZ16(regX);
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;
                case 0xa0: //SUBA indexed
                    addr = PostByte();
                    regA = oSUB(regA, memoryReadFunction(addr));
                    break;
                case 0xa1: //CMPA indexed
                    addr = PostByte();
                    oCMP(regA, memoryReadFunction(addr));
                    break;
                case 0xa2: //SBCA indexed
                    addr = PostByte();
                    regA = oSBC(regA, memoryReadFunction(addr));
                    break;
                case 0xa3: //SUBD indexed
                    addr = PostByte();
                    setD(oSUB16(getD(), ReadWord(addr)));
                    break;
                case 0xa4: //ANDA indexed
                    addr = PostByte();
                    regA = oAND(regA, memoryReadFunction(addr));
                    break;
                case 0xa5: //BITA indexed
                    addr = PostByte();
                    oAND(regA, memoryReadFunction(addr));
                    break;
                case 0xa6: //LDA indexed
                    addr = PostByte();
                    regA = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0xa7: //STA indexed
                    addr = PostByte();
                    memoryWriteFunction(addr, regA);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0xa8: //EORA indexed
                    addr = PostByte();
                    regA = oEOR(regA, memoryReadFunction(addr));
                    break;
                case 0xa9: //ADCA indexed
                    addr = PostByte();
                    regA = oADC(regA, memoryReadFunction(addr));
                    break;
                case 0xaa: //ORA indexed
                    addr = PostByte();
                    regA = oOR(regA, memoryReadFunction(addr));
                    break;
                case 0xab: //ADDA indexed
                    addr = PostByte();
                    regA = oADD(regA, memoryReadFunction(addr));
                    break;
                case 0xac: //CMPX indexed
                    addr = PostByte();
                    oCMP16(regX, ReadWord(addr));
                    break;

                case 0xad: //JSR indexed
                    addr = PostByte();
                    PUSHW(regPC);
                    regPC = addr;
                    break;
                case 0xae: //LDX indexed
                    addr = PostByte();
                    regX = ReadWord(addr);
                    flagsNZ16(regX);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;
                case 0xaf: //STX indexed
                    addr = PostByte();
                    WriteWord(addr, regX);
                    flagsNZ16(regX);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0xb0: //SUBA extended
                    addr = fetch16();
                    regA = oSUB(regA, memoryReadFunction(addr));
                    break;
                case 0xb1: //CMPA extended
                    addr = fetch16();
                    oCMP(regA, memoryReadFunction(addr));
                    break;
                case 0xb2: //SBCA extended
                    addr = fetch16();
                    regA = oSBC(regA, memoryReadFunction(addr));
                    break;
                case 0xb3: //SUBD extended
                    addr = fetch16();
                    setD(oSUB16(getD(), ReadWord(addr)));
                    break;
                case 0xb4: //ANDA extended
                    addr = fetch16();
                    regA = oAND(regA, memoryReadFunction(addr));
                    break;
                case 0xb5: //BITA extended
                    addr = fetch16();
                    oAND(regA, memoryReadFunction(addr));
                    break;
                case 0xb6: //LDA extended
                    addr = fetch16();
                    regA = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0xb7: //STA extended
                    addr = fetch16();
                    memoryWriteFunction(addr, regA);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regA & 0xFF];
                    break;
                case 0xb8: //EORA extended
                    addr = fetch16();
                    regA = oEOR(regA, memoryReadFunction(addr));
                    break;
                case 0xb9: //ADCA extended
                    addr = fetch16();
                    regA = oADC(regA, memoryReadFunction(addr));
                    break;
                case 0xba: //ORA extended
                    addr = fetch16();
                    regA = oOR(regA, memoryReadFunction(addr));
                    break;
                case 0xbb: //ADDA extended
                    addr = fetch16();
                    regA = oADD(regA, memoryReadFunction(addr));
                    break;
                case 0xbc: //CMPX extended
                    addr = fetch16();
                    oCMP16(regX, ReadWord(addr));
                    break;

                case 0xbd: //JSR extended
                    addr = fetch16();
                    PUSHW(regPC);
                    regPC = addr;
                    break;
                case 0xbe: //LDX extended
                    addr = fetch16();
                    regX = ReadWord(addr);
                    flagsNZ16(regX);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;
                case 0xbf: //STX extended
                    addr = fetch16();
                    WriteWord(addr, regX);
                    flagsNZ16(regX);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0xc0: //SUBB imm
                    regB = oSUB(regB, fetch());
                    break;
                case 0xc1: //CMPB imm
                    oCMP(regB, fetch());
                    break;
                case 0xc2: //SBCB imm
                    regB = oSBC(regB, fetch());
                    break;
                case 0xc3: //ADDD imm
                    setD(oADD16(getD(), fetch16()));
                    break;
                case 0xc4: //ANDB imm
                    regB = oAND(regB, fetch());
                    break;
                case 0xc5: //BITB imm
                    oAND(regB, fetch());
                    break;
                case 0xc6: //LDB imm
                    regB = fetch();
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0xc8: //EORB imm
                    regB = oEOR(regB, fetch());
                    break;
                case 0xc9: //ADCB imm
                    regB = oADC(regB, fetch());
                    break;
                case 0xca: //ORB imm
                    regB = oOR(regB, fetch());
                    break;
                case 0xcb: //ADDB imm
                    regB = oADD(regB, fetch());
                    break;
                case 0xcc: //LDD imm
                    addr = fetch16();
                    setD(addr);
                    flagsNZ16(addr);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0xce: //LDU imm
                    regU = fetch16();
                    flagsNZ16(regU);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0xd0: //SUBB direct
                    addr = dpadd();
                    regB = oSUB(regB, memoryReadFunction(addr));
                    break;
                case 0xd1: //CMPB direct
                    addr = dpadd();
                    oCMP(regB, memoryReadFunction(addr));
                    break;
                case 0xd2: //SBCB direct
                    addr = dpadd();
                    regB = oSBC(regB, memoryReadFunction(addr));
                    break;
                case 0xd3: //ADDD direct
                    addr = dpadd();
                    setD(oADD16(getD(), ReadWord(addr)));
                    break;
                case 0xd4: //ANDB direct
                    addr = dpadd();
                    regB = oAND(regB, memoryReadFunction(addr));
                    break;
                case 0xd5: //BITB direct
                    addr = dpadd();
                    oAND(regB, memoryReadFunction(addr));
                    break;
                case 0xd6: //LDB direct
                    addr = dpadd();
                    regB = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0xd7: //STB direct
                    addr = dpadd();
                    memoryWriteFunction(addr, regB);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0xd8: //EORB direct
                    addr = dpadd();
                    regB = oEOR(regB, memoryReadFunction(addr));
                    break;
                case 0xd9: //ADCB direct
                    addr = dpadd();
                    regB = oADC(regB, memoryReadFunction(addr));
                    break;
                case 0xda: //ORB direct
                    addr = dpadd();
                    regB = oOR(regB, memoryReadFunction(addr));
                    break;
                case 0xdb: //ADDB direct
                    addr = dpadd();
                    regB = oADD(regB, memoryReadFunction(addr));
                    break;
                case 0xdc: //LDD direct
                    addr = dpadd();
                    pb = (byte)ReadWord(addr);
                    setD(pb);
                    flagsNZ16(pb);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0xdd: //STD direct
                    addr = dpadd();
                    WriteWord(addr, getD());
                    flagsNZ16(getD());
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;
                case 0xde: //LDU direct
                    addr = dpadd();
                    regU = ReadWord(addr);
                    flagsNZ16(regU);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;
                case 0xdf: //STU direct
                    addr = dpadd();
                    WriteWord(addr, regU);
                    flagsNZ16(regU);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;
                case 0xe0: //SUBB indexed
                    addr = PostByte();
                    regB = oSUB(regB, memoryReadFunction(addr));
                    break;
                case 0xe1: //CMPB indexed
                    addr = PostByte();
                    oCMP(regB, memoryReadFunction(addr));
                    break;
                case 0xe2: //SBCB indexed
                    addr = PostByte();
                    regB = oSBC(regB, memoryReadFunction(addr));
                    break;
                case 0xe3: //ADDD indexed
                    addr = PostByte();
                    setD(oADD16(getD(), ReadWord(addr)));
                    break;
                case 0xe4: //ANDB indexed
                    addr = PostByte();
                    regB = oAND(regB, memoryReadFunction(addr));
                    break;
                case 0xe5: //BITB indexed
                    addr = PostByte();
                    oAND(regB, memoryReadFunction(addr));
                    break;
                case 0xe6: //LDB indexed
                    addr = PostByte();
                    regB = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0xe7: //STB indexed
                    addr = PostByte();
                    memoryWriteFunction(addr, regB);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0xe8: //EORB indexed
                    addr = PostByte();
                    regB = oEOR(regB, memoryReadFunction(addr));
                    break;
                case 0xe9: //ADCB indexed
                    addr = PostByte();
                    regB = oADC(regB, memoryReadFunction(addr));
                    break;
                case 0xea: //ORB indexed
                    addr = PostByte();
                    regB = oOR(regB, memoryReadFunction(addr));
                    break;
                case 0xeb: //ADDB indexed
                    addr = PostByte();
                    regB = oADD(regB, memoryReadFunction(addr));
                    break;
                case 0xec: //LDD indexed
                    addr = PostByte();
                    pb = (byte) ReadWord(addr);
                    setD(pb);
                    flagsNZ16(pb);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0xed: //STD indexed
                    addr = PostByte();
                    WriteWord(addr, getD());
                    flagsNZ16(getD());
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;
                case 0xee: //LDU indexed
                    addr = PostByte();
                    regU = ReadWord(addr);
                    flagsNZ16(regU);
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;
                case 0xef: //STU indexed
                    addr = PostByte();
                    WriteWord(addr, regU);
                    flagsNZ16(regU);
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;

                case 0xf0: //SUBB extended
                    addr = fetch16();
                    regB = oSUB(regB, memoryReadFunction(addr));
                    break;
                case 0xf1: //CMPB extended
                    addr = fetch16();
                    oCMP(regB, memoryReadFunction(addr));
                    break;
                case 0xf2: //SBCB extended
                    addr = fetch16();
                    regB = oSBC(regB, memoryReadFunction(addr));
                    break;
                case 0xf3: //ADDD extended
                    addr = fetch16();
                    setD(oADD16(getD(), ReadWord(addr)));
                    break;
                case 0xf4: //ANDB extended
                    addr = fetch16();
                    regB = oAND(regB, memoryReadFunction(addr));
                    break;
                case 0xf5: //BITB extended
                    addr = fetch16();
                    oAND(regB, memoryReadFunction(addr));
                    break;
                case 0xf6: //LDB extended
                    addr = fetch16();
                    regB = memoryReadFunction(addr);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0xf7: //STB extended
                    addr = fetch16();
                    memoryWriteFunction(addr, regB);
                    regCC = (byte) (regCC & ~(F_ZERO | F_NEGATIVE | F_OVERFLOW));
                    regCC |= flagsNZ[regB & 0xFF];
                    break;
                case 0xf8: //EORB extended
                    addr = fetch16();
                    regB = oEOR(regB, memoryReadFunction(addr));
                    break;
                case 0xf9: //ADCB extended
                    addr = fetch16();
                    regB = oADC(regB, memoryReadFunction(addr));
                    break;
                case 0xfa: //ORB extended
                    addr = fetch16();
                    regB = oOR(regB, memoryReadFunction(addr));
                    break;
                case 0xfb: //ADDB extended
                    addr = fetch16();
                    regB = oADD(regB, memoryReadFunction(addr));
                    break;
                case 0xfc: //LDD extended
                    addr = fetch16();
                    pb = (byte)ReadWord(addr);
                    setD(pb);
                    flagsNZ16(pb);
                    regCC = (byte) (regCC & ~F_OVERFLOW);
                    break;

                case 0xfd: //STD extended
                    addr = fetch16();
                    WriteWord(addr, getD());
                    flagsNZ16(getD());
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;
                case 0xfe: //LDU extended
                    addr = fetch16();
                    regU = ReadWord(addr);
                    flagsNZ16(regU);
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;
                case 0xff: //STU extended
                    addr = fetch16();
                    WriteWord(addr, regU);
                    flagsNZ16(regU);
                    regCC = (byte) (regCC & ~(F_OVERFLOW));
                    break;
                case 0x10:
                    //page 1
                    opcode = fetch();
                    tickCount += cycles2[opcode];
                    switch (opcode)
                    {
                        case 0x21: //BRN
                            addr = signed16(fetch16());
                            break;
                        case 0x22: //BHI
                            addr = signed16(fetch16());
                            if (!((regCC & (F_CARRY | F_ZERO)) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x23: //BLS
                            addr = signed16(fetch16());
                            if ((regCC & (F_CARRY | F_ZERO)) != 0)
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x24: //BCC
                            addr = signed16(fetch16());
                            if (!((regCC & F_CARRY) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x25: //BCS
                            addr = signed16(fetch16());
                            if ((regCC & F_CARRY) != 0)
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x26: //BNE
                            addr = signed16(fetch16());
                            if (!((regCC & F_ZERO) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x27: //LBEQ
                            addr = signed16(fetch16());
                            if ((regCC & F_ZERO) != 0)
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x28: //BVC
                            addr = signed16(fetch16());
                            if (!((regCC & F_OVERFLOW) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x29: //BVS
                            addr = signed16(fetch16());
                            if ((regCC & F_OVERFLOW) != 0)
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x2A: //BPL
                            addr = signed16(fetch16());
                            if (!((regCC & F_NEGATIVE) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x2B: //BMI
                            addr = signed16(fetch16());
                            if ((regCC & F_NEGATIVE) != 0)
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x2C: //BGE
                            addr = signed16(fetch16());
                            if (!(((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x2D: //BLT
                            addr = signed16(fetch16());
                            if (((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) != 0)
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x2E: //BGT
                            addr = signed16(fetch16());
                            if (!((((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) != 0) || (regCC & F_ZERO) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x2F: //BLE
                            addr = signed16(fetch16());
                            if ((((regCC & F_NEGATIVE) ^ ((regCC & F_OVERFLOW) << 2)) != 0) || ((regCC & F_ZERO) != 0))
                            {
                                regPC += addr;
                                tickCount++;
                            }
                            break;
                        case 0x3f: //SWI2
                            Debug.Print("SWI2 is untested!");
                            regCC |= F_ENTIRE;
                            PUSHW(regPC);
                            PUSHW(regU);
                            PUSHW(regY);
                            PUSHW(regX);
                            PUSHB(regDP);
                            PUSHB(regB);
                            PUSHB(regA);
                            PUSHB(regCC);
                            regPC = ReadWord(vecSWI2);
                            break;
                        case 0x83: //CMPD imm
                            oCMP16(getD(), fetch16());
                            break;
                        case 0x8c: //CMPY imm
                            oCMP16(regY, fetch16());
                            break;
                        case 0x8e: //LDY imm
                            regY = fetch16();
                            flagsNZ16(regY);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0x93: //CMPD direct
                            addr = dpadd();
                            oCMP16(getD(), ReadWord(addr));
                            break;
                        case 0x9c: //CMPY direct
                            addr = dpadd();
                            oCMP16(regY, ReadWord(addr));
                            break;
                        case 0x9e: //LDY direct
                            addr = dpadd();
                            regY = ReadWord(addr);
                            flagsNZ16(regY);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0x9f: //STY direct
                            addr = dpadd();
                            WriteWord(addr, regY);
                            flagsNZ16(regY);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xa3: //CMPD indexed
                            addr = PostByte();
                            oCMP16(getD(), ReadWord(addr));
                            break;
                        case 0xac: //CMPY indexed
                            addr = PostByte();
                            oCMP16(regY, ReadWord(addr));
                            break;
                        case 0xae: //LDY indexed
                            addr = PostByte();
                            regY = ReadWord(addr);
                            flagsNZ16(regY);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xaf: //STY indexed
                            addr = PostByte();
                            WriteWord(addr, regY);
                            flagsNZ16(regY);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xb3: //CMPD extended
                            addr = fetch16();
                            oCMP16(getD(), ReadWord(addr));
                            break;
                        case 0xbc: //CMPY extended
                            addr = fetch16();
                            oCMP16(regY, ReadWord(addr));
                            break;
                        case 0xbe: //LDY extended
                            addr = fetch16();
                            regY = ReadWord(addr);
                            flagsNZ16(regY);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xbf: //STY extended
                            addr = fetch16();
                            WriteWord(addr, regY);
                            flagsNZ16(regY);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xce: //LDS imm
                            regS = fetch16();
                            flagsNZ16(regS);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xde: //LDS direct
                            addr = dpadd();
                            regS = ReadWord(addr);
                            flagsNZ16(regS);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xdf: //STS direct
                            addr = dpadd();
                            WriteWord(addr, regS);
                            flagsNZ16(regS);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xee: //LDS indexed
                            addr = PostByte();
                            regS = ReadWord(addr);
                            flagsNZ16(regS);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xef: //STS indexed
                            addr = PostByte();
                            WriteWord(addr, regS);
                            flagsNZ16(regS);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xfe: //LDS extended
                            addr = fetch16();
                            regS = ReadWord(addr);
                            flagsNZ16(regS);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        case 0xff: //STS extended
                            addr = fetch16();
                            WriteWord(addr, regS);
                            flagsNZ16(regS);
                            regCC = (byte) (regCC & ~F_OVERFLOW);
                            break;
                        default:
                            throw new Exception("CPU_OPCODE_INVALID_PAGE1_" + opcode);
                    }
                    break;
                case 0x11:
                    // page 2
                    opcode = fetch();
                    tickCount += cycles2[opcode];
                    switch (opcode)
                    {
                        case 0x3f: //SWI3
                            Debug.Print("SWI3 is untested!");
                            regCC |= F_ENTIRE;
                            PUSHW(regPC);
                            PUSHW(regU);
                            PUSHW(regY);
                            PUSHW(regX);
                            PUSHB(regDP);
                            PUSHB(regB);
                            PUSHB(regA);
                            PUSHB(regCC);
                            regPC = ReadWord(vecSWI3);
                            break;
                        case 0x83: //CMPU imm
                            oCMP16(regU, fetch16());
                            break;
                        case 0x8c: //CMPS imm
                            oCMP16(regS, fetch16());
                            break;
                        case 0x93: //CMPU imm
                            addr = dpadd();
                            oCMP16(regU, ReadWord(addr));
                            break;
                        case 0x9c: //CMPS imm
                            addr = dpadd();
                            oCMP16(regS, ReadWord(addr));
                            break;
                        case 0xa3: //CMPU imm
                            addr = PostByte();
                            oCMP16(regU, ReadWord(addr));
                            break;
                        case 0xac: //CMPS imm
                            addr = PostByte();
                            oCMP16(regS, ReadWord(addr));
                            break;
                        case 0xb3: //CMPU imm
                            addr = fetch16();
                            oCMP16(regU, ReadWord(addr));
                            break;
                        case 0xbc: //CMPS imm
                            addr = fetch16();
                            oCMP16(regS, ReadWord(addr));
                            break;
                        default:
                            throw new Exception("CPU_OPCODE_INVALID_PAGE2_" + opcode);
                    }
                    break;
                default:
                    throw new Exception("CPU_OPCODE_INVALID_" + opcode);
            }

            regA &= 0xff;
            regB &= 0xff;
            regCC = (byte) (regCC & 0xff);
            regDP &= 0xff;
            regX &= 0xffff;
            regY &= 0xffff;
            regU &= 0xffff;
            regS &= 0xffff;
            regPC &= 0xffff;
            return tickCount - oldTickCount;
        }

        public void reset()
        {
            irqPending = false;
            firqPending = false;

            regDP = 0;
            missedIRQ = 0;
            missedFIRQ = 0;
            irqCount = 0;
            firqCount = 0;
            // disable IRQ and FIRQ
            regCC = F_IRQMASK | F_FIRQMASK;
            regPC = ReadWord(vecRESET);
            Debug.Print("exec reset {0}", regPC);
            tickCount = 0;
        }

        void _executeNmi()
        {
            Debug.Print("exec nmi");
            // Saves entire state and sets E=1 like IRQ,
            // NMI sets F=1 and I=1 disabling FIRQ and IRQ requests.
            PUSHW(regPC);
            PUSHW(regU);
            PUSHW(regY);
            PUSHW(regX);
            PUSHB(regDP);
            PUSHB(regB);
            PUSHB(regA);
            regCC |= F_ENTIRE;
            PUSHB(regCC);
            regCC |= F_IRQMASK | F_FIRQMASK;
            regPC = ReadWord(vecNMI);
            tickCount += 19;
        }

        /*
        FIRQ can be generated in two ways: from the dot matrix controller after a
        certain scanline is redrawn, or from the high-performance timer.  When
        an FIRQ is received, the CPU has to determine which of these occurred
        to determine how to process it.
        */
        void _executeFirq()
        {
            // TODO check if CWAI is pending
            Debug.Print("EXEC_FIRQ {0}", tickCount);
            // clear ENTIRE flag to regCC, used for RTI
            regCC = (byte) (regCC & ~F_ENTIRE);
            PUSHW(regPC);
            PUSHB(regCC);
            //Disable interrupts, Set F,I
            regCC |= F_IRQMASK | F_FIRQMASK;
            regPC = ReadWord(vecFIRQ);
            tickCount += 10;
        }

        void _executeIrq()
        {
            // TODO check if CWAI is pending
            Debug.Print("EXEC_IRQ {0}", tickCount);
            // set ENTIRE flag to regCC, used for RTI
            regCC |= F_ENTIRE;
            PUSHW(regPC);
            PUSHW(regU);
            PUSHW(regY);
            PUSHW(regX);
            PUSHB(regDP);
            PUSHB(regB);
            PUSHB(regA);
            PUSHB(regCC);
            // Disable interrupts, Set I
            regCC |= F_IRQMASK;
            regPC = ReadWord(vecIRQ);
            tickCount += 19;
        }

        public int steps(int ticks = 1)
        {
            int preTickCount = tickCount;
            int ticksToRun = ticks;
            int invalidStepDetected = 0;
            while (ticks > 0)
            {
                int cycles = step();
                if (cycles < 1)
                {
                    Debug.Print("WARNING, invalid step detected: {0}, {1}, {2}, pc: {3}", invalidStepDetected, ticksToRun, ticks, regPC);
                    invalidStepDetected++;
                    ticks--;
                }
                ticks -= cycles;
            }
            if (invalidStepDetected > 0 && ticksToRun > 1 && invalidStepDetected == ticksToRun)
            {
                throw new Exception("INVALID_CPU_STATE_DETECTED");
            }
            return tickCount - preTickCount;
        }

        public State getState()
        {
            return new State
            {
                regPC = regPC,
                regS = regS,
                regU = regU,
                regA = regA,
                regB = regB,
                regX = regX,
                regY = regY,
                regDP = regDP,
                regCC = regCC,
                missedIRQ = missedIRQ,
                missedFIRQ = missedFIRQ,
                irqCount = irqCount,
                firqCount = firqCount,
                nmiCount = nmiCount,
                tickCount = tickCount
            };
        }

        public void setState(State state)
        {
            regS = state.regS;
            regU = state.regU;
            regA = state.regA;
            regB = state.regB;
            regX = state.regX;
            regY = state.regY;
            regDP = state.regDP;
            regCC = state.regCC;
            missedIRQ = state.missedIRQ;
            missedFIRQ = state.missedFIRQ;
            irqCount = state.irqCount;
            firqCount = state.firqCount;
            nmiCount = state.nmiCount;
            tickCount = state.tickCount;
        }

        public void irq()
        {
            // simulate toggle line
            Debug.Print("SCHEDULE_IRQ tickCount: {0}, missedIRQ: {1}", tickCount, missedIRQ);
            irqPending = true;
        }

        public void firq()
        {
            // simulate toggle line
            Debug.Print("SCHEDULE_FIRQ tickCount: {0}, missedFIRQ: {1}", tickCount, missedFIRQ);
            firqPending = true;
        }

        public void nmi()
        {
            Debug.Print("schedule nmi, probably broken as untested");
            nmiCount++;
            _executeNmi();
        }

        public void set(string reg, ushort value)
        {
            switch (reg.ToUpper())
            {
                case "PC":
                    regPC = value;
                    break;
                case "A":
                    regA = (byte)value;
                    break;
                case "B":
                    regB = (byte)value;
                    break;
                case "X":
                    regX = value;
                    break;
                case "Y":
                    regY = value;
                    break;
                case "SP":
                    regS = value;
                    break;
                case "U":
                    regU = value;
                    break;
                case "FLAGS":
                    regCC = (byte) value;
                    break;
                default:
                    Debug.Print("INVALID REGISTER {0}", reg);
                    break;
            }
        }

        public string flagsToString()
        {
            string fx = "EFHINZVC";
            string f = "";
            for (int i = 0; i < 8; i++)
            {
                byte n = (byte) (regCC & (0x80 >> i));
                if (n == 0)
                {
                    f += Char.ToLower(fx[i]);
                    
                }
                else
                {
                    f += fx[i];
                }
            }
            return f;
        }
    }
}