using System.Linq;
using NUnit.Framework;
using WPCEmu.Rom;

namespace WPCEmu.Test.Rom
{
	[TestFixture]
	public class GameIdTests
	{
		static readonly byte[] MAGIC_STRING = {
		  // MAGIC PREFIX
			0xEC,
			0x9F,

			//POSITION
			0x80,
			0x00,

			// MAGIC POSTFIX
			0x83,
			0x12,
			0x34,
			0, 0, 0, 0, 0, 0, 0
		};

		[Test, Order(1)]
		public void ShouldReturnUndefinedIfMagixByteFound()
		{
			TestContext.WriteLine("gameId.search should return undefined if magix byte found");

			var result = GameId.search(new byte[] {}, new byte[] {});
			Assert.That(result, Is.EqualTo(null));
		}

		[Test, Order(2)]
		public void ShouldIgnoreMagixByteIfOnlyFoundOnce()
		{
			TestContext.WriteLine("gameId.search should ignore magic byte if only found once");

			var result = GameId.search(MAGIC_STRING, new byte[] { });
			Assert.That(result, Is.EqualTo(null));
		}

		[Test, Order(3)]
		public void ShouldFindMagicByte()
		{
			TestContext.WriteLine("gameId.search should find magic byte");

			var gameRom = MAGIC_STRING.Concat(MAGIC_STRING).ToArray();
			var systemRom = new byte[] { 0x11, 0x22 };
			var result = GameId.search(gameRom, systemRom);
			Assert.That(result, Is.EqualTo(0x1122));
		}
	}
}