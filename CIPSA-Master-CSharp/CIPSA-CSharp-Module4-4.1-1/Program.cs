using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module4_4._1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduce the first number");
            var firstLine = Console.ReadLine();

            Console.WriteLine("Introduce the second number:");
            var secondLine = Console.ReadLine();

            try
            {
                int firstParse = int.Parse(firstLine);
                int secondParse = int.Parse(secondLine);
                int division = firstParse / secondParse;
                Console.WriteLine("Division of first and second number is: " + division);
            }
            catch (DivideByZeroException zeroException)
            {
                Console.WriteLine("We´re sorry, you can´t divide one number by zero. Try again"
                                  + "\n The exception say: " + zeroException.Message);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine("We´re sorry, wrong format. You´ve to introduce one number. Try again"
                                  + "\n The exception say: " + formatException.Message);
            }
        }
    }
}
