using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using WPCEmu.Rom;

namespace WPCEmu.Test
{
	[TestFixture]
	public class EmulatorTests
	{
		[Test, Order(1)]
		public void GetVersion()
		{
			TestContext.WriteLine("Emulator get version");

			var emulator = Emulator.initVMwithRom(new RomBinary
			{
				u06 = new byte[262144]
			});

			var version = emulator.version();
			Assert.That(version, Is.EqualTo(Assembly.GetExecutingAssembly().GetName().Version.ToString()));
		}

		[Test, Order(2)]
		public void ToggleMidnightModeEnabled()
		{
			TestContext.WriteLine("Emulator toggle midnightModeEnabled");

			var emulator = Emulator.initVMwithRom(new RomBinary
			{
				u06 = new byte[262144]
			});

			emulator.toggleMidnightMadnessMode();
			Assert.That(emulator.cpuBoard.asic?.midnightModeEnabled, Is.EqualTo(true));
		}

		[Test, Order(3)]
		public void ToggleSwitchInput()
		{
			TestContext.WriteLine("Emulator toggle switch input");

			var emulator = Emulator.initVMwithRom(new RomBinary
			{
				u06 = new byte[262144]
			});
			var inputState1 = emulator.getState().asic?.wpc.inputState.ToArray();
			emulator.setSwitchInput(11);
			var inputState2 = emulator.getState().asic?.wpc.inputState.ToArray();
			Assert.That(inputState1, Is.EqualTo(new byte[] { 0, 0, 8, 0, 0, 0, 0, 0, 0, 0 }));
			Assert.That(inputState2, Is.EqualTo(new byte[] { 0, 1, 8, 0, 0, 0, 0, 0, 0, 0 }));
		}

		[Test, Order(4)]
		public void ClearSwitchInput()
		{
			TestContext.WriteLine("Emulator clear switch input");

			var emulator = Emulator.initVMwithRom(new RomBinary
			{
				u06 = new byte[262144]
			});
			emulator.setSwitchInput(11, false);
			var inputState = emulator.getState().asic?.wpc.inputState.ToArray();
			Assert.That(inputState, Is.EqualTo(new byte[] { 0, 0, 8, 0, 0, 0, 0, 0, 0, 0 }));
		}

		[Test, Order(5)]
		public void SetSwitchInput()
		{
			TestContext.WriteLine("Emulator set switch input");

			var emulator = Emulator.initVMwithRom(new RomBinary
			{
				u06 = new byte[262144]
			});
			emulator.setSwitchInput(11, true);
			var inputState = emulator.getState().asic?.wpc.inputState.ToArray();
			Assert.That(inputState, Is.EqualTo(new byte[] { 0, 1, 8, 0, 0, 0, 0, 0, 0, 0 }));
		}

		[Test, Order(6)]
		public void GetDefaultDipSwitchState()
		{
			TestContext.WriteLine("Emulator get default dip switch state");

			var emulator = Emulator.initVMwithRom(new RomBinary
			{
				u06 = new byte[262144]
			});
			var result = emulator.getDipSwitchByte();
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(7)]
		public void SetGetDefaultDipSwitchState()
		{
			TestContext.WriteLine("Emulator set/get default dip switch state");

			var emulator = Emulator.initVMwithRom(new RomBinary
			{
				u06 = new byte[262144]
			});
			emulator.setDipSwitchByte(222);
			var result = emulator.getDipSwitchByte();
			Assert.That(result, Is.EqualTo(222));
		}
	}
}