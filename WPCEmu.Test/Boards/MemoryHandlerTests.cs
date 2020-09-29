using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class MemoryHandlerTests
	{
		MemoryHandler memoryHandler;

		[SetUp]
		public void Init()
		{
			var config = new MemoryHandler.Config
			{
				checksum = new MemoryHandler.ChecksumPosition[] {
					new MemoryHandler.ChecksumPosition {
						dataStartOffset = 0x1D29,
						dataEndOffset = 0x1D48,
						checksumOffset = 0x1D49,
						checksum = "16bit",
						name = "HIGHSCORE"
					}
				}
			};

			memoryHandler = MemoryHandler.GetInstance(config, Enumerable.Repeat((byte)0, 8192).ToArray());
		}

		[Test, Order(1)]
		public void ShouldNotUpdateChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should not update checksum");

			ushort OFFSET = 0;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			bool newValueWritten = memoryHandler.ram[OFFSET] == 1;
			int valuesNotZero = 0;
			foreach (byte n in memoryHandler.ram)
			{
				if (n != 0)
				{
					valuesNotZero++;
				}
			}
			Assert.AreEqual(true, newValueWritten);
			Assert.AreEqual(1, valuesNotZero);
		}

		[Test, Order(2)]
		public void ShouldWriteNumberCheckChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should write number, check checksum");

			ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			Assert.AreEqual(0xFF, memoryHandler.ram[0x1D49]);
			Assert.AreEqual(0xFE, memoryHandler.ram[0x1D4A]);
		}

		[Test, Order(3)]
		public void ShouldWriteArrayCheckChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should write number, check checksum");

			ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, new byte[] { 1 });
			Assert.AreEqual(0xFF, memoryHandler.ram[0x1D49]);
			Assert.AreEqual(0xFE, memoryHandler.ram[0x1D4A]);
		}

		[Test, Order(4)]
		public void ShouldWriteStringCheckChecksum()
		{
			TestContext.WriteLine("MemoryHandler: should write string, check checksum");

			ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, "\x31");
			Assert.AreEqual(0xFF, memoryHandler.ram[0x1D49]);
			Assert.AreEqual(0xFE, memoryHandler.ram[0x1D4A]);
		}

		[Test, Order(5)]
		public void ShouldUpdateChecksumStart()
		{
			TestContext.WriteLine("MemoryHandler: should update checksum (start)");

			ushort OFFSET = 0x1D29;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			bool newValueWritten = memoryHandler.ram[OFFSET] == 1;
			int valuesNotZero = 0;
			foreach (byte n in memoryHandler.ram)
			{
				if (n != 0)
				{
					valuesNotZero++;
				}
			}
			Assert.AreEqual(true, newValueWritten);
			Assert.AreEqual(3, valuesNotZero);
		}

		[Test, Order(6)]
		public void ShouldUpdateChecksumEnd()
		{
			TestContext.WriteLine("MemoryHandler: should update checksum (end)");

			ushort OFFSET = 0x1D48;
			memoryHandler.writeMemory(OFFSET, (byte)1);
			bool newValueWritten = memoryHandler.ram[OFFSET] == 1;
			int valuesNotZero = 0;
			foreach (byte n in memoryHandler.ram)
			{
				if (n != 0)
				{
					valuesNotZero++;
				}
			}
			Assert.AreEqual(true, newValueWritten);
			Assert.AreEqual(3, valuesNotZero);
		}
	}
}
