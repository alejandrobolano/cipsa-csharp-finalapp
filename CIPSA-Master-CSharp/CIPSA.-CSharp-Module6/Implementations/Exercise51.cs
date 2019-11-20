using CIPSA._CSharp_Module6.Contracts;
using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    public class Exercise51 : IExercisesModule6
    {
        public void ExecuteExercise()
        {
            Console.WriteLine($"Escriba un par de numeros para realizar su cálculo y almacenarlos en {Util.USER_ADDITION_TEMP}");
            try
            {
                var firstValue = Util.GetFirstValue();
                var secondValue = Util.GetSecondValue();
                var result = firstValue + secondValue;
                var resultToText = $"{firstValue} + {secondValue} = {decimal.Round(result, 2)}";

                if (Helper.IsFileExist(Util.USER_ADDITION_TEMP))
                {
                    Helper.WriteFile(Util.USER_ADDITION_TEMP, resultToText, true);
                    Console.WriteLine($"El resultado de sumar {resultToText}", Color.DarkGreen);
                    Console.WriteLine("Se ha guardado correctamente los datos en el fichero", Color.DarkGreen);
                }
                else
                {
                    Console.WriteLine("Lo siento, pero el fichero no existe", Color.DarkRed);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Excepcion capturada" +
                    $"\n {exception.Message}", Color.DarkRed);
            }


        }
        
    }
}
