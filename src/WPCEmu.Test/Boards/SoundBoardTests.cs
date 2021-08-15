using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using WPCEmu.Boards;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class SoundBoardTests
	{
		const ushort WPC_SOUND_DATA = SoundBoard.OP.WPC_SOUND_DATA;
		const ushort WPC_SOUND_CONTROL_STATUS = SoundBoard.OP.WPC_SOUND_CONTROL_STATUS;

		SoundBoard instancePreDcs;
		SoundBoard instanceDcs;
		List<SoundBoardCallbackData> playbackArray;

		[SetUp]
		public void Init()
		{
			var initObjectPreDcs = new WpcCpuBoard.InitObject
			{
				interruptCallback = new InterruptCallbackData
				{
					firqFromDmd = () => { }
				},
				romObject = new RomObject
				{
					preDcsSoundboard = true
				}
			};
			playbackArray = new List<SoundBoardCallbackData>();

			instancePreDcs = SoundBoard.getInstance(initObjectPreDcs);
			instancePreDcs.reset();
			instancePreDcs.registerSoundBoardCallback((msg) =>
			{
				Debug.Print("CALLBACK!", msg);
				playbackArray.Add(msg);
			});

			var initObjectDcs = new WpcCpuBoard.InitObject
			{
				interruptCallback = new InterruptCallbackData
				{
					firqFromDmd = () => { }
				},
				romObject = new RomObject
				{
					preDcsSoundboard = false
				}
			};

			instanceDcs = SoundBoard.getInstance(initObjectDcs);
			instanceDcs.reset();
			instanceDcs.registerSoundBoardCallback((msg) =>
			{
				Debug.Print("CALLBACK!", msg);
				playbackArray.Add(msg);
			});
		}

		[Test, Order(1)]
		public void ShouldValidateCallbackFunction()
		{
			TestContext.WriteLine("should validate callback function");

			var soundBoard = instancePreDcs;
			var result = soundBoard.registerSoundBoardCallback(/*2*/);
			Assert.That(result, Is.EqualTo(false));
		}

		[Test, Order(2)]
		public void ShouldReadControlStatusNoDataAvailable()
		{
			TestContext.WriteLine("should read control status, no data available");

			var soundBoard = instanceDcs;
			var result = soundBoard.readInterface(WPC_SOUND_CONTROL_STATUS);
			Assert.That(result, Is.EqualTo(0xFF));
		}

		[Test, Order(3)]
		public void ShouldReadControlStatusDataAvailable()
		{
			TestContext.WriteLine("should read control status, data is available");

			var soundBoard = instanceDcs;
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x03);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0xD3);
			var result = soundBoard.readInterface(WPC_SOUND_CONTROL_STATUS);
			Assert.That(result, Is.EqualTo(0x80));
		}

		[Test, Order(4)]
		public void ShouldHandleMultipleWrites()
		{
			TestContext.WriteLine("should handle multiple writes");

			var soundBoard = instancePreDcs;
			soundBoard.writeInterface(WPC_SOUND_DATA, 0);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0xEE);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x22);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x00);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x01);

			var result = soundBoard.readInterface(WPC_SOUND_CONTROL_STATUS);

			Assert.That(result, Is.EqualTo(0xFF));
			Assert.That(playbackArray[0].command, Is.EqualTo("STOPSOUND"));
			Assert.That(playbackArray[1].command, Is.EqualTo("STOPSOUND"));
			Assert.That(playbackArray[2].command, Is.EqualTo("PLAYSAMPLE"));
			Assert.That(playbackArray[2].id, Is.EqualTo(0xEE));
			Assert.That(playbackArray[3].command, Is.EqualTo("PLAYSAMPLE"));
			Assert.That(playbackArray[3].id, Is.EqualTo(0x22));
			Assert.That(playbackArray[4].command, Is.EqualTo("STOPSOUND"));
			Assert.That(playbackArray[5].command, Is.EqualTo("PLAYSAMPLE"));
			Assert.That(playbackArray[5].id, Is.EqualTo(1));
		}

		[Test, Order(5)]
		public void PreDCSSetVolume()
		{
			TestContext.WriteLine("preDcs: set volume");

			var soundBoard = instancePreDcs;
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x79);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x0B);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0xF4);
			Assert.That(playbackArray[0].command, Is.EqualTo("MAINVOLUME"));
			Assert.That(playbackArray[0].value, Is.EqualTo(11));
		}

		[Test, Order(6)]
		public void DCSSetVolume()
		{
			TestContext.WriteLine("DCS: set volume");

			var soundBoard = instanceDcs;
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x55);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0xAA);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0xB7);
			soundBoard.writeInterface(WPC_SOUND_DATA, 0x48);
			Assert.That(playbackArray[0].command, Is.EqualTo("MAINVOLUME"));
			Assert.That(playbackArray[0].value, Is.EqualTo(22));
		}

		[Test, Order(7)]
		public void DCSIgnoreFirst0ByteAfterSoundBoardResets()
		{
			TestContext.WriteLine("DCS: ignore first 0 byte after alot of sound board resets");

			var soundBoard = instanceDcs;
			soundBoard.resetCount = 22;
			soundBoard.writeInterface(WPC_SOUND_DATA, 0);
			soundBoard.writeInterface(WPC_SOUND_DATA, 22);
			var actualState = soundBoard.getState();
			Assert.That(actualState.volume, Is.EqualTo(8));
			Assert.That(actualState.readDataBytes, Is.EqualTo(0));
			Assert.That(actualState.writeDataBytes, Is.EqualTo(1));
			Assert.That(actualState.readControlBytes, Is.EqualTo(0));
			Assert.That(actualState.writeControlBytes, Is.EqualTo(0));
		}

		[Test, Order(8)]
		public void DCSSetAndGetState()
		{
			TestContext.WriteLine("DCS: set and get state");

			var soundBoard = instanceDcs;
			var state = new SoundBoard.State
			{
				volume = 44,
				readDataBytes = 55,
				writeDataBytes = 66,
				readControlBytes = 77,
				writeControlBytes = 88
			};

			soundBoard.setState(state);
			var actualState = soundBoard.getState();
			Assert.That(actualState.volume, Is.EqualTo(44));
			Assert.That(actualState.readDataBytes, Is.EqualTo(55));
			Assert.That(actualState.writeDataBytes, Is.EqualTo(66));
			Assert.That(actualState.readControlBytes, Is.EqualTo(77));
			Assert.That(actualState.writeControlBytes, Is.EqualTo(88));
		}

		[Test, Order(9)]
		public void DCSEmptySetState()
		{
			TestContext.WriteLine("DCS: empty setState");

			var soundBoard = instanceDcs;
			var result = soundBoard.setState();
			Assert.That(result, Is.EqualTo(false));
		}
	}
}
