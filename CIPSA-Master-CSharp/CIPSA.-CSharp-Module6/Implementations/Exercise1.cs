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
    public class Exercise1 : IExercisesModule6
    {
        public void ExecuteExercise()
        {
            Console.WriteLine($"Escriba el texto para guardarlo en el fichero {Util.USER_FILE}");
            bool hasEndStringContainer = false;

            while (!hasEndStringContainer)
            {
                try
                {
                    var line = Console.ReadLine();
                    if (line.ToLower().EndsWith("fin"))
                    {
                        line = line.Replace(line.Substring(line.Length - 3), "");
                        hasEndStringContainer = true;
                    }
                    Helper.WriteFile(Util.USER_FILE, line, true);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Excepcion capturada" +
                        $"\n {exception.Message}", Color.DarkRed);
                }
            }

            Console.WriteLine("Ha realizado la escritura con exito", Color.DarkGreen);            
        }
    }
}
