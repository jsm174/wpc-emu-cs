using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class CpuBoardTests
	{
		const ushort PAGESIZE = 0x4000;
		const ushort WPC_ROM_BANK = 0x3FFC;

		WpcCpuBoard cpuBoard;

		[SetUp]
		public void Init()
		{
			var gamerom = Enumerable.Repeat((byte)0xFF, 0x18000).ToArray();
			var initObject = new RomObject
			{
				romSizeMBit = 1,
				systemRom = Enumerable.Repeat((byte)0xFF, 2 * PAGESIZE).ToArray(),
				fileName = "foo",
				gameRom = gamerom
			};

			cpuBoard = WpcCpuBoard.getInstance(initObject);
		}

		[Test, Order(1)]
		public void ShouldGetUIData()
		{
			TestContext.WriteLine("should get ui data");

			cpuBoard.reset();
			var result = cpuBoard.getState();
			Assert.That(result.cpuState.tickCount, Is.EqualTo(0));
			Assert.That(result.cpuState.missedIRQ, Is.EqualTo(0));
			Assert.That(result.cpuState.missedFIRQ, Is.EqualTo(0));
			Assert.That(result.cpuState.irqCount, Is.EqualTo(0));
			Assert.That(result.cpuState.firqCount, Is.EqualTo(0));
			Assert.That(result.cpuState.nmiCount, Is.EqualTo(0));
			Assert.That(result.protectedMemoryWriteAttempts, Is.EqualTo(0));
			Assert.That(result.memoryWrites, Is.EqualTo(0));
			Assert.That(result.version, Is.EqualTo(5));
		}

		[Test, Order(2)]
		public void ShouldStartCpuBoard()
		{
			TestContext.WriteLine("should start cpu board");

			cpuBoard.start();
			var result = cpuBoard.getState();
			Assert.That(result.cpuState.tickCount, Is.EqualTo(0));
		}

		[Test, Order(3)]
		public void ShouldIgnoreEmptySetState()
		{
			TestContext.WriteLine("should ignore empty setState");

			cpuBoard.start();
			var result = cpuBoard.setState();
			Assert.That(result, Is.EqualTo(false));
		}

		[Test, Order(4)]
		public void ShouldIgnoreInvalidVersion()
		{
			TestContext.WriteLine("should ignore empty setState");

			cpuBoard.start();
			var result = cpuBoard.setState(new WpcCpuBoard.State
			{
				version = 1
            });
			Assert.That(result, Is.EqualTo(false));
		}

		[Test, Order(5)]
		public void ShouldChangeCabinetInput()
		{
			TestContext.WriteLine("should change cabinet input");

			cpuBoard.start();
			cpuBoard.setCabinetInput(1);
			var state = cpuBoard.getState();
			var result = state.asic?.wpc.inputState;
			Assert.That(result[0], Is.EqualTo(1));
		}

		[Test, Order(6)]
		public void ShouldChangeSwitchInput()
		{
			TestContext.WriteLine("should change switch input");

			cpuBoard.start();
			cpuBoard.setSwitchInput(11);
			cpuBoard.setSwitchInput(13);
			var state = cpuBoard.getState();
			var result = state.asic?.wpc.inputState;
			Assert.That(result[1], Is.EqualTo(5));
		}

		[Test, Order(7)]
		public void ShouldChangeFliptronicsInput()
		{
			TestContext.WriteLine("should change fliptronics input");

			cpuBoard.start();
			cpuBoard.setFliptronicsInput("F1");
			var state = cpuBoard.getState();
			var result = state.asic?.wpc.inputState;
			Assert.That(result[9], Is.EqualTo(1));
		}

		[Test, Order(8)]
		public void ShouldEnableToggleMidnightMadnessMode()
		{
			TestContext.WriteLine("should enable toggleMidnightMadnessMode");

			cpuBoard.start();
			cpuBoard.toggleMidnightMadnessMode();
			var state = cpuBoard.getState();
			var result = state.asic?.wpc.time;
			Assert.That(result, Does.Match("MM!$"));
		}

		[Test, Order(9)]
		public void ShouldBankSwitchedReadBank0()
		{
			TestContext.WriteLine("should _bankswitchedRead, bank 0");

			const byte BANK = 0;
			cpuBoard.gameRom[0] = 12;
			cpuBoard.asic.write(WPC_ROM_BANK, BANK);
			var result = cpuBoard._bankswitchedRead(0);
			Assert.That(result, Is.EqualTo(12));
		}

		[Test, Order(10)]
		public void ShouldBankSwitchedReadBank1()
		{
			TestContext.WriteLine("should _bankswitchedRead, bank 1");

			const byte BANK = 1;
			cpuBoard.gameRom[PAGESIZE] = 12;
			cpuBoard.asic.write(WPC_ROM_BANK, BANK);
			var result = cpuBoard._bankswitchedRead(0);
			Assert.That(result, Is.EqualTo(12));
		}

		[Test, Order(11)]
		public void ShouldBankSwitchedReadBank5()
		{
			TestContext.WriteLine("should _bankswitchedRead, bank 5");

			const byte BANK = 5;
			cpuBoard.gameRom[5 * PAGESIZE] = 12;
			cpuBoard.asic.write(WPC_ROM_BANK, BANK);
			var result = cpuBoard._bankswitchedRead(0);
			Assert.That(result, Is.EqualTo(12));
		}

		[Test, Order(12)]
		public void ShouldBankSwitchedReadBank6_SystemRomOutOfBand()
		{
			TestContext.WriteLine("should _bankswitchedRead, bank 6 (systemrom, out of band)");

			const byte BANK = 6;
			// this read wraps already
			cpuBoard.asic.write(WPC_ROM_BANK, BANK);
			var result = cpuBoard._bankswitchedRead(0);
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(13)]
		public void ShouldBankSwitchedReadBank8()
		{
			TestContext.WriteLine("should _bankswitchedRead, bank 8");

			const byte BANK = 8;
			cpuBoard.gameRom[0] = 12;
			cpuBoard.asic.write(WPC_ROM_BANK, BANK);
			var result = cpuBoard._bankswitchedRead(0);
			Assert.That(result, Is.EqualTo(12));
		}

		[Test, Order(14)]
		public void ShouldMirrorWpcAsicCallsInMemory()
		{
			TestContext.WriteLine("should mirror wpc asic calls in memory");

			const byte BANK = 6;
			cpuBoard.asic.write(WPC_ROM_BANK, BANK);
			var result = cpuBoard.ram[WPC_ROM_BANK];
			Assert.That(result, Is.EqualTo(BANK));
		}
	}
}
