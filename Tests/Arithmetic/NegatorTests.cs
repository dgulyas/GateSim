using GateSim;
using GateSim.Arithmetic;

namespace Tests.Arithmetic
{
    [TestClass]
    public class NegatorTests
    {
        int bitWidth = 8;

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(0)]
        [DataRow(254)]
        public void AddOneWorks(int input){
            var a = input.ToBoolArray(bitWidth);
            Negator.AddOne(a);
            CollectionAssert.AreEqual((input+1).ToBoolArray(bitWidth), a);
        }

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(1)]
        [DataRow(255)]
        public void MinusOneWorks(int input){
            var a = input.ToBoolArray(bitWidth);
            Negator.MinusOne(a);
            CollectionAssert.AreEqual((input-1).ToBoolArray(bitWidth), a);
        }
    }
}