using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class OutputGeneralIlluminationTests
	{
		OutputGeneralIllumination preWpc95;
		OutputGeneralIllumination wpc95;

		[SetUp]
		public void Init()
		{
			preWpc95 = OutputGeneralIllumination.getInstance(false);
			wpc95 = OutputGeneralIllumination.getInstance(true);
		}

		[Test, Order(1)]
		public void UpdateWithValue0()
		{
			TestContext.WriteLine("generalIllumination, update with value 0");

			var generalIllumination = preWpc95;
			generalIllumination.update(0x0);
			Assert.That(generalIllumination.generalIlluminationState[0], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[1], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[2], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[3], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[4], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[5], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[6], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[7], Is.EqualTo(0x00));
		}

		[Test, Order(2)]
		public void Wpc95UpdateWithValue0()
		{
			TestContext.WriteLine("generalIllumination wpc95, update with value 0");

			var generalIllumination = wpc95;
			generalIllumination.update(0x0);
			Assert.That(generalIllumination.generalIlluminationState[0], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[1], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[2], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[3], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[4], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[5], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[6], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[7], Is.EqualTo(0x00));
		}

		[Test, Order(3)]
		public void UpdateWithValue0xFF()
		{
			TestContext.WriteLine("generalIllumination, update with value 0xFF");

			var generalIllumination = preWpc95;
			generalIllumination.update(0xFF);
			Assert.That(generalIllumination.generalIlluminationState[0], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[1], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[2], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[3], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[4], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[5], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[6], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[7], Is.EqualTo(0x07));
		}

		[Test, Order(4)]
		public void Wpc95UpdateWithValue0xFF()
		{
			TestContext.WriteLine("generalIllumination wpc95, update with value 0xFF");

			var generalIllumination = wpc95;
			generalIllumination.update(0xFF);
			Assert.That(generalIllumination.generalIlluminationState[0], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[1], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[2], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[3], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[4], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[5], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[6], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[7], Is.EqualTo(0x07));
		}

		[Test, Order(5)]
		public void UpdateWithValue0x2_0x4()
		{
			TestContext.WriteLine("generalIllumination, update with value 0x2 and 0x4");

			var generalIllumination = preWpc95;
			generalIllumination.update(0x2);
			generalIllumination.update(0x4);
			Assert.That(generalIllumination.generalIlluminationState[0], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[1], Is.EqualTo(0x06));
			Assert.That(generalIllumination.generalIlluminationState[2], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[3], Is.EqualTo(0x00));
		}

		[Test, Order(6)]
		public void UpdateWithValue0x4_0x4()
		{
			TestContext.WriteLine("generalIllumination, update with value 0x4 and 0x4");

			var generalIllumination = preWpc95;
			generalIllumination.update(0x4);
			generalIllumination.update(0x4);
			Assert.That(generalIllumination.generalIlluminationState[0], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[1], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[2], Is.EqualTo(0x07));
			Assert.That(generalIllumination.generalIlluminationState[3], Is.EqualTo(0x00));
		}

		[Test, Order(7)]
		public void GetUint8ArrayFromState()
		{
			TestContext.WriteLine("generalIllumination, getUint8ArrayFromState");

			var generalIllumination = preWpc95;
			var result = generalIllumination.getUint8ArrayFromState(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
			Assert.That(result, Is.EqualTo(new byte[] { 8, 8, 8, 8, 8, 8, 8, 8 }));
		}

		[Test, Order(8)]
		public void ShouldDecreateValueDim()
		{
			TestContext.WriteLine("generalIllumination, should decrease value (dim)");

			var generalIllumination = preWpc95;
			generalIllumination.update(0xFF);
			generalIllumination.update(0x00);
			generalIllumination.update(0x00);
			Assert.That(generalIllumination.generalIlluminationState[0], Is.EqualTo(0x05));
			Assert.That(generalIllumination.generalIlluminationState[1], Is.EqualTo(0x05));
			Assert.That(generalIllumination.generalIlluminationState[2], Is.EqualTo(0x05));
			Assert.That(generalIllumination.generalIlluminationState[3], Is.EqualTo(0x05));
			Assert.That(generalIllumination.generalIlluminationState[4], Is.EqualTo(0x05));
			Assert.That(generalIllumination.generalIlluminationState[5], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[6], Is.EqualTo(0x00));
			Assert.That(generalIllumination.generalIlluminationState[7], Is.EqualTo(0x00));
		}
	}
}
