using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Common
{
    public class Helper
    {

        /// <summary>
        /// Validate if the value is int and return this int. Return -1 if the value is not a number.
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
            decimal result;
            try
            {
                if (string.IsNullOrEmpty(value) || !decimal.TryParse(value, out result))
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
        /// <param name="append"></param>
        /// <returns>return true if the file was write</returns>
        public static bool WriteFile(string pathToWrite, string content, bool append)
        {
            bool isWrite;
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
                Console.WriteLine("Argumento nulo", Color.DarkRed);
                throw;
            }
            catch (IOException ioException)
            {
                Console.WriteLine($"Error de escritura en el archivo {pathToWrite}" +
                    $"\n {ioException.Message}", Color.DarkRed);
                throw;
            }
            catch (ObjectDisposedException objectException)
            {
                Console.WriteLine($"Exepción capturada:" +
                    $"\n {objectException.Message}", Color.DarkRed);
                throw;
            }

            return isWrite;
        }

        /// <summary>
        /// Write file while is writing in console
        /// </summary>
        /// <param name="filePath"></param>
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

        /// <summary>
        /// Check if the file exist in path
        /// </summary>
        /// <param name="pathToCheck"></param>
        /// <returns></returns>
        public static bool IsFileExist(string pathToCheck)
        {
            return File.Exists(pathToCheck);
        }

        /// <summary>
        /// Create a simple file and return true or false if this file was created
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsCanCreateFile(string filePath)
        {
            try
            {
                var newFile = File.Create(filePath);
                newFile.Close();
                return true;
            }
            catch (UnauthorizedAccessException unauthorized)
            {
                Console.WriteLine("Lo siento, ni tiene permisos:" +
                    $"\n {unauthorized.Message}", Color.DarkRed);
                throw;
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path demasiado extenso", Color.DarkRed);
                throw;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Argumento nulo", Color.DarkRed);
                throw;
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine("Exepción de argumento: " +
                    $"\n {argumentException.Message}", Color.DarkRed);
                throw;
            }
            catch (NotSupportedException notSupportedException)
            {
                Console.WriteLine($"Error de escritura no soportada" +
                    $"\n {notSupportedException.Message}", Color.DarkRed);
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado", Color.DarkRed);
                throw;
            }
            catch (IOException ioException)
            {
                Console.WriteLine($"Error de escritura en el archivo {filePath}" +
                    $"\n {ioException.Message}", Color.DarkRed);
                throw;
            }
        }

        /// <summary>
        /// Check if exist path as directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Create a directory and return true or false if this directory was created
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsCanCreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (UnauthorizedAccessException unauthorized)
            {
                Console.WriteLine("Lo siento, ni tiene permisos:" +
                    $"\n {unauthorized.Message}", Color.DarkRed);
                throw;
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path demasiado extenso", Color.DarkRed);
                throw;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Argumento nulo", Color.DarkRed);
                throw;
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine("Exepción de argumento: " +
                    $"\n {argumentException.Message}", Color.DarkRed);
                throw;
            }
            catch (NotSupportedException notSupportedException)
            {
                Console.WriteLine($"Error de escritura no soportada" +
                    $"\n {notSupportedException.Message}", Color.DarkRed);
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado", Color.DarkRed);
                throw;
            }
            catch (IOException ioException)
            {
                Console.WriteLine($"Error de escritura en el archivo {path}" +
                    $"\n {ioException.Message}", Color.DarkRed);
                throw;
            }
        }

        /// <summary>
        /// Read x quantity {countBytes} of bytes of path, returns char[{countBytes}]
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <param name="countBytes"></param>
        /// <param name="goToPosition"></param>
        /// <returns>char[2], byte + char </returns>
        public static char[] ReadXCountOfBytesConvertToCharArray(string path, int goToPosition, int index, int countBytes)
        {
            var charExeArray = new char[countBytes];
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    fileStream.Seek(goToPosition, SeekOrigin.Begin);
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        binaryReader.Read(charExeArray, index, countBytes);
                    }
                }
            }
            catch (SecurityException exception)
            {
                Console.WriteLine("Acceso denegado: " +
                    $"\n {exception.Message}", Color.DarkRed);
                throw;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Argumento fuera de rango", Color.DarkRed);
                throw;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Argumento nulo", Color.DarkRed);
                throw;
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine("Exepción de argumento: " +
                    $"\n {argumentException.Message}", Color.DarkRed);
                throw;
            }
            catch (NotSupportedException notSupportedException)
            {
                Console.WriteLine($"Error de escritura no soportada" +
                    $"\n {notSupportedException.Message}", Color.DarkRed);
                throw;
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
            catch (ObjectDisposedException objectException)
            {
                Console.WriteLine($"Exepción capturada:" +
                    $"\n {objectException.Message}", Color.DarkRed);
                throw;
            }
            catch (IOException ioException)
            {
                Console.WriteLine($"Error de escritura en el archivo {path}" +
                    $"\n {ioException.Message}", Color.DarkRed);
                throw;
            }


            return charExeArray;
        }

        public static bool IsCanCreateBinaryFile(string path, int bufferLength)
        {
            try
            {
                var newFile = new BinaryWriter(new FileStream(path, FileMode.Create));
                newFile.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool IsBiggerThan(int valueInserted, int valueToCompare)
        {
            return valueInserted > valueToCompare;
        }

        public static decimal GetMaxNumber(ArrayList elements)
        {
            var bigNumber = decimal.MinValue;

            foreach (var item in elements)
            {
                bigNumber = Math.Max(Convert.ToDecimal(item), bigNumber);
            }

            return bigNumber;
        }
        public static decimal GetMinNumber(ArrayList elements)
        {
            var minNumber = decimal.MaxValue;

            foreach (var item in elements)
            {
                minNumber = Math.Min(Convert.ToDecimal(item), minNumber);
            }

            return minNumber;
        }
        public static decimal GetTotalSumNumber(ArrayList elements)
        {
            decimal sumNumber = 0;

            foreach (var item in elements)
            {
                sumNumber += Convert.ToDecimal(item);
            }

            return sumNumber;
        }


    }
}
