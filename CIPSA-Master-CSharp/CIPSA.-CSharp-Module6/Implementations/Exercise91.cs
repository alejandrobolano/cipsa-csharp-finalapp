using CIPSA._CSharp_Module6.Contracts;
using CIPSA_CSharp_Common;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    class Exercise91 : IExercisesModule6
    {
        public void ExecuteExercise()
        {
            Console.WriteLine("Escriba el nombre del fichero para consultar si es un ejecutable (.exe)");
            var pathValue = Console.ReadLine();
            if (Helper.IsFileExist(pathValue))
            {
                if (IsExeFile(pathValue))
                {
                    Console.WriteLine("El fichero es correctamente un ejecutable (.exe)", Color.DarkGreen);
                }
                else
                {
                    Console.WriteLine("El fichero no es un ejecutable (.exe)", Color.DarkGreen);
                }
            }
            else
            {
                Console.WriteLine("El fichero o la ruta no existe", Color.DarkRed);
            }

        }

        private static bool IsExeFile(string path)
        {
            string[] variables = new string[2] { "M", "Z" };
            var foo = Helper.ReadXCountOfBytesConvertToCharArray(path,2);
            return foo[0].ToString().Equals(variables[0]) && foo[1].ToString().Equals(variables[1]);
        }


    }
}