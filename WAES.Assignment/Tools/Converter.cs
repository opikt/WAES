using System.Text;
using System.IO;
using System;

namespace WAES.Assignment
{
    /// <summary>
    /// A helper class to convert certain input into another object
    /// </summary>
    internal static class Converter
    {
        /// <summary>
        /// Convert string data-tye into a sequence of byte 
        /// </summary>
        /// <param name="stringData">String input</param>
        /// <returns>Byte array value of the string</returns>
        public static byte[] StringToByte(string stringData)
        {
            return Encoding.ASCII.GetBytes(stringData);
        }

        /// <summary>
        /// Convert Object from FilePath into a sequence of byte
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Byte array value of the file</returns>
        public static byte[] FileToByte(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    byte[] buffer = new byte[fs.Length];
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);

                    return buffer;
                }
                catch (OutOfMemoryException ex)
                {
                    OutOfMemoryException memoryEx = new OutOfMemoryException("File is too huge to process", ex);
                    throw memoryEx;
                }
            }
        }
    }
}
