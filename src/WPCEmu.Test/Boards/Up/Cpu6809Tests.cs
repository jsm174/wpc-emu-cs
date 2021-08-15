using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards.Up;

namespace WPCEmu.Test.Boards.Up
{
	[TestFixture]
	public class Cpu6809Tests
	{
		struct AddressValueData
		{
			public ushort address;
			public byte value;
		}

		List<ushort> readMemoryAddress;
		List<AddressValueData> writeMemoryAddress;

		Cpu6809 cpu;

		byte ReadMemoryMock(ushort address)
		{
			readMemoryAddress.Add(address);
			return 0;
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
			readMemoryAddress = new List<ushort>();
			writeMemoryAddress = new List<AddressValueData>();
		
			cpu = Cpu6809.getInstance(WriteMemoryMock, ReadMemoryMock);
			cpu.reset();
		}

		[Test, Order(1)]
		public void ReadInitialVector()
		{
			TestContext.WriteLine("read initial vector");

			Assert.That(readMemoryAddress[0], Is.EqualTo(0xFFFE));
			Assert.That(readMemoryAddress[1], Is.EqualTo(0xFFFF));
		}

		[Test, Order(2)]
		public void oCmp_8bit_CarryFlag()
		{
			TestContext.WriteLine("oCMP 8bit, carry flag");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0xFF);
			Assert.That("efhinzvC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(3)]
		public void oCmp_8bit_0xFF()
		{
			TestContext.WriteLine("oCMP 8bit, 0xFF");

			cpu.set("flags", 0x00);
			cpu.oCMP(0xFF, 0);
			Assert.That("efhiNzvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(4)]
		public void oCmp_8bit_NegativeFlag()
		{
			TestContext.WriteLine("oCMP 8bit, negative flag");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0x80);
			Assert.That("efhiNzVC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(5)]
		public void oCmp_8bit_Negative1()
		{
			TestContext.WriteLine("oCMP 8bit, -1");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 1);
			Assert.That("efhiNzvC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(6)]
		public void oCmp_8bit_ZeroFlag()
		{
			TestContext.WriteLine("oCMP 8bit, zero flag");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0);
			Assert.That("efhinZvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(7)]
		public void oCmp_16bit_CarryFlag()
		{
			TestContext.WriteLine("oCMP 16bit, carry flag");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0xFFFF);
			Assert.That("efhinzvC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(8)]
		public void oCmp_16bit_0xFFFF()
		{
			TestContext.WriteLine("oCMP 16bit, 0xFFFF");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0xFFFF, 0);
			Assert.That("efhiNzvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(9)]
		public void oCmp_16bit_NegativeFlag()
		{
			TestContext.WriteLine("oCMP 16bit, negative flag");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0x8000);
			Assert.That("efhiNzVC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(10)]
		public void oCmp_16bit_Negative1()
		{
			TestContext.WriteLine("oCMP 16bit, -1");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 1);
			Assert.That("efhiNzvC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(11)]
		public void oCmp_16bit_ZeroFlag()
		{
			TestContext.WriteLine("oCMP 16bit, zero flag");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0);
			Assert.That("efhinZvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(12)]
		public void FlagsCorrectAfterIRQ_InitFlags_0x00()
		{
			TestContext.WriteLine("flags should be correct after calling irq(), init flags to 0x00");

			cpu.set("flags", 0x00);
			cpu.irq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.That("EfhInzvc", Is.EqualTo(cpu.flagsToString()));
			Assert.That(readMemoryAddress[2], Is.EqualTo(0xFFF8));
			Assert.That(readMemoryAddress[3], Is.EqualTo(0xFFF9));
		}

		[Test, Order(13)]
		public void DetectFirqCouldNotBeTriggered()
		{
			TestContext.WriteLine("detect that firq could not be triggered");

			cpu.set("flags", 0x00);
			cpu.firq();
			Assert.That(0, Is.EqualTo(0));
		}

		[Test, Order(14)]
		public void FlagsCorrectAfterIRQ_InitFlags_0xEF()
		{
			TestContext.WriteLine("flags should be correct after calling irq(), init flags to 0xef");

			var flagClearedFirqBit = (byte) (0xFF & ~16);
			cpu.set("flags", flagClearedFirqBit);
			cpu.irq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.That(readMemoryAddress[2], Is.EqualTo(0xFFF8));
			Assert.That(readMemoryAddress[3], Is.EqualTo(0xFFF9));
			Assert.That("EFHINZVC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(15)]
		public void IRQNotCalledIf_F_IRQMASK_set()
		{
			TestContext.WriteLine("irq() should not be called if F_IRQMASK flag is set");

			cpu.set("flags", 0xFF);
			cpu.irq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.Throws<System.ArgumentOutOfRangeException>(() => readMemoryAddress.ElementAt(2));
			Assert.Throws<System.ArgumentOutOfRangeException>(() => readMemoryAddress.ElementAt(3));
		}

		[Test, Order(16)]
		public void FlagsCorrectAfterNMI()
		{
			TestContext.WriteLine("flags should be correct after calling nmi()");

			cpu.set("flags", 0x00);
			cpu.nmi();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.That("EFhInzvc", Is.EqualTo(cpu.flagsToString()));
			Assert.That(readMemoryAddress[2], Is.EqualTo(0xFFFC));
			Assert.That(readMemoryAddress[3], Is.EqualTo(0xFFFD));
		}

		[Test, Order(17)]
		public void FlagsCorrectAfterFIRQ_InitFlags_0x00()
		{
			TestContext.WriteLine("flags should be correct after calling firq(), init flags to 0x00");

			cpu.set("flags", 0x00);
			cpu.firq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.That("eFhInzvc", Is.EqualTo(cpu.flagsToString()));
			Assert.That(readMemoryAddress[2], Is.EqualTo(0xFFF6));
			Assert.That(readMemoryAddress[3], Is.EqualTo(0xFFF7));
		}

		[Test, Order(18)]
		public void FlagsCorrectAfterFIRQ_InitFlags_0xBF()
		{
			TestContext.WriteLine("flags should be correct after calling firq(), init flags to 0xbf");

			var flagClearedFirqBit = (byte) (0xFF & ~64);
			cpu.set("flags", flagClearedFirqBit);
			cpu.firq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.That(readMemoryAddress[2], Is.EqualTo(0xFFF6));
			Assert.That(readMemoryAddress[3], Is.EqualTo(0xFFF7));
			Assert.That("eFHINZVC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(19)]
		public void FIRQNotCalledIf_F_FIRQMASK_set()
		{
			TestContext.WriteLine("firq() should not be called if F_FIRQMASK flag is set");

			cpu.set("flags", 0xFF);
			cpu.firq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.Throws<System.ArgumentOutOfRangeException>(() => readMemoryAddress.ElementAt(2));
			Assert.Throws<System.ArgumentOutOfRangeException>(() => readMemoryAddress.ElementAt(3));
		}

		[Test, Order(20)]
		public void oNEG_CarryFlag()
		{
			TestContext.WriteLine("oNEG() should set CARRY flag correctly");

			cpu.set("flags", 0xFF);
			cpu.firq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.Throws<System.ArgumentOutOfRangeException>(() => readMemoryAddress.ElementAt(2));
			Assert.Throws<System.ArgumentOutOfRangeException>(() => readMemoryAddress.ElementAt(3));
		}

		[Test, Order(21)]
		public void SetOverflowFlag_8bit()
		{
			TestContext.WriteLine("set overflow flag (8bit)");

			cpu.set("flags", 0);
			cpu.setV8(1, 1, 0x80);
			Assert.That("efhinzVc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(22)]
		public void SetOverflowFlag_8bit_overflow_r()
		{
			TestContext.WriteLine("set overflow flag (8bit), overflow r value");

			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x180);
			Assert.That("efhinzvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(23)]
		public void setOverflowFlag_16bit()
		{
			TestContext.WriteLine("set overflow flag (16bit)");

			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x8000);
			Assert.That("efhinzVc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(24)]
		public void SetOverflowFlag_16bit_overflow_r()
		{
			TestContext.WriteLine("set overflow flag (16bit), overflow r value");

			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x18000);
			Assert.That("efhinzvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(25)]
		public void Signed_5bit()
		{
			TestContext.WriteLine("signed 5bit");

			var val0 = (sbyte) cpu.signed5bit(0);
			var valF = (sbyte) cpu.signed5bit(0xF);
			var val10 = (sbyte) cpu.signed5bit(0x10);
			var val1F = (sbyte) cpu.signed5bit(0x1F);
			//ushort valUndef = cpu.signed16();
			Assert.That(val0, Is.EqualTo(0));
			Assert.That(valF, Is.EqualTo(15));
			Assert.That(val10, Is.EqualTo(-16));
			Assert.That(val1F, Is.EqualTo(-1));
			//Assert.That(valUndef, Is.EqualTo(undefined));
		}

		[Test, Order(26)]
		public void Signed_8bit()
		{
			TestContext.WriteLine("signed 8bit");

			var val0 = (sbyte) cpu.signed(0);
			var val7f = (sbyte) cpu.signed(0x7F);
			var val80 = (sbyte) cpu.signed(0x80);
			var valff = (sbyte) cpu.signed(0xFF);
			//ushort valUndef = cpu.signed();
			Assert.That(val0, Is.EqualTo(0));
			Assert.That(val7f, Is.EqualTo(0x7f));
			Assert.That(val80, Is.EqualTo(-128));
			Assert.That(valff, Is.EqualTo(-1));
			//Assert.That(valUndef, Is.EqualTo(undefined));
		}

		[Test, Order(27)]
		public void Signed_16bit()
		{
			TestContext.WriteLine("signed 16bit");

			var val0 = (short) cpu.signed16(0);
			var val7fff = (short) cpu.signed16(0x7FFF);
			var val8000 = (short) cpu.signed16(0x8000);
			var valffff = (short) cpu.signed16(0xFFFF);
			//ushort valUndef = cpu.signed16();
			Assert.That(val0, Is.EqualTo(0));
			Assert.That(val7fff, Is.EqualTo(32767));
			Assert.That(val8000, Is.EqualTo(-32768));
			Assert.That(valffff, Is.EqualTo(-1));
			//Assert.That(valUndef, Is.EqualTo(undefined));
		}

		[Test, Order(28)]
		public void Flags_NZ16_0xFFFF()
		{
			TestContext.WriteLine("flagsNZ16 0xFFFF");

			cpu.set("flags", 0);
			cpu.flagsNZ16(0xFFFF);
			Assert.That("efhiNzvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(29)]
		public void FlagsNZ16_0x0000()
		{
			TestContext.WriteLine("flagsNZ16 0x0000");

			cpu.set("flags", 0);
			cpu.flagsNZ16(0);
			Assert.That("efhinZvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(30)]
		public void WriteWord_0_0x1234()
		{
			TestContext.WriteLine("WriteWord(0, 0x1234)");

			cpu.WriteWord(0x0, 0x1234);
			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(0));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0x12));
			Assert.That(writeMemoryAddress[1].address, Is.EqualTo(1));
			Assert.That(writeMemoryAddress[1].value, Is.EqualTo(0x34));
		}

		[Test, Order(31)]
		public void WriteWord_0xFFFF_0x1234()
		{
			TestContext.WriteLine("WriteWord(0xFFFF, 0x1234)");

			cpu.WriteWord(0xFFFF, 0x1234);
			Assert.That(writeMemoryAddress[0].address, Is.EqualTo(0xFFFF));
			Assert.That(writeMemoryAddress[0].value, Is.EqualTo(0x12));
			Assert.That(writeMemoryAddress[1].address, Is.EqualTo(0));
			Assert.That(writeMemoryAddress[1].value, Is.EqualTo(0x34));
		}

		[Test, Order(32)]
		public void GetD()
		{
			TestContext.WriteLine("getD");

			cpu.regA = 0xFF;
			cpu.regB = 0xEE;
			var result = cpu.getD();
			Assert.That(result, Is.EqualTo(0xFFEE));
		}

		[Test, Order(33)]
		public void SetD_0xFFEE()
		{
			TestContext.WriteLine("setD(0xFFEE)");

			cpu.setD(0xFFEE);
			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.regB, Is.EqualTo(0xEE));
		}

		[Test, Order(34)]
		public void dpadd_regDP_0()
		{
			TestContext.WriteLine("dpadd(), regDP = 0");

			cpu.regDP = 0;
			cpu.fetch = () => {
				return 0xFF;
			};
			var result = cpu.dpadd();
			Assert.That(result, Is.EqualTo(0xFF));
		}

		[Test, Order(35)]
		public void dpadd_regDP_0xFF()
		{
			TestContext.WriteLine("dpadd(), regDP = 0xFF");

			cpu.regDP = 0xFF;
			cpu.fetch = () => {
				return 0xFF;
			};
			var result = cpu.dpadd();
			Assert.That(result, Is.EqualTo(0xFFFF));
		}

		[Test, Order(36)]
		public void oNEG_0()
		{
			TestContext.WriteLine("oNEG(0)");

			cpu.set("flags", 0);
			var result = cpu.oNEG(0);
			Assert.That(result, Is.EqualTo(0));
			Assert.That("efhinZvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(37)]
		public void oNEG_1()
		{
			TestContext.WriteLine("oNEG(1)");

			cpu.set("flags", 0);
			var result = cpu.oNEG(1);
			Assert.That(result, Is.EqualTo(0xFF));
			Assert.That("efhiNzvC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(38)]
		public void oNEG_0x7F()
		{
			TestContext.WriteLine("oNEG(0x7F)");

			cpu.set("flags", 0);
			var result = cpu.oNEG(0x7F);
			Assert.That(result, Is.EqualTo(0x81));
			Assert.That("efhiNzvC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(39)]
		public void oNEG_0x80()
		{
			TestContext.WriteLine("oNEG(0x80)");

			cpu.set("flags", 0);
			var result = cpu.oNEG(0x80);
			Assert.That(result, Is.EqualTo(0x80));
			Assert.That("efhiNzVC", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(40)]
		public void oNEG_0xFF()
		{
			TestContext.WriteLine("oNEG(0xFF)");

			cpu.set("flags", 0);
			var result = cpu.oNEG(0xFF);
			Assert.That(result, Is.EqualTo(1));
			Assert.That("efhinzvc", Is.EqualTo(cpu.flagsToString()));
		}

		[Test, Order(41)]
		public void SetPostByteRegister_0_0xFFFF()
		{
			TestContext.WriteLine("setPostByteRegister(0, 0xFFFF)");

			cpu.setPostByteRegister(0, 0xFFFF);
			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.regB, Is.EqualTo(0xFF));
		}

		[Test, Order(42)]
		public void SetPostByteRegister_0x8_0xFFFF()
		{
			TestContext.WriteLine("setPostByteRegister(0x8, 0xFFFF)");

			cpu.setPostByteRegister(0x8, 0xFFFF);
			Assert.That(cpu.regA, Is.EqualTo(0xFF));
		}

		[Test, Order(43)]
		public void GetPostByteRegister_0x0_D()
		{
			TestContext.WriteLine("getPostByteRegister(0x0) - D");

			cpu.regA = 0x44;
			cpu.regB = 0x99;
			var result = cpu.getPostByteRegister(0x0);
			Assert.That(result, Is.EqualTo(0x4499));
		}

		[Test, Order(44)]
		public void GetPostByteRegister_0x5_PC()
		{
			TestContext.WriteLine("getPostByteRegister(0x5) - PC");

			cpu.regPC = 0x1234;
			var result = cpu.getPostByteRegister(0x5);
			Assert.That(result, Is.EqualTo(0x1234));
		}

		[Test, Order(45)]
		public void GetPostByteRegister_0xA_CC()
		{
			TestContext.WriteLine("getPostByteRegister(0xA) - CC");

			cpu.regCC = 0xFF;
			var result = cpu.getPostByteRegister(0xA);
			Assert.That(result, Is.EqualTo(0xFF));
		}

		[Test, Order(46)]
		public void TFREXG_Transfer_X_Y()
		{
			TestContext.WriteLine("TFREXG - transfer X -> Y");

			cpu.regX = 0xFFFF;
			cpu.regY = 0x7F;
			cpu.TFREXG(0x12, false);
			Assert.That(cpu.regX, Is.EqualTo(0xFFFF));
			Assert.That(cpu.regY, Is.EqualTo(0xFFFF));
		}

		[Test, Order(47)]
		public void TFREXG_Exchange_U_S()
		{
			TestContext.WriteLine("TFREXG - exchange U <-> S");

			cpu.regU = 0xFFFF;
			cpu.regS = 0x7F;
			cpu.TFREXG(0x34, true);
			Assert.That(cpu.regU, Is.EqualTo(0x7F));
			Assert.That(cpu.regS, Is.EqualTo(0xFFFF));
		}

		[Test, Order(48)]
		public void TFREXG_Transfer_A_CC()
		{
			TestContext.WriteLine("TFREXG - transfer A -> CC");

			cpu.regA = 0xFF;
			cpu.regCC = 0x7F;
			cpu.TFREXG(0x8A, false);
			Assert.That(cpu.regA, Is.EqualTo(0xFF));
			Assert.That(cpu.regCC, Is.EqualTo(0xFF));
		}

		[Test, Order(49)]
		public void TFREXG_Exchange_B_DP()
		{
			TestContext.WriteLine("TFREXG - exchange b <-> DP");

			cpu.regB = 0xFF;
			cpu.regDP = 0x7F;
			cpu.TFREXG(0x9B, true);
			Assert.That(cpu.regB, Is.EqualTo(0x7F));
			Assert.That(cpu.regDP, Is.EqualTo(0xFF));
		}

		[Test, Order(50)]
		public void GetAndSetState()
		{
			TestContext.WriteLine("get and set state");

			cpu.regA = 0x44;
			Cpu6809.State state = cpu.getState();
			cpu.regA = 0;
			cpu.setState(state);
			Assert.That(cpu.regA, Is.EqualTo(0x44));
		}
	}
}
