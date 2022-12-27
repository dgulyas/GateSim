using GateSim.Gates;
using GateSim.Wiring;

namespace GateSim.Simulations
{
	public class Adder
	{
		//Refrence Adder.cs for logic gate diagram

		/// <summary>
		/// Adds three binary digits together using logic gates.
		/// </summary>
		/// <param name="aInput">The first operand</param>
		/// <param name="bInput">The second operand</param>
		/// <param name="cInput">The carry input</param>
		/// <returns>A pair of bools: (sum, carry)</returns>
		public (bool, bool) Run(bool aInput, bool bInput, bool cInput)
		{
			var BW = 1; //Bitwidth

			var sim = new Sim();

			var inputA = sim.CreateDevice(new ConstantOutput(BW, aInput));
			var inputB = sim.CreateDevice(new ConstantOutput(BW, bInput));
			var inputCin = sim.CreateDevice(new ConstantOutput(BW, cInput));

			var xor1 = sim.CreateDevice(new XorGate(BW));
			var xor2 = sim.CreateDevice(new XorGate(BW));
			var and1 = sim.CreateDevice(new AndGate(BW));
			var and2 = sim.CreateDevice(new AndGate(BW));
			var or1 = sim.CreateDevice(new OrGate(BW));

			var outputS = xor2.Output;
			var outputCout = or1.Output;

			sim.Connect(inputA.Output,
							xor1.GetNewInput(),
							and2.GetNewInput());

			sim.Connect(inputB.Output,
							xor1.GetNewInput(),
							and2.GetNewInput());

			sim.Connect(inputCin.Output,
							xor2.GetNewInput(),
							and1.GetNewInput());

			sim.Connect(xor1.Output,
							xor2.GetNewInput(),
							and1.GetNewInput());

			sim.Connect(and1.Output, or1.GetNewInput());
			sim.Connect(and2.Output, or1.GetNewInput());

			sim.SettleState();

			return (outputS[0], outputCout[0]);
		}

	}
}
