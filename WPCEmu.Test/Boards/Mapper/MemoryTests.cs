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

			Memory.Model expectedResult = new Memory.Model
			{
				offset = 16322,
				subsystem = "hardware"
			};

            Memory.Model result = Memory.getAddress(16322);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(2)]
		public void ShouldGet_49090()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 49090 - this crashes the emu");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 16322,
				subsystem = "system"
			};

            Memory.Model result = Memory.getAddress(49090);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(3)]
		public void ShouldFail_InvalidOffset()
		{
			TestContext.WriteLine("MemoryMapper, should fail when using invalid offset");

            Assert.Throws<System.Exception>(() => Memory.getAddress(null) /*{ message: 'MEMORY_GET_ADDRESS_UNDEFINED' }*/);
		}

		[Test, Order(4)]
		public void ShouldGet_Negative1()
		{
			TestContext.WriteLine("MemoryMapper, should get address, -1");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 32767,
				subsystem = "system"
			};

            Memory.Model result = Memory.getAddress(-1);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(5)]
		public void ShouldGet_0()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x0");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0,
				subsystem = "ram"
			};

            Memory.Model result = Memory.getAddress(0x0);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(6)]
		public void ShouldGet_0x2000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x2000");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0x2000,
				subsystem = "ram"
			};

            Memory.Model result = Memory.getAddress(0x2000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(7)]
		public void ShouldGet_0x2900()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x2900");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0x2900,
				subsystem = "ram"
			};

            Memory.Model result = Memory.getAddress(0x2900);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(8)]
		public void ShouldGet_0x4000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x4000");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0,
				subsystem = "bank"
			};

            Memory.Model result = Memory.getAddress(0x4000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(8)]
		public void ShouldGet_0x8000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x8000");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0,
				subsystem = "system"
			};

            Memory.Model result = Memory.getAddress(0x8000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(9)]
		public void ShouldGet_0x10000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x10000");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0,
				subsystem = "ram"
			};

            Memory.Model result = Memory.getAddress(0x10000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(10)]
		public void ShouldGet_0x3C00()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x3C00");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0x3C00,
				subsystem = "ram"
			};

            Memory.Model result = Memory.getAddress(0x3C00);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(11)]
		public void ShouldGet_0x3FAF()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x3FAF");

            Memory.Model expectedResult = new Memory.Model
			{
				offset = 0x3FAF,
				subsystem = "ram"
			};

            Memory.Model result = Memory.getAddress(0x3FAF);
			Assert.AreEqual(expectedResult, result);
		}
	}
}