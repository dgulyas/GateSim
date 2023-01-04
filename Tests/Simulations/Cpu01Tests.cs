using GateSim;
using GateSim.Simulations;

namespace Tests.Simulations
{
	[TestClass]
	public class Cpu01Tests
    {
        Dictionary<string, int> opSelect;
        public Cpu01 cpu;

        public  Cpu01Tests()
        {
            opSelect = new Dictionary<string, int>();
            opSelect.Add("add", 0);
            opSelect.Add("sub", 1);
            opSelect.Add("neg", 2);
            opSelect.Add("lit", 3);

            cpu = new Cpu01();
        }

        [TestMethod]
        public void ExampleRun1()
        {
            ExecuteOperation("lit", 0, 0, 0, 15);
            ExecuteOperation("lit", 0, 0, 1, 20);
            ExecuteOperation("add", 0, 1, 2, 0);

            var thing = cpu.GetDevice("r2");
        }

        /// <summary>
        /// Sets the control bits for the cpu
        /// </summary>
        /// <param name="op">The operation to perform</param>
        /// <param name="r1">The register with the first operand</param>
        /// <param name="r2">The register with the second operand</param>
        /// <param name="rd">The register the result should be stored in</param>
        /// <param name="lit">The literal for the load operation</param>
        private void ExecuteOperation(string op, int r1, int r2, int rd, int lit)
        {
            Util.SetArrayToValues(cpu.FuncSelect, opSelect[op].ToBoolArray(2));
            Util.SetArrayToValues(cpu.Arg1Select, r1.ToBoolArray(2));
            Util.SetArrayToValues(cpu.Arg2Select, r2.ToBoolArray(2));
            Util.SetArrayToValues(cpu.RegSelect, rd.ToBoolArray(2));
            Util.SetArrayToValues(cpu.Literal, lit.ToBoolArray(8));

            cpu.SettleState();
        }

    }
}