using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class MemoryPatchSkipBootCheckTests
	{
		[Test, Order(1)]
		public void ShouldReturnPatchedGameId()
		{
			TestContext.WriteLine("MemoryPatchSkipBootCheck, should return a patched game id");

			MemoryPatch memoryPatch = MemoryPatch.GetInstance();
			MemoryPatchSkipBootCheck.run(memoryPatch);
			MemoryPatch.Patch? checkLo = memoryPatch.hasPatch(0xFFEC);
			MemoryPatch.Patch? checkHi = memoryPatch.hasPatch(0xFFED);
			Assert.AreEqual(0x00, checkLo.Value.value);
			Assert.AreEqual(0xFF, checkHi.Value.value);
		}
	}
}
