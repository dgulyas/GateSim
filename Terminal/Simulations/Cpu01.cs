using GateSim;

namespace Terminal.Simulations
{
    // This is basically a test harness for CPU 1. (GateSim\Simulations\Cpu01.cs)
    // Notice how it needs to create the instructions and handle iterating
    // through them/parsing them. This won't be needed when the CPU has program memory.
    // A class under Terminal.Simulation should only have to define the layout of
    // the screen (the coordinates) and then tick the cpu, getting the new screen
    // from the terminal instance.
    public class Cpu01
    {
        public GateSim.Simulations.Cpu01 Sim;
        public Interface Terminal;

        private List<int[]> instructions = new List<int[]>
        {
            //regSelect, arg1, arg2, func, lit
            //add, sub, neg, lit
            new []{0,0,0,3,1},
            new []{1,0,0,3,1},
            new []{3,0,1,0,0},
            new []{0,0,0,3,0}, //no-op so the result of the last instruction can be seen
            //new []{0,0,0,3,4},
            //new []{0,0,0,3,4},
            //new []{0,0,0,3,4},
            //new []{0,0,0,3,4}
        };

        private int currInstructionIndex = 0;

        public Cpu01()
        {
            Terminal = new Interface(22, 70);
            Sim = new GateSim.Simulations.Cpu01();

            DefineCoords();
        }

        public bool ClockTick()
        {
            SetNextInstruction();
            Terminal.PrintStateToConsole();
            Sim.ClockTick();
            return currInstructionIndex >= instructions.Count;
        }

        private void SetNextInstruction()
        {
            var inst = instructions[currInstructionIndex];
            Util.SetArrayToValues(Sim.RegSelect, inst[0].ToBoolArray(2));
            Util.SetArrayToValues(Sim.Arg1Select, inst[1].ToBoolArray(2));
            Util.SetArrayToValues(Sim.Arg2Select, inst[2].ToBoolArray(2));
            Util.SetArrayToValues(Sim.FuncSelect, inst[3].ToBoolArray(2));
            Util.SetArrayToValues(Sim.Literal, inst[4].ToBoolArray(8));

            currInstructionIndex++;
        }

        private void DefineCoords()
        {
            for (int i = 0; i < 4; i++)
            {
                Terminal.AddString("Reg " + i, 2, i * 2);
                Terminal.AddDevice(Sim.GetDevice($"r{i}"), 3, i * 2 + 1);
            }

            Terminal.AddString("aluOutput", 20, 3);
            Terminal.AddDevice(Sim.GetDevice("mux3"), 22, 4);

            Terminal.AddString("RegSelect", 2, 10);
            Terminal.AddIO(Sim.RegSelect, 4, 11);

            Terminal.AddString("Arg1Select", 12, 10);
            Terminal.AddIO(Sim.Arg1Select, 14, 11);

            Terminal.AddString("Arg2Select", 24, 10);
            Terminal.AddIO(Sim.Arg2Select, 26, 11);

            Terminal.AddString("FuncSelect", 36, 10);
            Terminal.AddIO(Sim.FuncSelect, 38, 11);

            Terminal.AddString("Literal", 48, 10);
            Terminal.AddIO(Sim.Literal, 50, 11);

            Terminal.AddString("Funcs: add, sub, neg, lit", 2, 13);
        }

    }
}
