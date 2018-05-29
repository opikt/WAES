namespace WAES.Assignment.Algorithms
{
    /// <summary>
    /// An abstract class that only can be inherited 
    /// from HammingDistance class
    /// </summary>
    public abstract class HammingDistanceValidator : Validator
    {
        /// <summary>
        /// Source Type Enumerable data
        /// </summary>
        protected enum SourceType
        {
            String,
            TextFile,
            File,
            Unidentified
        }

        /// <summary>
        /// Get the Source Type with identfying the input strings
        /// </summary>
        /// <param name="source1">String value for the first input</param>
        /// <param name="source2">String value for the second input</param>
        /// <returns>Type of the input</returns>
        protected SourceType SourceValidator(string source1, string source2)
        {
            if (ValidateString(source1, source2))
                return SourceType.String;
            
            else if (ValidateTextFiles(source1, source2))
                return SourceType.TextFile;

            else if (ExistFiles(source1, source2))
                return SourceType.File;
            
            else
                return SourceType.Unidentified;
        }

        /// <summary>
        /// Do the validation between 2 strings, 
        /// Make sure the string is not path file
        /// </summary>
        /// <param name="string1">String value for the first input</param>
        /// <param name="string2">String value for the second input</param>
        /// <returns>True if String is clear
        /// False if contains path</returns>
        private bool ValidateString(string string1, string string2)
        {
            return (IsValidString(string1) && IsValidString(string2));
        }

        /// <summary>
        /// Do the validation between 2 filePath
        /// Make sure the filePath is exist, has same length and text files
        /// </summary>
        /// <param name="filePath1">FilePath of the first input</param>
        /// <param name="filePath2">FilePath of the second input</param>
        /// <returns>True if the file is Exist with the same length
        /// And type is text file, otherwise False</returns>
        private bool ValidateTextFiles(string filePath1, string filePath2)
        {
            if (ExistFiles(filePath1, filePath2))
                return IsTextFile(filePath1) && IsTextFile(filePath2);
            
            return false;
        }

        /// <summary>
        /// Do the validation between 2 filePath
        /// Make sure the filePath is exist
        /// </summary>
        /// <param name="filePath1">FilePath of the first input</param>
        /// <param name="filePath2">FilePath of the second input</param>
        /// <returns></returns>
        private bool ExistFiles(string filePath1, string filePath2)
        {
            return (IsFileExist(filePath1) && IsFileExist(filePath2));
        }
    }
}
