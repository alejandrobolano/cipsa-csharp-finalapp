using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module9Console.Implementations
{
    internal class SchoolHelper
    {
        public static void ConsoleWriteLine(string message, Color color)
        {
            Console.WriteLine(message, color);
        }

        public static string GetStringToEvaluate(string message)
        {
            while (true)
            {
                Console.WriteLine($"{message}");
                var stringToEvaluate = Console.ReadLine();
                if (string.IsNullOrEmpty(stringToEvaluate) || stringToEvaluate.Any(char.IsDigit))
                {
                    continue;
                }

                return stringToEvaluate;
            }
        }
    }
}
