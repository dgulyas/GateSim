using GateSim;
using GateSim.InputOutput;
using NUnit.Framework;

namespace Tests.InputOutput
{
	[TestFixture]
	public class TtyTests
	{
		private readonly int m_bitWidth = 8;

		//private Action<char> thing;

		[TestCase(65, 'A')]
		[TestCase(66, 'B')]
		[TestCase(48, '0')]
		[TestCase(38, '&')]
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

		[Test]
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
