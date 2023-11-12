using System.Security.Cryptography;

namespace Terminal.Simulations
{
    public class Cpu01
    {
        public GateSim.Simulations.Cpu01 sim;
        public Interface terminal;

        public Cpu01()
        {
            terminal = new Interface(22, 70);
            sim = new GateSim.Simulations.Cpu01();

            DefineCoords();
        }

        public void ClockTick()
        {
            sim.ClockTick();
            var output = terminal.Refresh();

            Console.WriteLine();
            foreach(var line in output)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        private void DefineCoords()
        {
            for (int i = 0; i < 4; i++)
            {
                terminal.AddString("Reg " + i, 2, i * 2);
                terminal.AddDevice(sim.GetDevice($"r{i}"), 3, i * 2 + 1);
            }

            terminal.AddString("aluOutput", 20, 3);
            terminal.AddDevice(sim.GetDevice("mux3"), 22, 4);

            terminal.AddString("RegSelect", 2, 10);
            terminal.AddIO(sim.RegSelect, 4, 11);

            terminal.AddString("Arg1Select", 12, 10);
            terminal.AddIO(sim.Arg1Select, 14, 11);

            terminal.AddString("Arg2Select", 24, 10);
            terminal.AddIO(sim.Arg2Select, 26, 11);

            terminal.AddString("FuncSelect", 36, 10);
            terminal.AddIO(sim.FuncSelect, 38, 11);

            terminal.AddString("Literal", 48, 10);
            terminal.AddIO(sim.Literal, 50, 11);
        }

    }
}
