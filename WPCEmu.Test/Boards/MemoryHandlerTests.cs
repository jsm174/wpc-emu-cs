using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards;
using WPCEmu.Boards.Memory;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class MemoryHandlerTests
	{
		MemoryHandler memoryHandler;

		[SetUp]
		public void Init()
		{
			var config = new MemoryHandler.MemoryPosition
			{
				checksum = new Checksum.ChecksumData[] {
					new Checksum.ChecksumData {
						dataStartOffset = 0x1D29,
						dataEndOffset = 0x1D48,
						checksumOffset = 0x1D49,
						checksum = "16bit",
						name = "HIGHSCORE"
					}
				}
			};

			memoryHandler = MemoryHandler.getInstance(config, Enumerable.Repeat((byte)0, 8192).ToArray());
		}

		[Test, Order(1)]
		public void ShouldNotUpdateChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should not update checksum");

			const ushort OFFSET = 0;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			var newValueWritten = memoryHandler.ram[OFFSET] == 1;
			var valuesNotZero = 0;
			foreach (var n in memoryHandler.ram)
			{
				if (n != 0)
				{
					valuesNotZero++;
				}
			}
			Assert.That(newValueWritten, Is.EqualTo(true));
			Assert.That(valuesNotZero, Is.EqualTo(1));
		}

		[Test, Order(2)]
		public void ShouldWriteNumberCheckChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should write number, check checksum");

			const ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			Assert.That(memoryHandler.ram[0x1D49], Is.EqualTo(0xFF));
			Assert.That(memoryHandler.ram[0x1D4A], Is.EqualTo(0xFE));
		}

		[Test, Order(3)]
		public void ShouldWriteArrayCheckChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should write number, check checksum");

			const ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, new byte[] { 1 });
			Assert.That(memoryHandler.ram[0x1D49], Is.EqualTo(0xFF));
			Assert.That(memoryHandler.ram[0x1D4A], Is.EqualTo(0xFE));
		}

		[Test, Order(4)]
		public void ShouldWriteStringCheckChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should write string, check checksum");

			const ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, "\x31");
			Assert.That(memoryHandler.ram[0x1D49], Is.EqualTo(0xFF));
			Assert.That(memoryHandler.ram[0x1D4A], Is.EqualTo(0xFE));
		}

		[Test, Order(5)]
		public void ShouldUpdateChecksumStart()
		{
			TestContext.WriteLine("MemoryHandler: should update checksum (start)");

			const ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			var newValueWritten = memoryHandler.ram[OFFSET] == 1;
			var valuesNotZero = 0;
			foreach (var n in memoryHandler.ram)
			{
				if (n != 0)
				{
					valuesNotZero++;
				}
			}
			Assert.That(newValueWritten, Is.EqualTo(true));
			Assert.That(valuesNotZero, Is.EqualTo(3));
		}

		[Test, Order(6)]
		public void ShouldUpdateChecksumEnd()
		{
			TestContext.WriteLine("MemoryHandler: should update checksum (end)");

			const ushort OFFSET = 0x1D48;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			var newValueWritten = memoryHandler.ram[OFFSET] == 1;
			var valuesNotZero = 0;
			foreach (var n in memoryHandler.ram)
			{
				if (n != 0)
				{
					valuesNotZero++;
				}
			}
			Assert.That(newValueWritten, Is.EqualTo(true));
			Assert.That(valuesNotZero, Is.EqualTo(3));
		}
	}
}
