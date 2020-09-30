using NUnit.Framework;
using WPCEmu.Boards;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class AsicTests
	{
		CpuBoardAsic wpc;

		[SetUp]
		public void Init()
		{
			byte[] ram = new byte[0x4000];

			wpc = CpuBoardAsic.getInstance(new WpcCpuBoard.InitObject
			{
				interruptCallback = new WpcCpuBoard.InterruptCallback(),
				ram = ram
			});
		}

		[Test, Order(1)]
		public void ShouldSetZeroCrossFlag()
		{
			TestContext.WriteLine("wpc, should set zerocross flag");

			wpc.setZeroCrossFlag();
			Assert.That(wpc.zeroCrossFlag, Is.EqualTo(1));
		}

		[Test, Order(2)]
		public void ShouldClearZeroCrossFlagWhenRead()
		{
			TestContext.WriteLine("wpc, should clear zerocross flag when read");

			wpc.setZeroCrossFlag();
			var result = wpc.read(CpuBoardAsic.OP.WPC_ZEROCROSS_IRQ_CLEAR);
			Assert.That(result, Is.EqualTo(0x80));
			Assert.That(wpc.zeroCrossFlag, Is.EqualTo(0));
		}

		[Test, Order(3)]
		public void ShouldRespectOldZeroCrossFlagWhenRead()
		{
			TestContext.WriteLine("wpc, should respect old zerocross flag state when read");

			wpc.write(CpuBoardAsic.OP.WPC_ZEROCROSS_IRQ_CLEAR, 0x7F);
			wpc.setZeroCrossFlag();
			var result = wpc.read(CpuBoardAsic.OP.WPC_ZEROCROSS_IRQ_CLEAR);
			Assert.That(result, Is.EqualTo(0xFF));
			Assert.That(wpc.zeroCrossFlag, Is.EqualTo(0));
		}

		[Test, Order(4)]
		public void ShouldRespectOldZeroCrossFlagWhenReadXXX()
		{
			TestContext.WriteLine("wpc, should respect old zerocross flag state when read,xxx");

			wpc.write(CpuBoardAsic.OP.WPC_ZEROCROSS_IRQ_CLEAR, 0x10);
			var result = wpc.read(CpuBoardAsic.OP.WPC_ZEROCROSS_IRQ_CLEAR);
			Assert.That(result, Is.EqualTo(0x10));
			Assert.That(wpc.zeroCrossFlag, Is.EqualTo(0));
		}

		[Test, Order(5)]
		public void Should_WPC_SHIFTADDRL_By_0xFF()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTADDRL by 0xFF");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRL, 0x08);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0xFF);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTADDRL);
			Assert.That(result, Is.EqualTo(0x27));
		}

		[Test, Order(6)]
		public void Should_WPC_SHIFTADDRL_0x00_By_0x80()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTADDRL 0x00 by 0x80");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRL, 0x00);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0x80);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTADDRL);
			Assert.That(result, Is.EqualTo(0x10));
		}

		[Test, Order(7)]
		public void Should_WPC_SHIFTADDRL_0x00_By_0x40()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTADDRL 0x00 by 0x40");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRL, 0x00);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0x40);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTADDRL);
			Assert.That(result, Is.EqualTo(0x08));
		}

		[Test, Order(8)]
		public void Should_WPC_SHIFTADDRL_By_0x1()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTADDRL by 0x1 - not sure");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRL, 0x08);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0x1);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTADDRL);
			Assert.That(result, Is.EqualTo(0x08));
		}

		[Test, Order(9)]
		public void Should_WPC_SHIFTADDRH()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTADDRH - not sure");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRH, 0xA8);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRL, 0x08);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0xFF);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTADDRH);
			Assert.That(result, Is.EqualTo(0xA8));
		}

		[Test, Order(10)]
		public void Should_WPC_SHIFTADDRH_MaxValue()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTADDRH, max value");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRH, 0xFF);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTADDRL, 0xFF);
			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0xFF);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTADDRH);
			Assert.That(result, Is.EqualTo(0x0));
		}

		[Test, Order(11)]
		public void Should_WPC_SHIFTBIT_0x00_SetBit0()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTBIT 0x00 (set bit 0)");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0x00);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTBIT);
			Assert.That(result, Is.EqualTo(0x01));
		}

		[Test, Order(12)]
		public void Should_WPC_SHIFTBIT_0x01_SetBit1()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTBIT 0x01 (set bit 1)");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0x01);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTBIT);
			Assert.That(result, Is.EqualTo(0x02));
		}

		[Test, Order(13)]
		public void Should_WPC_SHIFTBIT_0x04_SetBit5()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTBIT 0x04 (set bit 5)");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0x04);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTBIT);
			Assert.That(result, Is.EqualTo(0x10));
		}

		[Test, Order(14)]
		public void Should_WPC_SHIFTBIT_0x07_SetBit7()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTBIT 0x07 (set bit 7)");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0xFF);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTBIT);
			Assert.That(result, Is.EqualTo(0x80));
		}

		[Test, Order(15)]
		public void Should_WPC_SHIFTBIT2_0x07_SetBit7()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTBIT2 0x07 (set bit 7)");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT2, 0xFF);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTBIT2);
			Assert.That(result, Is.EqualTo(0x80));
		}

		[Test, Order(16)]
		public void Should_WPC_SHIFTBIT_0xFF_SetBit7()
		{
			TestContext.WriteLine("wpc, should WPC_SHIFTBIT 0xFF (set bit 7)");

			wpc.write(CpuBoardAsic.OP.WPC_SHIFTBIT, 0xFF);
			var result = wpc.read(CpuBoardAsic.OP.WPC_SHIFTBIT);
			Assert.That(result, Is.EqualTo(0x80));
		}

		[Test, Order(17)]
		public void UpdateActiveLamp()
		{
			TestContext.WriteLine("wpc, update active lamp");

			wpc.write(CpuBoardAsic.OP.WPC_LAMP_ROW_OUTPUT, 0x4);
			wpc.write(CpuBoardAsic.OP.WPC_LAMP_COL_STROBE, 0x4);
			var result = wpc.getState().lampState;
			Assert.That(result[18], Is.EqualTo(0xFF));
		}

		//[Test, Order(18)]
		//public void GetTimeUpdateChecksum()
		//{
		//	TestContext.WriteLine("wpc, get time, should update checksum");
		//
		//	byte hours = wpc.read(CpuBoardAsic.OP.WPC_CLK_HOURS_DAYS);
		//	//I don't know why, but travis is always a hour behind
		//	bool is3or4Hour = hours == 4 || hours == 3;
		//	Assert.That(is3or4Hour, Is.EqualTo(true));
		//	Assert.That(wpc.ram[0x1807], Is.EqualTo(255));
		//	Assert.That(wpc.ram[0x1808], Is.EqualTo(62));
		//}

		[Test, Order(19)]
		public void WriteAndReadFliptronics()
		{
			TestContext.WriteLine("wpc, write and read fliptronics");

			wpc.setFliptronicsInput("F4");
			var result = wpc.read(CpuBoardAsic.OP.WPC_FLIPTRONICS_FLIPPER_PORT_A);
			Assert.That(result, Is.EqualTo(247));
		}

		[Test, Order(20)]
		public void IgnoreEmptyState()
		{
			TestContext.WriteLine("wpc, ignore empty setState");

			var result = wpc.setState();
			Assert.That(result, Is.EqualTo(false));
		}

		[Test, Order(21)]
		public void GetStateSetState()
		{
			TestContext.WriteLine("wpc, getState / setState");

			wpc.romBank = 11;
			CpuBoardAsic.State state = wpc.getState();
			wpc.romBank = 2;
			wpc.setState(state);
			Assert.That(wpc.romBank, Is.EqualTo(11));
		}

		[Test, Order(22)]
		public void ShouldResetBlanking()
		{
			TestContext.WriteLine("wpc, should reset blanking");

			wpc.write(CpuBoardAsic.OP.WPC_ZEROCROSS_IRQ_CLEAR, 0x02);
			Assert.That(wpc.blankSignalHigh, Is.EqualTo(false));
		}

		[Test, Order(23)]
		public void ShouldNotResetBlanking()
		{
			TestContext.WriteLine("wpc, should NOT reset blanking");

			wpc.write(CpuBoardAsic.OP.WPC_ZEROCROSS_IRQ_CLEAR, 0x04);
			Assert.That(wpc.blankSignalHigh, Is.EqualTo(true));
		}
	}
}
