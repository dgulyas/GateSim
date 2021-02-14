using System;
using System.Collections.Generic;
using GateSim.Gates;
using GateSim.InputOutputs;

namespace GateSim
{
	public class Sim
	{
		readonly List<Wire> m_wires = new List<Wire>();
		readonly List<IDevice> m_devices = new List<IDevice>();

		int m_bitWidth = 8;

		public void Run()
		{
			var and1 = CreateDevice(new AndGate(m_bitWidth));
			var const1 = CreateDevice(new ConstantOutput(m_bitWidth, true));
			var const2 = CreateDevice(new ConstantOutput(m_bitWidth, true));
			const1.SetSpecificBit(0, false);
			const2.SetSpecificBit(2, false);

			Connect(const1.Output, and1.GetNewInput());
			Connect(const2.Output, and1.GetNewInput());


		}



		public void Test1()
		{
			var and1 = CreateDevice(new AndGate(m_bitWidth));
			var const1 = CreateDevice(new ConstantOutput(m_bitWidth, true));
			var const2 = CreateDevice(new ConstantOutput(m_bitWidth, true));
			const1.SetSpecificBit(0, false);
			const2.SetSpecificBit(2, false);

			Connect(const1.Output, and1.GetNewInput());
			Connect(const2.Output, and1.GetNewInput());

			var and2 = CreateDevice(new AndGate(m_bitWidth));
			var const3 = CreateDevice(new ConstantOutput(m_bitWidth, true));
			const3.SetSpecificBit(4, false);

			Connect(and1.Output, and2.GetNewInput());
			Connect(const3.Output, and2.GetNewInput());

			var not1 = CreateDevice(new NotGate(m_bitWidth));

			Connect(and2.Output, not1.Input);

			for (int i = 0; i < 10; i++)
			{
				foreach (var wire in m_wires)
				{
					wire.CopyInputToOutput();
				}

				foreach (var device in m_devices)
				{
					device.Tick();
				}
			}

		}

		public Wire Connect(bool[] feederValue, bool[] eaterValue)
		{
			var wire = new Wire(feederValue, eaterValue);
			m_wires.Add(wire);
			return wire;
		}

		public T CreateDevice<T>(T device) where T : IDevice
		{
			m_devices.Add(device);
			return device;
		}

		//The state is "settled" when none of the outputs change.
		//That means everything should have propogated through the system.
		public void SettleState()
		{
			var somethingChanged = true;
			var numCycles = 0;

			while (somethingChanged && numCycles < 1000)
			{
				somethingChanged = false;

				foreach (var wire in m_wires)
				{
					wire.CopyInputToOutput();
				}

				foreach (var device in m_devices)
				{
					if (device.Tick())
					{
						somethingChanged = true;
					}
				}

				numCycles++;
			}

			if (numCycles >= 1000)
			{ //It might have settled on the 1000th cycle
				throw new Exception("State isn't settling");
			}
		}

		public string GetState(bool[] array)
		{
			var state = "";
			foreach (var x in array)
			{
				state += x ? "1" : "0";
			}

			return state;
		}

	}
}
