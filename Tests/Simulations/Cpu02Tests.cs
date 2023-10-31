using GateSim.Simulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Simulations
{
    [TestClass]
    public class Cpu02Tests
    {
        public void Test1()
        {
            var cpu = new Cpu02();
            cpu.LoadProgram(Program1());

        }

        private string[] Program1()
        {
            return new string[]
            {
                StripSpaces("0 000 00000000 000 000 00"),
            };
        }

        private string StripSpaces(string s)
        {
            var parsed = s.ToCharArray().Where(c => c != ' ').ToString();
            return parsed ?? ""; 
        }
    }
}
