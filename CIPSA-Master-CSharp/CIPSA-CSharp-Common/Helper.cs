using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Common
{
    public class Helper
    {

        /// <summary>
        /// Validate if the value is int
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
        /// Read file
        /// </summary>
        /// <param name="pathToRead"></param>
        /// <param name="content"></param>
        /// <returns>return true if the file was read</returns>
        public static bool ReadFile(string pathToRead, out string content)
        {
            bool isRead = false;
            var reader = string.Empty;
            try
            {
                using (var sr = new StreamReader(pathToRead))
                {
                    reader = sr.ReadToEnd();
                    reader = reader.ToUpper();
                    isRead = true;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Excepcion no controlado" +
                    $"\n {exception}");
            }
            content = reader;
            return isRead;
        }

        /// <summary>
        /// Write file
        /// </summary>
        /// <param name="pathToWrite"></param>
        /// <param name="content"></param>
        /// <returns>return true if the file was write</returns>
        public static bool WriteFile(string pathToWrite, string content)
        {
            bool isWrite = false;
            try
            {
                using (StreamWriter writer = new StreamWriter(pathToWrite))
                {
                    writer.WriteLine(content);
                    isWrite = true;
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path demasiado extenso");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Nulo");
            }
            catch (Exception)
            {
                Console.WriteLine("Excepcion no controlada");
            }

            return isWrite;
        }
    }
}
