using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA._CSharp_Module6.Implementations
{
    public class Exercise2 
    {
        public void ExecuteExercise()
        {
            int cont = 0;
            bool hasNext = false;
            bool isEmptyFile = false;

            try
            {
                using (var fileReader = new StreamReader(Util.USER_FILE))
                {
                    if (fileReader.BaseStream.Length > 0)
                    {
                        while (cont < 3 && !hasNext)
                        {
                            if (!fileReader.EndOfStream)
                            {
                                Console.WriteLine(fileReader.ReadLine());
                                cont++;
                            }
                            else
                            {
                                hasNext = true;
                            }
                        }
                    }
                    else
                    {
                        isEmptyFile = true;
                    }
                    if (isEmptyFile)
                    {
                        Util.EmptyFile(1);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado", Color.DarkRed);
                Util.EmptyFile(1);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Excepcion no controlado" +
                    $"\n {exception.Message}", Color.DarkRed);
            }
        }
    }
}
