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
    public class Exercise32 : IExercisesModule6
    {
        public void ExecuteExercise()
        {
            Console.WriteLine("Escriba el la ruta del fichero a leer:");
            var path = Console.ReadLine();
            if (!path.Equals(""))
            {
                try
                {
                    using (var streamReader = new StreamReader(path))
                    {
                        var pagination = 1;
                        while (!streamReader.EndOfStream)
                        {
                            Console.WriteLine($"Paginacion número {pagination}", Color.DarkBlue);
                            int cont = 0;
                            ReadFileRecursive(cont, streamReader);
                            pagination++;
                            if (!streamReader.EndOfStream)
                            {
                                Console.WriteLine("Continuar leyendo...", Color.DarkGreen);
                            }
                            Console.Read();
                        }
                    }
                    Console.WriteLine($"Fin del documento", Color.DarkGreen);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Fichero no encontrado", Color.DarkRed);
                    Util.EmptyFile(32);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Excepcion no controlado" +
                        $"\n {exception.Message}", Color.DarkRed);
                }
            }
            else
            {
                Program.GoToExercise(32);
            }
        }

        private static void ReadFileRecursive(int cont, StreamReader streamReader)
        {
            while (!streamReader.EndOfStream && cont < 25)
            {
                Console.WriteLine(streamReader.ReadLine());
                cont++;
            }
        }
    }
}
