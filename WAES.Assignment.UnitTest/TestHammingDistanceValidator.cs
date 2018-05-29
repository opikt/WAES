using NUnit.Framework;
using WAES.Assignment.Algorithms;

namespace WAES.Assignment.UnitTest
{
    [TestFixture]
    public class TestHammingDistanceValidator : HammingDistanceValidator
    {
        /// <summary>
        /// Check if both sources is file
        /// </summary>
        /// <param name="source1">source 1 </param>
        /// <param name="source2">source 2</param>
        [TestCase(@"C:\Temp\file1.huge", @"C:\Temp\file2.huge")]
        public void TestSourceValidatorFile(string source1, string source2)
        {
            SourceType type = SourceValidator(source1, source2);
            Assert.That(type, Is.EqualTo(SourceType.File));
        }

        /// <summary>
        /// Check if both sources is text file
        /// </summary>
        /// <param name="source1">source 1 </param>
        /// <param name="source2">source 2</param>
        [TestCase(@"C:\Temp\Text1.txt", @"C:\Temp\Text2.txt")]
        public void TestSourceValidatorTextFile(string source1, string source2)
        {
            SourceType type = SourceValidator(source1, source2);
            Assert.That(type, Is.EqualTo(SourceType.TextFile));
        }

        /// <summary>
        /// Check if both sources is strings
        /// </summary>
        /// <param name="source1">source 1 </param>
        /// <param name="source2">source 2</param>
        [TestCase(@"ABCDEFGHIJKL", @"ABCDEFGHIJKL")]
        public void TestSourceValidatorTextString(string source1, string source2)
        {
            SourceType type = SourceValidator(source1, source2);
            Assert.That(type, Is.EqualTo(SourceType.String));
        }

        /// <summary>
        /// Check if both sources is unidentified
        /// </summary>
        /// <param name="source1">source 1 </param>
        /// <param name="source2">source 2</param>
        [TestCase(@"ABCDEFGHIJKL", @"C:\Temp\file1.huge")]
        public void TestSourceValidatorUnidentified(string source1, string source2)
        {
            SourceType type = SourceValidator(source1, source2);
            Assert.That(type, Is.EqualTo(SourceType.Unidentified));
        }
    }
}
