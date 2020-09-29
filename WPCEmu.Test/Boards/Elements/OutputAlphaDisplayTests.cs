using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class OutputAlphaDisplayTests
	{
		OutputAlphaDisplay outputAlphaDisplay;

		[SetUp]
		public void Init()
		{
			outputAlphaDisplay = OutputAlphaDisplay.GetInstance(0x200);
		}

		[Test, Order(1)]
		public void SetSegmentColumn()
		{
			TestContext.WriteLine("outputAlphaDisplay, setSegmentColumn");

			outputAlphaDisplay.setSegmentColumn(0xFF);
			Assert.AreEqual(0xF, outputAlphaDisplay.segmentColumn);
		}

		[Test, Order(2)]
		public void GetState()
		{
			TestContext.WriteLine("outputAlphaDisplay, getState");

			var result = outputAlphaDisplay.getState();
			Assert.AreEqual(0, result.scanline);
			Assert.AreEqual(Enumerable.Repeat((byte)0, 0x200 * 8).ToArray(), result.dmdShaddedBuffer);
		}

		[Test, Order(3)]
		public void EmptySetState()
		{
			TestContext.WriteLine("outputAlphaDisplay, empty setState");
		
			var result = outputAlphaDisplay.setState();
			Assert.AreEqual(false, result);
		}

		[Test, Order(4)]
		public void SetState()
		{
			TestContext.WriteLine("outputAlphaDisplay, setState");

			outputAlphaDisplay.setState(new OutputAlphaDisplay.State
			{
				scanline = 5
			});
			var result = outputAlphaDisplay.getState();
			Assert.AreEqual(5, result.scanline);
		}

		[Test, Order(5)]
		public void SetRow1Low()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow1 low");

			outputAlphaDisplay.setRow1(false, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.AreEqual(0x00FF, result[0]);
			Assert.AreEqual(0x0000, result[1]);
		}

		[Test, Order(6)]
		public void SetRow1High()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow1 high");

			outputAlphaDisplay.setRow1(true, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.AreEqual(0xFF00, result[0]);
			Assert.AreEqual(0x0000, result[1]);
		}

		[Test, Order(7)]
		public void SetRow2Low()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow2 low");

			outputAlphaDisplay.setSegmentColumn(1);
			outputAlphaDisplay.setRow2(false, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.AreEqual(0x00FF, result[17]);
		}

		[Test, Order(8)]
		public void SetRow2High()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow2 high");

			outputAlphaDisplay.setSegmentColumn(2);
			outputAlphaDisplay.setRow2(true, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.AreEqual(0xFF00, result[18]);
		}

		[Test, Order(9)]
		public void Render()
		{
			TestContext.WriteLine("outputAlphaDisplay, render");

			for (var i = 0; i < 16; i++)
			{
				outputAlphaDisplay.setSegmentColumn((byte)i);
				outputAlphaDisplay.setRow1(true, 0xFFFF);
				outputAlphaDisplay.setRow1(false, 0xFFFF);
				outputAlphaDisplay.setRow2(true, 0xFFFF);
				outputAlphaDisplay.setRow2(false, 0xFFFF);
			}
			outputAlphaDisplay.executeCycle(10000000);

			var result = outputAlphaDisplay.getState();
			var dmdHash = string.Join("", new SHA1Managed().ComputeHash(result.dmdShaddedBuffer).Select(x => x.ToString("x2")).ToArray());
			Assert.AreEqual("d3706dc84ddfc27e5fa0ba57a8f469fac4472bda", dmdHash);
		}
	}
}
