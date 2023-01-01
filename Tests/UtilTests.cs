using GateSim;

namespace Tests
{
    [TestClass]
    public class UtilTests
    {
        const int BitWidth = 8;

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

        [TestMethod]
        public void InvertArrayWorks(){
            var a = new bool[]{true};
            a.Invert();
            CollectionAssert.AreEqual(a, new bool[]{false});

            a = new bool[]{false, true, false};
            a.Invert();
            CollectionAssert.AreEqual(a, new bool[]{true, false, true});
        }

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(0)]
        [DataRow(254)]
        public void AddOneWorks(int input){
            var a = input.ToBoolArray(BitWidth);
            a.AddOne();
            CollectionAssert.AreEqual((input+1).ToBoolArray(BitWidth), a);
        }

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(1)]
        [DataRow(255)]
        public void MinusOneWorks(int input){
            var a = input.ToBoolArray(BitWidth);
            a.MinusOne();
            CollectionAssert.AreEqual((input-1).ToBoolArray(BitWidth), a);
        }

        [TestMethod]
        public void PlusAndMinusOneCancelOut()
        {
            var a = new bool[]{true,false,true,false,true,false,true,false};
            var originalValue = a.ToInt();

            for(var i = 0; i < 25; i++){
                a.AddOne();
            }

            for(var i = 0; i < 25; i++){
                a.MinusOne();
            }

            Assert.AreEqual(originalValue, a.ToInt());
        }

    }
}