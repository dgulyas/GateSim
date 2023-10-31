using GateSim;
using GateSim.Memory;

namespace Tests.Memory
{
	[TestClass]
	public class RomTests
	{

		[TestMethod]
		public void SetChangesInternalStateCorrectly()
		{
			var rom = new Rom(2, 2);

			Assert.AreEqual(4, rom.Contents.Length);

			rom.Set(0, new bool[]{true, true});
			CollectionAssert.AreEqual(new bool[]{true, true}, rom.Contents[0]);

			rom.Set(3, new bool[]{true, false});
			CollectionAssert.AreEqual(new bool[]{true, false}, rom.Contents[3]);

			rom.Set(1, new bool[]{false, false});
			CollectionAssert.AreEqual(new bool[]{false, false}, rom.Contents[1]);
		}

		[TestMethod]
		public void LoadFromArrayWorksCorrectly(){
			var rom = new Rom(3,3);
			rom.LoadFromIntArray(new int[]{7,6,5,4,3,2,1,0});

			CollectionAssert.AreEqual(7.ToBoolArray(3), rom.Contents[0]);
			CollectionAssert.AreEqual(5.ToBoolArray(3), rom.Contents[2]);
			CollectionAssert.AreEqual(3.ToBoolArray(3), rom.Contents[4]);
			CollectionAssert.AreEqual(0.ToBoolArray(3), rom.Contents[7]);
		}

		[DataTestMethod]
		[DataRow(new bool[]{false,false,false}, 7 )]
		[DataRow(new bool[]{false,true,false}, 5 )]
		[DataRow(new bool[]{false,false,true}, 3 )]
		public void ChangingAddressChangesOutput(bool[] address, int expectedOutput)
		{
			var rom = new Rom(3, 3);
			rom.LoadFromIntArray(new int[]{7,6,5,4,3,2,1,0});

			for(int i = 0; i < address.Length; i++){
				rom.Input[i] = address[i];
			}

			rom.Tick();

			CollectionAssert.AreEqual(expectedOutput.ToBoolArray(3), rom.Output);
		}

	}
}