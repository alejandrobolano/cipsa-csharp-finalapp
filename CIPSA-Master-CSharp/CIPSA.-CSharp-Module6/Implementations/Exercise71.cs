using CIPSA_CSharp_Common;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    public class Exercise71
    {
        public void ExecuteExercise()
        {
            Console.WriteLine("Escriba el path del fichero de lectura");
            var pathFrom = Console.ReadLine();

            if (!pathFrom.Equals(""))
            {
                if (Helper.ReadFile(pathFrom, out string content))
                {
                    WriteInPath(content.ToUpper());
                }
                else
                {
                    Console.WriteLine("Lo sentimos, pero no se ha podido leer el fichero", Color.DarkRed);
                }
            }
            else
            {
                Console.WriteLine("Escriba nuevamente la ruta del fichero de lectura");
                Program.GoToExercise(71);
            }
        }

        private static void WriteInPath(string content)
        {
            Console.WriteLine("Escriba el path del fichero de escritura");
            var pathTo = Console.ReadLine();
            if (!pathTo.Equals(""))
            {
                try
                {
                    Helper.WriteFile(pathTo, content, true);
                    Console.WriteLine($"Se ha convertido a MAYUS correctamente el texto hacia el fichero {pathTo}", Color.DarkGreen);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Lo sentimos, pero no se pudo convertir el texto al fichero {pathTo}", Color.DarkRed);
                }

            }
            else
            {
                Console.WriteLine("Escriba nuevamente la ruta del fichero de escritura");
                WriteInPath(content);
            }
        }
    }
}
