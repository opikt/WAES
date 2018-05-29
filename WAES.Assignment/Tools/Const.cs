namespace WAES.Assignment
{
    public static class Const
    {
        // Allocate maximum 100 MB for each chunk of byte array
        public const int MAX_BUFFER_CHUNK = 1024 * 1024 * 100;

        // Value of invalid distance
        public const double INVALID_DISTANCE = -1;

        // Sources status
        public const string STRING_IS_PATH = "String is path";
        public const string FILE_NOT_FOUND = "File not found";
        public const string UNEQUAL_LENGTH = "Unequal length found";
        public const string INSUFFICIENT_SOURCE = "Insufficient source(s)";
    }
}
