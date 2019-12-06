using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise4
    {
        public void ExecuteExercise()
        {
            var elements = new ArrayList(10);
            var repeatedElements = new ArrayList();
            LoadData(elements);
            SearchRepeatedElements(elements, repeatedElements);

            Console.WriteLine("Lista de 10 números aleatorios", Color.DarkGreen);
            foreach (var element in elements)
            {
                Console.WriteLine(element);
            }

            if (repeatedElements.Count > 0)
            {
                Console.WriteLine("Listado de números repetidos", Color.DarkGreen);
                foreach (var element in repeatedElements)
                {
                    Console.WriteLine(element);
                }
            }

        }

        private static void SearchRepeatedElements(ArrayList elements, ArrayList repeatedElements)
        {
            var elementsCount = elements.Count;
            for (var i = 0; i < elements.Count; i++)
            {
                try
                {
                    var range = elements.GetRange(i + 1, elementsCount - 1);
                    var item = elements[i];
                    if (range.Contains(item))
                    {
                        if (!repeatedElements.Contains(item))
                        {
                            repeatedElements.Add(item);
                        }
                    }

                    elementsCount--;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Se ha lanzado una exception: {e.Message}");
                }
            }
        }

        private static void LoadData(ArrayList elements)
        {
            var random = new Random();
            int count = 0;
            while (count < 10)
            {
                elements.Add(random.Next(20));
                count++;
            }
        }
    }
}
