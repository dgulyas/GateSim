using GateSim;

namespace Tests
{
    [TestClass]
    public class UtilTests
    {
        //This isn't a super great test because it touches the hard drive
        [TestMethod]
        public void WriteAndReadToFileWorks(){
            var array = new int[]{1,2,3,4,5};
            var filePath = Directory.GetCurrentDirectory();
            filePath = Path.Combine(filePath, "testFile.json");

            Util.WriteArrayToFile(filePath, array);
            var deserializedArray = Util.ReadArrayFromFile(filePath);
            File.Delete(filePath);

            CollectionAssert.AreEqual(array, deserializedArray);
        }

    }
}