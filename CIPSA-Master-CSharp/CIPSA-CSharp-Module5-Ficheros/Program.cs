using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module5_Ficheros
{
    public class Program
    {
        static void Main(string[] args)
        {
            ReadAndWriteFile();
        }

        public static void ReadAndWriteFile()
        {
            Console.WriteLine("Escriba el path del fichero de lectura");
            var pathFrom = Console.ReadLine();


            if (!pathFrom.Equals(""))
            {
                if (Helper.ReadFile(pathFrom, out string content))
                {
                    Console.WriteLine("Escriba el path del fichero de escritura");
                    var pathTo = Console.ReadLine();
                    if (!pathTo.Equals(""))
                    {
                        if(Helper.WriteFile(pathTo, content))
                        {
                            Console.WriteLine("Se ha convertido a MAYUS correctamente el texto");
                        }
                    }
                }

            }
        }
    }
}
