using GateSim;
using GateSim.Arithmetic;
using GateSim.Wiring;

namespace Tests.Arithmetic
{
    [TestClass]
    public class NegatorTests
    {
        // [DataTestMethod]
        // public void NegatorWorks(){
        //     var negator = new Negator(8);
        //     var input =
        // }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(30)]
        [DataRow(200)]
        public void NegatorCancelsItselfOut(int input){
            var bitwidth = 8;

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
        public static void SubtractionWithNegatorWorks(int a, int b){

        }

    }
}