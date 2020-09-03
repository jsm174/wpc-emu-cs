﻿using System.Linq;
using System.Diagnostics;
using NUnit.Framework;
using WPCEmu.Boards.Up;

namespace WPCEmu.Test.Boards.Up
{
	public class SecurityPicTests
	{
		const int MACHINE_SERIAL = 530;

		SecurityPic securityPic;

		[SetUp]
		public void Init()
		{
			securityPic = SecurityPic.GetInstance(MACHINE_SERIAL);
		}

		[Test, Order(1)]
		public void Reset()
		{
			Debug.Print("SecurityPic, reset");
			securityPic.reset();

			Assert.AreEqual(0xFF, securityPic.lastByteWrite);
		}

		[Test, Order(2)]
		public void InvalidReadAfterReset()
		{
			Debug.Print("SecurityPic, invalid read after reset");

			byte result = securityPic.read();
			Assert.AreEqual(0, result);
		}

		[Test, Order(3)]
		public void ReadSerialNumber()
        {
			Debug.Print("SecurityPic, read serial number");

			const byte WPC_PIC_RESET = 0x00;
			securityPic.write(WPC_PIC_RESET);
			securityPic.write(0x7F);
			byte result = securityPic.read();
			Assert.AreEqual(178, result);
		}

		[Test, Order(4)]
		public void ReadFirstRow()
		{
			Debug.Print("SecurityPic, read first row");

			securityPic.write(0x16);
			byte result = securityPic.read((offset) => {
				return offset;
			});
			Assert.AreEqual(1, result);
		}

		[Test, Order(5)]
		public void ReadLastRow()
		{
			Debug.Print("SecurityPic, read last row");

			securityPic.write(0x1F);
			byte result = securityPic.read((offset) => {
				return offset;
			});
			Assert.AreEqual(10, result);
		}

		[Test, Order(6)]
		public void CalculateInitialSerialNumbers()
		{
			Debug.Print("SecurityPic, calculate initial serial numbers");

			byte[] expectedPicSerial = {
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
