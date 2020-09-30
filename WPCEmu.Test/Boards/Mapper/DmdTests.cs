using System;
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

			var expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 2
			};

			var result = Dmd.getAddress(0x3000);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(2)]
		public void ShouldGet_0x3200()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3200");

			var expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 3
			};

			var result = Dmd.getAddress(0x3200);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(3)]
		public void ShouldGet_0x3400()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3400");

			var expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 4
			};

			var result = Dmd.getAddress(0x3400);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(4)]
		public void ShouldGet_0x3600()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3600");

			var expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 5
			};

			var result = Dmd.getAddress(0x3600);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(5)]
		public void ShouldGet_0x3800()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3800");

			var expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 0
			};

			var result = Dmd.getAddress(0x3800);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(6)]
		public void ShouldGet_0x3A00()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00");

			var expectedResult = new Dmd.Model
			{
				offset = 0,
				subsystem = "videoram",
				bank = 1
			};

			var result = Dmd.getAddress(0x3A00);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(7)]
		public void ShouldGet_0x3A00_CalculateOffset()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00, should calculate offset correct");

			var expectedResult = new Dmd.Model
			{
				offset = 1,
				subsystem = "videoram",
				bank = 1
			};

			var result = Dmd.getAddress(0x3A01);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(8)]
		public void ShouldGet_0x3FB9()
		{
			TestContext.WriteLine("DmdMapper, should get address, 0x3A00, should calculate offset correct");

			var expectedResult = new Dmd.Model
			{
				offset = 0x3FB9,
				subsystem = "command"
			};

			var result = Dmd.getAddress(0x3FB9);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(9)]
		public void ShouldThrowInvalidAddress()
		{
			TestContext.WriteLine("DmdMapper, should throw when using invalid address, -1");

			var result = Assert.Throws<Exception>(() => Dmd.getAddress(-1));
			Assert.That(result.Message, Is.EqualTo("INVALID_DMD_ADDRESSRANGE_-1"));
		}
	}
}
