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
			preWpc95 = OutputGeneralIllumination.GetInstance(false);
			wpc95 = OutputGeneralIllumination.GetInstance(true);
		}

		[Test, Order(1)]
		public void UpdateWithValue0()
		{
			TestContext.WriteLine("generalIllumination, update with value 0");

			var generalIllumination = preWpc95;
			generalIllumination.update(0x0);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[0]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[1]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[2]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[3]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[4]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[5]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[6]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[7]);
		}

		[Test, Order(2)]
		public void Wpc95UpdateWithValue0()
		{
			TestContext.WriteLine("generalIllumination wpc95, update with value 0");

			var generalIllumination = wpc95;
			generalIllumination.update(0x0);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[0]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[1]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[2]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[3]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[4]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[5]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[6]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[7]);
		}

		[Test, Order(3)]
		public void UpdateWithValue0xFF()
		{
			TestContext.WriteLine("generalIllumination, update with value 0xFF");

			var generalIllumination = preWpc95;
			generalIllumination.update(0xFF);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[0]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[1]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[2]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[3]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[4]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[5]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[6]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[7]);
		}

		[Test, Order(4)]
		public void Wpc95UpdateWithValue0xFF()
		{
			TestContext.WriteLine("generalIllumination wpc95, update with value 0xFF");

			var generalIllumination = wpc95;
			generalIllumination.update(0xFF);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[0]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[1]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[2]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[3]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[4]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[5]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[6]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[7]);
		}

		[Test, Order(5)]
		public void UpdateWithValue0x2_0x4()
		{
			TestContext.WriteLine("generalIllumination, update with value 0x2 and 0x4");

			var generalIllumination = preWpc95;
			generalIllumination.update(0x2);
			generalIllumination.update(0x4);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[0]);
			Assert.AreEqual(0x06, generalIllumination.generalIlluminationState[1]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[2]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[3]);
		}

		[Test, Order(6)]
		public void UpdateWithValue0x4_0x4()
		{
			TestContext.WriteLine("generalIllumination, update with value 0x4 and 0x4");

			var generalIllumination = preWpc95;
			generalIllumination.update(0x4);
			generalIllumination.update(0x4);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[0]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[1]);
			Assert.AreEqual(0x07, generalIllumination.generalIlluminationState[2]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[3]);
		}

		[Test, Order(7)]
		public void GetUint8ArrayFromState()
		{
			TestContext.WriteLine("generalIllumination, getUint8ArrayFromState");

			var generalIllumination = preWpc95;
			var result = generalIllumination.getUint8ArrayFromState(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
			Assert.AreEqual(new byte[] { 8, 8, 8, 8, 8, 8, 8, 8 }, result);
		}

		[Test, Order(8)]
		public void ShouldDecreateValueDim()
		{
			TestContext.WriteLine("generalIllumination, should decrease value (dim)");

			var generalIllumination = preWpc95;
			generalIllumination.update(0xFF);
			generalIllumination.update(0x00);
			generalIllumination.update(0x00);
			Assert.AreEqual(0x05, generalIllumination.generalIlluminationState[0]);
			Assert.AreEqual(0x05, generalIllumination.generalIlluminationState[1]);
			Assert.AreEqual(0x05, generalIllumination.generalIlluminationState[2]);
			Assert.AreEqual(0x05, generalIllumination.generalIlluminationState[3]);
			Assert.AreEqual(0x05, generalIllumination.generalIlluminationState[4]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[5]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[6]);
			Assert.AreEqual(0x00, generalIllumination.generalIlluminationState[7]);
		}
	}
}
