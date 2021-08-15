using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class MemoryPatchTests
	{
		[Test, Order(1)]
		public void ShouldNotReturnValue()
		{
			TestContext.WriteLine("memoryPatch, should not return a value");

			var memoryPatch = MemoryPatch.getInstance();
			var result = memoryPatch.hasPatch(0);
			Assert.That(result, Is.EqualTo(null));
		}

		[Test, Order(2)]
		public void ShouldReturnPatchedValue()
		{
			TestContext.WriteLine("memoryPatch, should return a patched value");

			var memoryPatch = MemoryPatch.getInstance();
			memoryPatch.addPatch(0x50, 20);
			var result = memoryPatch.hasPatch(0x50);
			Assert.That(result?.value, Is.EqualTo(20));
		}

		[Test, Order(3)]
		public void ShouldRemovePatchedValue()
		{
			TestContext.WriteLine("memoryPatch, should remove a patched value");

			var memoryPatch = MemoryPatch.getInstance();
			memoryPatch.addPatch(0x50, 20);
			memoryPatch.removePatch(0x50);
			var result = memoryPatch.hasPatch(0x50);
			Assert.That(result, Is.EqualTo(null));
		}

		[Test, Order(4)]
		public void ShouldAddVolatilePatch()
		{
			TestContext.WriteLine("memoryPatch, should add a volatile patch");

			var memoryPatch = MemoryPatch.getInstance();
			memoryPatch.addPatch(0x50, 20, true);
			var result = memoryPatch.hasPatch(0x50);
			Assert.That(result?.value, Is.EqualTo(20));
		}

		[Test, Order(5)]
		public void ShouldCleanupVolatilePatches()
		{
			TestContext.WriteLine("memoryPatch, should cleanup volatile patches");

			var memoryPatch = MemoryPatch.getInstance();
			memoryPatch.addPatch(0x50, 20, true);
			memoryPatch.addPatch(0x70, 21, true);
			memoryPatch.addPatch(0x90, 23);

			memoryPatch.removeVolatileEntries();
			var result1 = memoryPatch.hasPatch(0x50);
			var result2 = memoryPatch.hasPatch(0x70);
			var result3 = memoryPatch.hasPatch(0x90);
			Assert.That(result1, Is.EqualTo(null));
			Assert.That(result2, Is.EqualTo(null));
			Assert.That(result3?.value, Is.EqualTo(23));
		}

		[Test, Order(6)]
		public void ShouldApplyPatchesToExposedMemory()
		{
			TestContext.WriteLine("memoryPatch, should applyPatchesToExposedMemory");

			var ram = Enumerable.Repeat((byte)0x00, 20).ToArray();
			var memoryPatch = MemoryPatch.getInstance();
			memoryPatch.addPatch(10, 20, true);
			memoryPatch.addPatch(11, 21, true);

			var result = memoryPatch.applyPatchesToExposedMemory(ram);
			Assert.That(result[0], Is.EqualTo(0));
			Assert.That(result[10], Is.EqualTo(20));
			Assert.That(result[11], Is.EqualTo(21));
		}
	}
}
