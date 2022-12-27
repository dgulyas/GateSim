using GateSim;
using GateSim.Memory;

namespace Tests.Memory
{
	[TestClass]
	public class RomTests
	{

        [TestMethod]
        public void SetChangesInternalStateCorrectly(){
            var rom = new Rom(2, 2);

            Assert.AreEqual(4, rom.Contents.Length);

            rom.Set(0, new bool[]{true, true});
            CollectionAssert.AreEqual(new bool[]{true, true}, rom.Contents[0]);

            rom.Set(3, new bool[]{true, false});
            CollectionAssert.AreEqual(new bool[]{true, false}, rom.Contents[3]);

            rom.Set(1, new bool[]{false, false});
            CollectionAssert.AreEqual(new bool[]{false, false}, rom.Contents[1]);
        }

        //public void ChangingAddressChangesOutput(){


    }
}