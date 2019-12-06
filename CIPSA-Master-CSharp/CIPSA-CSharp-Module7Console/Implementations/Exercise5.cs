using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Common;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise5
    {
        public void ExecuteExercise()
        {
            var elements = new ArrayList();

            Console.WriteLine("Diga la cantidad de números a introducir");
            var quantity = Helper.GetNumeric(Console.ReadLine());
            if (quantity != -1)
            {
                Console.WriteLine($"Introduzca {quantity} número(s): ");
                Util.SaveQuantityNumbersIn(elements,quantity);
            }

            var totalSum = Helper.GetTotalSumNumber(elements);
            var maxNumber = Helper.GetMaxNumber(elements);
            var minNumber = Helper.GetMinNumber(elements);

            Console.WriteLine($"La suma total de los valores es: {totalSum}", Color.DarkGreen);
            Console.WriteLine($"El número mayor es: {maxNumber}", Color.DarkGreen);
            Console.WriteLine($"El número menor es: {minNumber}", Color.DarkGreen);
        }
        
    }
}
