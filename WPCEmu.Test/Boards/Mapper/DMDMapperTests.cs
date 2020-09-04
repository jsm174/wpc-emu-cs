using NUnit.Framework;
using WPCEmu.Boards.Mapper;

namespace WPCEmu.Test.Boards.Mapper
{
	[TestFixture]
	public class DMDMapperTests
	{
		[Test, Order(1)]
		public void ShouldGet_0x3000()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3000");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				0,
				"videoram",
				2);

			DMDMapper.Model result = DMDMapper.getAddress(0x3000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(2)]
		public void ShouldGet_0x3200()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3200");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				0,
				"videoram",
				3);

			DMDMapper.Model result = DMDMapper.getAddress(0x3200);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(3)]
		public void ShouldGet_0x3400()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3400");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				0,
				"videoram",
				4);

			DMDMapper.Model result = DMDMapper.getAddress(0x3400);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(4)]
		public void ShouldGet_0x3600()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3600");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				0,
				"videoram",
				5);

			DMDMapper.Model result = DMDMapper.getAddress(0x3600);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(5)]
		public void ShouldGet_0x3800()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3800");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				0,
				"videoram",
				0);

			DMDMapper.Model result = DMDMapper.getAddress(0x3800);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(6)]
		public void ShouldGet_0x3A00()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				0,
				"videoram",
				1);

			DMDMapper.Model result = DMDMapper.getAddress(0x3A00);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(7)]
		public void ShouldGet_0x3A00_CalculateOffset()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00, should calculate offset correct");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				1,
				"videoram",
				1);

			DMDMapper.Model result = DMDMapper.getAddress(0x3A01);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(8)]
		public void ShouldGet_0x3FB9()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00, should calculate offset correct");

			DMDMapper.Model expectedResult = new DMDMapper.Model(
				0x3FB9,
				"command",
				null);

			DMDMapper.Model result = DMDMapper.getAddress(0x3FB9);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(9)]
		public void ShouldThrowInvalidAddress()
		{
			TestContext.WriteLine("DmdMapper, should throw when using invalid address, -1");

			Assert.Throws<System.Exception>(() => DMDMapper.getAddress(-1) /*{ message: 'INVALID_DMD_ADDRESSRANGE_-1' }*/);
		}
	}
}