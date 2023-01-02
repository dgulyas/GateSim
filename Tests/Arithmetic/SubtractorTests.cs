using GateSim;
using GateSim.Arithmetic;

namespace Tests.Arithmetic
{
    [TestClass]
    public class SubtractorTests
    {
        [TestMethod]
        public void SubtractorWorks()
        {
            var bitwidth = 5;
            var subtractor = new Subtractor(bitwidth);

            for(int i = 1; i < (int)Math.Pow(2,bitwidth); i++ )
            {
                for(int j = 0; j <= i; j++)
                {
                    Util.SetArrayToValues(subtractor.Input1, i.ToBoolArray(bitwidth));
                    Util.SetArrayToValues(subtractor.Input2, j.ToBoolArray(bitwidth));
                    subtractor.Tick();

                    Assert.AreEqual(i-j, subtractor.Output.ToInt());
                }
            }
        }

    }
}