using System.Collections.Generic;
using NUnit.Framework;
using WPCEmu.Boards;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class DisplayBoardTests
	{
		struct TestData
		{
			public ushort offset;
			public ushort command;
		}

		DisplayBoard displayBoard;

		[SetUp]
		public void Init()
		{
			byte[] ram = new byte[0x4000];

			displayBoard = DisplayBoard.GetInstance(new WpcCpuBoard.InitObject
			{
				interruptCallback = new WpcCpuBoard.InterruptCallback(),
				ram = ram
			});
		}

		[Test, Order(1)]
		public void ShouldWriteToHardwareRam()
		{
			TestContext.WriteLine("displayBoard, should write to hardwareRam");

			displayBoard.write(0x3FBD, 255);
			Assert.That(displayBoard.ram[0x3FBD], Is.EqualTo(0xFF));
		}

		[Test, Order(2)]
		public void ShouldReadToWrite_WPC_DMD_SCANLINE()
		{
			TestContext.WriteLine("displayBoard, should read to WPC_DMD_SCANLINE");

			var result = displayBoard.read(0x3FBD);
			Assert.That(result, Is.EqualTo(0x0));
		}

		[Test, Order(3)]
		public void ShouldMap_WPC_DMD_LOW_PAGE()
		{
			TestContext.WriteLine("displayBoard, should map WPC_DMD_LOW_PAGE");

			displayBoard.write(DisplayBoard.OP.WPC_DMD_LOW_PAGE, 2);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).dmdPageMapping[0], Is.EqualTo(2));
		}

		[Test, Order(4)]
		public void ShouldMap_WPC_DMD_LOW_PAGE_WrapValue()
		{
			TestContext.WriteLine("displayBoard, should map WPC_DMD_LOW_PAGE, wrap value");

			displayBoard.write(DisplayBoard.OP.WPC_DMD_LOW_PAGE, 0xFF);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).dmdPageMapping[0], Is.EqualTo(0xF));
		}

		[Test, Order(5)]
		public void ShouldMap_WPC_DMD_HIGH_PAGE()
		{
			TestContext.WriteLine("displayBoard, should map WPC_DMD_HIGH_PAGE");

			displayBoard.write(DisplayBoard.OP.WPC_DMD_HIGH_PAGE, 3);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).dmdPageMapping[1], Is.EqualTo(3));
		}

		[Test, Order(6)]
		public void ShouldMap_WPC95_DMD_PAGE3000()
		{
			TestContext.WriteLine("displayBoard, should map WPC95_DMD_PAGE3000");

			displayBoard.write(DisplayBoard.OP.WPC95_DMD_PAGE3000, 5);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).dmdPageMapping[2], Is.EqualTo(5));
		}

		[Test, Order(7)]
		public void ShouldMap_WPC95_DMD_PAGE3200()
		{
			TestContext.WriteLine("displayBoard, should map WPC95_DMD_PAGE3200");

			displayBoard.write(DisplayBoard.OP.WPC95_DMD_PAGE3200, 6);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).dmdPageMapping[3], Is.EqualTo(6));
		}

		[Test, Order(8)]
		public void ShouldMap_WPC95_DMD_PAGE3400()
		{
			TestContext.WriteLine("displayBoard, should map WPC95_DMD_PAGE3400");

			displayBoard.write(DisplayBoard.OP.WPC95_DMD_PAGE3400, 7);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).dmdPageMapping[4], Is.EqualTo(7));
		}

		[Test, Order(9)]
		public void ShouldMap_WPC95_DMD_PAGE3600()
		{
			TestContext.WriteLine("displayBoard, should map WPC95_DMD_PAGE3600");

			displayBoard.write(DisplayBoard.OP.WPC95_DMD_PAGE3600, 8);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).dmdPageMapping[5], Is.EqualTo(8));
		}

		[Test, Order(10)]
		public void ShouldWriteNextActivePageWrapAround()
		{
			TestContext.WriteLine("displayBoard, should write next active page, wrap around");

			displayBoard.write(DisplayBoard.OP.WPC_DMD_ACTIVE_PAGE, 0xFF);
			var result = displayBoard.getState();
			Assert.That(((OutputDmdDisplay.State)result).nextActivePage, Is.EqualTo(0xF));
		}

		[Test, Order(11)]
		public void ShouldWriteToDmdRam()
		{
			List<TestData> list = new List<TestData>()
			{
				new TestData { offset = 0x3800, command = DisplayBoard.OP.WPC_DMD_LOW_PAGE },
				new TestData { offset = 0x3A00, command = DisplayBoard.OP.WPC_DMD_HIGH_PAGE },
				new TestData { offset = 0x3000, command = DisplayBoard.OP.WPC95_DMD_PAGE3000 },
				new TestData { offset = 0x3200, command = DisplayBoard.OP.WPC95_DMD_PAGE3200 },
				new TestData { offset = 0x3400, command = DisplayBoard.OP.WPC95_DMD_PAGE3400 },
				new TestData { offset = 0x3600, command = DisplayBoard.OP.WPC95_DMD_PAGE3600 }
			};

			list.ForEach((testSet) =>
			{
				TestContext.WriteLine("displayboard, should write to DMD RAM {0}", testSet.offset);

				Init();

				const byte PAGE = 2;
				const byte VALUE = 0xFE;
				displayBoard.write(testSet.command, PAGE);
				displayBoard.write(testSet.offset, VALUE);
				var result = displayBoard.read(testSet.offset);
				Assert.That(result, Is.EqualTo(VALUE));
				var state = displayBoard.getState();
				Assert.That(((OutputDmdDisplay.State)state).videoRam[0x200 * PAGE], Is.EqualTo(VALUE));
			});
		}
	}
}
