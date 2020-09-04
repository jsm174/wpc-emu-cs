using NUnit.Framework;
using WPCEmu.Boards.Mapper;

namespace WPCEmu.Test.Boards.Mapper
{
	[TestFixture]
	public class HardwareMapperTests
	{
		[Test, Order(1)]
		public void ShouldGet_0x3FC2()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FC2");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FC2,
				"externalIo");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FC2);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(2)]
		public void ShouldGet_0()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0");

			Assert.Throws<System.Exception>(() => HardwareMapper.getAddress(0) /*{ message: 'HW_GET_ADDRESS_INVALID_MEMORY_REGION_0x0' }*/);
		}

		[Test, Order(3)]
		public void ShouldGet_Negative1()
		{
			TestContext.WriteLine("HardwareMapper, should get address, -1");

			Assert.Throws<System.Exception>(() => HardwareMapper.getAddress(-1) /*{ message: 'HW_GET_ADDRESS_INVALID_MEMORY_REGION_0xffff' }*/);
		}

		[Test, Order(4)]
		public void ShouldGet_0x3200()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3200 (DMD PAGE)");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3200,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3200);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(5)]
		public void ShouldGet_0x3BFF()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3BFF (DMD PAGE)");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3BFF,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3BFF);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(6)]
		public void ShouldGet_0x3800()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3800 (DMD PAGE 1)");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3800,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3800);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(7)]
		public void ShouldGet_0x3A00()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3A00 (DMD PAGE 2)");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3A00,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3A00);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(8)]
		public void ShouldFail_0x4000()
		{
			TestContext.WriteLine("HardwareMapper, should fail to get address, 0x4000");

			Assert.Throws<System.Exception>(() => HardwareMapper.getAddress(0xFFFF) /*{ message: 'HW_GET_ADDRESS_INVALID_MEMORY_REGION_0x4000' }*/);
		}

		[Test, Order(9)]
		public void ShouldFail_InvalidOffset()
		{
			TestContext.WriteLine("HardwareMapper, should fail to get invalid offset");
		
			Assert.Throws<System.Exception>(() => HardwareMapper.getAddress(null) /*{ message: 'HW_GET_ADDRESS_UNDEFINED' }*/);
		}

		[Test, Order(10)]
		public void ShouldFail_0x2000()
		{
			TestContext.WriteLine("HardwareMapper, should fail to get address, 0x4000");

			Assert.Throws<System.Exception>(() => HardwareMapper.getAddress(0xFFFF) /*{ message: 'HW_GET_ADDRESS_INVALID_MEMORY_REGION_0x2000' }*/);
		}

		[Test, Order(11)]
		public void ShouldGet_0x3C00()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3c00");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3C00,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3C00);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(12)]
		public void ShouldGet_0x3E66()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3E66 - WPC_SERIAL_CONTROL_PORT **FIXME**");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3E66,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3E66);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(13)]
		public void ShouldGet_0x3FC0()
		{
			TestContext.WriteLine("should get address, 0x3FC0");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FC0,
				"externalIo");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FC0);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(14)]
		public void ShouldGet_0x3FD6()
		{
			TestContext.WriteLine("should get address, 0x3FD6");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FD6,
				"externalIo");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FD6);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(15)]
		public void ShouldGet_0x3FDC_WPCS_DATA()
		{
			TestContext.WriteLine("should get address, 0x3FDC - WPCS_DATA");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FDC,
				"sound");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FDC);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(16)]
		public void ShouldGet_0x3FDD_WPCS_CONTROL_STATUS()
		{
			TestContext.WriteLine("should get address, 0x3FDD - WPCS_CONTROL_STATUS");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FDD,
				"sound");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FDD);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(17)]
		public void ShouldGet_0x3FDE()
		{
			TestContext.WriteLine("should get address, 0x3FDE");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FDE,
				"sound");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FDE);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(18)]
		public void ShouldGet_0x3FE0()
		{
			TestContext.WriteLine("should get address, 0x3FE0");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FE0,
				"wpcio");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FE0);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(18)]
		public void ShouldGetException_FliptronicsAddress()
		{
			TestContext.WriteLine("HardwareMapper, should get exception for fliptronics address");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				HardwareMapper.MEMORY_ADDR_FLIPTRONICS_FLIPPER_PORT_A,
				"wpcio");

			HardwareMapper.Model result = HardwareMapper.getAddress(HardwareMapper.MEMORY_ADDR_FLIPTRONICS_FLIPPER_PORT_A);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(18)]
		public void ShouldGet_0x3FEB()
		{
			TestContext.WriteLine("should get display address, 0x3FEB");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FEB,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FEB);
			Assert.AreEqual(expectedResult, result);
		}

		[Test, Order(19)]
		public void ShouldGet_0x3FEF()
		{
			TestContext.WriteLine("should get display address, 0x3FEF");

			HardwareMapper.Model expectedResult = new HardwareMapper.Model(
				0x3FEF,
				"display");

			HardwareMapper.Model result = HardwareMapper.getAddress(0x3FEF);
			Assert.AreEqual(expectedResult, result);
		}

	}
}
