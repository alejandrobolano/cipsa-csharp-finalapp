using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Common
{
    public class Helper
    {

        /// <summary>
        /// Validate if the value is int and return this int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>-1 if the value is not a number</returns>
        public static int GetNumeric(string value)
        {
            bool isString = false;
            int result;
            try
            {
                if (value == "" || value == null)
                {
                    return -1;
                }
                foreach (char c in value)
                {
                    if (c < '0' || c > '9')
                    {
                        isString = true;
                    }
                }
                result = isString ? -1 : Convert.ToInt32(value);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Excepción por número demasiado grande", Color.DarkRed);
                return -1;
            }
            return result;
        }

        /// <summary>
        /// Validate if the value is decimal and return this decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string value)
        {
            var result = 0.0m;
            try
            {
                if (value == "" || value == null || !decimal.TryParse(value, out result))
                {
                    return -1;
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("Excepción por número demasiado grande", Color.DarkRed);
                return -1;
            }
            return result;
        }

        /// <summary>
        /// Read file
        /// </summary>
        /// <param name="pathToRead"></param>
        /// <param name="content"></param>
        /// <returns>return true if the file was read</returns>
        public static bool ReadFile(string pathToRead, out string content)
        {
            bool isRead = false;
            content = string.Empty;
            try
            {
                using (var sr = new StreamReader(pathToRead))
                {
                    content = sr.ReadToEnd();
                    isRead = true;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado", Color.DarkRed);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Excepcion no controlada" +
                    $"\n {exception}", Color.DarkRed);
            }
            return isRead;
        }

        /// <summary>
        /// Write file
        /// </summary>
        /// <param name="pathToWrite"></param>
        /// <param name="content"></param>
        /// <returns>return true if the file was write</returns>
        public static bool WriteFile(string pathToWrite, string content, bool append)
        {
            bool isWrite = false;
            try
            {
                using (var writer = new StreamWriter(pathToWrite, append))
                {
                    writer.WriteLine(content);
                    isWrite = true;
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado", Color.DarkRed);
                throw;
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path demasiado extenso", Color.DarkRed);
                throw;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Nulo");
                throw;
            }
            catch (IOException ioException)
            {
                Console.WriteLine($"Error de escritura en el archivo {pathToWrite}" +
                    $"\n {ioException.Message}", Color.DarkRed);
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Excepcion no controlada:" +
                    $"\n {exception.Message}", Color.DarkRed);
                throw;
            }

            return isWrite;
        }

        public static void WriteFileWhileWriteInConsole(string filePath)
        {
            try
            {
                bool isEmptyNext = false;

                while (!isEmptyNext)
                {
                    try
                    {
                        var line = Console.ReadLine();
                        if (line.Equals(""))
                        {
                            isEmptyNext = true;
                        }
                        else
                        {
                            WriteFile(filePath, line, true);
                        }

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Excepcion capturada: " +
                            $"\n {exception.Message}", Color.DarkRed);
                    }

                }
            }
            catch (OutOfMemoryException outOfMemory)
            {
                Console.WriteLine("Se ha producido un error por desbordamiento de la memoria" +
                    $"\n {outOfMemory.Message}", Color.DarkRed);
            }
            catch (ArgumentOutOfRangeException argumentOutOfRange)
            {
                Console.WriteLine("Se ha producido un error por argumento fuera de rango" +
                    $"\n {argumentOutOfRange.Message}", Color.DarkRed);
            }
        }

        public static bool IsFileExist(string pathToCheck)
        {
            return File.Exists(pathToCheck);
        }

    }
}
