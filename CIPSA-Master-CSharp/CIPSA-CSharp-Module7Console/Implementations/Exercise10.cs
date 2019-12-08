using System;
using System.Collections;
using System.Drawing;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise10
    {
        private readonly int QUANTITY = 5;
        public void ExecuteExercise()
        {
            var items = new ArrayList();
            LoadData(items,10,100);
            Console.WriteLine("Lista original: ");
            ShowList(items);
            Console.WriteLine($"Introduzca {QUANTITY} números más para adicionarlo a la lista original");
            var newItemsByUser = new ArrayList();
            SaveNumbersInto(newItemsByUser);
            items.InsertRange(items.Count,newItemsByUser);
            items.Sort(new CustomComparer());
            Console.WriteLine("Lista ordenada: ");
            ShowList(items);
        }

        private void SaveNumbersInto(ArrayList newItemsByUser)
        {
            var count = 0;
            while (count < QUANTITY)
            {
                var value = Console.ReadLine();
                if (string.IsNullOrEmpty(value)) continue;
                var isCorrect = decimal.TryParse(value, out var result);
                if (!isCorrect)
                {
                    Console.WriteLine($"Este valor {value} no se guarda en la lista porque no cumple con los requisitos", Color.DarkRed);
                    continue;
                }
                count++;
                newItemsByUser.Add(value);
                Console.WriteLine("Otro número: ");
            }
        }

        private void ShowList(ArrayList items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private void LoadData(ArrayList items, int interval, int max)
        {
            var valueIntoArray = 0;
            while (valueIntoArray < max)
            {
                valueIntoArray += interval;
                items.Add(valueIntoArray);
            }
        }
    }
}
