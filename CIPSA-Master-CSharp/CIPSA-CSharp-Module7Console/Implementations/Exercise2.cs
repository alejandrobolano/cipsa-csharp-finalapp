using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CIPSA_CSharp_Common;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise2
    {
        public void ExecuteExercise()
        {
            var elements = new List<int>();
            var elementsGreaterThan = new List<int>();
            var valueToCompare = 22;
            Console.WriteLine("Ingrese 10 números al azar para mostrarlo de manera inversa");
            SaveTenNumbersIn(elements);
            elements.Reverse();
            Console.WriteLine("A continuación se muestra la lista de número introducidos de manera inversa", Color.DarkGreen);

            foreach (var element in elements)
            {
                Console.WriteLine(element);
                if (Helper.IsBiggerThan(element, valueToCompare))
                {
                    elementsGreaterThan.Add(element);
                }
            }

            var countBigger = elementsGreaterThan.Count;
            elementsGreaterThan.Reverse();
            if (countBigger == 0)
            {
                Console.WriteLine($"No existe números mayores introducidos que {valueToCompare}", Color.DarkBlue);
            }
            else if (countBigger == 1)
            {
                Console.WriteLine($"En los números introducidos, sólo existe {countBigger} número mayor que {valueToCompare}" +
                                  $" y ese número es: {elementsGreaterThan.FirstOrDefault()}", Color.DarkBlue);
            }
            else
            {
                Console.WriteLine($"En los números introducidos, existe {countBigger} números mayores que {valueToCompare}", Color.DarkBlue);
                foreach (var element in elementsGreaterThan)
                {
                    Console.WriteLine(element, Color.DarkGreen);
                }
            }
        }

        private static void SaveTenNumbersIn(List<int> elementList)
        {
            var realCount = 0;
            var unrealCount = 0;
            bool showNotification = false;
            while (realCount < 10)
            {
                var element = Helper.GetNumeric(Console.ReadLine());
                if (element != -1)
                {
                    elementList.Add(element);
                    realCount++;
                }

                if (unrealCount >= 10 && !showNotification)
                {
                    Console.WriteLine("El ciclo se repite hasta que haya 10 números enteros escritos", Color.DarkRed);
                    showNotification = true;
                }

                unrealCount++;
            }
        }
    }
}
