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
			outputAlphaDisplay = OutputAlphaDisplay.getInstance(0x200);
		}

		[Test, Order(1)]
		public void SetSegmentColumn()
		{
			TestContext.WriteLine("outputAlphaDisplay, setSegmentColumn");

			outputAlphaDisplay.setSegmentColumn(0xFF);
			Assert.That(outputAlphaDisplay.segmentColumn, Is.EqualTo(0xF));
		}

		[Test, Order(2)]
		public void GetState()
		{
			TestContext.WriteLine("outputAlphaDisplay, getState");

			var result = outputAlphaDisplay.getState();
			Assert.That(result.scanline, Is.EqualTo(0));
			Assert.That(result.dmdShaddedBuffer, Is.EqualTo(Enumerable.Repeat((byte)0, 0x200 * 8).ToArray()));
		}

		[Test, Order(3)]
		public void EmptySetState()
		{
			TestContext.WriteLine("outputAlphaDisplay, empty setState");
		
			var result = outputAlphaDisplay.setState();
			Assert.That(result, Is.EqualTo(false));
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
			Assert.That(result.scanline, Is.EqualTo(5));
		}

		[Test, Order(5)]
		public void SetRow1Low()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow1 low");

			outputAlphaDisplay.setRow1(false, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.That(result[0], Is.EqualTo(0x00FF));
			Assert.That(result[1], Is.EqualTo(0x0000));
		}

		[Test, Order(6)]
		public void SetRow1High()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow1 high");

			outputAlphaDisplay.setRow1(true, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.That(result[0], Is.EqualTo(0xFF00));
			Assert.That(result[1], Is.EqualTo(0x0000));
		}

		[Test, Order(7)]
		public void SetRow2Low()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow2 low");

			outputAlphaDisplay.setSegmentColumn(1);
			outputAlphaDisplay.setRow2(false, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.That(result[17], Is.EqualTo(0x00FF));
		}

		[Test, Order(8)]
		public void SetRow2High()
		{
			TestContext.WriteLine("outputAlphaDisplay, setRow2 high");

			outputAlphaDisplay.setSegmentColumn(2);
			outputAlphaDisplay.setRow2(true, 0xFFFF);
			var result = outputAlphaDisplay.displayData;
			Assert.That(result[18], Is.EqualTo(0xFF00));
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
			Assert.That(dmdHash, Is.EqualTo("d3706dc84ddfc27e5fa0ba57a8f469fac4472bda"));
		}
	}
}
