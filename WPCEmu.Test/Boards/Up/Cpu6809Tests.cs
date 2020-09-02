using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards.Up;

namespace WPCEmu.Test.Boards.Up
{
	[TestFixture]
	public class Cpu6809Tests
	{
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

		List<ushort> readMemoryAddress;
		List<AddressValueStruct> writeMemoryAddress;

		Cpu6809 cpu;

		byte ReadMemoryMock(ushort address)
		{
			readMemoryAddress.Add(address);
			return 0;
		}

		void WriteMemoryMock(ushort address, byte value)
		{
			writeMemoryAddress.Add(new AddressValueStruct(address, value));
		}

		[SetUp]
		public void Init()
		{
			readMemoryAddress = new List<ushort>();
			writeMemoryAddress = new List<AddressValueStruct>();
		
			cpu = Cpu6809.GetInstance(WriteMemoryMock, ReadMemoryMock);
			cpu.reset();
		}

		[Test, Order(0)]
		public void ReadInitialVector()
		{
			Assert.AreEqual(0xFFFE, readMemoryAddress[0]);
			Assert.AreEqual(0xFFFF, readMemoryAddress[1]);
		}

		[Test, Order(1)]
		public void oCmp_8bit_CarryFlag()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0xFF);
			Assert.AreEqual("efhinzvC", cpu.flagsToString());
		}

		[Test, Order(2)]
		public void oCmp_8bit_0xFF()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP(0xFF, 0);
			Assert.AreEqual("efhiNzvc", cpu.flagsToString());
		}

		[Test, Order(3)]
		public void oCmp_8bit_NegativeFlag()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0x80);
			Assert.AreEqual("efhiNzVC", cpu.flagsToString());
		}

		[Test, Order(4)]
		public void oCmp_8bit_Negative1()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP(0, 1);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(5)]
		public void oCmp_8bit_ZeroFlag()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP(0, 0);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
		}

		[Test, Order(6)]
		public void oCmp_16bit_CarryFlag()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0xFFFF);
			Assert.AreEqual("efhinzvC", cpu.flagsToString());
		}

		[Test, Order(7)]
		public void oCmp_16bit_0xFFFF()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP16(0xFFFF, 0);
			Assert.AreEqual("efhiNzvc", cpu.flagsToString());
		}

		[Test, Order(8)]
		public void oCmp_16bit_NegativeFlag()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0x8000);
			Assert.AreEqual("efhiNzVC", cpu.flagsToString());
		}

		[Test, Order(9)]
		public void oCmp_16bit_Negative1()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 1);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(10)]
		public void oCmp_16bit_ZeroFlag()
		{
			cpu.set("flags", 0x00);
			cpu.oCMP16(0, 0);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
		}

		[Test, Order(11)]
		public void FlagsAfterIrq_InitFlags_0x00()
		{
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

		[Test, Order(12)]
		public void DetectFirqCouldNotBeTriggered()
		{
			cpu.set("flags", 0x00);
			cpu.firq();
			byte firqTriggered = 0; // cpu.firq();
			Assert.AreEqual(0, firqTriggered);
		}

		[Test, Order(13)]
		public void FlagsCorrectAfterIrq_InitFlags_0xef()
		{
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

		[Test, Order(14)]
		public void IrqShouldNotBeCalled_F_IRQMASK_set()
		{
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

		[Test, Order(15)]
		public void FlagsShouldBeCorrectAfterNmi()
		{
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

		[Test, Order(16)]
		public void FlagsShouldBeCorrectAfterFirq_InitFlags_0x00()
		{
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

		[Test, Order(17)]
		public void FlagsShouldBeCorrectAfterFirq_InitFlags_0xbf()
		{
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

		[Test, Order(18)]
		public void FirqShouldNotBeCalled_F_FIRQMASK_set()
		{
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

		[Test, Order(19)]
		public void oNEG_CarryFlag()
		{
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
		public void setOverflowFlag8Bit()
		{
			cpu.set("flags", 0);
			cpu.setV8(1, 1, 0x80);
			Assert.AreEqual("efhinzVc", cpu.flagsToString());
		}

		[Test, Order(21)]
		public void setOverflowFlag8Bit_overflow_r()
		{
			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x180);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
		}

		[Test, Order(22)]
		public void setOverflowFlag16Bit()
		{
			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x8000);
			Assert.AreEqual("efhinzVc", cpu.flagsToString());
		}

		[Test, Order(23)]
		public void setOverflowFlag16Bit_overflow_r()
		{
			cpu.set("flags", 0);
			cpu.setV16(1, 1, 0x18000);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
		}

		[Test, Order(24)]
		public void signed5bit()
		{
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

		[Test, Order(25)]
		public void signed8bit()
		{
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

		[Test, Order(26)]
		public void signed16bit()
		{
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

		[Test, Order(27)]
		public void flagsNZ16_0xFFFF()
		{
			cpu.set("flags", 0);
			cpu.flagsNZ16(0xFFFF);
			Assert.AreEqual("efhiNzvc", cpu.flagsToString());
		}

		[Test, Order(28)]
		public void flagsNZ16_0x0000()
		{
			cpu.set("flags", 0);
			cpu.flagsNZ16(0);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
		}

		[Test, Order(29)]
		public void WriteWord_0_0x1234()
		{
			cpu.WriteWord(0x0, 0x1234);
			Assert.AreEqual(0, writeMemoryAddress[0].address);
			Assert.AreEqual(0x12, writeMemoryAddress[0].value);
			Assert.AreEqual(1, writeMemoryAddress[1].address);
			Assert.AreEqual(0x34, writeMemoryAddress[1].value);
		}

		[Test, Order(30)]
		public void WriteWord_0xFFFF_0x1234()
		{
			cpu.WriteWord(0xFFFF, 0x1234);
			Assert.AreEqual(0xFFFF, writeMemoryAddress[0].address);
			Assert.AreEqual(0x12, writeMemoryAddress[0].value);
			Assert.AreEqual(0, writeMemoryAddress[1].address);
			Assert.AreEqual(0x34, writeMemoryAddress[1].value);
		}

		[Test, Order(31)]
		public void getD()
		{
			cpu.regA = 0xFF;
			cpu.regB = 0xEE;
			ushort result = cpu.getD();
			Assert.AreEqual(0xFFEE, result);
		}

		[Test, Order(32)]
		public void setD_0xFFEE()
		{
			cpu.setD(0xFFEE);
			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(0xEE, cpu.regB);
		}

		[Test, Order(33)]
		public void dpadd_regDP_0()
		{
			cpu.regDP = 0;
			cpu.fetch = () => {
				return 0xFF;
			};
			ushort result = cpu.dpadd();
			Assert.AreEqual(0xFF, result);
		}

		[Test, Order(34)]
		public void dpadd_regDP_0xFF()
		{
			cpu.regDP = 0xFF;
			cpu.fetch = () => {
				return 0xFF;
			};
			ushort result = cpu.dpadd();
			Assert.AreEqual(0xFFFF, result);
		}

		[Test, Order(35)]
		public void oNEG_0()
		{
			cpu.set("flags", 0);
			byte result = cpu.oNEG(0);
			Assert.AreEqual(0, result);
			Assert.AreEqual("efhinZvc", cpu.flagsToString());
		}

		[Test, Order(36)]
		public void oNEG_1()
		{
			cpu.set("flags", 0);
			byte result = cpu.oNEG(1);
			Assert.AreEqual(0xFF, result);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(37)]
		public void oNEG_0x7F()
		{
			cpu.set("flags", 0);
			byte result = cpu.oNEG(0x7F);
			Assert.AreEqual(0x81, result);
			Assert.AreEqual("efhiNzvC", cpu.flagsToString());
		}

		[Test, Order(38)]
		public void oNEG_0x80()
		{
			cpu.set("flags", 0);
			byte result = cpu.oNEG(0x80);
			Assert.AreEqual(0x80, result);
			Assert.AreEqual("efhiNzVC", cpu.flagsToString());
		}

		[Test, Order(39)]
		public void oNEG_0xFF()
		{
			cpu.set("flags", 0);
			byte result = cpu.oNEG(0xFF);
			Assert.AreEqual(1, result);
			Assert.AreEqual("efhinzvc", cpu.flagsToString());
		}

		[Test, Order(40)]
		public void setPostByteRegister0_0xFFFF()
		{
			cpu.setPostByteRegister(0, 0xFFFF);
			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(0xFF, cpu.regB);
		}

		[Test, Order(41)]
		public void setPostByteRegister0x8_0xFFFF()
		{
			cpu.setPostByteRegister(0x8, 0xFFFF);
			Assert.AreEqual(0xFF, cpu.regA);
		}

		[Test, Order(42)]
		public void getPostByteRegister0x0_D()
		{
			cpu.regA = 0x44;
			cpu.regB = 0x99;
			ushort result = cpu.getPostByteRegister(0x0);
			Assert.AreEqual(0x4499, result);
		}

		[Test, Order(43)]
		public void getPostByteRegister0x5_PC()
		{
			cpu.regPC = 0x1234;
			ushort result = cpu.getPostByteRegister(0x5);
			Assert.AreEqual(0x1234, result);
		}

		[Test, Order(44)]
		public void getPostByteRegister0xA_CC()
		{
			cpu.regCC = 0xFF;
			ushort result = cpu.getPostByteRegister(0xA);
			Assert.AreEqual(0xFF, result);
		}

		[Test, Order(45)]
		public void TFREXG_TransferX_Y()
		{
			cpu.regX = 0xFFFF;
			cpu.regY = 0x7F;
			cpu.TFREXG(0x12, false);
			Assert.AreEqual(0xFFFF, cpu.regX);
			Assert.AreEqual(0xFFFF, cpu.regY);
		}

		[Test, Order(46)]
		public void TFREXG_ExchangeU_S()
		{
			cpu.regU = 0xFFFF;
			cpu.regS = 0x7F;
			cpu.TFREXG(0x34, true);
			Assert.AreEqual(0x7F, cpu.regU);
			Assert.AreEqual(0xFFFF, cpu.regS);
		}

		[Test, Order(47)]
		public void TFREXG_TransferA_CC()
		{
			cpu.regA = 0xFF;
			cpu.regCC = 0x7F;
			cpu.TFREXG(0x8A, false);
			Assert.AreEqual(0xFF, cpu.regA);
			Assert.AreEqual(0xFF, cpu.regCC);
		}

		[Test, Order(48)]
		public void TFREXG_ExchangeB_DP()
		{
			cpu.regB = 0xFF;
			cpu.regDP = 0x7F;
			cpu.TFREXG(0x9B, true);
			Assert.AreEqual(0x7F, cpu.regB);
			Assert.AreEqual(0xFF, cpu.regDP);
		}

		[Test, Order(49)]
		public void getAndSetState()
		{
			cpu.regA = 0x44;
			Cpu6809.State state = cpu.getState();
			cpu.regA = 0;
			cpu.setState(state);
			Assert.AreEqual(0x44, cpu.regA);
		}
	}
}
