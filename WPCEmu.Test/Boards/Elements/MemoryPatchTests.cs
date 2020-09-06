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

			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			MemoryPatch.Patch? result = memoryPatch.hasPatch(0);
			Assert.AreEqual(null, result);
		}

		[Test, Order(2)]
		public void ShouldReturnPatchedValue()
		{
			TestContext.WriteLine("memoryPatch, should return a patched value");

			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			memoryPatch.addPatch(0x50, 20);
			MemoryPatch.Patch? result = memoryPatch.hasPatch(0x50);
			Assert.AreEqual(20, result.Value.value);
		}

		[Test, Order(3)]
		public void ShouldRemovePatchedValue()
		{
			TestContext.WriteLine("memoryPatch, should remove a patched value");

			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			memoryPatch.addPatch(0x50, 20);
			memoryPatch.removePatch(0x50);
			MemoryPatch.Patch? result = memoryPatch.hasPatch(0x50);
			Assert.AreEqual(null, result);
		}

		[Test, Order(4)]
		public void ShouldAddVolatilePatch()
		{
			TestContext.WriteLine("memoryPatch, should add a volatile patch");

			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			memoryPatch.addPatch(0x50, 20, true);
			MemoryPatch.Patch? result = memoryPatch.hasPatch(0x50);
			Assert.AreEqual(20, result.Value.value);
		}

		[Test, Order(5)]
		public void ShouldCleanupVolatilePatches()
		{
			TestContext.WriteLine("memoryPatch, should cleanup volatile patches");

			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			memoryPatch.addPatch(0x50, 20, true);
			memoryPatch.addPatch(0x70, 21, true);
			memoryPatch.addPatch(0x90, 23);

			memoryPatch.removeVolatileEntries();
			MemoryPatch.Patch? result1 = memoryPatch.hasPatch(0x50);
			MemoryPatch.Patch? result2 = memoryPatch.hasPatch(0x70);
			MemoryPatch.Patch? result3 = memoryPatch.hasPatch(0x90);
			Assert.AreEqual(null, result1);
			Assert.AreEqual(null, result2);
			Assert.AreEqual(23, result3.Value.value);
		}

		[Test, Order(6)]
		public void ShouldApplyPatchesToExposedMemory()
		{
			TestContext.WriteLine("memoryPatch, should applyPatchesToExposedMemory");

			byte[] ram = Enumerable.Repeat((byte)0x00, 20).ToArray();
			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			memoryPatch.addPatch(10, 20, true);
			memoryPatch.addPatch(11, 21, true);

			byte[] result = memoryPatch.applyPatchesToExposedMemory(ram);
			Assert.AreEqual(0, result[0]);
			Assert.AreEqual(20, result[10]);
			Assert.AreEqual(21, result[11]);
		}
	}
}
