using NUnit.Framework;
using WPCEmu.Boards.Mapper;

namespace WPCEmu.Test.Boards.Mapper
{
	[TestFixture]
	public class MemoryMapperTests
	{
		[Test, Order(1)]
		public void ShouldGet_16322()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 16322");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 16322,
				subsystem = "hardware"
			};
				
			MemoryMapper.Model result = MemoryMapper.getAddress(16322);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(2)]
		public void ShouldGet_49090()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 49090 - this crashes the emu");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 16322,
				subsystem = "system"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(49090);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(3)]
		public void ShouldFail_InvalidOffset()
		{
			TestContext.WriteLine("MemoryMapper, should fail when using invalid offset");
		
			Assert.Throws<System.Exception>(() => MemoryMapper.getAddress(null) /*{ message: 'MEMORY_GET_ADDRESS_UNDEFINED' }*/);
		}

		[Test, Order(4)]
		public void ShouldGet_Negative1()
		{
			TestContext.WriteLine("MemoryMapper, should get address, -1");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 32767,
				subsystem = "system"
			};
		
			MemoryMapper.Model result = MemoryMapper.getAddress(-1);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(5)]
		public void ShouldGet_0()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x0");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0,
				subsystem = "ram"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x0);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(6)]
		public void ShouldGet_0x2000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x2000");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0x2000,
				subsystem = "ram"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x2000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(7)]
		public void ShouldGet_0x2900()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x2900");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0x2900,
				subsystem = "ram"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x2900);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(8)]
		public void ShouldGet_0x4000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x4000");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0,
				subsystem = "bank"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x4000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(8)]
		public void ShouldGet_0x8000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x8000");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0,
				subsystem = "system"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x8000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(9)]
		public void ShouldGet_0x10000()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x10000");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0,
				subsystem = "ram"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x10000);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(10)]
		public void ShouldGet_0x3C00()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x3C00");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0x3C00,
				subsystem = "ram"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x3C00);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(11)]
		public void ShouldGet_0x3FAF()
		{
			TestContext.WriteLine("MemoryMapper, should get address, 0x3FAF");

			MemoryMapper.Model expectedResult = new MemoryMapper.Model
			{
				offset = 0x3FAF,
				subsystem = "ram"
			};

			MemoryMapper.Model result = MemoryMapper.getAddress(0x3FAF);
			Assert.AreEqual(expectedResult, result);
		}
	}
}