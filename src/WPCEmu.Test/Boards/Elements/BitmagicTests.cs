using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class BitmagicTests
	{
		[Test, Order(1)]
		public void ShouldGetMsb_0x0A()
		{
			TestContext.WriteLine("bitmagic, should get msb from 0x0A");

			var result = Bitmagic.findMsbBit(0x0A);
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(2)]
		public void ShouldGetMsb_0x80()
		{
			TestContext.WriteLine("bitmagic, should get msb from 0x80");

			var result = Bitmagic.findMsbBit(0x80);
			Assert.That(result, Is.EqualTo(8));
		}

		[Test, Order(3)]
		public void ShouldGetMsb_0x01()
		{
			TestContext.WriteLine("bitmagic, should get msb from 0x01");

			var result = Bitmagic.findMsbBit(0x01);
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(4)]
		public void ShouldGetMsb_0x00()
		{
			TestContext.WriteLine("bitmagic, should get msb from 0x00");

			var result = Bitmagic.findMsbBit(0x00);
			Assert.That(result, Is.EqualTo(0));
		}

		//[Test, Order(5)]
		//public void ShouldGetMsb_undefined()
		//{
		//	TestContext.WriteLine("bitmagic, should get msb from undefined");
		//
		//	byte result = Bitmagic.findMsbBit();
		//	Assert.That(result, Is.EqualTo(0));
		//}

		[Test, Order(6)]
		public void ShouldSetMsb_0()
		{
			TestContext.WriteLine("bitmagic, should set msb 0");

			var result = Bitmagic.setMsbBit(0);
			Assert.That(result, Is.EqualTo(0x01));
		}

		[Test, Order(7)]
		public void ShouldSetMsb_3()
		{
			TestContext.WriteLine("bitmagic, should set msb 3");

			var result = Bitmagic.setMsbBit(3);
			Assert.That(result, Is.EqualTo(0x08));
		}

		[Test, Order(8)]
		public void ShouldSetMsb_7()
		{
			TestContext.WriteLine("bitmagic, should set msb 7");

			var result = Bitmagic.setMsbBit(7);
			Assert.That(result, Is.EqualTo(0x80));
		}
	}
}
