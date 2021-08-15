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

			var memoryPatch = MemoryPatch.getInstance();
			MemoryPatchSkipBootCheck.run(memoryPatch);
			var checkLo = memoryPatch.hasPatch(0xFFEC);
			var checkHi = memoryPatch.hasPatch(0xFFED);
			Assert.That(checkLo?.value, Is.EqualTo(0x00));
			Assert.That(checkHi?.value, Is.EqualTo(0xFF));
		}
	}
}
