using NUnit.Framework;
using WPCEmu.Boards.Elements;

namespace WPCEmu.Test.Boards.Elements
{
	[TestFixture]
	public class InputSwitchMatrixTests
	{
		InputSwitchMatrix inputSwitchMatrix;

		[SetUp]
		public void Init()
		{
			inputSwitchMatrix = InputSwitchMatrix.GetInstance();
		}

		[Test, Order(1)]
		public void SetCabinetKey_0x1()
		{
			TestContext.WriteLine("InputSwitchMatrix, setCabinetKey - 0x1");

			inputSwitchMatrix.setCabinetKey(0x1);
			Assert.That(inputSwitchMatrix.switchState[0], Is.EqualTo(1));
		}

		[Test, Order(2)]
		public void SetCabinetKey_0x5()
		{
			TestContext.WriteLine("InputSwitchMatrix, setCabinetKey - 0x5");

			inputSwitchMatrix.setCabinetKey(0x5);
			Assert.That(inputSwitchMatrix.switchState[0], Is.EqualTo(5));
		}

		[Test, Order(3)]
		public void GetCabinetKey()
		{
			TestContext.WriteLine("InputSwitchMatrix, getCabinetKey");

			inputSwitchMatrix.setCabinetKey(0x5);
			Assert.That(inputSwitchMatrix.getCabinetKey(), Is.EqualTo(5));
		}

		[Test, Order(4)]
		public void SetFliptronicsInput_Invalid()
		{
			TestContext.WriteLine("InputSwitchMatrix, setFliptronicsInput invalid");

			Assert.That(inputSwitchMatrix.getFliptronicsKeys(), Is.EqualTo(255));
		}

		[Test, Order(5)]
		public void SetFliptronicsInput_true()
		{
			TestContext.WriteLine("InputSwitchMatrix, setFliptronicsInput true");

			inputSwitchMatrix.setFliptronicsInput("F0", true);
			Assert.That(inputSwitchMatrix.getFliptronicsKeys(), Is.EqualTo(255));
		}

		[Test, Order(6)]
		public void SetFliptronicsInput_false()
		{
			TestContext.WriteLine("InputSwitchMatrix, setFliptronicsInput true");

			inputSwitchMatrix.setFliptronicsInput("F0", false);
			Assert.That(inputSwitchMatrix.getFliptronicsKeys(), Is.EqualTo(255));
		}

		[Test, Order(7)]
		public void GetFliptronicsKeysInverted()
		{
			TestContext.WriteLine("InputSwitchMatrix, getFliptronicsKeys - return inverted value");

			inputSwitchMatrix.setFliptronicsInput("F0");
			inputSwitchMatrix.setFliptronicsInput("F6");
			Assert.That(inputSwitchMatrix.getFliptronicsKeys(), Is.EqualTo(223));
		}

		[Test, Order(8)]
		public void GetRow()
		{
			TestContext.WriteLine("InputSwitchMatrix, getRow");

			var result = inputSwitchMatrix.getRow(0);
			Assert.That(result, Is.EqualTo(0));
		}

		[Test, Order(9)]
		public void IgnoreInvalidKeySetInputKey()
		{
			TestContext.WriteLine("InputSwitchMatrix, ignore invalid key setInputKey");

			inputSwitchMatrix.setInputKey(10);
			Assert.That(inputSwitchMatrix.switchState[1], Is.EqualTo(0));
		}

		[Test, Order(10)]
		public void ValidSetInputKey()
		{
			TestContext.WriteLine("InputSwitchMatrix, valid setInputKey");

			inputSwitchMatrix.setInputKey(25);
			Assert.That(inputSwitchMatrix.switchState[2], Is.EqualTo(24));
		}

		[Test, Order(11)]
		public void SetInputKeyShouldSetKey()
		{
			TestContext.WriteLine("InputSwitchMatrix, setInputKey should set key");

			inputSwitchMatrix.setInputKey(25, true);
			Assert.That(inputSwitchMatrix.switchState[2], Is.EqualTo(24));
		}

		[Test, Order(12)]
		public void SetInputKeyShouldClearKey()
		{
			TestContext.WriteLine("InputSwitchMatrix, setInputKey should clear key");

			inputSwitchMatrix.setInputKey(25, false);
			Assert.That(inputSwitchMatrix.switchState[2], Is.EqualTo(8));
		}
	}
}
