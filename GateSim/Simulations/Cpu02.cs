using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateSim.Arithmetic;
using GateSim.InputOutput;
using GateSim.Memory;
using GateSim.Plexers;
using GateSim.Wiring;

namespace GateSim.Simulations
{
    //This will be iplementing the CPU shown in GateSim\Diagrams\CPU-D.png
    //It contains ROM for holding a program and treats the instruction counter
    //as a register so it can be written to.

    internal class Cpu02
    {
        public const int BitWidth = 8;
        private Sim sim = new Sim();

        //public bool[] RegSelect;
        //public bool[] Arg1Select;
        //public bool[] Arg2Select;
        //public bool[] FuncSelect;
        //public bool[] Literal;
        public bool[] Clock;

        public Dictionary<string, int> FuncCodes = new Dictionary<string, int>
        {
            {"add", 0},
            {"sub", 1},
            {"neg", 2},
            {"lit", 3}
        };

        public Cpu02()
        {
            var deco1 = sim.AddDevice(new Decoder(3), "deco1");

            var registers = new Register[8];
            for(int i = 0; i < registers.Length - 1; i++)
            {
                    registers[i] = sim.AddDevice(new Register(BitWidth), "r" + i);
            }
            var rc = sim.AddDevice(new Counter(BitWidth), "rc");

            var mux_arg1 = sim.AddDevice(new Multiplexer(BitWidth, 3), "mux_arg1");
            var mux_arg2 = sim.AddDevice(new Multiplexer(BitWidth, 3), "mux_arg2");
            var mux_litOrAlu = sim.AddDevice(new Multiplexer(BitWidth, 1), "mux_litOrAlu");

            var adder = sim.AddDevice(new Adder(BitWidth), "adder");
            var subtractor = sim.AddDevice(new Subtractor(BitWidth), "subtractor");
            var negator = sim.AddDevice(new Negator(BitWidth), "negator");
            var literal = sim.AddDevice(new ConstantOutput(BitWidth), "literal");

            var clk = sim.AddDevice(new Switch(), "clk");

            var rom = sim.AddDevice(new Rom(BitWidth, BitWidth), "rom");

            var splitterRanges = new Dictionary<int, Tuple<int, int>>
            {
                { 0, new Tuple<int, int>(0,0) },   //Load literal or ALU output
                { 1, new Tuple<int, int>(1,3) },   //Which reg should be loaded
                { 2, new Tuple<int, int>(4,11) },  //Literal value
                { 3, new Tuple<int, int>(12,14) }, //Arg 1 register
                { 4, new Tuple<int, int>(15,17) }, //Arg 2 register 
                { 5, new Tuple<int, int>(18,19) }  //Which ALU function
            };

            var splitter = sim.AddDevice(new Splitter(20, splitterRanges), "splitter");

            Clock = clk.Output;
        }

        public void SettleState()
        {
            sim.SettleState();
        }

        public IDevice GetDevice(string ID)
        {
            return sim.GetDevice(ID);
        }

        public void ClockTick()
        {
            Clock[0] = false;
            SettleState();
            Clock[0] = true;
            SettleState();
        }

    }
}
