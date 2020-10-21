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
			solenoidMatrix = OutputSolenoidMatrix.getInstance(UPDATE_AFTER_TICKS);
		}

		[Test, Order(1)]
		public void UpdateAllHighPowerSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all high power solenoids");

			solenoidMatrix.write(0x3FE1, 0xFF);
			Assert.That(solenoidMatrix.solenoidState[0], Is.EqualTo(0xFF));
			Assert.That(solenoidMatrix.solenoidState[7], Is.EqualTo(0xFF));
			Assert.That(solenoidMatrix.solenoidState[8], Is.EqualTo(0));
		}

		[Test, Order(2)]
		public void UpdateAllLowPowerSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all low power solenoids");

			solenoidMatrix.write(0x3FE3, 0xFF);
			Assert.That(solenoidMatrix.solenoidState[7], Is.EqualTo(0));
			Assert.That(solenoidMatrix.solenoidState[8], Is.EqualTo(0xFF));
			Assert.That(solenoidMatrix.solenoidState[15], Is.EqualTo(0xFF));
		}

		[Test, Order(3)]
		public void UpdateAllFlashlightSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all flashlight solenoids");

			solenoidMatrix.write(0x3FE2, 0xFF);
			Assert.That(solenoidMatrix.solenoidState[15], Is.EqualTo(0));
			Assert.That(solenoidMatrix.solenoidState[16], Is.EqualTo(0xFF));
			Assert.That(solenoidMatrix.solenoidState[23], Is.EqualTo(0xFF));
		}

		[Test, Order(4)]
		public void UpdateAllGenericSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update all generic solenoids");

			solenoidMatrix.write(0x3FE0, 0xFF);
			Assert.That(solenoidMatrix.solenoidState[23], Is.EqualTo(0));
			Assert.That(solenoidMatrix.solenoidState[24], Is.EqualTo(0xFF));
			Assert.That(solenoidMatrix.solenoidState[31], Is.EqualTo(0xFF));
		}

		[Test, Order(5)]
		public void UpdateFliptronicsSolenoids()
		{
			TestContext.WriteLine("solenoidMatrix, update fliptronics solenoids");

			solenoidMatrix.writeFliptronic(0xFF);
			Assert.That(solenoidMatrix.solenoidState[31], Is.EqualTo(0));
			Assert.That(solenoidMatrix.solenoidState[32], Is.EqualTo(0xFF));
			Assert.That(solenoidMatrix.solenoidState[33], Is.EqualTo(0xFF));
			Assert.That(solenoidMatrix.solenoidState[39], Is.EqualTo(0xFF));
		}

		[Test, Order(6)]
		public void FailIfValueExceedsUnsignedByteRange()
		{
			TestContext.WriteLine("solenoidMatrix, fail if value exceeds unsigned byte range");

			Assert.Throws<System.Exception>(() => solenoidMatrix.write(0x3FE0, 0xFFF));
		}

		[Test, Order(7)]
		public void FailIfAddressIsInvalid()
		{
			TestContext.WriteLine("solenoidMatrix, fail if address is invalid");

			Assert.Throws<System.Exception>(() => solenoidMatrix.write(0, 0xFF));
		}

		[Test, Order(8)]
		public void UpdateCycles()
		{
			TestContext.WriteLine("solenoidMatrix, update cycles");

			solenoidMatrix.write(0x3FE1, 0xFF);
			solenoidMatrix.executeCycle(UPDATE_AFTER_TICKS);
			Assert.That(solenoidMatrix.solenoidState[0], Is.EqualTo(0x7F));
		}
	}
}
