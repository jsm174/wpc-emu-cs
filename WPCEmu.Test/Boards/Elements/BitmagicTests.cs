using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class BitmagicTests
	{
		[Test, Order(1)]
		public void ShouldGetMsb_0x80()
		{
			TestContext.WriteLine("bitmagic, should get msb from 0x80");

			byte result = Bitmagic.findMsbBit(0x80);
			Assert.AreEqual(8, result);
		}

		[Test, Order(2)]
		public void ShouldGetMsb_0x01()
		{
			TestContext.WriteLine("bitmagic, should get msb from 0x01");

			byte result = Bitmagic.findMsbBit(0x01);
			Assert.AreEqual(1, result);
		}

		[Test, Order(3)]
		public void ShouldGetMsb_0x00()
		{
			TestContext.WriteLine("bitmagic, should get msb from 0x00");

			byte result = Bitmagic.findMsbBit(0x00);
			Assert.AreEqual(0, result);
		}

		//[Test, Order(4)]
		//public void ShouldGetMsb_undefined()
		//{
		//	TestContext.WriteLine("bitmagic, should get msb from undefined");
		//
		//	byte result = Bitmagic.findMsbBit();
		//	Assert.AreEqual(0, result);
		//}

		[Test, Order(5)]
		public void ShouldSetMsb_0()
		{
			TestContext.WriteLine("bitmagic, should set msb 0");

			byte result = Bitmagic.setMsbBit(0);
			Assert.AreEqual(0x01, result);
		}

		[Test, Order(6)]
		public void ShouldSetMsb_3()
		{
			TestContext.WriteLine("bitmagic, should set msb 3");

			byte result = Bitmagic.setMsbBit(3);
			Assert.AreEqual(0x08, result);
		}

		[Test, Order(7)]
		public void ShouldSetMsb_7()
		{
			TestContext.WriteLine("bitmagic, should set msb 7");

			byte result = Bitmagic.setMsbBit(7);
			Assert.AreEqual(0x80, result);
		}
	}
}
