using System;
using NUnit.Framework;
using WPCEmu.Boards.Mapper;

namespace WPCEmu.Test.Boards.Mapper
{
	[TestFixture]
	public class HardwareTests
	{
		[Test, Order(1)]
		public void ShouldGetAddress_0x3FC2()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FC2");

			var expectedResult = new MapperModel
			{
				offset = 0x3FC2,
				subsystem = "externalIo"
			};

			var result = Hardware.getAddress(0x3FC2);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(2)]
		public void ShouldGetAddress_0()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0");

			var result = Assert.Throws<Exception>(() => Hardware.getAddress(0));
			Assert.That(result.Message, Is.EqualTo("HW_GET_ADDRESS_INVALID_MEMORY_REGION_0x0"));
		}

		[Test, Order(3)]
		public void ShouldGet_Negative1()
		{
			TestContext.WriteLine("HardwareMapper, should get address, -1");

			var result = Assert.Throws<Exception>(() => Hardware.getAddress(-1));
			Assert.That(result.Message, Is.EqualTo("HW_GET_ADDRESS_INVALID_MEMORY_REGION_0xFFFF"));
		}

		[Test, Order(4)]
		public void ShouldGetAddress_0x3200()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3200 (DMD PAGE)");

			var expectedResult = new MapperModel
			{
				offset = 0x3200,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3200);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(5)]
		public void ShouldGetAddress_0x3BFF()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3BFF (DMD PAGE)");

			var expectedResult = new MapperModel
			{
				offset = 0x3BFF,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3BFF);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(6)]
		public void ShouldGetAddress_0x3800()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3800 (DMD PAGE 1)");

			var expectedResult = new MapperModel
			{
				offset = 0x3800,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3800);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(7)]
		public void ShouldGetAddress_0x3A00()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3A00 (DMD PAGE 2)");

			var expectedResult = new MapperModel
			{
				offset = 0x3A00,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3A00);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(8)]
		public void ShouldFailGetAddress_0x4000()
		{
			TestContext.WriteLine("HardwareMapper, should fail to get address, 0x4000");

			var result = Assert.Throws<Exception>(() => Hardware.getAddress(0x4000));
			Assert.That(result.Message, Is.EqualTo("HW_GET_ADDRESS_INVALID_MEMORY_REGION_0x4000"));
		}

		[Test, Order(9)]
		public void ShouldFailGetInvalidOffset()
		{
			TestContext.WriteLine("HardwareMapper, should fail to get invalid offset");

			var result = Assert.Throws<Exception>(() => Hardware.getAddress(null));
			Assert.That(result.Message, Is.EqualTo("HW_GET_ADDRESS_UNDEFINED"));
		}

		[Test, Order(10)]
		public void ShouldFailGetAddress_0x2000()
		{
			TestContext.WriteLine("HardwareMapper, should fail to get address, 0x4000");

			var result = Assert.Throws<Exception>(() => Hardware.getAddress(0x2000));
			Assert.That(result.Message, Is.EqualTo("HW_GET_ADDRESS_INVALID_MEMORY_REGION_0x2000"));
		}

		[Test, Order(11)]
		public void ShouldGetAddress_0x3C00()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3c00");

			var expectedResult = new MapperModel
			{
				offset = 0x3C00,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3C00);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(12)]
		public void ShouldGetAddress_0x3E66()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3E66 - WPC_SERIAL_CONTROL_PORT **FIXME**");

			var expectedResult = new MapperModel
			{
				offset = 0x3E66,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3E66);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(13)]
		public void ShouldGetAddress_0x3FC0()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FC0");

			var expectedResult = new MapperModel
			{
				offset = 0x3FC0,
				subsystem = "externalIo"
			};

			var result = Hardware.getAddress(0x3FC0);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(14)]
		public void ShouldGetAddress_0x3FD6()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FD6");

			var expectedResult = new MapperModel
			{
				offset = 0x3FD6,
				subsystem = "externalIo"
			};

			var result = Hardware.getAddress(0x3FD6);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(15)]
		public void ShouldGetAddress_0x3FDC_WPCS_DATA()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FDC - WPCS_DATA");

			var expectedResult = new MapperModel
			{
				offset = 0x3FDC,
				subsystem = "sound"
			};

			var result = Hardware.getAddress(0x3FDC);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(16)]
		public void ShouldGetAddress_0x3FDD_WPCS_CONTROL_STATUS()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FDD - WPCS_CONTROL_STATUS");

			var expectedResult = new MapperModel
			{
				offset = 0x3FDD,
				subsystem = "sound"
			};

			var result = Hardware.getAddress(0x3FDD);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(17)]
		public void ShouldGetAddress_0x3FDE()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FDE");

			var expectedResult = new MapperModel
			{
				offset = 0x3FDE,
				subsystem = "sound"
			};

			var result = Hardware.getAddress(0x3FDE);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(18)]
		public void ShouldGetAddress_0x3FE0()
		{
			TestContext.WriteLine("HardwareMapper, should get address, 0x3FE0");

			var expectedResult = new MapperModel
			{
				offset = 0x3FE0,
				subsystem = "wpcio"
			};

			var result = Hardware.getAddress(0x3FE0);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(19)]
		public void ShouldGetException_FliptronicsAddress()
		{
			TestContext.WriteLine("HardwareMapper, should get exception for fliptronics address");

			var expectedResult = new MapperModel
			{
				offset = Hardware.MEMORY_ADDR_FLIPTRONICS_FLIPPER_PORT_A,
				subsystem = "wpcio"
			};

			var result = Hardware.getAddress(Hardware.MEMORY_ADDR_FLIPTRONICS_FLIPPER_PORT_A);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(20)]
		public void ShouldGetDisplayAddress_0x3FEB()
		{
			TestContext.WriteLine("HardwareMapper, should get display address, 0x3FEB");

			var expectedResult = new MapperModel
			{
				offset = 0x3FEB,
				subsystem = "wpcio"
			};

			var result = Hardware.getAddress(0x3FEB);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(21)]
		public void ShouldGetDisplayAddressAlphaNumericDisplays_0x3FEB()
		{
			TestContext.WriteLine("HardwareMapper, should get display address for alpha numeric displays, 0x3FEB");

			var expectedResult = new MapperModel
			{
				offset = 0x3FEB,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3FEB, true);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(22)]
		public void ShouldGetDisplayAddress_0x3FEF()
		{
			TestContext.WriteLine("HardwareMapper, should get display address, 0x3FEF");

			var expectedResult = new MapperModel
			{
				offset = 0x3FEF,
				subsystem = "wpcio"
			};

			var result = Hardware.getAddress(0x3FEF);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(23)]
		public void ShouldGetDisplayAddressAlphaNumericDisplays_0x3FEF()
		{
			TestContext.WriteLine("HardwareMapper, should get display address for alpha numeric displays, 0x3FEF");

			var expectedResult = new MapperModel
			{
				offset = 0x3FEF,
				subsystem = "display"
			};

			var result = Hardware.getAddress(0x3FEF, true);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(24)]
		public void ShouldGetDCSData_0x3FDC()
		{
			TestContext.WriteLine("HardwareMapper, should get DCS data, 0x3FDC");

			var expectedResult = new MapperModel
			{
				offset = 0x3FDC,
				subsystem = "sound"
			};

			var result = Hardware.getAddress(0x3FDC);
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test, Order(25)]
		public void ShouldGetDCSData_0x3FDD()
		{
			TestContext.WriteLine("HardwareMapper, should get DCS data, 0x3FDD");

			var expectedResult = new MapperModel
			{
				offset = 0x3FDD,
				subsystem = "sound"
			};

			var result = Hardware.getAddress(0x3FDD);
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}
