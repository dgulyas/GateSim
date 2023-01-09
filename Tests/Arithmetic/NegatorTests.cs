using GateSim;
using GateSim.Arithmetic;
using GateSim.Wiring;

namespace Tests.Arithmetic
{
	[TestClass]
	public class NegatorTests
	{
		private static int bitwidth = 8;
		// [DataTestMethod]
		// public void NegatorWorks(){
		//	 var negator = new Negator(8);
		//	 var input =
		// }

		[DataTestMethod]
		[DataRow(0)]
		[DataRow(1)]
		[DataRow(30)]
		[DataRow(200)]
		public void NegatorCancelsItselfOut(int input){
			var negator1 = new Negator(bitwidth);
			var negator2 = new Negator(bitwidth);
			var wire = new Wire(negator1.Output, negator2.Input);

			var inputArray = input.ToBoolArray(bitwidth);
			Util.SetArrayToValues(negator1.Input, inputArray);

			negator1.Tick();
			wire.CopyInputToOutput();
			negator2.Tick();

			var outputInt = negator2.Output.ToInt();

			Assert.AreEqual(input, outputInt);
		}

		[DataTestMethod]
		[DataRow(2,1)]
		[DataRow(20,1)]
		[DataRow(200,1)]
		[DataRow(255,1)]
		[DataRow(255,100)]
		[DataRow(26,26)]
		public void SubtractionWithNegatorWorks(int a, int b){
			var negator = new Negator(8);
			var adder = new Adder(8);
			var wire = new Wire(negator.Output, adder.Input1);

			Util.SetArrayToValues(negator.Input, b.ToBoolArray(bitwidth));
			Util.SetArrayToValues(adder.Input2, a.ToBoolArray(bitwidth));

			negator.Tick();
			wire.CopyInputToOutput();
			adder.Tick();

			CollectionAssert.AreEqual((a-b).ToBoolArray(bitwidth), adder.Output);
		}

	}
}