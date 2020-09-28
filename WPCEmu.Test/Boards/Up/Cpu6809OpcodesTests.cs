using System;
using System.Collections.Generic;
using NUnit.Framework;
using WPCEmu.Boards.Up;

namespace WPCEmu.Test.Boards.Up
{
	[TestFixture]
	public class Cpu6809OpcodesTests
	{
		struct AddressValueData
		{
			public ushort address;
			public byte value;
		}

		struct ComplexData
		{
			public byte offset;
			public string register;
			public ushort? initialValue;
			public byte? initialRegB;
			public ushort expectedResult;
			public int expectedTicks;
			public ushort expectedReturn;
			public ushort[] expectedMemoryRead;

			public ComplexData(byte offset, string register, ushort? initialValue, byte? initialRegB, ushort expectedResult, int expectedTicks, ushort expectedReturn, ushort[] expectedMemoryRead)
			{
				this.offset = offset;
				this.register = register;
				this.initialValue = initialValue;
				this.initialRegB = initialRegB;
				this.expectedResult = expectedResult;
				this.expectedTicks = expectedTicks;
				this.expectedReturn = expectedReturn;
				this.expectedMemoryRead = expectedMemoryRead;
			}
		}

		const byte RESET_VECTOR_VALUE_LO = 0x01;
		const byte RESET_VECTOR_VALUE_HI = 0x02;

		const ushort EXPECTED_RESET_READ_OFFSET_LO = 0x201;
		const ushort EXPECTED_RESET_READ_OFFSET_HI = 0x202;

		const ushort RESET_VECTOR_OFFSET_LO = 0xFFFE;
		const ushort RESET_VECTOR_OFFSET_HI = 0xFFFF;

		List<ushort> readMemoryAddressAccess;
		List<byte> readMemoryAddress;
		List<AddressValueData> writeMemoryAddress;

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
			writeMemoryAddress.Add(new AddressValueData
			{
				address = address,
				value = value
			});
		}

		[SetUp]
		public void Init()
		{
			readMemoryAddressAccess = new List<ushort>();
			readMemoryAddress = new List<byte>();
			writeMemoryAddress = new List<AddressValueData>();

			cpu = Cpu6809.GetInstance(WriteMemoryMock, ReadMemoryMock);
		}

		[Test, Order(1)]
		public void ShouldReadResetVectorOnBoot()
		{
			TestContext.WriteLine("should read RESET vector on boot");

			cpu.reset();
			Assert.AreEqual(RESET_VECTOR_OFFSET_LO, readMemoryAddressAccess[0]);
			Assert.AreEqual(RESET_VECTOR_OFFSET_HI, readMemoryAddressAccess[1]);
		}

		[Test, Order(2)]
		public void ROLA_0xFF()
		{
			TestContext.WriteLine("ROLA 0xFF");

			const byte OP_ROLA = 0x49;
			runRegisterATest(OP_ROLA, 0xFF);

			Assert.AreEqual(0xFE, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhINzvC", cpu.flagsToString());
		}

		[Test, Order(3)]
		public void ROLA_0xFF_CarryFlagSet()
		{
			TestContext.WriteLine("ROLA, 0xFF - carry flag set");

			const byte OP_ROLA = 0x49;
			runRegisterATest(OP_ROLA, 0xFF, () =>
			{
				byte cc = cpu.regCC |= 1;
				cpu.set("flags", cc);
			});

			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhINzvC", cpu.flagsToString());
		}

		[Test, Order(4)]
		public void RORA_0x01_NoOverflow()
		{
			TestContext.WriteLine("RORA, 0x01 (no overflow)");

			const byte OP_RORA = 0x46;
			runRegisterATest(OP_RORA, 0x01);

			Assert.AreEqual(0x00, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInZvC", cpu.flagsToString());
		}

		[Test, Order(5)]
		public void RORA_0xFF_CarryFlagNotSet()
		{
			TestContext.WriteLine("RORA, 0xFF - carry flag not set");

			const byte OP_RORA = 0x46;
			runRegisterATest(OP_RORA, 0xFF);

			Assert.AreEqual(0x7F, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzvC", cpu.flagsToString());
		}

		[Test, Order(6)]
		public void RORA_0xFF_CarryFlagSet()
		{
			TestContext.WriteLine("RORA, 0xFF - carry flag set");

			const byte OP_RORA = 0x46;
			runRegisterATest(OP_RORA, 0xFF, () =>
			{
				byte cc = cpu.regCC |= 1;
				cpu.set("flags", cc);
			});

			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhINzvC", cpu.flagsToString());
		}

		[Test, Order(7)]
		public void ADDA_oADD()
		{
			TestContext.WriteLine("ADDA / oADD");

			const byte OP_ADDA = 0x8B;
			const byte ADD_VALUE_1 = 0xFF;
			const byte ADD_VALUE_2 = 0xFF;

			cpu.set("flags", 0x00);

			// add command in reverse order
			readMemoryAddress = new List<byte>()
			{
				ADD_VALUE_2, OP_ADDA, RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
			};

			cpu.reset();
			cpu.regA = ADD_VALUE_1;
			cpu.step();

			Assert.AreEqual(RESET_VECTOR_OFFSET_LO, readMemoryAddressAccess[0]);
			Assert.AreEqual(RESET_VECTOR_OFFSET_HI, readMemoryAddressAccess[1]);
			Assert.AreEqual(EXPECTED_RESET_READ_OFFSET_LO, readMemoryAddressAccess[2]);
			Assert.AreEqual(EXPECTED_RESET_READ_OFFSET_HI, readMemoryAddressAccess[3]);

			Assert.AreEqual(254, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFHINzvC", cpu.flagsToString());
		}

		[Test, Order(8)]
		public void LSRA()
		{
			TestContext.WriteLine("LSRA");

			const byte OP_LSRA = 0x44;
			runRegisterATest(OP_LSRA, 0xFF);

			Assert.AreEqual(0x7F, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzvC", cpu.flagsToString());
		}

		[Test, Order(9)]
		public void ASLA_Overflow()
		{
			TestContext.WriteLine("ASLA, overflow");

			const byte OP_ASLA = 0x48;
			runRegisterATest(OP_ASLA, 0x81);

			Assert.AreEqual(2, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzVC", cpu.flagsToString());
		}

		[Test, Order(10)]
		public void ASLA_NoOverflow()
		{
			TestContext.WriteLine("ASLA, no overflow");

			const byte OP_ASLA = 0x48;
			runRegisterATest(OP_ASLA, 0x01);

			Assert.AreEqual(2, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzvc", cpu.flagsToString());
		}

		[Test, Order(11)]
		public void ASRA_0xFF()
		{
			TestContext.WriteLine("ASRA (0xFF)");

			const byte OP_ASRA = 0x47;
			runRegisterATest(OP_ASRA, 0xFF);

			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhINzvC", cpu.flagsToString());
		}

		[Test, Order(12)]
		public void ASRA_0x7F()
		{
			TestContext.WriteLine("ASRA (0x7F)");

			const byte OP_ASRA = 0x47;
			runRegisterATest(OP_ASRA, 0x7F);

			Assert.AreEqual(0x3F, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzvC", cpu.flagsToString());
		}

		[Test, Order(13)]
		public void ASRA_0()
		{
			TestContext.WriteLine("ASRA (0)");

			const byte OP_ASRA = 0x47;
			runRegisterATest(OP_ASRA, 0);

			Assert.AreEqual(0, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInZvc", cpu.flagsToString());
		}

		[Test, Order(14)]
		public void oNEG_0x1()
		{
			TestContext.WriteLine("oNEG, 0x1");

			const byte OP_NEG = 0x40;
			runRegisterATest(OP_NEG, 0x01);

			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhINzvC", cpu.flagsToString());
		}

		[Test, Order(15)]
		public void oNEG_0xFF()
		{
			TestContext.WriteLine("oNEG, 0xFF");

			const byte OP_NEG = 0x40;
			runRegisterATest(OP_NEG, 0xFF);

			Assert.AreEqual(0x01, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzvc", cpu.flagsToString());
		}

		[Test, Order(16)]
		public void oDEC_0x80_NoOverflow()
		{
			TestContext.WriteLine("oDEC, 0x80 (no overflow)");

			const byte OP_DEC = 0x4A;
			runRegisterATest(OP_DEC, 0x80);

			Assert.AreEqual(0x7F, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzVc", cpu.flagsToString());
		}

		[Test, Order(17)]
		public void oDEC_0x0_Overflow()
		{
			TestContext.WriteLine("oDEC, 0x0 (overflow)");

			const byte OP_DEC = 0x4A;
			runRegisterATest(OP_DEC, 0x00);

			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhINzvc", cpu.flagsToString());
		}

		[Test, Order(18)]
		public void oDEC_ExtendedMemory_0x0_Overflow()
		{
			TestContext.WriteLine("oDEC extended memory, 0x0 (overflow)");

			const byte OP_DEC = 0x7A;
			runExtendedMemoryTest(OP_DEC, 0x00);

			Assert.AreEqual(8721, writeMemoryAddress[0].address);
			Assert.AreEqual(0xFF, writeMemoryAddress[0].value);
			Assert.AreEqual(7, cpu.tickCount);
			Assert.AreEqual("eFhINzvc", cpu.flagsToString());
		}

		[Test, Order(19)]
		public void oINC_0x00_NoOverflow()
		{
			TestContext.WriteLine("oINC, 0x00 (no overflow)");

			const byte OP_INC = 0x4C;
			runRegisterATest(OP_INC, 0x00);

			Assert.AreEqual(0x01, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInzvc", cpu.flagsToString());
		}

		[Test, Order(20)]
		public void oINC_0xFF_Overflow()
		{
			TestContext.WriteLine("oINC, 0xFF (overflow)");

			const byte OP_INC = 0x4C;
			runRegisterATest(OP_INC, 0xFF);

			Assert.AreEqual(0x00, cpu.regA);
			Assert.AreEqual(2, cpu.tickCount);
			Assert.AreEqual("eFhInZvc", cpu.flagsToString());
		}

		[Test, Order(21)]
		public void oINC_ExtendedMemory_0xFF_Overflow()
		{
			TestContext.WriteLine("oINC extended memory, 0xFF (overflow)");

			const byte OP_INC = 0x7C;
			runExtendedMemoryTest(OP_INC, 0xFF);

			Assert.AreEqual(8721, writeMemoryAddress[0].address);
			Assert.AreEqual(0x00, writeMemoryAddress[0].value);
			Assert.AreEqual(7, cpu.tickCount);
			Assert.AreEqual("eFhInZvc", cpu.flagsToString());
		}

		[Test, Order(22)]
		public void PUSHB_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHB should wrap around");

			cpu.reset();
			cpu.PUSHB(0x23);
			Assert.AreEqual(65535, writeMemoryAddress[0].address);
			Assert.AreEqual(0x23, writeMemoryAddress[0].value);
		}

		[Test, Order(23)]
		public void PUSHW_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHW should wrap around");

			cpu.reset();
			cpu.PUSHW(0x1234);
			Assert.AreEqual(65535, writeMemoryAddress[0].address);
			Assert.AreEqual(0x34, writeMemoryAddress[0].value);
			Assert.AreEqual(65534, writeMemoryAddress[1].address);
			Assert.AreEqual(0x12, writeMemoryAddress[1].value);
		}

		[Test, Order(24)]
		public void PUSHBU_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHBU should wrap around");

			cpu.reset();
			cpu.PUSHBU(0x23);
			Assert.AreEqual(65535, writeMemoryAddress[0].address);
			Assert.AreEqual(0x23, writeMemoryAddress[0].value);
		}

		[Test, Order(25)]
		public void PUSHWU_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHWU should wrap around");

			cpu.reset();
			cpu.PUSHWU(0x1234);
			Assert.AreEqual(65535, writeMemoryAddress[0].address);
			Assert.AreEqual(0x34, writeMemoryAddress[0].value);
			Assert.AreEqual(65534, writeMemoryAddress[1].address);
			Assert.AreEqual(0x12, writeMemoryAddress[1].value);
		}

		void runRegisterATest(byte opcode, byte registerA, Action postCpuResetInitFunction = null)
		{
			// add command in reverse order
			readMemoryAddress = new List<byte>()
			{
				opcode, RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
			};

			cpu.reset();
			postCpuResetInitFunction?.Invoke();

			cpu.regA = registerA;
			cpu.step();

			Assert.AreEqual(RESET_VECTOR_OFFSET_LO, readMemoryAddressAccess[0]);
			Assert.AreEqual(RESET_VECTOR_OFFSET_HI, readMemoryAddressAccess[1]);
			Assert.AreEqual(EXPECTED_RESET_READ_OFFSET_LO, readMemoryAddressAccess[2]);
		}

		void runExtendedMemoryTest(byte opcode, byte memoryContent, Action postCpuResetInitFunction = null)
		{
			const byte hardcodedReadOffsetLo = 0x11;
			const byte hardcodedReadOffsetHi = 0x22;
			const short hardcodedReadOffset = 0x2211;

			// add command in reverse order
			readMemoryAddress = new List<byte>()
			{
				memoryContent, hardcodedReadOffsetLo, hardcodedReadOffsetHi, opcode, RESET_VECTOR_VALUE_LO, RESET_VECTOR_VALUE_HI
			};

			cpu.reset();
			postCpuResetInitFunction?.Invoke();

			cpu.step();

			Assert.AreEqual(RESET_VECTOR_OFFSET_LO, readMemoryAddressAccess[0]);
			Assert.AreEqual(RESET_VECTOR_OFFSET_HI, readMemoryAddressAccess[1]);
			Assert.AreEqual(EXPECTED_RESET_READ_OFFSET_LO, readMemoryAddressAccess[2]);
			Assert.AreEqual(EXPECTED_RESET_READ_OFFSET_LO + 1, readMemoryAddressAccess[3]);
			Assert.AreEqual(EXPECTED_RESET_READ_OFFSET_LO + 2, readMemoryAddressAccess[4]);
			Assert.AreEqual(hardcodedReadOffset, readMemoryAddressAccess[5]);
		}

		[Test, Order(26)]
		public void PostByteSimpleX_0_15()
		{
			for (byte offset = 0; offset < 16; offset++) {
				TestContext.WriteLine("postbyte simple X: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					offset
				};
				cpu.set("flags", 0);
				cpu.regX = 0;
				cpu.regPC = 0;
				ushort result = cpu.PostByte();
				Assert.AreEqual(offset, result);
				Assert.AreEqual("efhinzvc", cpu.flagsToString());
				Assert.AreEqual(0, cpu.regX);
				Assert.AreEqual(1, cpu.tickCount);
				Assert.AreEqual(0, readMemoryAddressAccess[0]);
			}
		}

		[Test, Order(27)]
		public void PostByteSimpleX_16_31()
		{
			for (byte offset = 16; offset < 32; offset++)
			{
				TestContext.WriteLine("postbyte simple X: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					offset
				};
				cpu.set("flags", 0);
				cpu.regX = 0;
				cpu.regPC = 0;
				ushort result = cpu.PostByte();
				Assert.AreEqual(0x10000 - (32 - offset), result);
				Assert.AreEqual("efhinzvc", cpu.flagsToString());
				Assert.AreEqual(0, cpu.regX);
				Assert.AreEqual(1, cpu.tickCount);
				Assert.AreEqual(0, readMemoryAddressAccess[0]);
			}
		}

		[Test, Order(28)]
		public void PostByteSimpleS_0x60_0x70()
		{
			for (byte offset = 0x60; offset < 0x70; offset++)
			{
				TestContext.WriteLine("postbyte simple S: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					offset
				};
				cpu.set("flags", 0);
				cpu.regS = 0;
				cpu.regPC = 0;
				ushort result = cpu.PostByte();
				Assert.AreEqual(offset - 0x60, result);
				Assert.AreEqual("efhinzvc", cpu.flagsToString());
				Assert.AreEqual(0, cpu.regS);
				Assert.AreEqual(1, cpu.tickCount);
				Assert.AreEqual(0, readMemoryAddressAccess[0]);
			}
		}

		[Test, Order(29)]
		public void PostByteSimpleS_0x70_0x80()
		{
			for (byte offset = 0x70; offset < 0x80; offset++)
			{
				TestContext.WriteLine("postbyte simple S: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					offset
				};
				cpu.set("flags", 0);
				cpu.regS = 0;
				cpu.regPC = 0;
				ushort result = cpu.PostByte();
				Assert.AreEqual(0x10000 - (32 - offset) - 0x60, result);
				Assert.AreEqual("efhinzvc", cpu.flagsToString());
				Assert.AreEqual(0, cpu.regS);
				Assert.AreEqual(1, cpu.tickCount);
				Assert.AreEqual(0, readMemoryAddressAccess[0]);
			}
		}

		[Test, Order(30)]
		public void PostByteComplex()
		{
			List<ComplexData> list = new List<ComplexData>()
			{
				new ComplexData(0x80, "regX", null, null, 11, 2, 10, new ushort[] { 0 }),
				new ComplexData(0x81, "regX", null, null, 12, 3, 10, new ushort[] { 0 }),
				new ComplexData(0x85, "regX", 0xFFFF, 10, 0xFFFF, 1, 9, new ushort[] { 0 }),
				new ComplexData(0x88, "regX", null, null, 10, 1, 9 /*0*/, new ushort[] { 0, 1 }),
				new ComplexData(0x88, "regX", 100, null, 100, 1, 99 /*0*/, new ushort[] { 0, 1 }),
				new ComplexData(0x89, "regX", null, null, 10, 4, 9 /*0*/, new ushort[] { 0, 1, 2 }),
				new ComplexData(0x8B, "regX", null, null, 10, 4, 10, new ushort[] { 0 }),
				new ComplexData(0x8B, "regX", null, 5, 10, 4, 15, new ushort[] { 0 }),
				new ComplexData(0x8C, "regX", null, null, 10, 1, 1 /*0*/, new ushort[] { 0, 1 }),
				new ComplexData(0x8D, "regX", null, null, 10, 5, 2 /*0*/, new ushort[] { 0, 1, 2 }),
				new ComplexData(0x8F, "regX", null, null, 10, 5, 0xFFFF /*0*/, new ushort[] { 0, 1, 2 }),
				new ComplexData(0x90, "regX", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0x91, "regX", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0x91, "regX", 0xFFFF, null, 1, 6, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFF, 0 }),
				new ComplexData(0x92, "regX", null, null, 9, 5, 0xFFFF /*0*/, new ushort[] { 0, 9, 10 }),
				new ComplexData(0x92, "regX", 0, null, 0xFFFF, 5, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFF, 0 }),
				new ComplexData(0x93, "regX", null, null, 8, 6, 0xFFFF /*0*/, new ushort[] { 0, 8, 9 }),
				new ComplexData(0x93, "regX", 0, null, 0xFFFE, 6, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFE, 0xFFFF }),
				new ComplexData(0x94, "regX", null, null, 10, 3, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0x94, "regX", 0xFFFF, null, 0xFFFF, 3, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFF, 0 }),
				new ComplexData(0x95, "regX", null, 10, 10, 4, 0xFFFF /*0*/, new ushort[] { 0, 20, 21 }),
				new ComplexData(0x95, "regX", null, 0x80, 10, 4, 0xFFFF /*0*/, new ushort[] { 0, 0xFF8A, 0xFF8B }),
				new ComplexData(0xA0, "regY", null, null, 11, 2, 10, new ushort[] { 0 }),
				new ComplexData(0xA1, "regY", null, null, 12, 3, 10, new ushort[] { 0 }),
				new ComplexData(0xB0, "regY", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0xB1, "regY", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0xC0, "regU", null, null, 11, 2, 10, new ushort[] { 0 }),
				new ComplexData(0xC1, "regU", null, null, 12, 3, 10, new ushort[] { 0 }),
				new ComplexData(0xD0, "regU", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0xD1, "regU", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0xE0, "regS", null, null, 11, 2, 10, new ushort[] { 0 }),
				new ComplexData(0xE1, "regS", null, null, 12, 3, 10, new ushort[] { 0 }),
				new ComplexData(0xF0, "regS", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new ComplexData(0xF1, "regS", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 })
			};
			
			list.ForEach(delegate (ComplexData testData)
			{
				ushort initialValue = (ushort)(testData.initialValue.HasValue ? testData.initialValue.Value : 10);

				TestContext.WriteLine("test: postbyte complex {0}: {1}", testData.register, initialValue);

				Init();
				readMemoryAddress = new List<byte>()
				{
					testData.offset
				};
				cpu.set("flags", 0);
				cpu.regB = (byte) (testData.initialRegB.HasValue ? testData.initialRegB.Value : 0);
				switch (testData.register)
				{
					case "regX":
						cpu.regX = initialValue;
						break;
					case "regY":
						cpu.regY = initialValue;
						break;
					case "regU":
						cpu.regU = initialValue;
						break;
					case "regS":
						cpu.regS = initialValue;
						break;
					default:
						break;
				}
				cpu.regPC = 0;
				ushort result = cpu.PostByte();
				Assert.AreEqual(testData.expectedReturn, result);
				switch (testData.register)
				{
					case "regX":
						result = cpu.regX;
						break;
					case "regY":
						result = cpu.regY;
						break;
					case "regU":
						result = cpu.regU;
						break;
					case "regS":
						result = cpu.regS;
						break;
					default:
						break;
				}
				Assert.AreEqual(testData.expectedResult, result);
				Assert.AreEqual(testData.expectedTicks, cpu.tickCount);
				for (int index = 0; index < testData.expectedMemoryRead.Length; index++)
				{
					Assert.AreEqual(testData.expectedMemoryRead[index], readMemoryAddressAccess[index]);
				}
			});
		}

		[Test, Order(31)]
		public void PostByteComplex_0x8C()
		{
			TestContext.WriteLine("postbyte complex 0x8C");

			Init();
			readMemoryAddress = new List<byte>()
			{
				0x55, 0x8C
			};
			cpu.set("flags", 0);
			cpu.regPC = 0x1000;
			ushort result = cpu.PostByte();
			Assert.AreEqual(0x1057, result);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
			Assert.AreEqual(0x1002, cpu.regPC);
			Assert.AreEqual(1, cpu.tickCount);
			Assert.AreEqual(0x1000, readMemoryAddressAccess[0]);
			Assert.AreEqual(0x1001, readMemoryAddressAccess[1]);
		}

		[Test, Order(32)]
		public void PostByteComplex_0x8D()
		{
			TestContext.WriteLine("postbyte complex 0x8D");

			Init();
			readMemoryAddress = new List<byte>()
			{
				0x99, 0x55, 0x8D
			};
			cpu.set("flags", 0);
			cpu.regPC = 0x1000;
			ushort result = cpu.PostByte();
			Assert.AreEqual(0x659C, result);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
			Assert.AreEqual(0x1003, cpu.regPC);
			Assert.AreEqual(5, cpu.tickCount);
			Assert.AreEqual(0x1000, readMemoryAddressAccess[0]);
			Assert.AreEqual(0x1001, readMemoryAddressAccess[1]);
			Assert.AreEqual(0x1002, readMemoryAddressAccess[2]);
		}
	}
}
