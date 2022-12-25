using GateSim;
using GateSim.InputOutput;

namespace Tests.InputOutput
{
	[TestClass]
	public class TtyTests
	{
		private readonly int m_bitWidth = 8;

		[DataTestMethod] 
		[DataRow(65, 'A')]
		[DataRow(66, 'B')]
		[DataRow(48, '0')]
		[DataRow(38, '&')]
		public void TTYPrintsCorrectChar(int input, char printedChar)
		{
			char outputChar = '.';
			void WriteAction(char c) => outputChar = c;

			var tty = new Tty(m_bitWidth, WriteAction);
			tty.Enable[0] = true;
			tty.Clock[0] = true;
			Util.SetArrayToValues(tty.Input, input.ToBoolArray(m_bitWidth));

			Assert.IsFalse(tty.Tick());
			Assert.AreEqual(printedChar, outputChar);
		}

		[TestMethod]
		public void TTYPrintsNothingWhenEnableLow()
		{
			char outputChar = '.';
			void WriteAction(char c) => outputChar = c;

			var tty = new Tty(m_bitWidth, WriteAction);
			tty.Enable[0] = false;
			tty.Clock[0] = true;
			Util.SetArrayToValues(tty.Input, 65.ToBoolArray(m_bitWidth));

			Assert.IsFalse(tty.Tick());
			Assert.AreEqual('.', outputChar);
		}


	}
}
