using CIPSA_CSharp_Common;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    class Exercise111
    {
        public void ExecuteExercise()
        {
            Console.WriteLine("Escriba el nombre del fichero para consultar si el segundo byte es Z");
            var pathValue = Console.ReadLine();
            if (Helper.IsFileExist(pathValue))
            {
                if (CheckSecondByte(pathValue, "Z"))
                {
                    Console.WriteLine("El segundo byte del fichero correctamente es una Z", Color.DarkGreen);
                }
                else
                {
                    Console.WriteLine("El segundo byte no es una Z", Color.DarkGreen);
                }
            }
            else
            {
                Console.WriteLine("El fichero o la ruta no existe", Color.DarkRed);
            }

        }

        private static bool CheckSecondByte(string path, string valueToCompare)
        {            
            var foo = Helper.ReadXCountOfBytesConvertToCharArray(path,1,0,1);
            return foo[0].ToString().Equals(valueToCompare);
        }


    }
}