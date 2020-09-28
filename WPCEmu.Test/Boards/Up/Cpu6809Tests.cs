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
		
			cpu = Cpu6809.GetInstance(WriteMemoryMock, ReadMemoryMock);
			cpu.reset();
		}

		[Test, Order(1)]
		public void ReadInitialVector()
		{
			TestContext.WriteLine("read initial vector");

			Assert.AreEqual(0xFFFE, readMemoryAddress[0]);
			Assert.AreEqual(0xFFFF, readMemoryAddress[1]);
		}

		[Test, Order(2)]
		public void oCmp_8bit_CarryFlag()
		{
			TestContext.WriteLine("oCMP 8bit, carry flag");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0xFF);
			Assert.AreEqual("efhinzvC", cpu.flagsToString());
		}

		[Test, Order(3)]
		public void oCmp_8bit_0xFF()
		{
			TestContext.WriteLine("oCMP 8bit, 0xFF");

			cpu.set("flags", 0x00);
			cpu.oCMP(0xFF, 0);
			Assert.AreEqual("efhiNzvc", cpu.flagsToString());
		}

		[Test, Order(4)]
		public void oCmp_8bit_NegativeFlag()
		{
			TestContext.WriteLine("oCMP 8bit, negative flag");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0x80);
			Assert.AreEqual("efhiNzVC", cpu.flagsToString());
		}

		[Test, Order(5)]
		public void oCmp_8bit_Negative1()
		{
			TestContext.WriteLine("oCMP 8bit, -1");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 1);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(6)]
		public void oCmp_8bit_ZeroFlag()
		{
			TestContext.WriteLine("oCMP 8bit, zero flag");

			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
		}

		[Test, Order(7)]
		public void oCmp_16bit_CarryFlag()
		{
			TestContext.WriteLine("oCMP 16bit, carry flag");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0xFFFF);
			Assert.AreEqual("efhinzvC", cpu.flagsToString());
		}

		[Test, Order(8)]
		public void oCmp_16bit_0xFFFF()
		{
			TestContext.WriteLine("oCMP 16bit, 0xFFFF");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0xFFFF, 0);
			Assert.AreEqual("efhiNzvc", cpu.flagsToString());
		}

		[Test, Order(9)]
		public void oCmp_16bit_NegativeFlag()
		{
			TestContext.WriteLine("oCMP 16bit, negative flag");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0x8000);
			Assert.AreEqual("efhiNzVC", cpu.flagsToString());
		}

		[Test, Order(10)]
		public void oCmp_16bit_Negative1()
		{
			TestContext.WriteLine("oCMP 16bit, -1");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 1);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(11)]
		public void oCmp_16bit_ZeroFlag()
		{
			TestContext.WriteLine("oCMP 16bit, zero flag");

			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
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
			Assert.AreEqual("EfhInzvc", cpu.flagsToString());
			Assert.AreEqual(0xFFF8, readMemoryAddress[2]);
			Assert.AreEqual(0xFFF9, readMemoryAddress[3]);
		}

		[Test, Order(13)]
		public void DetectFirqCouldNotBeTriggered()
		{
			TestContext.WriteLine("detect that firq could not be triggered");

			cpu.set("flags", 0x00);
			cpu.firq();
			Assert.AreEqual(0, 0);
		}

		[Test, Order(14)]
		public void FlagsCorrectAfterIRQ_InitFlags_0xEF()
		{
			TestContext.WriteLine("flags should be correct after calling irq(), init flags to 0xef");

			byte flagClearedFirqBit = 0xFF & ~16;
			cpu.set("flags", flagClearedFirqBit);
			cpu.irq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.AreEqual(0xFFF8, readMemoryAddress[2]);
			Assert.AreEqual(0xFFF9, readMemoryAddress[3]);
			Assert.AreEqual("EFHINZVC", cpu.flagsToString());
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
			Assert.AreEqual("EFhInzvc", cpu.flagsToString());
			Assert.AreEqual(0xFFFC, readMemoryAddress[2]);
			Assert.AreEqual(0xFFFD, readMemoryAddress[3]);
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
			Assert.AreEqual("eFhInzvc", cpu.flagsToString());
			Assert.AreEqual(0xFFF6, readMemoryAddress[2]);
			Assert.AreEqual(0xFFF7, readMemoryAddress[3]);
		}

		[Test, Order(18)]
		public void FlagsCorrectAfterFIRQ_InitFlags_0xBF()
		{
			TestContext.WriteLine("flags should be correct after calling firq(), init flags to 0xbf");

			byte flagClearedFirqBit = 0xFF & ~64;
			cpu.set("flags", flagClearedFirqBit);
			cpu.firq();
			cpu.fetch = () =>
			{
				return 0x12;
			};
			cpu.steps();
			Assert.AreEqual(0xFFF6, readMemoryAddress[2]);
			Assert.AreEqual(0xFFF7, readMemoryAddress[3]);
			Assert.AreEqual("eFHINZVC", cpu.flagsToString());
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
			Assert.AreEqual("efhinzVc", cpu.flagsToString());
		}

		[Test, Order(22)]
		public void SetOverflowFlag_8bit_overflow_r()
		{
			TestContext.WriteLine("set overflow flag (8bit), overflow r value");

			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x180);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
		}

		[Test, Order(23)]
		public void setOverflowFlag_16bit()
		{
			TestContext.WriteLine("set overflow flag (16bit)");

			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x8000);
			Assert.AreEqual("efhinzVc", cpu.flagsToString());
		}

		[Test, Order(24)]
		public void SetOverflowFlag_16bit_overflow_r()
		{
			TestContext.WriteLine("set overflow flag (16bit), overflow r value");

			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x18000);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
		}

		[Test, Order(25)]
		public void Signed_5bit()
		{
			TestContext.WriteLine("signed 5bit");

			sbyte val0 = (sbyte) cpu.signed5bit(0);
			sbyte valF = (sbyte) cpu.signed5bit(0xF);
			sbyte val10 = (sbyte) cpu.signed5bit(0x10);
			sbyte val1F = (sbyte) cpu.signed5bit(0x1F);
			//ushort valUndef = cpu.signed16();
			Assert.AreEqual(0, val0);
			Assert.AreEqual(15, valF);
			Assert.AreEqual(-16, val10);
			Assert.AreEqual(-1, val1F);
			//Assert.AreEqual(undefined, valUndef);
		}

		[Test, Order(26)]
		public void Signed_8bit()
		{
			TestContext.WriteLine("signed 8bit");

			sbyte val0 = (sbyte) cpu.signed(0);
			sbyte val7f = (sbyte) cpu.signed(0x7F);
			sbyte val80 = (sbyte) cpu.signed(0x80);
			sbyte valff = (sbyte) cpu.signed(0xFF);
			//ushort valUndef = cpu.signed();
			Assert.AreEqual(0, val0);
			Assert.AreEqual(0x7f, val7f);
			Assert.AreEqual(-128, val80);
			Assert.AreEqual(-1, valff);
			//Assert.AreEqual(undefined, valUndef);
		}

		[Test, Order(27)]
		public void Signed_16bit()
		{
			TestContext.WriteLine("signed 16bit");

			short val0 = (short) cpu.signed16(0);
			short val7fff = (short) cpu.signed16(0x7FFF);
			short val8000 = (short) cpu.signed16(0x8000);
			short valffff = (short) cpu.signed16(0xFFFF);
			//ushort valUndef = cpu.signed16();
			Assert.AreEqual(0, val0);
			Assert.AreEqual(32767, val7fff);
			Assert.AreEqual(-32768, val8000);
			Assert.AreEqual(-1, valffff);
			//Assert.AreEqual(undefined, valUndef);
		}

		[Test, Order(28)]
		public void Flags_NZ16_0xFFFF()
		{
			TestContext.WriteLine("flagsNZ16 0xFFFF");

			cpu.set("flags", 0);
			cpu.flagsNZ16(0xFFFF);
			Assert.AreEqual("efhiNzvc", cpu.flagsToString());
		}

		[Test, Order(29)]
		public void FlagsNZ16_0x0000()
		{
			TestContext.WriteLine("flagsNZ16 0x0000");

			cpu.set("flags", 0);
			cpu.flagsNZ16(0);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
		}

		[Test, Order(30)]
		public void WriteWord_0_0x1234()
		{
			TestContext.WriteLine("WriteWord(0, 0x1234)");

			cpu.WriteWord(0x0, 0x1234);
			Assert.AreEqual(0, writeMemoryAddress[0].address);
			Assert.AreEqual(0x12, writeMemoryAddress[0].value);
			Assert.AreEqual(1, writeMemoryAddress[1].address);
			Assert.AreEqual(0x34, writeMemoryAddress[1].value);
		}

		[Test, Order(31)]
		public void WriteWord_0xFFFF_0x1234()
		{
			TestContext.WriteLine("WriteWord(0xFFFF, 0x1234)");

			cpu.WriteWord(0xFFFF, 0x1234);
			Assert.AreEqual(0xFFFF, writeMemoryAddress[0].address);
			Assert.AreEqual(0x12, writeMemoryAddress[0].value);
			Assert.AreEqual(0, writeMemoryAddress[1].address);
			Assert.AreEqual(0x34, writeMemoryAddress[1].value);
		}

		[Test, Order(32)]
		public void GetD()
		{
			TestContext.WriteLine("getD");

			cpu.regA = 0xFF;
			cpu.regB = 0xEE;
			ushort result = cpu.getD();
			Assert.AreEqual(0xFFEE, result);
		}

		[Test, Order(33)]
		public void SetD_0xFFEE()
		{
			TestContext.WriteLine("setD(0xFFEE)");

			cpu.setD(0xFFEE);
			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(0xEE, cpu.regB);
		}

		[Test, Order(34)]
		public void dpadd_regDP_0()
		{
			TestContext.WriteLine("dpadd(), regDP = 0");

			cpu.regDP = 0;
			cpu.fetch = () => {
				return 0xFF;
			};
			ushort result = cpu.dpadd();
			Assert.AreEqual(0xFF, result);
		}

		[Test, Order(35)]
		public void dpadd_regDP_0xFF()
		{
			TestContext.WriteLine("dpadd(), regDP = 0xFF");

			cpu.regDP = 0xFF;
			cpu.fetch = () => {
				return 0xFF;
			};
			ushort result = cpu.dpadd();
			Assert.AreEqual(0xFFFF, result);
		}

		[Test, Order(36)]
		public void oNEG_0()
		{
			TestContext.WriteLine("oNEG(0)");

			cpu.set("flags", 0);
			byte result = cpu.oNEG(0);
			Assert.AreEqual(0, result);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
		}

		[Test, Order(37)]
		public void oNEG_1()
		{
			TestContext.WriteLine("oNEG(1)");

			cpu.set("flags", 0);
			byte result = cpu.oNEG(1);
			Assert.AreEqual(0xFF, result);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(38)]
		public void oNEG_0x7F()
		{
			TestContext.WriteLine("oNEG(0x7F)");

			cpu.set("flags", 0);
			byte result = cpu.oNEG(0x7F);
			Assert.AreEqual(0x81, result);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(39)]
		public void oNEG_0x80()
		{
			TestContext.WriteLine("oNEG(0x80)");

			cpu.set("flags", 0);
			byte result = cpu.oNEG(0x80);
			Assert.AreEqual(0x80, result);
			Assert.AreEqual("efhiNzVC", cpu.flagsToString());
		}

		[Test, Order(40)]
		public void oNEG_0xFF()
		{
			TestContext.WriteLine("oNEG(0xFF)");

			cpu.set("flags", 0);
			byte result = cpu.oNEG(0xFF);
			Assert.AreEqual(1, result);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
		}

		[Test, Order(41)]
		public void SetPostByteRegister_0_0xFFFF()
		{
			TestContext.WriteLine("setPostByteRegister(0, 0xFFFF)");

			cpu.setPostByteRegister(0, 0xFFFF);
			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(0xFF, cpu.regB);
		}

		[Test, Order(42)]
		public void SetPostByteRegister_0x8_0xFFFF()
		{
			TestContext.WriteLine("setPostByteRegister(0x8, 0xFFFF)");

			cpu.setPostByteRegister(0x8, 0xFFFF);
			Assert.AreEqual(0xFF, cpu.regA);
		}

		[Test, Order(43)]
		public void GetPostByteRegister_0x0_D()
		{
			TestContext.WriteLine("getPostByteRegister(0x0) - D");

			cpu.regA = 0x44;
			cpu.regB = 0x99;
			ushort result = cpu.getPostByteRegister(0x0);
			Assert.AreEqual(0x4499, result);
		}

		[Test, Order(44)]
		public void GetPostByteRegister_0x5_PC()
		{
			TestContext.WriteLine("getPostByteRegister(0x5) - PC");

			cpu.regPC = 0x1234;
			ushort result = cpu.getPostByteRegister(0x5);
			Assert.AreEqual(0x1234, result);
		}

		[Test, Order(45)]
		public void GetPostByteRegister_0xA_CC()
		{
			TestContext.WriteLine("getPostByteRegister(0xA) - CC");

			cpu.regCC = 0xFF;
			ushort result = cpu.getPostByteRegister(0xA);
			Assert.AreEqual(0xFF, result);
		}

		[Test, Order(46)]
		public void TFREXG_Transfer_X_Y()
		{
			TestContext.WriteLine("TFREXG - transfer X -> Y");

			cpu.regX = 0xFFFF;
			cpu.regY = 0x7F;
			cpu.TFREXG(0x12, false);
			Assert.AreEqual(0xFFFF, cpu.regX);
			Assert.AreEqual(0xFFFF, cpu.regY);
		}

		[Test, Order(47)]
		public void TFREXG_Exchange_U_S()
		{
			TestContext.WriteLine("TFREXG - exchange U <-> S");

			cpu.regU = 0xFFFF;
			cpu.regS = 0x7F;
			cpu.TFREXG(0x34, true);
			Assert.AreEqual(0x7F, cpu.regU);
			Assert.AreEqual(0xFFFF, cpu.regS);
		}

		[Test, Order(48)]
		public void TFREXG_Transfer_A_CC()
		{
			TestContext.WriteLine("TFREXG - transfer A -> CC");

			cpu.regA = 0xFF;
			cpu.regCC = 0x7F;
			cpu.TFREXG(0x8A, false);
			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(0xFF, cpu.regCC);
		}

		[Test, Order(49)]
		public void TFREXG_Exchange_B_DP()
		{
			TestContext.WriteLine("TFREXG - exchange b <-> DP");

			cpu.regB = 0xFF;
			cpu.regDP = 0x7F;
			cpu.TFREXG(0x9B, true);
			Assert.AreEqual(0x7F, cpu.regB);
			Assert.AreEqual(0xFF, cpu.regDP);
		}

		[Test, Order(50)]
		public void GetAndSetState()
		{
			TestContext.WriteLine("get and set state");

			cpu.regA = 0x44;
			Cpu6809.State state = cpu.getState();
			cpu.regA = 0;
			cpu.setState(state);
			Assert.AreEqual(0x44, cpu.regA);
		}
	}
}
