using NUnit.Framework;
using GateSim.Plexers;

namespace Tests.Plexers
{
	[TestFixture]
	public class DecoderTests
	{
		[TestCase(0, new []{false, false})]
		[TestCase(1, new []{true, false})]
		[TestCase(2, new []{false, true})]
		[TestCase(3, new []{true, true})]
		public void OutputsSetToCorrectValues(int selectedOutputIndex, bool[] selectorValue)
		{
			var dec = new Decoder(2);
			TestHelpers.SetArrayToValues(dec.OutputSelect, selectorValue);

			//Decoders start with output 0 set to high, so nothings changes during tick
			Assert.AreEqual(selectedOutputIndex != 0, dec.Tick());

			for (int i = 0; i < 4; i++)
			{
				Assert.AreEqual(i == selectedOutputIndex, dec.GetOutput(i)[0]);
			}

		}

	}
}
