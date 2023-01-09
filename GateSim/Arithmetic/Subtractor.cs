using GateSim.Wiring;

namespace GateSim.Arithmetic
{
	public class Subtractor : IDevice
	{
		public bool[] Input1 { get; }
		public bool[] Input2 { get; }
		public bool[] CarryOut { get; }
		public bool[] Output { get; }
		public int Id { get; set; }

		private Adder adder;
		private Wire wire;
		private Negator negator;

		public bool Tick(bool printDebug = false){
			var oldState = (bool[]) Output.Clone();

			negator.Tick();
			wire.CopyInputToOutput();
			adder.Tick();

			return !oldState.SequenceEqual(Output);
		}

		public string GetStateString()
		{
			throw new NotImplementedException();
		}

		public Subtractor(int bitWidth){
			negator = new Negator(bitWidth);
			adder = new Adder(bitWidth);
			wire = new Wire(negator.Output, adder.Input2);

			Input1 = adder.Input1;
			Input2 = negator.Input;
			Output = adder.Output;
			CarryOut = adder.CarryOut; //Is this correct?

			negator.Tick();
			wire.CopyInputToOutput();
			adder.Tick();
		}
	}
}