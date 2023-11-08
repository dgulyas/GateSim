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

            var deviceStateCoords = new List<(string, int, int)>();
            var labelCoords = new List<(string, int, int)>();

            DefineCoords();
        }

        public void ClockTick()
        {
            sim.ClockTick();
            terminal.Refresh();
            var output = terminal.GetCurrentState();

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
        }

    }
}
