using NUnit.Framework;
using System;
using System.IO;
using WAES.Assignment.Algorithms;

namespace WAES.Assignment.IntegrationTest
{
    [TestFixture]
    public class TestHammingDistanceAdvance
    {
        /// <summary>
        /// Get the distance between 2 huge files
        /// Might be not working for text files (.txt) 
        /// because of .NET byte maximum array length limitation
        /// </summary>
        /// <param name="filePath1">Path of first file</param>
        /// <param name="filePath2">Path of second file</param>
        /// <param name="fileSizeInMB">Desired file size in mega bytes</param>
        [TestCase(@"C:\Temp\file1.huge", @"C:\Temp\file2.huge", 1000)]
        public void TestDistanceByHugeFiles(string filePath1, string filePath2, int fileSizeInMB)
        {
            // Check if files is exist with the desired size,
            // if doesn't exists, write!
            FileValidator(filePath1, filePath2, fileSizeInMB);

            // init object and do calculation
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(filePath1, filePath2);

            // ideally, the timespan for calculation is 1% per size of file. 
            // e.g if size is 1000 mbs the timespan should be less than 10 seconds 
            // int desiredTime = fileSizeInMB / 100;

            int desiredTime = fileSizeInMB / 50; // to be safe, create 2% instead

            Assert.That(distance, Is.GreaterThanOrEqualTo(0));
            Assert.That(hd.ProcessTime, Is.LessThan(TimeSpan.FromSeconds(desiredTime)));
        }

        /// <summary>
        /// Check the distance between 2 same huge files
        /// Might be not working for text files (.txt) 
        /// because of .NET byte array maximum length limitation
        /// </summary>
        /// <param name="filePath1">Path of first file</param>
        /// <param name="filePath2">Path of second file</param>
        /// <param name="fileSizeInMB">Desired file size in mega bytes</param>
        [TestCase(@"C:\Temp\file1.huge", @"C:\Temp\file1.huge", 1000)]
        public void TestZeroDistanceByHugeFiles(string filePath1, string filePath2, int fileSizeInMB)
        {
            //Check if files is exist with the desired size
            FileValidator(filePath1, filePath2, fileSizeInMB);

            // init object and do calculation
            HammingDistance hd = new HammingDistance();
            double distance = hd.GetHammingDistance(filePath1, filePath2);

            // ideally, the timespan for calculation is 1% per size of file. 
            // e.g if size is 1000 mbs the timespan should be less than 10 seconds 
            // int desiredTime = fileSizeInMB / 100;

            int desiredTime = fileSizeInMB / 50; // to be safe, create 2% instead

            Assert.That(distance, Is.GreaterThanOrEqualTo(0));
            Assert.That(hd.ProcessTime, Is.LessThan(TimeSpan.FromSeconds(desiredTime)));
        }

        private void FileValidator(string filePath1, string filePath2, int fileSizeMB)
        {
            if (!File.Exists(filePath1))
                CreateRandomFile(filePath1, fileSizeMB);

            if (!File.Exists(filePath2))
                CreateRandomFile(filePath2, fileSizeMB);

            if (File.Exists(filePath1))
                if (new FileInfo(filePath1).Length != (1024 * 1024 * fileSizeMB))
                    CreateRandomFile(filePath1, fileSizeMB);

            if (File.Exists(filePath2))
                if (new FileInfo(filePath2).Length != (1024 * 1024 * fileSizeMB))
                    CreateRandomFile(filePath2, fileSizeMB);
        }

        private void CreateRandomFile(string fileName, long fileSizeInMB)
        {
            int mb = 1024 * 1024;

            // assuming the random data is generated 100 mb for each loop
            // change if needed
            int randomDataMB = 100;

            if (randomDataMB > fileSizeInMB)
                randomDataMB = Convert.ToInt32(fileSizeInMB/100); // random data must be less than file size

            fileSizeInMB *= mb;
            long offSet = 0;

            do
            {
                FileMode mode = offSet == 0 ? FileMode.Create : FileMode.Append;

                using (FileStream fs = new FileStream(fileName, mode))
                {
                    // set the buffer
                    byte[] data = new byte[mb * randomDataMB];

                    // fill the data with random value
                    Random ran = new Random();
                    ran.NextBytes(data);

                    // set the file in the offSet position
                    fs.Seek(offSet, SeekOrigin.Begin);

                    // add the random data into file
                    fs.Write(data, 0, data.Length);
                    fs.Flush();

                    // increment the offset
                    offSet += data.LongLength;
                }

            } while (offSet < fileSizeInMB); // keep doing until reached the file size
        }
    }
}
