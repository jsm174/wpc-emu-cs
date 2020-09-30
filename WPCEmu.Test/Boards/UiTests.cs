using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class UiTests
	{
		UiState.MemoryPositionInitObject memoryPosition;
		WpcCpuBoard.Asic dummyState;
		WpcCpuBoard.Asic dummyStateString;

		UiState ui;

		[SetUp]
		public void Init()
		{
			memoryPosition = new UiState.MemoryPositionInitObject
			{
				knownValues = new UiState.MemoryPosition[]
				{
					new UiState.MemoryPosition { offset = 0, description = "valid1", type = "string" },
					new UiState.MemoryPosition { offset = 1, description = "valid2", type = "uint8" },
					new UiState.MemoryPosition { offset = 2, description = "valid3", type = "bcd" },
					new UiState.MemoryPosition { offset = 3, description = "valid4", type = "uint8", length = 4 },
					new UiState.MemoryPosition { offset = 0x3AD, description = "invalid1", type = "foo" },
					new UiState.MemoryPosition { description = "invalid2", type = "string" },
				}
			};

			ui = UiState.GetInstance(memoryPosition);

			dummyState = new WpcCpuBoard.Asic
			{
				display = new OutputDmdDisplay.State
				{
					dmdShadedBuffer = new byte[] { },
					videoRam = new byte[] { }
				},
				wpc = new CpuBoardAsic.State
				{
					lampState = new byte[] { },
					solenoidState = new byte[] { },
					inputState = new byte[] { }
				},
				ram = new byte[] { 11, 22, 0x65, 0x66, 0x67, 0x68, 0x69, 0x70, 0x71, 1, 2, 3 }
			};

			dummyStateString = new WpcCpuBoard.Asic
			{
				display = new OutputDmdDisplay.State
				{
					dmdShadedBuffer = new byte[] { },
					videoRam = new byte[] { }
				},
				wpc = new CpuBoardAsic.State
				{
					lampState = new byte[] { },
					solenoidState = new byte[] { },
					inputState = new byte[] { }
				},
				ram = new byte[] { 65, 65, 65, 0, 65, 65 }
			};
		}

		[Test, Order(1)]
		public void ShouldFilterOutInvalidMemoryPositionEntries()
		{
			TestContext.WriteLine("UI: should filter out invalid memoryPosition entries");

			Assert.That(ui.memoryPosition.Length, Is.EqualTo(4));
		}

		[Test, Order(2)]
		public void ShouldFetchDataFromRam()
		{
			TestContext.WriteLine("UI: should fetch data from ram");

			var result = ui.getChangedAsicState(dummyState);

			Assert.That(result.memoryPosition.Length, Is.EqualTo(4));
			Assert.That(result.memoryPosition[0].value, Is.EqualTo(""));
			Assert.That(result.memoryPosition[1].value, Is.EqualTo(22));
			Assert.That(result.memoryPosition[2].value, Is.EqualTo(6566));
			Assert.That(result.memoryPosition[3].value, Is.EqualTo(0x66676869));
		}

		[Test, Order(3)]
		public void ShouldFetchDataFromRamString()
		{
			TestContext.WriteLine("UI: should fetch data from ram (string)");

			var result = ui.getChangedAsicState(dummyStateString);
			Assert.That(result.memoryPosition[0].value, Is.EqualTo("AAA"));
		}

		[Test, Order(4)]
		public void ShouldCapVeryLongString()
		{
			TestContext.WriteLine("UI: should cap very long string");

			var data = dummyStateString;
			data.ram = Enumerable.Repeat((byte)65, 500).ToArray();
			var result = ui.getChangedAsicState(data);
			TestContext.WriteLine(result.memoryPosition[0]);
			Assert.That(((string)result.memoryPosition[0].value).Length, Is.EqualTo(32));
		}
	}
}