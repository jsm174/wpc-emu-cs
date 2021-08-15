using System;
using NUnit.Framework;

namespace WPCEmu.Test.Boards.Mapper
{
	using Memory = WPCEmu.Boards.Mapper.Memory;

	[TestFixture]
	public class MemoryTests
	{
		[Test, Order(1)]
		public void ShouldGet_16322()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 16322");

			var expectedResult = new MapperModel
			{
				offset = 16322,
				subsystem = "hardware"
			};

            var result = Memory.getAddress(16322);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(2)]
		public void ShouldGet_49090()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 49090 - this crashes the emu");

            var expectedResult = new MapperModel
			{
				offset = 16322,
				subsystem = "system"
			};

            var result = Memory.getAddress(49090);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(3)]
		public void ShouldFail_InvalidOffset()
		{
			TestContext.WriteLine("MemoryMapper, should fail when using invalid offset");

			var result = Assert.Throws<Exception>(() => Memory.getAddress(null));
			Assert.That(result.Message, Is.EqualTo("MEMORY_GET_ADDRESS_UNDEFINED"));
		}

		[Test, Order(4)]
		public void ShouldGet_Negative1()
		{
			TestContext.WriteLine("MemoryMapper, should get address, -1");

            var expectedResult = new MapperModel
			{
				offset = 32767,
				subsystem = "system"
			};

            var result = Memory.getAddress(-1);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(5)]
		public void ShouldGet_0()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x0");

            var expectedResult = new MapperModel
			{
				offset = 0,
				subsystem = "ram"
			};

			var result = Memory.getAddress(0x0);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(6)]
		public void ShouldGet_0x2000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x2000");

			var expectedResult = new MapperModel
			{
				offset = 0x2000,
				subsystem = "ram"
			};

			var result = Memory.getAddress(0x2000);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(7)]
		public void ShouldGet_0x2900()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x2900");

			var expectedResult = new MapperModel
			{
				offset = 0x2900,
				subsystem = "ram"
			};

			var result = Memory.getAddress(0x2900);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(8)]
		public void ShouldGet_0x4000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x4000");

			var expectedResult = new MapperModel
			{
				offset = 0,
				subsystem = "bank"
			};

			var result = Memory.getAddress(0x4000);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(8)]
		public void ShouldGet_0x8000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x8000");

			var expectedResult = new MapperModel
			{
				offset = 0,
				subsystem = "system"
			};

			var result = Memory.getAddress(0x8000);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(9)]
		public void ShouldGet_0x10000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x10000");

            MapperModel expectedResult = new MapperModel
			{
				offset = 0,
				subsystem = "ram"
			};

            MapperModel result = Memory.getAddress(0x10000);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(10)]
		public void ShouldGet_0x3C00()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x3C00");

			var expectedResult = new MapperModel
			{
				offset = 0x3C00,
				subsystem = "ram"
			};

			var result = Memory.getAddress(0x3C00);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(11)]
		public void ShouldGet_0x3FAF()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x3FAF");

			var expectedResult = new MapperModel
			{
				offset = 0x3FAF,
				subsystem = "ram"
			};

			var result = Memory.getAddress(0x3FAF);
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}
