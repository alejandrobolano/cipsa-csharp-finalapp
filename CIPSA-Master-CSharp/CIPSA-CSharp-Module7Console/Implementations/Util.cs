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
    public class Util
    {
        public static void SaveQuantityNumbersIn(ArrayList elementList, int quantity)
        {
            var realCount = 0;
            var unrealCount = 0;
            var showNotification = false;
            while (realCount < quantity)
            {
                var element = Helper.GetDecimal(Console.ReadLine());
                if (element != -1)
                {
                    elementList.Add(element);
                    realCount++;
                }

                if (unrealCount >= quantity && !showNotification)
                {
                    Console.WriteLine($"El ciclo se repite hasta que haya {quantity} números escritos", Color.DarkRed);
                    showNotification = true;
                }

                unrealCount++;
            }
        }

    }
}
