using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class MemoryPatchGameIdTests
	{
		[Test, Order(1)]
		public void ShouldReturnPatchedGameId()
		{
			TestContext.WriteLine("MemoryPatchGameId, should return a patched game id");

			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			MemoryPatchGameId.run(memoryPatch, 0x50);
			MemoryPatch.Patch? gameIdLo = memoryPatch.hasPatch(0x50);
			MemoryPatch.Patch? gameIdHi = memoryPatch.hasPatch(0x51);
			Assert.AreEqual(20, gameIdLo.Value.value);
			Assert.AreEqual(99, gameIdHi.Value.value);
		}
	}
}
