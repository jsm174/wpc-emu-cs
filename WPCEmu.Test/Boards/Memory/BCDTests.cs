using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards.Memory;

namespace WPCEmu.Test.Boards.Memory
{
	[TestFixture]
	public class BCDTests
	{
		[Test, Order(1)]
		public void toBCD_12345()
		{
			TestContext.WriteLine("BCD: toBCD, encode 12345 to BCD");

			var result = BCD.toBCD(12345);
			Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x23, 0x45 }));
		}

		[Test, Order(2)]
		public void toBCD_1000020()
		{
			TestContext.WriteLine("BCD: toBCD, encode 1000020 to BCD");

			var result = BCD.toBCD(1000020);
			Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x00, 0x00, 0x20 }));
		}

		[Test, Order(3)]
		public void toNumber_0x123456()
		{
			TestContext.WriteLine("BCD: toNumber, 0x123456)");

			var result = BCD.toNumber(new byte[] { 0x12, 0x34, 0x56 });
			Assert.That(result, Is.EqualTo(123456));
		}

		[Test, Order(4)]
		public void toNumber_0x1234()
		{
			TestContext.WriteLine("BCD: toNumber, 0x1234)");

			var result = BCD.toNumber(new byte[] { 0x12, 0x34 });
			Assert.That(result, Is.EqualTo(1234));
		}

		[Test, Order(5)]
		public void toNumber_Empty()
		{
			TestContext.WriteLine("BCD: toNumber, empty");

			var result = BCD.toNumber(new byte[] { });
			Assert.That(result, Is.EqualTo(0));
		}
	}
}
