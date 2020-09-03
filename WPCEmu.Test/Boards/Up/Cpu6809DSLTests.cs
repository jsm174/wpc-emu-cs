﻿using System.Collections.Generic;
using NUnit.Framework;
using WPCEmu.Boards.Up;
using System.Linq;
using System.Diagnostics;
using System;

namespace WPCEmu.Test.Boards.Up
{
	[TestFixture]
	public class Cpu6809DSLTests
	{
		struct InstructionStruct
		{
			public ushort op;
			public string instruction;
			public string addressMode;
			public int cycles;
			public string flags;
			public string desc;
		}

		struct AddressValueStruct
		{
			public ushort address;
			public byte value;

			public AddressValueStruct(ushort address, byte value)
			{
				this.address = address;
				this.value = value;
			}
		}

		const byte RESET_VECTOR_VALUE_LO = 0x01;
		const byte RESET_VECTOR_VALUE_HI = 0x02;
		string[] ADDRESSMODE_TO_IGNORE = { "ILLEGAL", "VARIANT" };

		/*
		Source: Description Of The Motorola 6809 Instruction Set by Paul D. Burgin
		| Opcode     |             | Addressing   |               |       |
		| Hex   Dec  | Instruction | Mode         | Cycles  Bytes | HNZVC | Additional cycles
		*/
		const string PAGE0_OPS = @"
+------------+-------------+--------------+-------+-------+-------+
| 00    0000 | NEG         | DIRECT       |   6   |   2   | uaaaa |
| 01    0001 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 02    0002 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 03    0003 | COM         | DIRECT       |   6   |   2   | -aa01 |
| 04    0004 | LSR         | DIRECT       |   6   |   2   | -0a-s |
| 05    0005 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 06    0006 | ROR         | DIRECT       |   6   |   2   | -aa-s |
| 07    0007 | ASR         | DIRECT       |   6   |   2   | uaa-s |
| 08    0008 | LSL/ASL     | DIRECT       |   6   |   2   | naaas |
| 09    0009 | ROL         | DIRECT       |   6   |   2   | -aaas |
| 0A    0010 | DEC         | DIRECT       |   6   |   2   | -aaa- |
| 0B    0011 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 0C    0012 | INC         | DIRECT       |   6   |   2   | -aaa- |
| 0D    0013 | TST         | DIRECT       |   6   |   2   | -aa0- |
| 0E    0014 | JMP         | DIRECT       |   3   |   2   | ----- |
| 0F    0015 | CLR         | DIRECT       |   6   |   2   | -0100 |
| 10    0016 | PAGE1+      | VARIANT      |   1   |   1   | +++++ |
| 11    0017 | PAGE2+      | VARIANT      |   1   |   1   | +++++ |
| 12    0018 | NOP         | INHERENT     |   2   |   1   | ----- |
| 13    0019 | SYNC        | INHERENT     |   2   |   1   | ----- |
| 14    0020 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 15    0021 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 16    0022 | LBRA        | RELATIVE     |   5   |   3   | ----- |
| 17    0023 | LBSR        | RELATIVE     |   9   |   3   | ----- |
| 18    0024 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 19    0025 | DAA         | INHERENT     |   2   |   1   | -aa0a |
| 1A    0026 | ORCC        | IMMEDIATE    |   3   |   2   | ddddd |
| 1B    0027 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 1C    0028 | ANDCC       | IMMEDIATE    |   3   |   2   | ddddd |
//NOTE: fixed overflow flag, not touched!
| 1D    0029 | SEX         | INHERENT     |   2   |   1   | -aa-- |
| 1E    0030 | EXG         | INHERENT     |   8   |   2   | ccccc |
| 1F    0031 | TFR         | INHERENT     |   7   |   2   | ccccc |
| 20    0032 | BRA         | RELATIVE     |   3   |   2   | ----- |
| 21    0033 | BRN         | RELATIVE     |   3   |   2   | ----- |
| 22    0034 | BHI         | RELATIVE     |   3   |   2   | ----- |
| 23    0035 | BLS         | RELATIVE     |   3   |   2   | ----- |
| 24    0036 | BHS/BCC     | RELATIVE     |   3   |   2   | ----- |
| 25    0037 | BLO/BCS     | RELATIVE     |   3   |   2   | ----- |
| 26    0038 | BNE         | RELATIVE     |   3   |   2   | ----- |
| 27    0039 | BEQ         | RELATIVE     |   3   |   2   | ----- |
| 28    0040 | BVC         | RELATIVE     |   3   |   2   | ----- |
| 29    0041 | BVS         | RELATIVE     |   3   |   2   | ----- |
| 2A    0042 | BPL         | RELATIVE     |   3   |   2   | ----- |
| 2B    0043 | BMI         | RELATIVE     |   3   |   2   | ----- |
| 2C    0044 | BGE         | RELATIVE     |   3   |   2   | ----- |
| 2D    0045 | BLT         | RELATIVE     |   3   |   2   | ----- |
| 2E    0046 | BGT         | RELATIVE     |   3   |   2   | ----- |
| 2F    0047 | BLE         | RELATIVE     |   3   |   2   | ----- |
| 30    0048 | LEAX        | INDEXED      |   4   |   2   | --a-- | 1
| 31    0049 | LEAY        | INDEXED      |   4   |   2   | --a-- | 1
| 32    0050 | LEAS        | INDEXED      |   4   |   2   | ----- | 1
| 33    0051 | LEAU        | INDEXED      |   4   |   2   | ----- | 1
| 34    0052 | PSHS        | INHERENT     |   5   |   2   | ----- |
| 35    0053 | PULS        | INHERENT     |   5   |   2   | ccccc |
| 36    0054 | PSHU        | INHERENT     |   5   |   2   | ----- |
| 37    0055 | PULU        | INHERENT     |   5   |   2   | ccccc |
| 38    0056 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 39    0057 | RTS         | INHERENT     |   5   |   1   | ----- |
| 3A    0058 | ABX         | INHERENT     |   3   |   1   | ----- |
//NOTE: Cycle count is 6 or 15 (FIRQ/IRQ)
| 3B    0059 | RTI         | INHERENT     |   6   |   1   | ----- |
| 3C    0060 | CWAI        | INHERENT     |  21   |   2   | ddddd |
| 3D    0061 | MUL         | INHERENT     |  11   |   1   | --a-a |
//NOTE: RESET IS EXCLUDED
| 3E    0062 | RESET*      | ILLEGAL      |   1   |   1   | ***** |
| 3F    0063 | SWI         | INHERENT     |  19   |   1   | ----- |
| 40    0064 | NEGA        | INHERENT     |   2   |   1   | uaaaa |
| 41    0065 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 42    0066 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 43    0067 | COMA        | INHERENT     |   2   |   1   | -aa01 |
| 44    0068 | LSRA        | INHERENT     |   2   |   1   | -0a-s |
| 45    0069 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 46    0070 | RORA        | INHERENT     |   2   |   1   | -aa-s |
| 47    0071 | ASRA        | INHERENT     |   2   |   1   | uaa-s |
| 48    0072 | LSLA/ASLA   | INHERENT     |   2   |   1   | naaas |
| 49    0073 | ROLA        | INHERENT     |   2   |   1   | -aaas |
| 4A    0074 | DECA        | INHERENT     |   2   |   1   | -aaa- |
| 4B    0075 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 4C    0076 | INCA        | INHERENT     |   2   |   1   | -aaa- |
| 4D    0077 | TSTA        | INHERENT     |   2   |   1   | -aa0- |
| 4E    0078 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 4F    0079 | CLRA        | INHERENT     |   2   |   1   | -0100 |
| 50    0080 | NEGB        | INHERENT     |   2   |   1   | uaaaa |
| 51    0081 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 52    0082 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 53    0083 | COMB        | INHERENT     |   2   |   1   | -aa01 |
| 54    0084 | LSRB        | INHERENT     |   2   |   1   | -0a-s |
| 55    0085 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 56    0086 | RORB        | INHERENT     |   2   |   1   | -aa-s |
| 57    0087 | ASRB        | INHERENT     |   2   |   1   | uaa-s |
| 58    0088 | LSLB/ASLB   | INHERENT     |   2   |   1   | naaas |
| 59    0089 | ROLB        | INHERENT     |   2   |   1   | -aaas |
| 5A    0090 | DECB        | INHERENT     |   2   |   1   | -aaa- |
| 5B    0091 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 5C    0092 | INCB        | INHERENT     |   2   |   1   | -aaa- |
| 5D    0093 | TSTB        | INHERENT     |   2   |   1   | -aa0- |
| 5E    0094 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 5F    0095 | CLRB        | INHERENT     |   2   |   1   | -0100 |
| 60    0096 | NEG         | INDEXED      |   6   |   2   | uaaaa | 1
| 61    0097 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 62    0098 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 63    0099 | COM         | INDEXED      |   6   |   2   | -aa01 | 1
| 64    0100 | LSR         | INDEXED      |   6   |   2   | -0a-s | 1
| 65    0101 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 66    0102 | ROR         | INDEXED      |   6   |   2   | -aa-s | 1
| 67    0103 | ASR         | INDEXED      |   6   |   2   | uaa-s | 1
| 68    0104 | LSL/ASL     | INDEXED      |   6   |   2   | naaas | 1
| 69    0105 | ROL         | INDEXED      |   6   |   2   | -aaas | 1
| 6A    0106 | DEC         | INDEXED      |   6   |   2   | -aaa- | 1
| 6B    0107 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 6C    0108 | INC         | INDEXED      |   6   |   2   | -aaa- | 1
| 6D    0109 | TST         | INDEXED      |   6   |   2   | -aa0- | 1
| 6E    0110 | JMP         | INDEXED      |   3   |   2   | ----- | 1
| 6F    0111 | CLR         | INDEXED      |   6   |   2   | -0100 | 1
| 70    0112 | NEG         | EXTENDED     |   7   |   3   | uaaaa |
| 71    0113 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 72    0114 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 73    0115 | COM         | EXTENDED     |   7   |   3   | -aa01 |
| 74    0116 | LSR         | EXTENDED     |   7   |   3   | -0a-s |
| 75    0117 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 76    0118 | ROR         | EXTENDED     |   7   |   3   | -aa-s |
| 77    0119 | ASR         | EXTENDED     |   7   |   3   | uaa-s |
| 78    0120 | LSL/ASL     | EXTENDED     |   7   |   3   | naaas |
| 79    0121 | ROL         | EXTENDED     |   7   |   3   | -aaas |
| 7A    0122 | DEC         | EXTENDED     |   7   |   3   | -aaa- |
| 7B    0123 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 7C    0124 | INC         | EXTENDED     |   7   |   3   | -aaa- |
| 7D    0125 | TST         | EXTENDED     |   7   |   3   | -aa0- |
//NOTE: FIXED CYCLE COUNT TO 4
| 7E    0126 | JMP         | EXTENDED     |   4   |   3   | ----- |
| 7F    0127 | CLR         | EXTENDED     |   7   |   3   | -0100 |
| 80    0128 | SUBA        | IMMEDIATE    |   2   |   2   | uaaaa |
| 81    0129 | CMPA        | IMMEDIATE    |   2   |   2   | uaaaa |
| 82    0130 | SBCA        | IMMEDIATE    |   2   |   2   | uaaaa |
| 83    0131 | SUBD        | IMMEDIATE    |   4   |   3   | -aaaa |
| 84    0132 | ANDA        | IMMEDIATE    |   2   |   2   | -aa0- |
| 85    0133 | BITA        | IMMEDIATE    |   2   |   2   | -aa0- |
| 86    0134 | LDA         | IMMEDIATE    |   2   |   2   | -aa0- |
| 87    0135 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 88    0136 | EORA        | IMMEDIATE    |   2   |   2   | -aa0- |
| 89    0137 | ADCA        | IMMEDIATE    |   2   |   2   | aaaaa |
| 8A    0138 | ORA         | IMMEDIATE    |   2   |   2   | -aa0- |
| 8B    0139 | ADDA        | IMMEDIATE    |   2   |   2   | aaaaa |
| 8C    0140 | CMPX        | IMMEDIATE    |   4   |   3   | -aaaa |
| 8D    0141 | BSR         | RELATIVE     |   7   |   2   | ----- |
| 8E    0142 | LDX         | IMMEDIATE    |   3   |   3   | -aa0- |
| 8F    0143 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| 90    0144 | SUBA        | DIRECT       |   4   |   2   | uaaaa |
| 91    0145 | CMPA        | DIRECT       |   4   |   2   | uaaaa |
| 92    0146 | SBCA        | DIRECT       |   4   |   2   | uaaaa |
| 93    0147 | SUBD        | DIRECT       |   6   |   2   | -aaaa |
| 94    0148 | ANDA        | DIRECT       |   4   |   2   | -aa0- |
| 95    0149 | BITA        | DIRECT       |   4   |   2   | -aa0- |
| 96    0150 | LDA         | DIRECT       |   4   |   2   | -aa0- |
| 97    0151 | STA         | DIRECT       |   4   |   2   | -aa0- |
| 98    0152 | EORA        | DIRECT       |   4   |   2   | -aa0- |
| 99    0153 | ADCA        | DIRECT       |   4   |   2   | aaaaa |
| 9A    0154 | ORA         | DIRECT       |   4   |   2   | -aa0- |
| 9B    0155 | ADDA        | DIRECT       |   4   |   2   | aaaaa |
| 9C    0156 | CMPX        | DIRECT       |   6   |   2   | -aaaa |
| 9D    0157 | JSR         | DIRECT       |   7   |   2   | ----- |
| 9E    0158 | LDX         | DIRECT       |   5   |   2   | -aa0- |
| 9F    0159 | STX         | DIRECT       |   5   |   2   | -aa0- |
| A0    0160 | SUBA        | INDEXED      |   4   |   2   | uaaaa | 1
| A1    0161 | CMPA        | INDEXED      |   4   |   2   | uaaaa | 1
| A2    0162 | SBCA        | INDEXED      |   4   |   2   | uaaaa | 1
| A3    0163 | SUBD        | INDEXED      |   6   |   2   | -aaaa | 1
| A4    0164 | ANDA        | INDEXED      |   4   |   2   | -aa0- | 1
| A5    0165 | BITA        | INDEXED      |   4   |   2   | -aa0- | 1
| A6    0166 | LDA         | INDEXED      |   4   |   2   | -aa0- | 1
| A7    0167 | STA         | INDEXED      |   4   |   2   | -aa0- | 1
| A8    0168 | EORA        | INDEXED      |   4   |   2   | -aa0- | 1
| A9    0169 | ADCA        | INDEXED      |   4   |   2   | aaaaa | 1
| AA    0170 | ORA         | INDEXED      |   4   |   2   | -aa0- | 1
| AB    0171 | ADDA        | INDEXED      |   4   |   2   | aaaaa | 1
| AC    0172 | CMPX        | INDEXED      |   6   |   2   | -aaaa | 1
| AD    0173 | JSR         | INDEXED      |   7   |   2   | ----- | 1
| AE    0174 | LDX         | INDEXED      |   5   |   2   | -aa0- | 1
| AF    0175 | STX         | INDEXED      |   5   |   2   | -aa0- | 1
| B0    0176 | SUBA        | EXTENDED     |   5   |   3   | uaaaa |
| B1    0177 | CMPA        | EXTENDED     |   5   |   3   | uaaaa |
| B2    0178 | SBCA        | EXTENDED     |   5   |   3   | uaaaa |
| B3    0179 | SUBD        | EXTENDED     |   7   |   3   | -aaaa |
| B4    0180 | ANDA        | EXTENDED     |   5   |   3   | -aa0- |
| B5    0181 | BITA        | EXTENDED     |   5   |   3   | -aa0- |
| B6    0182 | LDA         | EXTENDED     |   5   |   3   | -aa0- |
| B7    0183 | STA         | EXTENDED     |   5   |   3   | -aa0- |
| B8    0184 | EORA        | EXTENDED     |   5   |   3   | -aa0- |
| B9    0185 | ADCA        | EXTENDED     |   5   |   3   | aaaaa |
| BA    0186 | ORA         | EXTENDED     |   5   |   3   | -aa0- |
| BB    0187 | ADDA        | EXTENDED     |   5   |   3   | aaaaa |
| BC    0188 | CMPX        | EXTENDED     |   7   |   3   | -aaaa |
| BD    0189 | JSR         | EXTENDED     |   8   |   3   | ----- |
| BE    0190 | LDX         | EXTENDED     |   6   |   3   | -aa0- |
| BF    0191 | STX         | EXTENDED     |   6   |   3   | -aa0- |
| C0    0192 | SUBB        | IMMEDIATE    |   2   |   2   | uaaaa |
| C1    0193 | CMPB        | IMMEDIATE    |   2   |   2   | uaaaa |
| C2    0194 | SBCB        | IMMEDIATE    |   2   |   2   | uaaaa |
| C3    0195 | ADDD        | IMMEDIATE    |   4   |   3   | -aaaa |
| C4    0196 | ANDB        | IMMEDIATE    |   2   |   2   | -aa0- |
| C5    0197 | BITB        | IMMEDIATE    |   2   |   2   | -aa0- |
| C6    0198 | LDB         | IMMEDIATE    |   2   |   2   | -aa0- |
| C7    0199 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| C8    0200 | EORB        | IMMEDIATE    |   2   |   2   | -aa0- |
| C9    0201 | ADCB        | IMMEDIATE    |   2   |   2   | aaaaa |
| CA    0202 | ORB         | IMMEDIATE    |   2   |   2   | -aa0- |
| CB    0203 | ADDB        | IMMEDIATE    |   2   |   2   | aaaaa |
| CC    0204 | LDD         | IMMEDIATE    |   3   |   3   | -aa0- |
| CD    0205 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| CE    0206 | LDU         | IMMEDIATE    |   3   |   3   | -aa0- |
| CF    0207 | ILLEGAL     | ILLEGAL      |   1   |   1   | uuuuu |
| D0    0208 | SUBB        | DIRECT       |   4   |   2   | uaaaa |
| D1    0209 | CMPB        | DIRECT       |   4   |   2   | uaaaa |
| D2    0210 | SBCB        | DIRECT       |   4   |   2   | uaaaa |
| D3    0211 | ADDD        | DIRECT       |   6   |   2   | -aaaa |
| D4    0212 | ANDB        | DIRECT       |   4   |   2   | -aa0- |
| D5    0213 | BITB        | DIRECT       |   4   |   2   | -aa0- |
| D6    0214 | LDB         | DIRECT       |   4   |   2   | -aa0- |
| D7    0215 | STB         | DIRECT       |   4   |   2   | -aa0- |
| D8    0216 | EORB        | DIRECT       |   4   |   2   | -aa0- |
| D9    0217 | ADCB        | DIRECT       |   4   |   2   | aaaaa |
| DA    0218 | ORB         | DIRECT       |   4   |   2   | -aa0- |
| DB    0219 | ADDB        | DIRECT       |   4   |   2   | aaaaa |
| DC    0220 | LDD         | DIRECT       |   5   |   2   | -aa0- |
| DD    0221 | STD         | DIRECT       |   5   |   2   | -aa0- |
| DE    0222 | LDU         | DIRECT       |   5   |   2   | -aa0- |
| DF    0223 | STU         | DIRECT       |   5   |   2   | -aa0- |
| E0    0224 | SUBB        | INDEXED      |   4   |   2   | uaaaa | 1
| E1    0225 | CMPB        | INDEXED      |   4   |   2   | uaaaa | 1
| E2    0226 | SBCB        | INDEXED      |   4   |   2   | uaaaa | 1
| E3    0227 | ADDD        | INDEXED      |   6   |   2   | -aaaa | 1
| E4    0228 | ANDB        | INDEXED      |   4   |   2   | -aa0- | 1
| E5    0229 | BITB        | INDEXED      |   4   |   2   | -aa0- | 1
| E6    0230 | LDB         | INDEXED      |   4   |   2   | -aa0- | 1
| E7    0231 | STB         | INDEXED      |   4   |   2   | -aa0- | 1
| E8    0232 | EORB        | INDEXED      |   4   |   2   | -aa0- | 1
| E9    0233 | ADCB        | INDEXED      |   4   |   2   | aaaaa | 1
| EA    0234 | ORB         | INDEXED      |   4   |   2   | -aa0- | 1
| EB    0235 | ADDB        | INDEXED      |   4   |   2   | aaaaa | 1
| EC    0236 | LDD         | INDEXED      |   5   |   2   | -aa0- | 1
| ED    0237 | STD         | INDEXED      |   5   |   2   | -aa0- | 1
| EE    0238 | LDU         | INDEXED      |   5   |   2   | -aa0- | 1
| EF    0239 | STU         | INDEXED      |   5   |   2   | -aa0- | 1
| F0    0240 | SUBB        | EXTENDED     |   5   |   3   | uaaaa |
| F1    0241 | CMPB        | EXTENDED     |   5   |   3   | uaaaa |
| F2    0242 | SBCB        | EXTENDED     |   5   |   3   | uaaaa |
| F3    0243 | ADDD        | EXTENDED     |   7   |   3   | -aaaa |
| F4    0244 | ANDB        | EXTENDED     |   5   |   3   | -aa0- |
| F5    0245 | BITB        | EXTENDED     |   5   |   3   | -aa0- |
| F6    0246 | LDB         | EXTENDED     |   5   |   3   | -aa0- |
| F7    0247 | STB         | EXTENDED     |   5   |   3   | -aa0- |
| F8    0248 | EORB        | EXTENDED     |   5   |   3   | -aa0- |
| F9    0249 | ADCB        | EXTENDED     |   5   |   3   | aaaaa |
| FA    0250 | ORB         | EXTENDED     |   5   |   3   | -aa0- |
| FB    0251 | ADDB        | EXTENDED     |   5   |   3   | aaaaa |
| FC    0252 | LDD         | EXTENDED     |   6   |   3   | -aa0- |
| FD    0253 | STD         | EXTENDED     |   6   |   3   | -aa0- |
| FE    0254 | LDU         | EXTENDED     |   6   |   3   | -aa0- |
| FF    0255 | STU         | EXTENDED     |   6   |   3   | -aa0- |
+------------+-------------+--------------+-------+-------+-------+";

		/*
		Source: Description Of The Motorola 6809 Instruction Set by Paul D. Burgin
		| Opcode     |             | Addressing   |               |       |
		| Hex   Dec  | Instruction | Mode         | Cycles  Bytes | HNZVC | Additional cycles
		*/
		const string PAGE1_OPS = @"
+------------+-------------+--------------+-------+-------+-------+
| 1021  4129 | LBRN        | RELATIVE     |   5   |   4   | ----- |
| 1022  4130 | LBHI        | RELATIVE     |   6   |   4   | ----- |
| 1023  4131 | LBLS        | RELATIVE     |   5   |   4   | ----- |
| 1024  4132 | LBHS/LBCC   | RELATIVE     |   6   |   4   | ----- |
| 1025  4133 | LBLO/LBCS   | RELATIVE     |   5   |   4   | ----- |
| 1026  4134 | LBNE        | RELATIVE     |   6   |   4   | ----- |
| 1027  4135 | LBEQ        | RELATIVE     |   5   |   4   | ----- |
| 1028  4136 | LBVC        | RELATIVE     |   6   |   4   | ----- |
| 1029  4137 | LBVS        | RELATIVE     |   5   |   4   | ----- |
| 102A  4138 | LBPL        | RELATIVE     |   6   |   4   | ----- |
| 102B  4139 | LBMI        | RELATIVE     |   5   |   4   | ----- |
| 102C  4140 | LBGE        | RELATIVE     |   6   |   4   | ----- |
| 102D  4141 | LBLT        | RELATIVE     |   5   |   4   | ----- |
| 102E  4142 | LBGT        | RELATIVE     |   6   |   4   | ----- |
| 102F  4143 | LBLE        | RELATIVE     |   5   |   4   | ----- |
| 103F  4159 | SWI2        | INHERENT     |  20   |   2   | ----- |
| 1083  4227 | CMPD        | IMMEDIATE    |   5   |   4   | -aaaa |
| 108C  4236 | CMPY        | IMMEDIATE    |   5   |   4   | -aaaa |
| 108E  4238 | LDY         | IMMEDIATE    |   4   |   4   | -aa0- |
| 1093  4243 | CMPD        | DIRECT       |   7   |   3   | -aaaa |
| 109C  4252 | CMPY        | DIRECT       |   7   |   3   | -aaaa |
| 109E  4254 | LDY         | DIRECT       |   6   |   3   | -aa0- |
| 109F  4255 | STY         | DIRECT       |   6   |   3   | -aa0- |
| 10A3  4259 | CMPD        | INDEXED      |   7   |   3   | -aaaa | 1
| 10AC  4268 | CMPY        | INDEXED      |   7   |   3   | -aaaa | 1
| 10AE  4270 | LDY         | INDEXED      |   6   |   3   | -aa0- | 1
| 10AF  4271 | STY         | INDEXED      |   6   |   3   | -aa0- | 1
| 10B3  4275 | CMPD        | EXTENDED     |   8   |   4   | -aaaa |
| 10BC  4284 | CMPY        | EXTENDED     |   8   |   4   | -aaaa |
| 10BE  4286 | LDY         | EXTENDED     |   7   |   4   | -aa0- |
| 10BF  4287 | STY         | EXTENDED     |   7   |   4   | -aa0- |
| 10CE  4302 | LDS         | IMMEDIATE    |   4   |   4   | -aa0- |
| 10DE  4318 | LDS         | DIRECT       |   6   |   3   | -aa0- |
| 10DF  4319 | STS         | DIRECT       |   6   |   3   | -aa0- |
| 10EE  4334 | LDS         | INDEXED      |   6   |   3   | -aa0- | 1
| 10EF  4335 | STS         | INDEXED      |   6   |   3   | -aa0- | 1
| 10FE  4350 | LDS         | EXTENDED     |   7   |   4   | -aa0- |
| 10FF  4351 | STS         | EXTENDED     |   7   |   4   | -aa0- |
+------------+-------------+--------------+-------+-------+-------+";


		/*
		Source: Description Of The Motorola 6809 Instruction Set by Paul D. Burgin
		| Opcode     |             | Addressing   |               |       |
		| Hex   Dec  | Instruction | Mode         | Cycles  Bytes | HNZVC | Additional cycles
		*/
		const string PAGE2_OPS = @"
+------------+-------------+--------------+-------+-------+-------+
| 113F  4415 | SWI3        | INHERENT     |  20   |   2   | ----- |
| 1183  4438 | CMPU        | IMMEDIATE    |   5   |   4   | -aaaa |
| 118C  4492 | CMPS        | IMMEDIATE    |   5   |   4   | -aaaa |
| 1193  4499 | CMPU        | DIRECT       |   7   |   3   | -aaaa |
| 119C  4508 | CMPS        | DIRECT       |   7   |   3   | -aaaa |
| 11A3  4515 | CMPU        | INDEXED      |   7   |   3   | -aaaa | 1
| 11AC  4524 | CMPS        | INDEXED      |   7   |   3   | -aaaa | 1
| 11B3  4531 | CMPU        | EXTENDED     |   8   |   4   | -aaaa |
| 11BC  4540 | CMPS        | EXTENDED     |   8   |   4   | -aaaa |
+------------+-------------+--------------+-------+-------+-------+";

		/*
			a Affected.
			- Unaffected.
			u Undefined.
			d Changed directly.
			s Contains the carry from a shift operation.
			c Affected only if CC register selected.
			n Unaffected by LSL, undefined by ASL (according to Motorola)!
		*/

		const char FLAG_CLEAR = '0';
		const char FLAG_SET = '1';
		const char FLAG_UNAFFECTED = '-';
		//TODO add missing flags d s c n

		//HNZVC
		byte[] EXPECTED_FLAG_MAP = {
			32, //F_HALFCARRY
			8, //F_NEGATIVE
			4, //F_ZERO
			2, //F_OVERFLOW
			1, //F_CARRY
		};

		List<ushort> readMemoryAddressAccess;
		List<byte> readMemoryAddress;
		List<AddressValueStruct> writeMemoryAddress;

		Cpu6809 cpu;

		byte ReadMemoryMock(ushort address)
		{
			readMemoryAddressAccess.Add(address);

			if (readMemoryAddress.Count == 0)
			{
				return 0xFF;
			}

			byte value = readMemoryAddress[(readMemoryAddress.Count - 1)];
			readMemoryAddress.RemoveAt(readMemoryAddress.Count - 1);
			return value;
		}

		void WriteMemoryMock(ushort address, byte value)
		{
			writeMemoryAddress.Add(new AddressValueStruct(address, value));
		}

		[SetUp]
		public void Init()
		{
			readMemoryAddressAccess = new List<ushort>();
			readMemoryAddress = new List<byte>();
			writeMemoryAddress = new List<AddressValueStruct>();

			cpu = Cpu6809.GetInstance(WriteMemoryMock, ReadMemoryMock);
		}

		private void flagCheckTest(InstructionStruct testData)
		{
			cpu.reset();
			cpu.set("flags", 0xFF);
			cpu.step();

			for (var x = 0; x < testData.flags.Length; x++)
			{
				char flag = testData.flags[x];
				byte mask = EXPECTED_FLAG_MAP[x];
				switch (flag)
				{
					case FLAG_CLEAR:
						Assert.AreEqual(0, cpu.regCC & mask);
						break;
					case FLAG_SET:
						Assert.IsTrue((cpu.regCC & mask) > 0);
						break;
					case FLAG_UNAFFECTED:
						byte unaffectedFlag = (byte)(cpu.regCC & mask);
						Debug.Print("offset: {0}, {1}, mask: {2}", x, unaffectedFlag, mask);
						Assert.IsTrue(unaffectedFlag > 0);
						break;
					default:
						break;
				}
			}
		}

		[Test, Order(1)]
		public void Page0_OPS()
		{
			void runCyclecountTest(InstructionStruct testData, ushort flags)
			{
				// add command in reverse order
				readMemoryAddress = new List<byte>()
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					(byte) testData.op, RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
				};

				cpu.reset();
				cpu.set("flags", flags);
				cpu.step();
				Assert.AreEqual(cpu.tickCount, testData.cycles);
			}

			marshall(PAGE0_OPS).ForEach(delegate (InstructionStruct testData)
			{
				Debug.Print("PAGE0 CYCLECOUNT(flags 0x00): 0x{0}: {1}", testData.desc, testData.cycles);
				runCyclecountTest(testData, 0x00);

				Debug.Print("PAGE0 CYCLECOUNT(flags 0xFF): 0x{0}: {1}", testData.desc, testData.cycles);
				Init();
				runCyclecountTest(testData, 0xFF);

				Debug.Print("PAGE0 FLAGCHECK: 0x{0}: {1}", testData.desc, testData.flags);
				Init();
				// add command in reverse order
				readMemoryAddress = new List<byte>()
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					(byte) testData.op, RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
				};
				if (testData.op == 0x3B)
				{
					//RTI does a pullb
					readMemoryAddress[7] = 0xFF;
				}
				flagCheckTest(testData);
			});
		}

		[Test, Order(2)]
		public void Page1_OPS()
		{
			void runCyclecountTest(InstructionStruct testData, ushort flags, int expectedTickCount)
			{
				const byte OP_0X10_OPCODE_CYCLE = 1;
				// add command in reverse order
				readMemoryAddress = new List<byte>()
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					(byte) (testData.op & 0xFF), (byte) ((testData.op >> 8) & 0xFF), RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
				};

				cpu.reset();
				cpu.set("flags", flags);
				cpu.step();
				Assert.AreEqual(cpu.tickCount - OP_0X10_OPCODE_CYCLE, expectedTickCount);
			}

			marshall(PAGE1_OPS).ForEach(delegate (InstructionStruct testData)
			{
				Debug.Print("PAGE1 CYCLECOUNT(flags 0x00): 0x{0}: {1}", testData.desc, testData.cycles);
				Init();
				runCyclecountTest(testData, 0x00, testData.cycles);

				Debug.Print("PAGE1 CYCLECOUNT(flags 0xFF): 0x{0}: {1}", testData.desc, testData.cycles);
				Init();
				int expectedTickCount = testData.op > 0x1021 && testData.op < 0x103F ?
				(testData.cycles == 5) ? 6 : 5 :
				testData.cycles;
				if (testData.op == 0x102C || testData.op == 0x102D)
				{
					runCyclecountTest(testData, 0xF7, expectedTickCount);
				}
				else
				{
					runCyclecountTest(testData, 0xFF, expectedTickCount);
				}

				Debug.Print("PAGE1 FLAGCHECK: 0x{0}: {1}", testData.desc, testData.flags);
				Init();
				// add command in reverse order
				readMemoryAddress = new List<byte>()
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					(byte) (testData.op & 0xFF), (byte) ((testData.op >> 8) & 0xFF), RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
				};
				flagCheckTest(testData);
			});
		}

		[Test, Order(3)]
		public void Page2_OPS()
		{
			void runCyclecountTest(InstructionStruct testData, ushort flags)
			{
				const byte OP_0X11_OPCODE_CYCLE = 1;
				// add command in reverse order
				readMemoryAddress = new List<byte>()
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					(byte) (testData.op & 0xFF), (byte) ((testData.op >> 8) & 0xFF), RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
				};

				cpu.reset();
				cpu.set("flags", flags);
				cpu.step();
				Assert.AreEqual(cpu.tickCount - OP_0X11_OPCODE_CYCLE, testData.cycles);
			}

			marshall(PAGE2_OPS).ForEach(delegate (InstructionStruct testData)
			{
				Debug.Print("PAGE2 CYCLECOUNT(flags 0x00): 0x{0}: {1}", testData.desc, testData.cycles);
				Init();
				runCyclecountTest(testData, 0x00);

				Debug.Print("PAGE2 CYCLECOUNT(flags 0xFF): 0x{0}: {1}", testData.desc, testData.cycles);
				Init();
				runCyclecountTest(testData, 0xFF);

				Debug.Print("PAGE2 FLAGCHECK: 0x{0}: {1}", testData.desc, testData.flags);
				Init();
				// add command in reverse order
				readMemoryAddress = new List<byte>()
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					(byte) (testData.op & 0xFF), (byte) ((testData.op >> 8) & 0xFF), RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
				};
				flagCheckTest(testData);
			});
		}

		private List<InstructionStruct> marshall(string instructions)
		{
			List<InstructionStruct> list = new List<InstructionStruct>();

			foreach (string line in instructions.Split("\n"))
			{
				if (line.StartsWith("|"))
				{
					string[] junks = line.Split("|");
					string addressMode = junks[3].Trim();

					if (!ADDRESSMODE_TO_IGNORE.Any(addressMode.Contains))
					{
						InstructionStruct instructionStruct = new InstructionStruct();
						instructionStruct.op = (ushort)Convert.ToUInt16(junks[1].Trim().Split(" ")[0], 16);
						instructionStruct.instruction = junks[2].Trim();
						instructionStruct.addressMode = junks[3].Trim();
						instructionStruct.cycles = Convert.ToInt32(junks[4].Trim(), 10);
						if (junks[7].Length > 0)
						{
							instructionStruct.cycles += Convert.ToInt32(junks[7].Trim(), 10);
						}
						instructionStruct.flags = junks[6].Trim();
						instructionStruct.desc = (junks[1] + junks[2] + junks[3]).Trim();

						list.Add(instructionStruct);
					}
				}
			}

			return list;
		}
	}
}
