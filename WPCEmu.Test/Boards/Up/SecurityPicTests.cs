using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards.Up;

namespace WPCEmu.Test.Boards.Up
{
	public class SecurityPicTests
	{
		const int MACHINE_SERIAL = 530;

		SecurityPic securityPic;

		byte GetRowFunction(byte offset)
		{
			return offset;
		}

		[SetUp]
		public void Init()
		{
			securityPic = SecurityPic.GetInstance(MACHINE_SERIAL);
			securityPic.Reset();
		}

		[Test, Order(0)]
		public void Reset()
		{
			Assert.AreEqual(0xFF, securityPic.lastByteWrite);
		}

		[Test, Order(1)]
		public void InvalidReadAfterReset()
		{
			byte result = securityPic.Read(GetRowFunction);
			Assert.AreEqual(0, result);
		}

		[Test, Order(2)]
		public void ReadSerialNumber()
        {
			const byte WPC_PIC_RESET = 0x00;
			securityPic.Write(WPC_PIC_RESET);
			securityPic.Write(0x7F);
			byte result = securityPic.Read(GetRowFunction);
			Assert.AreEqual(178, result);
		}

		[Test, Order(3)]
		public void ReadFirstRow()
		{
			securityPic.Write(0x16);
			byte result = securityPic.Read(GetRowFunction);
			Assert.AreEqual(1, result);
		}

		[Test, Order(4)]
		public void ReadLastRow()
		{
			securityPic.Write(0x1F);
			byte result = securityPic.Read(GetRowFunction);
			Assert.AreEqual(10, result);
		}

		[Test, Order(5)]
		public void CalculateInitialSerialNumbers()
		{
			byte[] expectedPicSerial = new byte[] {
				197, 49, 52, 28,
				110, 0, 95, 1,
				253, 226, 18, 243,
				28, 0, 117, 178,
			};
			Assert.AreEqual(expectedPicSerial.ToArray(), securityPic.originalPicSerialNumber);
			Assert.AreEqual(0xA5, securityPic.serialNumberScrambler);
		}
	}
}
