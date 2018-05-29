using System;
using WAES.Assignment.Algorithms;

namespace WAES.Assignment.Presentation
{
    class Program
    {
        /// <summary>
        /// Show simple main menu
        /// </summary>
        /// <param name="source1">data from first argument parameters</param>
        /// <param name="source2">data from second argument parameters</param>
        /// <returns>TRUE if user want to do another calculation
        /// FALSE if user want to quit the application</returns>
        private static bool DisplayMenu(string source1, string source2)
        {
            //show the menu
            Console.WriteLine("HAMMING DISTANCE CALCULATOR");
            Console.WriteLine("");
            Console.WriteLine("Input Sources");
            Console.WriteLine("");
            Console.WriteLine("*. Input String Manually"); // get data directly from keyboard input
            Console.WriteLine("*. Input from Full Path of Text File (Must text file - *.txt)"); // get  data from text file content
            Console.WriteLine("*. Input from Full Path of File (any file format except text file)"); // get data from converting the whole file into byte array 
            Console.WriteLine("");

            // No arguments, set the sources based on user input
            if (string.IsNullOrEmpty(source1) && string.IsNullOrEmpty(source2))
            {
                Console.Write("Input Source 1 : ");
                source1 = Console.ReadLine();
                Console.Write("Input Source 2 : ");
                source2 = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Input Source 1 : {0}", source1);
                Console.WriteLine("Input Source 1 : {0}", source2);
            }

            if (!string.IsNullOrEmpty(source1) && !string.IsNullOrEmpty(source2))
            {
                // init hamming distance, calculate based by sources
                var HammingDistanceObject = new HammingDistance();
                double distance = HammingDistanceObject.GetHammingDistance(source1, source2);

                // distance should be >= 0
                if (distance >= 0)
                {
                    Console.WriteLine("\nDistance is   : " + distance);
                    Console.WriteLine("Calc. Time is : " + HammingDistanceObject.ProcessTime.Duration());
                }
                else // else, found some error during calculate the distance
                {
                    Console.WriteLine("\nGet Hamming Distance Failed!");
                    Console.WriteLine("Source Status : " + HammingDistanceObject.SourceStatus);
                }
            }
            else
            {
                Console.WriteLine("\nInsufficient arguments");
            }

            // press key to do another calculation, otherwise quite application
            Console.Write("\nPress ENTER to retry, otherwise quit application..");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
                return false;

            return true;
        }

        /// <summary>
        /// Main method of the application
        /// </summary>
        /// <param name="args">argument(s) from application caller</param>
        static void Main(string[] args)
        {
            bool isContinue = false;
            string source1 = string.Empty, source2 = string.Empty;
            
            if (args.Length == 1)
            {
                Console.WriteLine("\nInsufficient arguments");
                return;
            }
            if (args.Length == 2) // set the sources if there 2 arguments found
            {
                source1 = args[0];
                source2 = args[1];
            }

            // loop if user want to retry the calculation   
            do
            {
                isContinue = DisplayMenu(source1, source2);

                // refresh the parameters and console
                source1 = string.Empty;
                source2 = string.Empty;
                Console.Clear();
            } 
            while (isContinue);
        }
    }
}
