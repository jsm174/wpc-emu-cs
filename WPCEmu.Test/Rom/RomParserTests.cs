using System;
using System.Linq;
using NUnit.Framework;
using WPCEmu.Boards;
using WPCEmu.Rom;

namespace WPCEmu.Test.Rom
{
	[TestFixture]
	public class RomParserTests
	{
		[Test, Order(1)]
		public void ShouldRejectEmptyRom()
		{
			TestContext.WriteLine("romParser should reject empty rom");

			var result = Assert.Throws<Exception>(() => RomParser.parse());
			Assert.That(result.Message, Is.EqualTo("INVALID_ROM_DATA"));
		}

		[Test, Order(2)]
		public void ShouldRejectInvalidData()
		{
			TestContext.WriteLine("romParser should reject invalid data");

			var romData = new RomParser.Roms {
				u06 = new byte[7]
            };

			var result = Assert.Throws<Exception>(() => RomParser.parse(romData));
			Assert.That(result.Message, Is.EqualTo("INVALID_ROM_SIZE"));
		}

		[Test, Order(3)]
		public void ShouldParseGameRom()
		{
			TestContext.WriteLine("romParser should parse game rom");

			var romData = buildRomData();
			var result = RomParser.parse(romData);
			Assert.That(result.gameRom[0x000000], Is.EqualTo(11));
			Assert.That(result.fileName, Is.EqualTo("Unknown"));
			Assert.That(result.romSizeMBit, Is.EqualTo(2));
			Assert.That(result.skipWpcRomCheck, Is.EqualTo(false));
			Assert.That(result.hasSecurityPic, Is.EqualTo(false));
			Assert.That(result.preDcsSoundboard, Is.EqualTo(false));
		}

		[Test, Order(4)]
		public void ShouldParseWpc95Board()
		{
			TestContext.WriteLine("romParser should parse wpc95 board");

			var romData = buildRomData();
			var metaData = new RomParser.RomMetaData
			{
				features = new string[] { "wpc95", "securityPic" }
			};
			var result = RomParser.parse(romData, metaData);
			Assert.That(result.preDcsSoundboard, Is.EqualTo(false));
			Assert.That(result.hasSecurityPic, Is.EqualTo(true));
		}

		[Test, Order(5)]
		public void ShouldParseWpcDmdBoard()
		{
			TestContext.WriteLine("romParser should parse wpcDmd board");

			var romData = buildRomData();
			var metaData = new RomParser.RomMetaData
			{
				features = new string[] { "wpcDmd" }
			};
			var result = RomParser.parse(romData, metaData);
			Assert.That(result.preDcsSoundboard, Is.EqualTo(true));
			Assert.That(result.hasSecurityPic, Is.EqualTo(false));
		}

		[Test, Order(6)]
		public void ShouldParseWpcFliptronicsBoard()
		{
			TestContext.WriteLine("romParser should parse wpcFliptronics board");

			var romData = buildRomData();
			var metaData = new RomParser.RomMetaData
			{
				features = new string[] { "wpcFliptronics" }
			};
			var result = RomParser.parse(romData, metaData);
			Assert.That(result.preDcsSoundboard, Is.EqualTo(true));
			Assert.That(result.hasSecurityPic, Is.EqualTo(false));
			Assert.That(result.hasAlphanumericDisplay, Is.EqualTo(false));
		}

		[Test, Order(7)]
		public void ShouldParseWpcAlphanumericBoard()
		{
			TestContext.WriteLine("romParser should parse wpcAlphanumeric board");

			var romData = buildRomData();
			var metaData = new RomParser.RomMetaData
			{
				features = new string[] { "wpcAlphanumeric" }
			};
			var result = RomParser.parse(romData, metaData);
			Assert.That(result.hasAlphanumericDisplay, Is.EqualTo(true));
		}

		[Test, Order(8)]
		public void ShouldParseMemoryPosition()
		{
			TestContext.WriteLine("romParser should parse wpcAlphanumeric board");

			var romData = buildRomData();
			var metaData = new RomParser.RomMetaData
			{
				memoryPosition = new MemoryHandler.MemoryPositionData[]
				{
					new MemoryHandler.MemoryPositionData
					{
						offset = 0x326,
						description = "current text",
						type = "string"
					}
				}
			};
			var result = RomParser.parse(romData, metaData);
			Assert.That(result.memoryPosition[0].offset, Is.EqualTo(0x326));
			Assert.That(result.memoryPosition[0].description, Is.EqualTo("current text"));
			Assert.That(result.memoryPosition[0].type, Is.EqualTo("string"));
		}

		private RomParser.Roms buildRomData()
        {
			byte[] u06 = Enumerable.Repeat((byte)11, 256 * 1024).ToArray();
			return new RomParser.Roms
			{
				u06 = u06
			};
        }
	}
}