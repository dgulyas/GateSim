using System;
using GateSim.Gates;
using GateSim.Wiring;

namespace GateSim.Simulations
{
	public class Adder
	{
		//Refrence Adder.cs for diagram

		public void Run(bool aInput, bool bInput, bool cInput, bool sOutput, bool cOutput)
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

			var outputS = xor2.Output;
			var outputCout = or1.Output;

			sim.SettleState();

			if (outputS[0] != sOutput || outputCout[0] != cOutput)
			{
				throw new Exception("Wrong");
			}

		}

		public void Test()
		{ //This should be a nUnit test
			Run(false, false, false, false, false);
			Run(false, false, true, true, false);
			Run(false, true, false, true, false);
			Run(false, true, true, false, true);
			Run(true, false, false, true, false);
			Run(true, false, true, false, true);
			Run(true, true, false, false, true);
			Run(true, true, true, true, true);
		}

	}
}
