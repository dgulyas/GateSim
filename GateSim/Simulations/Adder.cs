using GateSim.Gates;
using GateSim.Wiring;

namespace GateSim.Simulations
{
	public class Adder
	{
		//Refrence Adder.cs for diagram

		public (bool[], bool[]) Run(bool aInput, bool bInput, bool cInput)
		{
			var sim = new Sim();

			var inputA = sim.CreateDevice(new ConstantOutput(1, aInput));
			var inputB = sim.CreateDevice(new ConstantOutput(1, bInput));
			var inputCin = sim.CreateDevice(new ConstantOutput(1, cInput));

			var xor1 = sim.CreateDevice(new XorGate(1));
			var xor2 = sim.CreateDevice(new XorGate(1));
			var and1 = sim.CreateDevice(new AndGate(1));
			var and2 = sim.CreateDevice(new AndGate(1));
			var or1 = sim.CreateDevice(new OrGate(1));

			var w1 = sim.Connect(inputA.Output, xor1.GetNewInput());
			w1.AddOutput(and2.GetNewInput());

			var w2 = sim.Connect(inputB.Output, xor1.GetNewInput());
			w2.AddOutput(and2.GetNewInput());

			var w3 = sim.Connect(inputCin.Output, xor2.GetNewInput());
			w3.AddOutput(and1.GetNewInput());

			var w4 = sim.Connect(xor1.Output, xor2.GetNewInput());
			w4.AddOutput(and1.GetNewInput());

			sim.Connect(and1.Output, or1.GetNewInput());
			sim.Connect(and2.Output, or1.GetNewInput());

			sim.SettleState();

			return (xor2.Output, or1.Output);
		}

	}
}
