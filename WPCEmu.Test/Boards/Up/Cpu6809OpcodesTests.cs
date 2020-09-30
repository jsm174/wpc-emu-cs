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

		struct TestData
		{
			public byte offset;
			public string register;
			public ushort? initialValue;
			public byte? initialRegB;
			public ushort expectedResult;
			public int expectedTicks;
			public ushort expectedReturn;
			public ushort[] expectedMemoryRead;

			public TestData(byte offset, string register, ushort? initialValue, byte? initialRegB, ushort expectedResult, int expectedTicks, ushort expectedReturn, ushort[] expectedMemoryRead)
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

			var value = readMemoryAddress[(readMemoryAddress.Count - 1)];
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
			Assert.That(readMemoryAddressAccess[0], Is.EqualTo(RESET_VECTOR_OFFSET_LO));
			Assert.That(readMemoryAddressAccess[1], Is.EqualTo(RESET_VECTOR_OFFSET_HI));
		}

		[Test, Order(2)]
		public void ROLA_0xFF()
		{
			TestContext.WriteLine("ROLA 0xFF");

			const byte OP_ROLA = 0x49;
			runRegisterATest(OP_ROLA, 0xFF);

			Assert.That(cpu.regA, Is.EqualTo(0xFE));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhINzvC"));
		}

		[Test, Order(3)]
		public void ROLA_0xFF_CarryFlagSet()
		{
			TestContext.WriteLine("ROLA, 0xFF - carry flag set");

			const byte OP_ROLA = 0x49;
			runRegisterATest(OP_ROLA, 0xFF, () =>
			{
				var cc = cpu.regCC |= 1;
				cpu.set("flags", cc);
			});

			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhINzvC"));
		}

		[Test, Order(4)]
		public void RORA_0x01_NoOverflow()
		{
			TestContext.WriteLine("RORA, 0x01 (no overflow)");

			const byte OP_RORA = 0x46;
			runRegisterATest(OP_RORA, 0x01);

			Assert.That(cpu.regA, Is.EqualTo(0x00));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInZvC"));
		}

		[Test, Order(5)]
		public void RORA_0xFF_CarryFlagNotSet()
		{
			TestContext.WriteLine("RORA, 0xFF - carry flag not set");

			const byte OP_RORA = 0x46;
			runRegisterATest(OP_RORA, 0xFF);

			Assert.That(cpu.regA, Is.EqualTo(0x7F));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzvC"));
		}

		[Test, Order(6)]
		public void RORA_0xFF_CarryFlagSet()
		{
			TestContext.WriteLine("RORA, 0xFF - carry flag set");

			const byte OP_RORA = 0x46;
			runRegisterATest(OP_RORA, 0xFF, () =>
			{
				var cc = cpu.regCC |= 1;
				cpu.set("flags", cc);
			});

			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhINzvC"));
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

			Assert.That(readMemoryAddressAccess[0], Is.EqualTo(RESET_VECTOR_OFFSET_LO));
			Assert.That(readMemoryAddressAccess[1], Is.EqualTo(RESET_VECTOR_OFFSET_HI));
			Assert.That(readMemoryAddressAccess[2], Is.EqualTo(EXPECTED_RESET_READ_OFFSET_LO));
			Assert.That(readMemoryAddressAccess[3], Is.EqualTo(EXPECTED_RESET_READ_OFFSET_HI));

			Assert.That(cpu.regA, Is.EqualTo(254));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFHINzvC"));
		}

		[Test, Order(8)]
		public void LSRA()
		{
			TestContext.WriteLine("LSRA");

			const byte OP_LSRA = 0x44;
			runRegisterATest(OP_LSRA, 0xFF);

			Assert.That(cpu.regA, Is.EqualTo(0x7F));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzvC"));
		}

		[Test, Order(9)]
		public void ASLA_Overflow()
		{
			TestContext.WriteLine("ASLA, overflow");

			const byte OP_ASLA = 0x48;
			runRegisterATest(OP_ASLA, 0x81);

			Assert.That(cpu.regA, Is.EqualTo(2));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzVC"));
		}

		[Test, Order(10)]
		public void ASLA_NoOverflow()
		{
			TestContext.WriteLine("ASLA, no overflow");

			const byte OP_ASLA = 0x48;
			runRegisterATest(OP_ASLA, 0x01);

			Assert.That(cpu.regA, Is.EqualTo(2));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzvc"));
		}

		[Test, Order(11)]
		public void ASRA_0xFF()
		{
			TestContext.WriteLine("ASRA (0xFF)");

			const byte OP_ASRA = 0x47;
			runRegisterATest(OP_ASRA, 0xFF);

			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhINzvC"));
		}

		[Test, Order(12)]
		public void ASRA_0x7F()
		{
			TestContext.WriteLine("ASRA (0x7F)");

			const byte OP_ASRA = 0x47;
			runRegisterATest(OP_ASRA, 0x7F);

			Assert.That(cpu.regA, Is.EqualTo(0x3F));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzvC"));
		}

		[Test, Order(13)]
		public void ASRA_0()
		{
			TestContext.WriteLine("ASRA (0)");

			const byte OP_ASRA = 0x47;
			runRegisterATest(OP_ASRA, 0);

			Assert.That(cpu.regA, Is.EqualTo(0));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInZvc"));
		}

		[Test, Order(14)]
		public void oNEG_0x1()
		{
			TestContext.WriteLine("oNEG, 0x1");

			const byte OP_NEG = 0x40;
			runRegisterATest(OP_NEG, 0x01);

			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhINzvC"));
		}

		[Test, Order(15)]
		public void oNEG_0xFF()
		{
			TestContext.WriteLine("oNEG, 0xFF");

			const byte OP_NEG = 0x40;
			runRegisterATest(OP_NEG, 0xFF);

			Assert.That(cpu.regA, Is.EqualTo(0x01));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzvc"));
		}

		[Test, Order(16)]
		public void oDEC_0x80_NoOverflow()
		{
			TestContext.WriteLine("oDEC, 0x80 (no overflow)");

			const byte OP_DEC = 0x4A;
			runRegisterATest(OP_DEC, 0x80);

			Assert.That(cpu.regA, Is.EqualTo(0x7F));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzVc"));
		}

		[Test, Order(17)]
		public void oDEC_0x0_Overflow()
		{
			TestContext.WriteLine("oDEC, 0x0 (overflow)");

			const byte OP_DEC = 0x4A;
			runRegisterATest(OP_DEC, 0x00);

			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhINzvc"));
		}

		[Test, Order(18)]
		public void oDEC_ExtendedMemory_0x0_Overflow()
		{
			TestContext.WriteLine("oDEC extended memory, 0x0 (overflow)");

			const byte OP_DEC = 0x7A;
			runExtendedMemoryTest(OP_DEC, 0x00);

			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(8721));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0xFF));
			Assert.That(cpu.tickCount, Is.EqualTo(7));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhINzvc"));
		}

		[Test, Order(19)]
		public void oINC_0x00_NoOverflow()
		{
			TestContext.WriteLine("oINC, 0x00 (no overflow)");

			const byte OP_INC = 0x4C;
			runRegisterATest(OP_INC, 0x00);

			Assert.That(cpu.regA, Is.EqualTo(0x01));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInzvc"));
		}

		[Test, Order(20)]
		public void oINC_0xFF_Overflow()
		{
			TestContext.WriteLine("oINC, 0xFF (overflow)");

			const byte OP_INC = 0x4C;
			runRegisterATest(OP_INC, 0xFF);

			Assert.That(cpu.regA, Is.EqualTo(0x00));
			Assert.That(cpu.tickCount, Is.EqualTo(2));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInZvc"));
		}

		[Test, Order(21)]
		public void oINC_ExtendedMemory_0xFF_Overflow()
		{
			TestContext.WriteLine("oINC extended memory, 0xFF (overflow)");

			const byte OP_INC = 0x7C;
			runExtendedMemoryTest(OP_INC, 0xFF);

			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(8721));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0x00));
			Assert.That(cpu.tickCount, Is.EqualTo(7));
			Assert.That(cpu.flagsToString(), Is.EqualTo("eFhInZvc"));
		}

		[Test, Order(22)]
		public void PUSHB_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHB should wrap around");

			cpu.reset();
			cpu.PUSHB(0x23);
			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(65535));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0x23));
		}

		[Test, Order(23)]
		public void PUSHW_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHW should wrap around");

			cpu.reset();
			cpu.PUSHW(0x1234);
			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(65535));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0x34));
			Assert.That(writeMemoryAddress[1].address, Is.EqualTo(65534));
			Assert.That(writeMemoryAddress[1].value, Is.EqualTo(0x12));
		}

		[Test, Order(24)]
		public void PUSHBU_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHBU should wrap around");

			cpu.reset();
			cpu.PUSHBU(0x23);
			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(65535));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0x23));
		}

		[Test, Order(25)]
		public void PUSHWU_ShouldWrapAround()
		{
			TestContext.WriteLine("PUSHWU should wrap around");

			cpu.reset();
			cpu.PUSHWU(0x1234);
			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(65535));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0x34));
			Assert.That(writeMemoryAddress[1].address, Is.EqualTo(65534));
			Assert.That(writeMemoryAddress[1].value, Is.EqualTo(0x12));
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

			Assert.That(readMemoryAddressAccess[0], Is.EqualTo(RESET_VECTOR_OFFSET_LO));
			Assert.That(readMemoryAddressAccess[1], Is.EqualTo(RESET_VECTOR_OFFSET_HI));
			Assert.That(readMemoryAddressAccess[2], Is.EqualTo(EXPECTED_RESET_READ_OFFSET_LO));
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

			Assert.That(readMemoryAddressAccess[0], Is.EqualTo(RESET_VECTOR_OFFSET_LO));
			Assert.That(readMemoryAddressAccess[1], Is.EqualTo(RESET_VECTOR_OFFSET_HI));
			Assert.That(readMemoryAddressAccess[2], Is.EqualTo(EXPECTED_RESET_READ_OFFSET_LO));
			Assert.That(readMemoryAddressAccess[3], Is.EqualTo(EXPECTED_RESET_READ_OFFSET_LO + 1));
			Assert.That(readMemoryAddressAccess[4], Is.EqualTo(EXPECTED_RESET_READ_OFFSET_LO + 2));
			Assert.That(readMemoryAddressAccess[5], Is.EqualTo(hardcodedReadOffset));
		}

		[Test, Order(26)]
		public void PostByteSimpleX_0_15()
		{
			for (var offset = 0; offset < 16; offset++) {
				TestContext.WriteLine("postbyte simple X: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					(byte)offset
				};
				cpu.set("flags", 0);
				cpu.regX = 0;
				cpu.regPC = 0;
				var result = cpu.PostByte();
				Assert.That(result, Is.EqualTo(offset));
				Assert.That(cpu.flagsToString(), Is.EqualTo("efhinzvc"));
				Assert.That(cpu.regX, Is.EqualTo(0));
				Assert.That(cpu.tickCount, Is.EqualTo(1));
				Assert.That(readMemoryAddressAccess[0], Is.EqualTo(0));
			}
		}

		[Test, Order(27)]
		public void PostByteSimpleX_16_31()
		{
			for (var offset = 16; offset < 32; offset++)
			{
				TestContext.WriteLine("postbyte simple X: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					(byte)offset
				};
				cpu.set("flags", 0);
				cpu.regX = 0;
				cpu.regPC = 0;
				var result = cpu.PostByte();
				Assert.That(result, Is.EqualTo(0x10000 - (32 - offset)));
				Assert.That(cpu.flagsToString(), Is.EqualTo("efhinzvc"));
				Assert.That(cpu.regX, Is.EqualTo(0));
				Assert.That(cpu.tickCount, Is.EqualTo(1));
				Assert.That(readMemoryAddressAccess[0], Is.EqualTo(0));
			}
		}

		[Test, Order(28)]
		public void PostByteSimpleS_0x60_0x70()
		{
			for (var offset = 0x60; offset < 0x70; offset++)
			{
				TestContext.WriteLine("postbyte simple S: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					(byte)offset
				};
				cpu.set("flags", 0);
				cpu.regS = 0;
				cpu.regPC = 0;
				var result = cpu.PostByte();
				Assert.That(result, Is.EqualTo(offset - 0x60));
				Assert.That(cpu.flagsToString(), Is.EqualTo("efhinzvc"));
				Assert.That(cpu.regS, Is.EqualTo(0));
				Assert.That(cpu.tickCount, Is.EqualTo(1));
				Assert.That(readMemoryAddressAccess[0], Is.EqualTo(0));
			}
		}

		[Test, Order(29)]
		public void PostByteSimpleS_0x70_0x80()
		{
			for (var offset = 0x70; offset < 0x80; offset++)
			{
				TestContext.WriteLine("postbyte simple S: {0}", offset);

				Init();
				readMemoryAddress = new List<byte>()
				{
					(byte)offset
				};
				cpu.set("flags", 0);
				cpu.regS = 0;
				cpu.regPC = 0;
				var result = cpu.PostByte();
				Assert.That(result, Is.EqualTo(0x10000 - (32 - offset) - 0x60));
				Assert.That(cpu.flagsToString(), Is.EqualTo("efhinzvc"));
				Assert.That(cpu.regS, Is.EqualTo(0));
				Assert.That(cpu.tickCount, Is.EqualTo(1));
				Assert.That(readMemoryAddressAccess[0], Is.EqualTo(0));
			}
		}

		[Test, Order(30)]
		public void PostByteComplex()
		{
			List<TestData> list = new List<TestData>()
			{
				new TestData(0x80, "regX", null, null, 11, 2, 10, new ushort[] { 0 }),
				new TestData(0x81, "regX", null, null, 12, 3, 10, new ushort[] { 0 }),
				new TestData(0x85, "regX", 0xFFFF, 10, 0xFFFF, 1, 9, new ushort[] { 0 }),
				new TestData(0x88, "regX", null, null, 10, 1, 9 /*0*/, new ushort[] { 0, 1 }),
				new TestData(0x88, "regX", 100, null, 100, 1, 99 /*0*/, new ushort[] { 0, 1 }),
				new TestData(0x89, "regX", null, null, 10, 4, 9 /*0*/, new ushort[] { 0, 1, 2 }),
				new TestData(0x8B, "regX", null, null, 10, 4, 10, new ushort[] { 0 }),
				new TestData(0x8B, "regX", null, 5, 10, 4, 15, new ushort[] { 0 }),
				new TestData(0x8C, "regX", null, null, 10, 1, 1 /*0*/, new ushort[] { 0, 1 }),
				new TestData(0x8D, "regX", null, null, 10, 5, 2 /*0*/, new ushort[] { 0, 1, 2 }),
				new TestData(0x8F, "regX", null, null, 10, 5, 0xFFFF /*0*/, new ushort[] { 0, 1, 2 }),
				new TestData(0x90, "regX", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0x91, "regX", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0x91, "regX", 0xFFFF, null, 1, 6, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFF, 0 }),
				new TestData(0x92, "regX", null, null, 9, 5, 0xFFFF /*0*/, new ushort[] { 0, 9, 10 }),
				new TestData(0x92, "regX", 0, null, 0xFFFF, 5, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFF, 0 }),
				new TestData(0x93, "regX", null, null, 8, 6, 0xFFFF /*0*/, new ushort[] { 0, 8, 9 }),
				new TestData(0x93, "regX", 0, null, 0xFFFE, 6, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFE, 0xFFFF }),
				new TestData(0x94, "regX", null, null, 10, 3, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0x94, "regX", 0xFFFF, null, 0xFFFF, 3, 0xFFFF /*0*/, new ushort[] { 0, 0xFFFF, 0 }),
				new TestData(0x95, "regX", null, 10, 10, 4, 0xFFFF /*0*/, new ushort[] { 0, 20, 21 }),
				new TestData(0x95, "regX", null, 0x80, 10, 4, 0xFFFF /*0*/, new ushort[] { 0, 0xFF8A, 0xFF8B }),
				new TestData(0xA0, "regY", null, null, 11, 2, 10, new ushort[] { 0 }),
				new TestData(0xA1, "regY", null, null, 12, 3, 10, new ushort[] { 0 }),
				new TestData(0xB0, "regY", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0xB1, "regY", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0xC0, "regU", null, null, 11, 2, 10, new ushort[] { 0 }),
				new TestData(0xC1, "regU", null, null, 12, 3, 10, new ushort[] { 0 }),
				new TestData(0xD0, "regU", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0xD1, "regU", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0xE0, "regS", null, null, 11, 2, 10, new ushort[] { 0 }),
				new TestData(0xE1, "regS", null, null, 12, 3, 10, new ushort[] { 0 }),
				new TestData(0xF0, "regS", null, null, 11, 5, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 }),
				new TestData(0xF1, "regS", null, null, 12, 6, 0xFFFF /*0*/, new ushort[] { 0, 10, 11 })
			};
			
			list.ForEach((testData) => 
			{
				var initialValue = (ushort)(testData.initialValue.HasValue ? testData.initialValue.Value : 10);

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
				var result = cpu.PostByte();
				Assert.That(result, Is.EqualTo(testData.expectedReturn));
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
				Assert.That(result, Is.EqualTo(testData.expectedResult));
				Assert.That(cpu.tickCount, Is.EqualTo(testData.expectedTicks));
				for (var index = 0; index < testData.expectedMemoryRead.Length; index++)
				{
					Assert.That(readMemoryAddressAccess[index], Is.EqualTo(testData.expectedMemoryRead[index]));
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
			var result = cpu.PostByte();
			Assert.That(result, Is.EqualTo(0x1057));
			Assert.That(cpu.flagsToString(), Is.EqualTo("efhinzvc"));
			Assert.That(cpu.regPC, Is.EqualTo(0x1002));
			Assert.That(cpu.tickCount, Is.EqualTo(1));
			Assert.That(readMemoryAddressAccess[0], Is.EqualTo(0x1000));
			Assert.That(readMemoryAddressAccess[1], Is.EqualTo(0x1001));
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
			var result = cpu.PostByte();
			Assert.That(result, Is.EqualTo(0x659C));
			Assert.That(cpu.flagsToString(), Is.EqualTo("efhinzvc"));
			Assert.That(cpu.regPC, Is.EqualTo(0x1003));
			Assert.That(cpu.tickCount, Is.EqualTo(5));
			Assert.That(readMemoryAddressAccess[0], Is.EqualTo(0x1000));
			Assert.That(readMemoryAddressAccess[1], Is.EqualTo(0x1001));
			Assert.That(readMemoryAddressAccess[2], Is.EqualTo(0x1002));
		}
	}
}
