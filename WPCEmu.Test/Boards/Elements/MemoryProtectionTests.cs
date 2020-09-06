using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class MemoryProtectionTests
	{
		[Test, Order(1)]
		public void ShouldGetMemoryProtectionMask_0()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 0");

			ushort result = MemoryProtection.getMemoryProtectionMask(0);
			Assert.AreEqual(0x1000, result);
		}

		[Test, Order(2)]
		public void ShouldGetMemoryProtectionMask_1()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 1");

			ushort result = MemoryProtection.getMemoryProtectionMask(1);
			Assert.AreEqual(0x1800, result);
		}

		[Test, Order(3)]
		public void ShouldGetMemoryProtectionMask_15()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 15");

			ushort result = MemoryProtection.getMemoryProtectionMask(15);
			Assert.AreEqual(0x1F00, result);
		}

		[Test, Order(4)]
		public void ShouldGetMemoryProtectionMask_16()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 16");

			ushort result = MemoryProtection.getMemoryProtectionMask(16);
			Assert.AreEqual(0x2000, result);
		}

		[Test, Order(5)]
		public void ShouldGetMemoryProtectionMask_17()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 17");

			ushort result = MemoryProtection.getMemoryProtectionMask(17);
			Assert.AreEqual(0x2800, result);
		}

		[Test, Order(6)]
		public void ShouldGetMemoryProtectionMask_255_unsure()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 255 - unsure");

			ushort result = MemoryProtection.getMemoryProtectionMask(255);
			Assert.AreEqual(0xF00, result);
		}

		[Test, Order(7)]
		public void ShouldGetMemoryProtectionMask_256_wraparound()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 256 (wrap around)");

			ushort result = MemoryProtection.getMemoryProtectionMask(256);
			Assert.AreEqual(0x1000, result);
		}
	}
}
