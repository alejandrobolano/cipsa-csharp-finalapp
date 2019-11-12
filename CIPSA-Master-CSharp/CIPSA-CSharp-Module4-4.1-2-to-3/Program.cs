using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module4_4._1_2_to_3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Introduce the year: ");
                var yearSelected = Convert.ToInt32(Console.ReadLine());
                var isLeapYear = (yearSelected % 4 == 0) && (yearSelected % 100 != 0 || yearSelected % 400 != 0);
                var daysOfYear = 365;
                int[] dayOfMonth = new[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                if (isLeapYear)
                {
                    Console.WriteLine("Year selected is leap-year :-) ");
                    daysOfYear = 366;
                    dayOfMonth[1] = 29;
                }

                Console.WriteLine("Introduce one number between 1 and {0}: ", daysOfYear);
                var numberSelected = Convert.ToInt32(Console.ReadLine());
                ValidateNumber(numberSelected, daysOfYear);

                var monthName = (Month)GetMonthNumber(ref numberSelected, dayOfMonth);
                Console.WriteLine("{0} -> {1}", numberSelected, monthName);

            }
            catch (ArgumentOutOfRangeException caughtException)
            {
                Console.WriteLine(caughtException);
                throw;
            }
        }

        private static void ValidateNumber(int number, int daysOfYear)
        {
            if (number < 1 || number > daysOfYear)
            {
                throw new ArgumentOutOfRangeException("Sorry, the number is out of range");
            }
        }

        private static int GetMonthNumber(ref int number, int[] dayOfMonth)
        {
            int monthNumber = 1;
            foreach (var day in dayOfMonth)
            {
                if (number <= day)
                {
                    break;
                }
                else
                {
                    number -= day;
                    monthNumber++;
                }
            }

            return monthNumber;
        }
    }
}
