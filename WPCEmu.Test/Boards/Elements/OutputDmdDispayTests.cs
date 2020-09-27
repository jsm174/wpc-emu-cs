using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class OutputDmdDisplayTests
	{
		OutputDmdDisplay outputDmdDisplay;

		[SetUp]
		public void Init()
		{
			outputDmdDisplay = OutputDmdDisplay.GetInstance(0x200);
		}

		[Test, Order(1)]
		public void ShouldReturnUndefinedWhenNoScanlineCopied()
		{
			TestContext.WriteLine("outputDmdDisplay, should return undefined when no scanline is copied");

			var result = outputDmdDisplay.executeCycle(1);
			Assert.AreEqual(null, result);
		}

		[Test, Order(2)]
		public void ShouldReturnJsonObjectWhenUndefinedWhenScanlineIsCopied()
		{
			TestContext.WriteLine("outputDmdDisplay, should return json object when scanline is copied");

			var result = outputDmdDisplay.executeCycle(100000000);
			Assert.AreEqual(true, result?.requestFIRQ);
			Assert.AreEqual(1, result?.scanline);

		}

		[Test, Order(3)]
		public void GetState()
		{
			TestContext.WriteLine("outputDmdDisplay, getState");

			var result = outputDmdDisplay.getState();
			Assert.AreEqual(0, result.scanline);
			Assert.AreEqual(0, result.activepage);
			Assert.AreEqual(null, result.nextActivePage);
			Assert.AreEqual(true, result.requestFIRQ);
			Assert.AreEqual(0, result.videoOutputPointer);
			Assert.AreEqual(0, result.ticksUpdateDmd);
			Assert.AreEqual(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, result.dmdPageMapping);
		}

		[Test, Order(4)]
		public void SelectDmdPage()
		{
			TestContext.WriteLine("outputDmdDisplay, selectDmdPage");

			outputDmdDisplay.selectDmdPage(1, 0xFF);
			var result = outputDmdDisplay.getState();
			Assert.AreEqual(new byte[] { 0, 0xF, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, result.dmdPageMapping);
		}

		[Test, Order(5)]
		public void SetNextActivePage()
		{
			TestContext.WriteLine("outputDmdDisplay, setNextActivePage");

			outputDmdDisplay.setNextActivePage(0xFF);
			var result = outputDmdDisplay.getState();
			Assert.AreEqual(0xF, result.nextActivePage);
		}

		[Test, Order(6)]
		public void WriteVideoRamAndReadVideoRam()
		{
			TestContext.WriteLine("outputDmdDisplay, writeVideoRam and readVideoRam");
			byte BANK = 2;
			byte OFFSET = 1;
			outputDmdDisplay.writeVideoRam(BANK, OFFSET, 33);
			var result = outputDmdDisplay.readVideoRam(BANK, OFFSET);
			Assert.AreEqual(33, result);
		}

		[Test, Order(7)]
		public void ShouldSwitchToNextActivePage()
		{
			TestContext.WriteLine("outputDmdDisplay, should switch to next active page");

			outputDmdDisplay.setNextActivePage(1);
			for (var i = 0; i < 0x20; i++)
			{
				outputDmdDisplay.executeCycle(10000000);
			}

			var result = outputDmdDisplay.getState();
			Assert.AreEqual(1, result.activepage);
			Assert.AreEqual(null, result.nextActivePage);
		}
	
		[Test, Order(9)]
		public void Render()
		{
			TestContext.WriteLine("outputDmdDisplay, render");

			for (byte i = 0; i < 16; i++)
			{
				for (byte j = 0; j < 222; j++)
				{
					outputDmdDisplay.writeVideoRam(i, j, 0xAA);
				}
			}
			outputDmdDisplay.executeCycle(10000000);

			var result = outputDmdDisplay.getState();
			var dmdHash = string.Join("", new SHA1Managed().ComputeHash(result.dmdShadedBuffer).Select(x => x.ToString("x2")).ToArray());
			Assert.AreEqual("1ceaf73df40e531df3bfb26b4fb7cd95fb7bff1d", dmdHash);
		}

		//[Test, Order(10)]
		//public void EmptySetState()
		//{
		//	TestContext.WriteLine("outputDmdDisplay, empty setState");
		//
		//	var result = outputDmdDisplay.setState();
		//	Assert.AreEqual(false, result);
		//}
	}
}
