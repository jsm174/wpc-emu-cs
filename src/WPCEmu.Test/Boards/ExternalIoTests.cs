using NUnit.Framework;
using WPCEmu.Boards;

namespace WPCEmu.Test.Boards
{
	[TestFixture]
	public class ExternalIoTests
	{
		ExternalIo ioBoard;

		[SetUp]
		public void Init()
		{
			ioBoard = ExternalIo.getInstance();
		}

		[Test, Order(1)]
		public void ShouldReadWriteToTicketDispense()
		{
			TestContext.WriteLine("external-io, should read write to ticket dispense");

			const ushort offset = 0x3FC6;
			ioBoard.write(offset, 255);
			var result = ioBoard.read(offset);
			Assert.That(result, Is.EqualTo(0xFF));
		}
	}
}
