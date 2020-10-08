using NUnit.Framework;
using WPCEmu.Db;

namespace WPCEmu.Test.Db
{
	[TestFixture]
	public class GamelistTests
	{
		[Test, Order(1)]
		public void ShouldGetAllNames()
		{
			TestContext.WriteLine("gamelist, should getAllNames");

			var result = Gamelist.getAllNames();
			Assert.That(result.Length > 20, Is.EqualTo(true));
		}

		[Test, Order(2)]
		public void ShouldGetByName()
		{
			TestContext.WriteLine("gamelist, should getByName");

			var name = "WPC-DMD: Terminator 2";
			var result = Gamelist.getByName(name);
			Assert.That(result.name, Is.EqualTo(name));
			Assert.That(result.version, Is.EqualTo("L-8"));
		}

		[Test, Order(3)]
		public void ShouldGetByPinmameName()
		{
			TestContext.WriteLine("gamelist, should getByPinmameName");

			var result = Gamelist.getByPinmameName("tz_94h");
			Assert.That(result.pinmame?.gameName, Is.EqualTo("Twilight Zone"));
		}

		[Test, Order(4)]
		public void ShouldGetByPinmameNameUppercase()
		{
			TestContext.WriteLine("gamelist, should getByPinmameName (UPPERCASE)");

			var result = Gamelist.getByPinmameName("TZ_94H");
			Assert.That(result.pinmame?.gameName, Is.EqualTo("Twilight Zone"));
		}

		[Test, Order(5)]
		public void ShouldGetByPinmameNameWithUnknownName()
		{
			TestContext.WriteLine("gamelist, getByPinmameName with unknown name");

			var result = Gamelist.getByPinmameName("i do not exist!");
			Assert.That(result, Is.EqualTo(null));
		}
	}
}
