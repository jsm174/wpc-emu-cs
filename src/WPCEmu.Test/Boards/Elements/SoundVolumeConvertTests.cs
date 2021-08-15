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

			var result = SoundVolumeConvert.getRelativeVolumeDcs(0x00, 0xFF);
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(2)]
		public void DCSConvertMaxVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert DCS, convert max volume");

			var result = SoundVolumeConvert.getRelativeVolumeDcs(0xFF, 0x00);
			Assert.That(result, Is.EqualTo(31));
		}

		[Test, Order(3)]
		public void DCSRefuseInvalidVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert DCS, refuse invalid volume");

			var result = SoundVolumeConvert.getRelativeVolumeDcs(0xAA, 0xAA);
			Assert.That(result, Is.EqualTo(null));
		}

		[Test, Order(4)]
		public void PreDCSConvertMinVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert preDCS, convert min volume");

			var result = SoundVolumeConvert.getRelativeVolumePreDcs(0x00, 0xFF);
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(5)]
		public void PreDCSConvertMaxVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert preDCS, convert max volume");

			var result = SoundVolumeConvert.getRelativeVolumePreDcs(0x1F, 0xE0);
			Assert.That(result, Is.EqualTo(31));
		}

		[Test, Order(6)]
		public void PreDCSRefuseInvalidVolume()
		{
			TestContext.WriteLine("SoundVolumeConvert preDCS, refuse invalid volume");

			var result = SoundVolumeConvert.getRelativeVolumePreDcs(1, 1);
			Assert.That(result, Is.EqualTo(null));
		}
	}
}
