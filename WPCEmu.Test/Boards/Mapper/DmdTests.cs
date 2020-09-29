using NUnit.Framework;
using WPCEmu.Boards.Mapper;

namespace WPCEmu.Test.Boards.Mapper
{
	[TestFixture]
	public class DmdTests
	{
		[Test, Order(1)]
		public void ShouldGet_0x3000()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3000");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 2
			};

			Dmd.Model result = Dmd.getAddress(0x3000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(2)]
		public void ShouldGet_0x3200()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3200");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 3
			};

			Dmd.Model result = Dmd.getAddress(0x3200);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(3)]
		public void ShouldGet_0x3400()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3400");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 4
			};

			Dmd.Model result = Dmd.getAddress(0x3400);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(4)]
		public void ShouldGet_0x3600()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3600");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 5
			};

			Dmd.Model result = Dmd.getAddress(0x3600);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(5)]
		public void ShouldGet_0x3800()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3800");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 0
			};

			Dmd.Model result = Dmd.getAddress(0x3800);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(6)]
		public void ShouldGet_0x3A00()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 1
			};

			Dmd.Model result = Dmd.getAddress(0x3A00);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(7)]
		public void ShouldGet_0x3A00_CalculateOffset()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00, should calculate offset correct");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 1,
				subsystem = "videoram",
				bank = 1
			};

			Dmd.Model result = Dmd.getAddress(0x3A01);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(8)]
		public void ShouldGet_0x3FB9()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00, should calculate offset correct");

			Dmd.Model expectedResult = new Dmd.Model
			{
				offset = 0x3FB9,
				subsystem = "command"
			};

			Dmd.Model result = Dmd.getAddress(0x3FB9);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(9)]
		public void ShouldThrowInvalidAddress()
		{
			TestContext.WriteLine("DmdMapper, should throw when using invalid address, -1");

			Assert.Throws<System.Exception>(() => Dmd.getAddress(-1) /*{ message: 'INVALID_DMD_ADDRESSRANGE_-1' }*/);
		}
	}
}