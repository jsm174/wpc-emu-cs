using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards.Up;

namespace WPCEmu.Test.Boards.Up
{
	[TestFixture]
	public class SecurityPicTests
	{
		const int MACHINE_SERIAL = 530;

		SecurityPic securityPic;

		[SetUp]
		public void Init()
		{
			securityPic = SecurityPic.getInstance(MACHINE_SERIAL);
		}

		[Test, Order(1)]
		public void Reset()
		{
			TestContext.WriteLine("SecurityPic, reset");

			securityPic.reset();
			Assert.That(securityPic.lastByteWrite, Is.EqualTo(0xFF));
		}

		[Test, Order(2)]
		public void InvalidReadAfterReset()
		{
			TestContext.WriteLine("SecurityPic, invalid read after reset");

			var result = securityPic.read();
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(3)]
		public void ReadSerialNumber()
        {
			TestContext.WriteLine("SecurityPic, read serial number");

			const byte WPC_PIC_RESET = 0x00;
			securityPic.write(WPC_PIC_RESET);
			securityPic.write(0x7F);
			var result = securityPic.read();
			Assert.That(result, Is.EqualTo(178));
		}

		[Test, Order(4)]
		public void ReadFirstRow()
		{
			TestContext.WriteLine("SecurityPic, read first row");

			securityPic.write(0x16);
			var result = securityPic.read((offset) => {
				return offset;
			});
			Assert.That(result, Is.EqualTo(1));
		}

		[Test, Order(5)]
		public void ReadLastRow()
		{
			TestContext.WriteLine("SecurityPic, read last row");

			securityPic.write(0x1F);
			var result = securityPic.read((offset) => {
				return offset;
			});
			Assert.That(result, Is.EqualTo(10));
		}

		[Test, Order(6)]
		public void CalculateInitialSerialNumbers()
		{
			TestContext.WriteLine("SecurityPic, calculate initial serial numbers");

			byte[] expectedPicSerial = {
				197, 49, 52, 28,
				110, 0, 95, 1,
				253, 226, 18, 243,
				28, 0, 117, 178,
			};
			Assert.That(securityPic.originalPicSerialNumber, Is.EqualTo(expectedPicSerial.ToArray()));
			Assert.That(securityPic.serialNumberScrambler, Is.EqualTo(0xA5));
		}
	}
}
