using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GateSim;
using Terminal.Simulations;

namespace Terminal
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var cpu = new Cpu01();
			cpu.ClockTick();
		}

	}
}