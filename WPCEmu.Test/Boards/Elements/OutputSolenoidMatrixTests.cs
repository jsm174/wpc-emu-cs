using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class OutputSolenoidMatrixTests
	{
		const int UPDATE_AFTER_TICKS = 8;

		OutputSolenoidMatrix solenoidMatrix;
		
		[SetUp]
		public void Init()
		{
			solenoidMatrix = OutputSolenoidMatrix.GetInstance(UPDATE_AFTER_TICKS);
		}

		[Test, Order(1)]
		public void UpdateAllHighPowerSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all high power solenoids");

			solenoidMatrix.write(0x3FE1, 0xFF);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[0]);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[7]);
			Assert.AreEqual(0, solenoidMatrix.solenoidState[8]);
		}

		[Test, Order(2)]
		public void UpdateAllLowPowerSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all low power solenoids");

			solenoidMatrix.write(0x3FE3, 0xFF);
			Assert.AreEqual(0, solenoidMatrix.solenoidState[7]);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[8]);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[15]);
		}

		[Test, Order(3)]
		public void UpdateAllFlashlightSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all flashlight solenoids");

			solenoidMatrix.write(0x3FE2, 0xFF);
			Assert.AreEqual(0, solenoidMatrix.solenoidState[15]);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[16]);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[23]);
		}

		[Test, Order(4)]
		public void UpdateAllGenericSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all generic solenoids");

			solenoidMatrix.write(0x3FE0, 0xFF);
			Assert.AreEqual(0, solenoidMatrix.solenoidState[23]);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[24]);
			Assert.AreEqual(0xFF, solenoidMatrix.solenoidState[31]);
		}

		[Test, Order(5)]
		public void FailIfValueExceedsUnsignedByteRange()
		{
			TestContext.WriteLine("solenoidMatrix, fail if value exceeds unsigned byte range");

			Assert.Throws<System.Exception>(() => solenoidMatrix.write(0x3FE0, 0xFFF));
		}

		[Test, Order(6)]
		public void FailIfAddressIsInvalid()
		{
			TestContext.WriteLine("solenoidMatrix, fail if address is invalid");

			Assert.Throws<System.Exception>(() => solenoidMatrix.write(0, 0xFF));
		}

		[Test, Order(7)]
		public void UpdateCycles()
		{
			TestContext.WriteLine("solenoidMatrix, update cycles");

			solenoidMatrix.write(0x3FE1, 0xFF);
			solenoidMatrix.executeCycle(UPDATE_AFTER_TICKS);
			Assert.AreEqual(0x7F, solenoidMatrix.solenoidState[0]);
		}
	}
}
