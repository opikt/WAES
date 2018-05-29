using System;
using System.Diagnostics;
using System.IO;

namespace WAES.Assignment.Algorithms
{
    /// <summary>
    /// A class to handle a calculation for hamming distance
    /// based by user input. Class cannot be inherited
    /// </summary>
    public sealed class HammingDistance : HammingDistanceValidator
    {
        /// <summary>
        /// Get the TimeSpan after do calculation
        /// </summary>
        public TimeSpan ProcessTime { get; private set; }

        /// <summary>
        /// Get and Set the status of the source
        /// </summary>
        public override string SourceStatus { get; set; }

        /// <summary>
        /// Initialize the Hamming Distance Object
        /// </summary>
        public HammingDistance() { }

        /// <summary>
        /// Calculate the Hamming Distance
        /// Based by given parameters. 
        /// The parameters must meet any of below criteria
        /// <para/>1. Input String Manually
        /// <para/>2. Input Full Path of Text File (Must text file - *.txt)
        /// <para/>3. Input Full Path of File (any file format except text file)
        /// Both of the parameters must with same criteria
        /// </summary>
        /// <param name="source1">Input of the first parameter</param>
        /// <param name="source2">Input of the second parameter</param>
        /// <returns>Value of the distance</returns>
        public double GetHammingDistance(string source1, string source2)
        {
            double distance = -1;

            if (string.IsNullOrEmpty(source1) || string.IsNullOrEmpty(source2))
            {
                SourceStatus = Const.INSUFFICIENT_SOURCE;
                return distance;
            }
            
            // init stopwatch object to calculate the timespan
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // do the calculation based by type of source
            switch (SourceValidator(source1, source2))
            {
                case SourceType.String:
                    distance = GetDistanceFromString(source1, source2);
                    break;

                case SourceType.TextFile:
                    distance = GetDistanceFromTextFile(source1, source2);
                    break;

                case SourceType.File:
                    distance = GetDistanceFromFile(source1, source2);
                    break;

                default:
                    break;
            }

            // stop the stopwatch and store the value to ProcessTime property
            stopwatch.Stop();
            ProcessTime = stopwatch.Elapsed;
      
            return distance;
        }

        /// <summary>
        /// Get the Hamming Distance value between 2 byte arrays
        /// Algorithm details and example can be found in 
        /// <para/>https://en.wikipedia.org/wiki/Hamming_distance
        /// </summary>
        /// <param name="data1">Byte array for first data, 
        /// Make sure the buffer is not overflow</param>
        /// <param name="data2">Byte array for second data, 
        /// Make sure the buffer is not overflow</param>
        /// <returns>Value of the distance</returns>
        public double GetHammingDistance(byte[] data1, byte[] data2)
        {
            double distance = 0;

            if (data1.Length != data2.Length)
            {
                SourceStatus = Const.UNEQUAL_LENGTH;
                return Const.INVALID_DISTANCE;
            }
            
            // lock the calculation so it become thread safe 
            lock (new object())
            {
                for (int i = 0; i < data1.Length; i++)
                    if (data1[i] != data2[i])
                        distance++;
            }

            return distance;
        }

        /// <summary>
        /// Get the distance value between 2 strings 
        /// </summary>
        /// <param name="string1">String value for the first parameter</param>
        /// <param name="string2">String value for the second parameter</param>
        /// <returns>Value of the distance</returns>
        private double GetDistanceFromString(string string1, string string2)
        {
            return  IsStringSameLength(string1, string2) ?

                    GetHammingDistance(Converter.StringToByte(string1), Converter.StringToByte(string2))

                    : Const.INVALID_DISTANCE;
        }

        /// <summary>
        /// Get the distance value between 2 text file contents
        /// </summary>
        /// <param name="filePath1">File path for the first text file</param>
        /// <param name="filePath2">File path for the second text file</param>
        /// <returns>Value of the distance</returns>
        private double GetDistanceFromTextFile(string textFilePath1, string textFilePath2)
        {
            return  IsFileSameLength(textFilePath1, textFilePath2) ?

                    GetHammingDistance(File.ReadAllBytes(textFilePath1), File.ReadAllBytes(textFilePath2))

                    : Const.INVALID_DISTANCE;
        }

        /// <summary>
        /// Get the distance value between 2 files
        /// </summary>
        /// <param name="filePath1">File path for the first file</param>
        /// <param name="filePath1">File path for the second file</param>
        /// <returns>Value of the distance</returns>
        private double GetDistanceFromFile(string filePath1, string filePath2)
        {
            double distance = 0;

            if (!IsFileSameLength(filePath1, filePath2))
                distance = Const.INVALID_DISTANCE;
            else
            {
                long sizeSource = new FileInfo(filePath1).Length;
                long offSet = 0;
                int maxBuffer = Const.MAX_BUFFER_CHUNK;

                byte[] data1 = new byte[maxBuffer];
                byte[] data2 = new byte[maxBuffer];
                
                int byteRead = 0;

                // do the calculation until the offset less than length of data
                while (offSet < sizeSource)
                {
                    // get first chunk of byte array data
                    using (FileStream fileStream = new FileStream(filePath1, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.Seek(offSet, SeekOrigin.Begin);
                        byteRead = fileStream.Read(data1, 0, maxBuffer);
                    }

                    // get second chunk of byte array data
                    using (FileStream fileStream = new FileStream(filePath2, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.Seek(offSet, SeekOrigin.Begin);
                        byteRead = fileStream.Read(data2, 0, Const.MAX_BUFFER_CHUNK);
                    }

                    // increment the offset 
                    offSet += byteRead;

                    // calculate the distance based by chunk of datas
                    distance += GetHammingDistance(data1, data2);
                }
            }

            return distance;
        }

        /// <summary>
        /// Get the distance value between 2 small file
        /// </summary>
        /// <param name="filePath1">File path for the first file</param>
        /// <param name="filePath2">File path for the second file</param>
        /// <returns>Value of the distance</returns>
        [Obsolete(@"
        Only working for small files (less than 500 mb), 
        use GetDistanceFromFile(string filePath1, string filePath2) instead")]
        private double GetDistanceFromSmallFile(string filePath1, string filePath2)
        {
            return IsFileSameLength(filePath1, filePath2) ?

                    GetHammingDistance(Converter.FileToByte(filePath1), Converter.FileToByte(filePath2))

                    : Const.INVALID_DISTANCE;
        }
    }
}