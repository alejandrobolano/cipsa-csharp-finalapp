using System;
using System.CodeDom;
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
    public class Exercise9
    {
        public void ExecuteExercise()
        {
            Console.WriteLine("Introduzca diferentes números para guardarlo en una lista, sólo se guardarán números y para detener," +
                              " introduzca cualquier número negativo");
            var items = new ArrayList();
            SaveNumbersInto(items);
            Console.WriteLine($"La cantidad de números introducidos es: {items.Count}");
            ShowList(items);
        }

        private void ShowList(ArrayList items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public void SaveNumbersInto(ArrayList items)
        {
            while (true)
            {
                var value = Console.ReadLine();
                if (string.IsNullOrEmpty(value)) continue;
                var isCorrect = decimal.TryParse(value, out var result);
                if (!isCorrect)
                {
                    Console.Clear();
                    Console.WriteLine($"Este valor {value} no se guarda en la lista porque no cumple con los requisitos", Color.DarkRed);
                    continue;
                }
                if (result < 0)
                {
                    break;
                }
                Console.Clear();
                items.Add(value);
                Console.WriteLine("Otro número: ");
            }
        }
    }
}
