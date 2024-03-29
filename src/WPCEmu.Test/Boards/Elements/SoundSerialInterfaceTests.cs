using NUnit.Framework;
using System.Collections.Generic;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class SoundSerialInterfaceTests
	{
		const bool PREDCS_SOUND = true;
		const bool DCS_SOUND = false;

		const byte PREDCS_VOLUME_COMMAND = 0x79;
		const byte PREDCS_EXTENDED_COMMAND = 0x7A;
		const byte DCS_VOLUME_COMMAND = 0x55;
		const byte DCS_VOLUME_GLOBAL = 0xAA;

		List<SoundBoardCallbackData> preDcsData;
		List<SoundBoardCallbackData> dcsData;
		SoundSerialInterface preDcsSound;
		SoundSerialInterface dcsSound;

		[SetUp]
		public void Init()
		{
			preDcsData = new List<SoundBoardCallbackData>();
			dcsData = new List<SoundBoardCallbackData>();
			preDcsSound = SoundSerialInterface.getInstance(PREDCS_SOUND);
			dcsSound = SoundSerialInterface.getInstance(DCS_SOUND);

			preDcsSound.reset();
			dcsSound.reset();

			preDcsSound.registerCallBack((data) => {
				preDcsData.Add(data);
			});
			dcsSound.registerCallBack((data) => {
				dcsData.Add(data);
			});
		}

		[Test, Order(1)]
		public void PreDCSNoControlDataAvailable()
		{
			TestContext.WriteLine("SoundSerialInterface preDCS, no control data available");

			var readControl = preDcsSound.readControl();
			Assert.That(readControl, Is.EqualTo(0xFF));
		}

		[Test, Order(2)]
		public void PreDCSShouldReadData()
		{
			TestContext.WriteLine("SoundSerialInterface preDCS, should read data");

			var readControl = preDcsSound.readData();
			Assert.That(readControl, Is.EqualTo(0x00));
		}

		[Test, Order(3)]
		public void PreDCSShouldProcessVolumeCommand()
		{
			TestContext.WriteLine("SoundSerialInterface preDCS, should process volume command");

			preDcsSound.writeData(PREDCS_VOLUME_COMMAND);
			preDcsSound.writeData(0x1F);
			preDcsSound.writeData(0xE0);
			Assert.That(preDcsData[0].command, Is.EqualTo("MAINVOLUME"));
			Assert.That(preDcsData[0].value, Is.EqualTo(31));
		}

		[Test, Order(4)]
		public void PreDCSShouldPlaySample1()
		{
			TestContext.WriteLine("SoundSerialInterface preDCS, should play sample 1");

			preDcsSound.writeData(1);
			Assert.That(preDcsData[0].command, Is.EqualTo("PLAYSAMPLE"));
			Assert.That(preDcsData[0].id, Is.EqualTo(1));
		}

		[Test, Order(5)]
		public void PreDCSShouldStopAllSamples()
		{
			TestContext.WriteLine("SoundSerialInterface preDCS, should stop all samples");

			preDcsSound.writeData(0);
			Assert.That(preDcsData[0].command, Is.EqualTo("STOPSOUND"));
		}
		
		[Test, Order(6)]
		public void PreDCSShouldPlayExtendedSample()
		{
			TestContext.WriteLine("SoundSerialInterface preDCS, should play extended sample");

			preDcsSound.writeData(PREDCS_EXTENDED_COMMAND);
			preDcsSound.writeData(1);
			Assert.That(preDcsData[0].command, Is.EqualTo("PLAYSAMPLE"));
			Assert.That(preDcsData[0].id, Is.EqualTo(31233));
		}

		[Test, Order(7)]
		public void DCSWriteVolume()
		{
			TestContext.WriteLine("SoundSerialInterface DCS, write volume");

			dcsSound.writeData(DCS_VOLUME_COMMAND);
			dcsSound.writeData(DCS_VOLUME_GLOBAL);
			dcsSound.writeData(0xFF);
			dcsSound.writeData(0x00);
			Assert.That(dcsData[0].command, Is.EqualTo("MAINVOLUME"));
			Assert.That(dcsData[0].value, Is.EqualTo(31));
		}

		[Test, Order(8)]
		public void DCSPlaySample()
		{
			TestContext.WriteLine("SoundSerialInterface DCS, play sample");

			dcsSound.writeData(0x88);
			dcsSound.writeData(0x77);
			Assert.That(dcsData[0].command, Is.EqualTo("PLAYSAMPLE"));
			Assert.That(dcsData[0].id, Is.EqualTo(0x8877));
		}

		[Test, Order(9)]
		public void DCSReplyFromUnknown_0x03D2_Command()
		{
			TestContext.WriteLine("SoundSerialInterface DCS, get reply from unknown 0x03D2 command (SAFE CRACKER)");

			dcsSound.writeData(0x03);
			dcsSound.writeData(0xD2);
			Assert.That(dcsSound.readData(), Is.EqualTo(0x01));
		}

		[Test, Order(10)]
		public void DCSReplyFromUnknown_0x03D3_Command()
		{
			TestContext.WriteLine("SoundSerialInterface DCS, get reply from unknown 0x03D3 command (AFM + CONGO)");

			dcsSound.writeData(0x03);
			dcsSound.writeData(0xD3);
			Assert.That(dcsSound.readData(), Is.EqualTo(0x01));
		}

		[Test, Order(11)]
		public void DCSReplyFromUnknown_0x03E7_Command()
		{
			TestContext.WriteLine("SoundSerialInterface DCS, get reply from getVersion call (0x03E7)");

			dcsSound.writeData(0x03);
			dcsSound.writeData(0xE7);
			Assert.That(dcsSound.readData(), Is.EqualTo(0x10));
		}
	}
}
