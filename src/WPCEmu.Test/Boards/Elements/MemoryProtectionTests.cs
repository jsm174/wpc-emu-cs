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

			var result = MemoryProtection.getMemoryProtectionMask(0);
			Assert.That(result, Is.EqualTo(0x1000));
		}

		[Test, Order(2)]
		public void ShouldGetMemoryProtectionMask_1()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 1");

			var result = MemoryProtection.getMemoryProtectionMask(1);
			Assert.That(result, Is.EqualTo(0x1800));
		}

		[Test, Order(3)]
		public void ShouldGetMemoryProtectionMask_15()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 15");

			var result = MemoryProtection.getMemoryProtectionMask(15);
			Assert.That(result, Is.EqualTo(0x1F00));
		}

		[Test, Order(4)]
		public void ShouldGetMemoryProtectionMask_16()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 16");

			var result = MemoryProtection.getMemoryProtectionMask(16);
			Assert.That(result, Is.EqualTo(0x2000));
		}

		[Test, Order(5)]
		public void ShouldGetMemoryProtectionMask_17()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 17");

			var result = MemoryProtection.getMemoryProtectionMask(17);
			Assert.That(result, Is.EqualTo(0x2800));
		}

		[Test, Order(6)]
		public void ShouldGetMemoryProtectionMask_255_unsure()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 255 - unsure");

			var result = MemoryProtection.getMemoryProtectionMask(255);
			Assert.That(result, Is.EqualTo(0xF00));
		}

		[Test, Order(7)]
		public void ShouldGetMemoryProtectionMask_256_wraparound()
		{
			TestContext.WriteLine("memoryProtection, should get memoryProtection mask for 256 (wrap around)");

			var result = MemoryProtection.getMemoryProtectionMask(256);
			Assert.That(result, Is.EqualTo(0x1000));
		}
	}
}
