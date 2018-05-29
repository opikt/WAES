using System.IO;

namespace WAES.Assignment
{
    /// <summary>
    /// An abstract class to handle basic validation
    /// and to store information of source status
    /// </summary>
    public abstract class Validator
    {
        /// <summary>
        /// Virtual property to store Source Status message
        /// </summary>
        public virtual string SourceStatus { get; set; }

        /// <summary>
        /// Check if both byte arrays has same length
        /// </summary>
        /// <param name="data1">Input of first byte array</param>
        /// <param name="data2">Input of second byte array</param>
        /// <returns>True if has same length, otherwise False</returns>
        protected bool IsBinarySameLength(byte[] data1, byte[] data2)
        {
            bool ret = data1.Length == data2.Length ? true : false;

            if (!ret)
                SourceStatus = Const.UNEQUAL_LENGTH;

            return ret;
        }

        /// <summary>
        /// Check if String is path or directories
        /// </summary>
        /// <param name="stringData">Input of checked string</param>
        /// <returns>True if input doesn't contain path or directories
        /// , otherwise False</returns>
        protected bool IsValidString(string stringData)
        {
            bool isPath = Path.IsPathRooted(stringData);

            if (isPath)
            {
                SourceStatus = Const.STRING_IS_PATH;
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Check if input is text file
        /// </summary>
        /// <param name="source">Input of checked source</param>
        /// <returns>True if source is text file, otherwise False</returns>
        protected bool IsTextFile(string source)
        {
            return Path.GetExtension(source).ToUpper() == ".TXT";
        }

        /// <summary>
        /// Check if full file Path is exist
        /// </summary>
        /// <param name="filePath">Full file path with the file name</param>
        /// <returns>True if file exist, otherwise False</returns>
        protected bool IsFileExist(string filePath)
        {
            bool ret = File.Exists(filePath);

            if (!ret)
                SourceStatus = Const.FILE_NOT_FOUND;

            return ret;
        }

        /// <summary>
        /// Get the length of the file
        /// </summary>
        /// <param name="filePath">Full file path with the file name</param>
        /// <returns></returns>
        private long GetFileLength(string filePath)
        {
            long size = new FileInfo(filePath).Length;
            return size;
        }

        /// <summary>
        /// Check if both strings has same length 
        /// </summary>
        /// <param name="string1">String of first parameter</param>
        /// <param name="string2">String of second parameter</param>
        /// <returns>True if both strings has same length, otherwise False</returns>
        protected bool IsStringSameLength(string string1, string string2)
        {
            bool ret = string1.Length == string2.Length;

            if (!ret)
                SourceStatus = Const.UNEQUAL_LENGTH;

            return ret;

        }

        /// <summary>
        /// Check if both files has same length
        /// </summary>
        /// <param name="filePath1">Full path of first file</param>
        /// <param name="filePath2">Full path of second file</param>
        /// <returns>True if both file has same length, otherwise False</returns>
        protected bool IsFileSameLength(string filePath1, string filePath2)
        {
            bool ret = GetFileLength(filePath1) == GetFileLength(filePath2);

            if (!ret)
                SourceStatus = Const.UNEQUAL_LENGTH;

            return ret;
        }
    }
}
