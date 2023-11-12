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
    //This will be implementing the CPU shown in GateSim\Diagrams\CPU-D.png
    //It contains ROM for holding a program and treats the instruction counter
    //as a register so it can be written to.

    public class Cpu02
    {
        public const int BitWidth = 8;
        private Sim sim = new Sim();
        private Rom rom;

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
            //Step 1: Create devices.

            var deco_regSelect = sim.AddDevice(new Decoder(3), "deco1");

            var registers = new Register[8];
            for(int i = 0; i < registers.Length - 1; i++)
            {
                    registers[i] = sim.AddDevice(new Register(BitWidth), "r" + i);
            }
            //program counter is treated like a register
            var r_pc = sim.AddDevice(new Counter(BitWidth), "rc");

            var mux_arg1 = sim.AddDevice(new Multiplexer(BitWidth, 3), "mux_arg1");
            var mux_arg2 = sim.AddDevice(new Multiplexer(BitWidth, 3), "mux_arg2");
            var mux_litOrAlu = sim.AddDevice(new Multiplexer(BitWidth, 1), "mux_litOrAlu");
            var mux_aluOpSelect = sim.AddDevice(new Multiplexer(BitWidth, 2), "mux_aluOpSelect");

            var adder = sim.AddDevice(new Adder(BitWidth), "adder");
            var subtractor = sim.AddDevice(new Subtractor(BitWidth), "subtractor");
            var negator = sim.AddDevice(new Negator(BitWidth), "negator");
            var literal = sim.AddDevice(new ConstantOutput(BitWidth), "literal");

            var clk = sim.AddDevice(new Switch(), "clk");

            rom = sim.AddDevice(new Rom(BitWidth, BitWidth), "rom");

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

            //Step 2: Set up wiring connecting device outputs to device inputs.

            for(int i = 0; i < registers.Length; i++)
            {
                //Hook up register write select
                sim.Connect(deco_regSelect.GetOutput(i), registers[i].Enable);

                //Hook up register data inputs
                sim.Connect(mux_litOrAlu.Output, registers[i].Input);

                //Hook up register data outputs to argument mux's
                sim.Connect(registers[i].Output, mux_arg1.GetInput(i), mux_arg2.GetInput(i));

                //Hook up the clock to each register
                sim.Connect(clk.Output, registers[i].Clock);
            }
            //Hook up the program counter
            sim.Connect(deco_regSelect.GetOutput(7), r_pc.Load);
            sim.Connect(mux_litOrAlu.Output, r_pc.Input);
            sim.Connect(r_pc.Output, mux_arg1.GetInput(7), mux_arg2.GetInput(7));
            sim.Connect(clk.Output, r_pc.Clock);

            //Hook up the argument mux's to the inputs for the function devices
            sim.Connect(mux_arg1.Output, adder.Input1, subtractor.Input1, negator.Input);
            sim.Connect(mux_arg2.Output, adder.Input2, subtractor.Input2);

            //Hook the function devices to the mux that decides which function's output is saved
            sim.Connect(adder.Output, mux_aluOpSelect.GetInput(0));
            sim.Connect(subtractor.Output, mux_aluOpSelect.GetInput(1));
            sim.Connect(negator.Output, mux_aluOpSelect.GetInput(2));
            sim.Connect(literal.Output, mux_aluOpSelect.GetInput(3));

            //Hook up alu output to 'lit or alu' mux
            sim.Connect(mux_aluOpSelect.Output, mux_litOrAlu.GetInput(1));

            //Hook up instruction splitter to inputs
            sim.Connect(splitter.GetOutput(0), mux_litOrAlu.InputSelect);
            sim.Connect(splitter.GetOutput(1), deco_regSelect.OutputSelect);
            sim.Connect(splitter.GetOutput(2), mux_litOrAlu.GetInput(0));
            sim.Connect(splitter.GetOutput(3), mux_arg1.InputSelect);
            sim.Connect(splitter.GetOutput(4), mux_arg2.InputSelect);
            sim.Connect(splitter.GetOutput(5), mux_aluOpSelect.InputSelect);

            //Hook up instruction splitter input to rom output
            sim.Connect(rom.Output, splitter.Input);
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

        public void LoadProgram(string[] program)
        {
            rom.LoadFromStringArray(program);
        }

    }
}
