using System;
using System.Collections;
using CIPSA_CSharp_Common;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise6
    {
        public void ExecuteExercise()
        {
            var firstSerialElements = new ArrayList();
            var secondSerialElements = new ArrayList();

            ExecuteProcess(firstSerialElements, secondSerialElements);
        }

        private static void ExecuteProcess(ArrayList firstSerialElements, ArrayList secondSerialElements)
        {
            Console.WriteLine("Diga la cantidad de números a introducir, para luego introducir dos series de números");
            var quantity = Helper.GetNumeric(Console.ReadLine());
            if (quantity != -1)
            {
                Console.WriteLine($"Introduzca {quantity} número(s) de la primera serie: ");
                Util.SaveQuantityNumbersIn(firstSerialElements, quantity);

                Console.WriteLine($"Introduzca {quantity} número(s) de la segunda serie: ");
                Util.SaveQuantityNumbersIn(secondSerialElements, quantity);
                for (var i = 0; i < firstSerialElements.Count; i++)
                {
                    var firstSerialItem = firstSerialElements[i];
                    var secondSerialItem = secondSerialElements[i];
                    if (Convert.ToDecimal(firstSerialItem) > Convert.ToDecimal(secondSerialItem))
                    {
                        Console.WriteLine(
                            $"El número {firstSerialItem} de la posición {i + 1} de la primera lista es mayor que " +
                            $"{secondSerialItem} de la misma posición");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"El número {firstSerialItem} de la posición {i + 1} de la primera lista es menor que " +
                            $"{secondSerialItem} de la misma posición");
                    }
                }
            }
            else
            {
                ExecuteProcess(firstSerialElements, secondSerialElements);
            }
        }
    }
}
