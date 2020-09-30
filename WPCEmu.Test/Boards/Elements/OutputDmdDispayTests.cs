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
			Assert.That(result, Is.EqualTo(null));
		}

		[Test, Order(2)]
		public void ShouldReturnJsonObjectWhenUndefinedWhenScanlineIsCopied()
		{
			TestContext.WriteLine("outputDmdDisplay, should return json object when scanline is copied");

			var result = outputDmdDisplay.executeCycle(100000000);
			Assert.That(result?.requestFIRQ, Is.EqualTo(true));
			Assert.That(result?.scanline, Is.EqualTo(1));

		}

		[Test, Order(3)]
		public void GetState()
		{
			TestContext.WriteLine("outputDmdDisplay, getState");

			var result = outputDmdDisplay.getState();
			Assert.That(result.scanline, Is.EqualTo(0));
			Assert.That(result.activepage, Is.EqualTo(0));
			Assert.That(result.nextActivePage, Is.EqualTo(null));
			Assert.That(result.requestFIRQ, Is.EqualTo(true));
			Assert.That(result.videoOutputPointer, Is.EqualTo(0));
			Assert.That(result.ticksUpdateDmd, Is.EqualTo(0));
			Assert.That(result.dmdPageMapping, Is.EqualTo(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));
		}

		[Test, Order(4)]
		public void SelectDmdPage()
		{
			TestContext.WriteLine("outputDmdDisplay, selectDmdPage");

			outputDmdDisplay.selectDmdPage(1, 0xFF);
			var result = outputDmdDisplay.getState();
			Assert.That(result.dmdPageMapping, Is.EqualTo(new byte[] { 0, 0xF, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));
		}

		[Test, Order(5)]
		public void SetNextActivePage()
		{
			TestContext.WriteLine("outputDmdDisplay, setNextActivePage");

			outputDmdDisplay.setNextActivePage(0xFF);
			var result = outputDmdDisplay.getState();
			Assert.That(result.nextActivePage, Is.EqualTo(0xF));
		}

		[Test, Order(6)]
		public void WriteVideoRamAndReadVideoRam()
		{
			TestContext.WriteLine("outputDmdDisplay, writeVideoRam and readVideoRam");
			byte BANK = 2;
			byte OFFSET = 1;
			outputDmdDisplay.writeVideoRam(BANK, OFFSET, 33);
			var result = outputDmdDisplay.readVideoRam(BANK, OFFSET);
			Assert.That(result, Is.EqualTo(33));
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
			Assert.That(result.activepage, Is.EqualTo(1));
			Assert.That(result.nextActivePage, Is.EqualTo(null));
		}
	
		[Test, Order(9)]
		public void Render()
		{
			TestContext.WriteLine("outputDmdDisplay, render");

			for (var i = 0; i < 16; i++)
			{
				for (var j = 0; j < 222; j++)
				{
					outputDmdDisplay.writeVideoRam((byte) i, (ushort)j, 0xAA);
				}
			}
			outputDmdDisplay.executeCycle(10000000);

			var result = outputDmdDisplay.getState();
			var dmdHash = string.Join("", new SHA1Managed().ComputeHash(result.dmdShadedBuffer).Select(x => x.ToString("x2")).ToArray());
			Assert.That(dmdHash, Is.EqualTo("1ceaf73df40e531df3bfb26b4fb7cd95fb7bff1d"));
		}

		[Test, Order(10)]
		public void EmptySetState()
		{
			TestContext.WriteLine("outputDmdDisplay, empty setState");
		
			var result = outputDmdDisplay.setState();
			Assert.That(result, Is.EqualTo(false));
		}
	}
}
