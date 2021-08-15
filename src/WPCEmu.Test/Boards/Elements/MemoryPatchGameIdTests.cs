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

			var memoryPatch = MemoryPatch.getInstance();
			MemoryPatchGameId.run(memoryPatch, 0x50);
			var gameIdLo = memoryPatch.hasPatch(0x50);
			var gameIdHi = memoryPatch.hasPatch(0x51);
			Assert.That(gameIdLo?.value, Is.EqualTo(20));
			Assert.That(gameIdHi?.value, Is.EqualTo(99));
		}
	}
}
