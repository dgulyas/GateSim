using GateSim;
using GateSim.Plexers;

namespace Tests.Plexers
{
	[TestClass]
	public class DecoderTests
	{
		[DataTestMethod]
		[DataRow(0, new []{false, false})]
		[DataRow(1, new []{true, false})]
		[DataRow(2, new []{false, true})]
		[DataRow(3, new []{true, true})]
		public void OutputsSetToCorrectValues(int selectedOutputIndex, bool[] selectorValue)
		{
			var dec = new Decoder(2);
			Util.SetArrayToValues(dec.OutputSelect, selectorValue);

			//Decoders start with output 0 set to high, so nothings changes during tick
			Assert.AreEqual(selectedOutputIndex != 0, dec.Tick());

			for (int i = 0; i < 4; i++)
			{
				Assert.AreEqual(i == selectedOutputIndex, dec.GetOutput(i)[0]);
			}

		}

	}
}
