using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class SoundVolumeConvertTests
	{
		[Test, Order(1)]
		public void DCSConvertMinVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert DCS, convert min volume");

			byte? result = SoundVolumeConvert.getRelativeVolumeDcs(0x00, 0xFF);
			Assert.AreEqual(0, result);
		}

		[Test, Order(2)]
		public void DCSConvertMaxVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert DCS, convert max volume");

			byte? result = SoundVolumeConvert.getRelativeVolumeDcs(0xFF, 0x00);
			Assert.AreEqual(31, result);
		}

		[Test, Order(3)]
		public void DCSRefuseInvalidVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert DCS, refuse invalid volume");

			byte? result = SoundVolumeConvert.getRelativeVolumeDcs(0xAA, 0xAA);
			Assert.AreEqual(null, result);
		}

		[Test, Order(4)]
		public void PreDCSConvertMinVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert preDCS, convert min volume");

			byte? result = SoundVolumeConvert.getRelativeVolumePreDcs(0x00, 0xFF);
			Assert.AreEqual(0, result);
		}

		[Test, Order(5)]
		public void PreDCSConvertMaxVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert preDCS, convert max volume");

			byte? result = SoundVolumeConvert.getRelativeVolumePreDcs(0x1F, 0xE0);
			Assert.AreEqual(31, result);
		}

		[Test, Order(6)]
		public void PreDCSRefuseInvalidVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert preDCS, refuse invalid volume");

			byte? result = SoundVolumeConvert.getRelativeVolumePreDcs(1, 1);
			Assert.AreEqual(null, result);
		}
	}
}
