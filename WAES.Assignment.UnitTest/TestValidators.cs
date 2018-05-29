using NUnit.Framework;
using WAES.Assignment;

namespace WAES.Assignment.UnitTest
{
    [TestFixture]
    public class TestValidators : Validator
    {
        /// <summary>
        /// Check if both sources is byte array with same length
        /// </summary>
        /// <param name="data1">first byte array data</param>
        /// <param name="data2">second byte array data</param>
        [TestCase(new byte[] { }, new byte[] { })]
        [TestCase(new byte[] { 0, 1, 2, 3, 4 }, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04 })]
        public void TestBinarySameLength(byte[] data1, byte[] data2)
        {
            Assert.That(IsBinarySameLength(data1, data2));
        }

        /// <summary>
        /// Check if both sources is byte array with different length
        /// Make sure the source status is correct
        /// </summary>
        /// <param name="data1">first byte array data</param>
        /// <param name="data2">second byte array data</param>
        [TestCase(new byte[] { 0, 1, 2, 3, 4 }, new byte[] { 0x00, 0x01, 0x02, 0x03 })]
        public void TestBinaryNotSameLength(byte[] data1, byte[] data2)
        {
            Assert.That(!IsBinarySameLength(data1, data2));
            Assert.That(SourceStatus, Is.EqualTo(Const.UNEQUAL_LENGTH));
        }

        /// <summary>
        /// Check if source is string, not path.
        /// </summary>
        /// <param name="stringData">source</param>
        [TestCase("01010101010101")]
        [TestCase("ABCDEFGHIJ")]
        public void TestValidString(string stringData)
        {
            Assert.That(IsValidString(stringData));
        }

        /// <summary>
        /// Check if source is invalid string and text file
        /// Make sure the source status is correct
        /// </summary>
        /// <param name="stringData">source</param>
        [TestCase(@"C:\Temp\Text1.txt")]
        [TestCase(@"C:\Temp\Text4.TXT")]
        public void TestInvalidStringAndPath(string stringData)
        {
            Assert.That(!IsValidString(stringData));
            Assert.That(IsTextFile(stringData));
            Assert.That(SourceStatus, Is.EqualTo(Const.STRING_IS_PATH));
        }

        /// <summary>
        /// Check if file is exist
        /// </summary>
        /// <param name="path">source 1 </param>
        [TestCase(@"C:\Temp\Text1.txt")]
        public void TestCheckFileExist(string path)
        {
            Assert.That(IsFileExist(path));
        }

        /// <summary>
        /// Check if file is does not exist
        /// Make sure Source Status is correct
        /// </summary>
        /// <param name="path">source 1 </param>
        [TestCase(@"C:\Temp\Text5.txt")]
        public void TestCheckFileDoesntExist(string path)
        {
            Assert.That(!IsFileExist(path));
            Assert.That(SourceStatus, Is.EqualTo(Const.FILE_NOT_FOUND));
        }

        /// <summary>
        /// Check if strings has same length
        /// </summary>
        /// <param name="stringData1">String 1</param>
        /// <param name="stringData2">String 1</param>
        [TestCase(@"QWERTYUIOP", "POIUYTREWQ")]
        public void TestStringSameLength(string stringData1, string stringData2)
        {
            Assert.That(IsStringSameLength(stringData1, stringData2));
        }

        /// <summary>
        /// Check if strings has different length
        /// Make sure Source Status is correct
        /// </summary>
        /// <param name="stringData1">String 1</param>
        /// <param name="stringData2">String 1</param>
        [TestCase(@"QWERTYUIOP", "POIUYTREWQ!")]
        public void TestStringDifferentLength(string stringData1, string stringData2)
        {
            Assert.That(!IsStringSameLength(stringData1, stringData2));
            Assert.That(SourceStatus, Is.EqualTo(Const.UNEQUAL_LENGTH));
        }

        /// <summary>
        /// Check if files has same length
        /// </summary>
        /// <param name="file1">String 1</param>
        /// <param name="file2">String 1</param>
        [TestCase(@"C:\Temp\Data.zip", @"C:\Temp\Data2.zip")]
        public void TestFilesSameLength(string file1, string stringData2)
        {
            Assert.That(IsFileSameLength(file1, stringData2));
        }

        /// <summary>
        /// Check if strings has different length
        /// Make sure Source Status is correct
        /// </summary>
        /// <param name="file1">String 1</param>
        /// <param name="file2">String 1</param>
        [TestCase(@"C:\Temp\Data.zip", @"C:\Temp\Data3.zip")]
        public void TestFilesDifferentLength(string file1, string file2)
        {
            Assert.That(!IsStringSameLength(file1, file2));
            Assert.That(SourceStatus, Is.EqualTo(Const.UNEQUAL_LENGTH));
        }
    }
}
