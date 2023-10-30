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

			Assert.AreEqual(35, GetRegValue("r2"));
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
			cpu.SetInputs(op, r1, r2, rd, lit);
			cpu.ClockTick();
		}

        private int GetRegValue(string regName){
            return ((Register)(cpu.GetDevice("r2"))).Output.ToInt();
        }

	}
}