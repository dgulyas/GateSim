namespace Terminal.Simulations
{
    internal class Cpu02
    {
        public GateSim.Simulations.Cpu02 Sim;
        public Interface Terminal;

        public Cpu02()
        {
            Terminal = new Interface(22, 70);
            Sim = new GateSim.Simulations.Cpu02();
            LoadProgram();

            DefineCoords();
        }

        public bool ClockTick()
        {
            Terminal.PrintStateToConsole();
            Sim.ClockTick();
            return false; //Get the sim to return this value.
        }

        private void LoadProgram()
        {
            //section#, (bit range)  :purpose
            //0, (0, 0)   :Load literal or ALU output
            //1, (1, 3)   :Which reg should be loaded
            //2, (4, 11)  :Literal value
            //3, (12, 14) :Arg 1 register
            //4, (15, 17) :Arg 2 register 
            //5, (18, 19) :Which ALU function

            Sim.LoadProgram(new string[]
            {
                "",
            });
        }

        private void DefineCoords()
        {
            for (int i = 0; i < 4; i++) //I think there are more regs in cpu 2
            {
                Terminal.AddString("Reg " + i, 2, i * 2);
                Terminal.AddDevice(Sim.GetDevice($"r{i}"), 3, i * 2 + 1);
            }

            Terminal.AddString("aluOutput", 20, 3);
            //Terminal.AddDevice(Sim.GetDevice("mux3"), 22, 4);

            //Terminal.AddString("Funcs: add, sub, neg, lit", 2, 13);
        }
    }
}
