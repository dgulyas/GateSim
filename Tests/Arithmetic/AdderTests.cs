using GateSim;
using GateSim.Arithmetic;

namespace Tests.Arithmetic
{
    [TestClass]
    public class AdderTests
    {
        [DataTestMethod]
        [DataRow(1, 1, 2, 0)]
        [DataRow(3, 100, 103, 0)]
        [DataRow(123, 123, 246, 0)]
        [DataRow(255, 1, 0, 1)]
        [DataRow(255, 255, 254, 1)]
        public void AdderWorksCorrectly(int a, int b, int sum, int carryOut){
            var bitWidth = 8;
            var adder = new Adder(bitWidth);
            Util.SetArrayToValues(adder.Input1, a.ToBoolArray(bitWidth));
            Util.SetArrayToValues(adder.Input2, b.ToBoolArray(bitWidth));

            adder.Tick();

            CollectionAssert.AreEqual(sum.ToBoolArray(bitWidth), adder.Output);
            CollectionAssert.AreEqual(carryOut.ToBoolArray(1), adder.CarryOut);
        }
    }
}