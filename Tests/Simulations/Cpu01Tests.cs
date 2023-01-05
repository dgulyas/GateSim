using GateSim;
using GateSim.Simulations;
using GateSim.Memory;

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
            cpu.Clock[0] = false;
        }

        [TestMethod]
        public void ExampleRun1()
        {
            ExecuteOperation("lit", 0, 0, 0, 15); //load 15 into r0
            ExecuteOperation("lit", 0, 0, 1, 20); //load 10 into r1
            ExecuteOperation("add", 0, 1, 2, 0); //add r0 and r1 and store in r2

            var r2 = (Register)(cpu.GetDevice("r2"));
            Assert.AreEqual(35, r2.Output.ToInt());
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

            cpu.Clock[0] = false;
            cpu.SettleState();
            cpu.Clock[0] = true;
            cpu.SettleState();
        }

    }
}