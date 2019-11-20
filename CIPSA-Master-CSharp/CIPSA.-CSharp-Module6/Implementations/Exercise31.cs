using CIPSA._CSharp_Module6.Contracts;
using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    public class Exercise31 : IExercisesModule6
    {
        public void ExecuteExercise()
        {
            Console.WriteLine($"Escriba el texto para guardarlo en el fichero {Util.USER_PHRASES_FILE}");
            Helper.WriteFileWhileWriteInConsole(Util.USER_PHRASES_FILE);

            Console.WriteLine("Pasamos a la lectura de todo el documento", Color.DarkGreen);
            Console.WriteLine("Inicio del documento", Color.DarkBlue);
            try
            {
                using (var streamReader = new StreamReader(Util.USER_PHRASES_FILE))
                {

                    while (!streamReader.EndOfStream)
                    {
                        Console.WriteLine(streamReader.ReadLine());
                    }
                }
                Console.WriteLine("Final del documento", Color.DarkBlue);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Excepcion capturada" +
                    $"\n {exception.Message}", Color.DarkRed);
            }
        }


    }
}
