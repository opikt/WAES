using NUnit.Framework;
using WAES.Assignment.Algorithms;

namespace WAES.Assignment.IntegrationTest
{
    [TestFixture]
    public class TestHammingDistance
    {
        /// <summary>
        /// Test using same string and same file 
        /// </summary>
        /// <param name="source1">source 1</param>
        /// <param name="source2">source 2</param>
        [TestCase("abcdwxyz", "abcdwxyz")]
        [TestCase("01010101010101010101010", "01010101010101010101010")]
        [TestCase(@"C:\Temp\Text1.txt", @"C:\Temp\Text1.txt")]
        [TestCase(@"C:\Temp\Data.zip", @"C:\Temp\Data.zip")]
        public void TestZeroDistanceSimilarSources(string source1, string source2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(source1, source2);

            Assert.That(distance, Is.EqualTo(0));
        }

        /// <summary>
        /// Test using same byte array 
        /// </summary>
        /// <param name="data1">first byte array data</param>
        /// <param name="data2">second byte array data</param>
        [TestCase(new byte[] { }, new byte[] { })]
        [TestCase(new byte[] { 0, 1, 2, 3, 4 }, new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04 })]
        public void TestZeroDistanceSimilarSources(byte[] data1, byte[] data2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(data1, data2);

            Assert.That(distance, Is.EqualTo(0));
        }

        /// <summary>
        /// Test using same files but getting renamed 
        /// </summary>
        /// <param name="source1">source 1</param>
        /// <param name="source2">source 2</param>
        [TestCase(@"C:\Temp\Text1.txt", @"C:\Temp\Text2.txt")]
        [TestCase(@"C:\Temp\Data.zip", @"C:\Temp\Data2.zip")]
        public void TestZeroDistanceDifferentSources(string source1, string source2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(source1, source2);

            Assert.That(distance, Is.EqualTo(0));
        }

        /// <summary>
        /// Test using only 1 source
        /// Make sure the source status is correct
        /// </summary>
        /// <param name="source1"source>source 1</param>
        [TestCase(@"C:\Temp\Data.zip")]
        [TestCase(@"abcdefgh")]
        public void TestOnlyOneSource(string source1)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(source1, null);

            Assert.That(hd.SourceStatus, Is.EqualTo(Const.INSUFFICIENT_SOURCE));
            Assert.That(distance, Is.Not.EqualTo(0));
        }

        /// <summary>
        /// Test using different length
        /// Make sure the source status is correct
        /// </summary>
        /// <param name="source1">source 1</param>
        /// <param name="source2">source 2</param>
        [TestCase(@"abcdwxyz", @"abcd")]
        [TestCase(@"101010101010", @"010101011")]
        [TestCase(@"C:\Temp\Data.zip", @"C:\Temp\Data3.zip")]
        public void TestDifferentLengthSource(string source1, string source2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(source1, source2);

            Assert.That(hd.SourceStatus, Is.EqualTo(Const.UNEQUAL_LENGTH));
            Assert.That(distance, Is.Not.EqualTo(0));
        }

        /// <summary>
        /// Test using different length of byte
        /// Make sure the source status is correct
        /// </summary>
        /// <param name="data1">first byte array</param>
        /// <param name="data2">second byte array</param>
        [TestCase(new byte[] { 0, 1, 2, 3, 4, 5 }, new byte[] { 0, 1, 2, 3, 4 })]
        public void TestDifferentLengthSource(byte[] data1, byte[] data2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(data1, data2);

            Assert.That(hd.SourceStatus, Is.EqualTo(Const.UNEQUAL_LENGTH));
            Assert.That(distance, Is.Not.EqualTo(0));
        }

        /// <summary>
        /// Test using different length
        /// Make sure the source status is correct
        /// </summary>
        /// <param name="source1"></param>
        /// <param name="source2"></param>
        [TestCase(@"C:\Temp\Data.zip", @"C:\Temp\Data4.zip")]
        public void TestFileNotFound(string source1, string source2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(source1, source2);

            Assert.That(hd.SourceStatus, Is.EqualTo(Const.FILE_NOT_FOUND));
            Assert.That(distance, Is.Not.EqualTo(0));
        }

        /// <summary>
        /// Test using different dataset
        /// </summary>
        /// <param name="source1">Source 1</param>
        /// <param name="source2">Source 2</param>
        [TestCase(@"abcdefgh", @"abcdefff")]
        [TestCase(@"000000000000000000000", @"010000000000000000001")]
        public void TestDistanceByStrings(string source1, string source2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(source1, source2);

            Assert.That(distance, Is.EqualTo(2));
        }

        /// <summary>
        /// Test using different dataset of files
        /// </summary>
        /// <param name="filePath1">First File</param>
        /// <param name="filePath2">Second File</param>
        [TestCase(@"C:\Temp\Text1.txt", @"C:\Temp\Text3.txt")]
        [TestCase(@"C:\Temp\file1.small", @"C:\Temp\file2.small")]
        public void TestDistanceByFiles(string filePath1, string filePath2)
        {
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(filePath1, filePath2);

            Assert.That(distance, Is.EqualTo(2));
        }
    }
}
