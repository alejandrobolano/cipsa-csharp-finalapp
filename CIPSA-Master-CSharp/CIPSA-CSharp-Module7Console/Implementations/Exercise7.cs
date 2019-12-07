using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Common;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise7
    {

        public void ExecuteExercise()
        {
            Console.WriteLine("Diga la cantidad de elementos pares que quiere que tenga el array, el número debe ser par");
            var size = GetSizeOfArray();
            var elements = new ArrayList(size);
            var firstCopyElements = new ArrayList(size / 2);
            var secondCopyElements = new ArrayList(size / 2);
            LoadData(elements, size);
            try
            {
                SplitArray(elements, firstCopyElements, secondCopyElements, size / 2);
                Console.WriteLine("Lista original", Color.DarkGreen);
                ShowArray(elements);
                Console.WriteLine("Muestra de la primera copia de la lista", Color.DarkGreen);
                ShowArray(firstCopyElements);
                Console.WriteLine("Muestra de la segunda copia de la lista", Color.DarkGreen);
                ShowArray(secondCopyElements);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Se ha producido un error a causa de: {e.Message}");
            }
            
        }

        private void ShowArray(ArrayList elements)
        {
            foreach (var item in elements)
            {
                Console.WriteLine(item);
            }
        }

        private void SplitArray(ArrayList elements, ArrayList firstCopyElements, ArrayList secondCopyElements, int partialSize)
        {
            firstCopyElements.AddRange(elements.GetRange(0, partialSize));
            secondCopyElements.AddRange(elements.GetRange(partialSize, partialSize));
        }

        private static void LoadData(ArrayList elements, int size)
        {
            var random = new Random();
            int count = 0;
            while (count < size)
            {
                elements.Add(random.Next(20));
                count++;
            }
        }

        private int GetSizeOfArray()
        {
            Console.WriteLine("Recuerde escribir un número y que sea par");
            var number = Helper.GetNumeric(Console.ReadLine());
            if (number != -1 && Helper.IsPairNumber(number))
            {
                return number;
            }
            else
            {
                return GetSizeOfArray();
            }
        }

    }
}
