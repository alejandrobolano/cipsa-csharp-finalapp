using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    class Exercise112
    {
        public void ExecuteExercise()
        {
            var contentList = new List<string>();

            Console.WriteLine("Escriba el nombre del fichero binario que se va a escribir existente o no" +
                              "\n Se va a escribir string, bytes e int (estos dos último de manera aleatoria los valores) ");
            var namePath = Console.ReadLine();
            var isCorrectProcess = Helper.IsFileExist(namePath);

            if (!isCorrectProcess)
            {
                isCorrectProcess = Helper.IsCanCreateBinaryFile(namePath, 0);
            }

            if (isCorrectProcess)
            {
                WriteInBinaryFile(namePath);
                ReadInBinaryFile(namePath, contentList);
            }

            foreach (var item in contentList)
            {
                Console.WriteLine(item, Color.DarkGreen);
            }

        }

        private static void ReadInBinaryFile(string namePath, List<string> contentList)
        {
            try
            {
                var binaryReader = new BinaryReader(File.Open(namePath, FileMode.Open));

                contentList.Add("String aleatorio leido: " + binaryReader.ReadString());
                contentList.Add("Byte aleatorio leido: " + Convert.ToString(binaryReader.ReadByte()));
                contentList.Add("Integer aleatorio leido: " + Convert.ToString(binaryReader.ReadInt32()));
                contentList.Add("Double aleatorio leido: " + Convert.ToString(binaryReader.ReadDouble(), CultureInfo.InvariantCulture));
                contentList.Add("String aleatorio leido: " + binaryReader.ReadString());
                binaryReader.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Se ha lanzado una excepción: {exception}");
            }
        }

        private static void WriteInBinaryFile(string namePath)
        {
            var random = new Random();
            byte[] exampleBytes = new byte[5];
            var dataString = "Muestra de datos: byte, int y double";
            random.NextBytes(exampleBytes);
            byte dataByte = exampleBytes[random.Next(5)];
            var dataInteger = random.Next(1000);
            var dataDouble = random.NextDouble();
            var dataOtherString = "Fin de la muestra de datos";
            try
            {
                var binaryFile = new BinaryWriter(File.Open(namePath, FileMode.Create));
                binaryFile.Write(dataString);
                binaryFile.Write(dataByte);
                binaryFile.Write(dataInteger);
                binaryFile.Write(dataDouble);
                binaryFile.Write(dataOtherString);
                binaryFile.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Se ha lanzado una excepción: {exception}");
            }
        }
    }
}