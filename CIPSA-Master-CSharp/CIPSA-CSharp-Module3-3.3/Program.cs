using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module3_3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A continuación sumará dos números introducidos:");
            Console.WriteLine("Introduzca el primer número");
            var firstNumber = Console.ReadLine();
            Console.WriteLine("Introduzca el segundo número");
            var secondNumber = Console.ReadLine();
            Console.WriteLine("El resultado es: " + (Convert.ToInt32(firstNumber) + Convert.ToInt32(secondNumber)));
        }
    }
}
