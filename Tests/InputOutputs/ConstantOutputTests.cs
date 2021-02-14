using GateSim.InputOutputs;
using NUnit.Framework;

namespace Tests.InputOutputs
{
	[TestFixture]
	public class ConstantOutputTests
	{
		[Test]
		public void DeepCopyCreatesNewArray()
		{
			var output = new ConstantOutput(4, false);
			var newOutput = new [] { true, false, true, false};
			output.SetOutputDeep(newOutput);
			Assert.AreEqual(true, output.Output[0]);
			Assert.AreEqual(false, output.Output[1]);
			Assert.AreEqual(true, output.Output[2]);
			Assert.AreEqual(false, output.Output[3]);
			newOutput[0] = false;
			Assert.AreEqual(true, output.Output[0]);
		}

		[Test]
		public void DeepCopyConstructorCreatesNewArray()
		{
			var newOutput = new[] { true, false, true, false };
			var output = new ConstantOutput(newOutput);
			output.SetOutputDeep(newOutput);
			Assert.AreEqual(true, output.Output[0]);
			Assert.AreEqual(false, output.Output[1]);
			Assert.AreEqual(true, output.Output[2]);
			Assert.AreEqual(false, output.Output[3]);
			newOutput[0] = false;
			Assert.AreEqual(true, output.Output[0]);
		}


	}
}
