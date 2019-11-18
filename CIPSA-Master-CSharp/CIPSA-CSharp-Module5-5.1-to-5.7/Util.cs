using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module5_5._1_to_5._7
{
    class Util
    {
        public static int Bigger(int firstValue, int secondValue)
        {
            return firstValue > secondValue ? firstValue : secondValue;
        }

        public static void Swap(ref int firstValue, ref int secondValue)
        {
            int temp = firstValue;
            firstValue = secondValue;
            secondValue = temp;
        }

        public static bool Factorial(int value, out int factorial)
        {
            bool isCorrect = true;
            int valueTemp = 1;
            if (value < 0)
            {
                valueTemp = 0;
                isCorrect = false;
            }
            try
            {
                checked
                {
                    for (int k = 2; k <= value; ++k)
                    {
                        valueTemp *= k;
                    }
                }
            }
            catch (OverflowException)
            {
                valueTemp = 0;
                isCorrect = false;
            }

            factorial = valueTemp;
            return isCorrect;
        }

        public static bool RecursiveFactorial(int value, out int factorial)
        {
            bool isCorrect = true;
            if (value < 0)
            {
                factorial = 0;
                isCorrect = false;
            }
            else if (value <= 1)
            {
                factorial = 1;
            }
            else
            {
                try
                {
                    int recursiveFactorial;
                    checked
                    {
                        isCorrect = RecursiveFactorial(value - 1, out recursiveFactorial);
                        factorial = value * recursiveFactorial;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Resultado demasiado grande");
                    factorial = 0;
                    isCorrect = false;

                }
            }
            return isCorrect;
        }

        public static void Reverse(ref string value)
        {
            string reverseValue = string.Empty;
            for (int k = value.Length - 1; k >= 0; k--)
            {
                reverseValue = reverseValue + value[k];
            }
            value = reverseValue;
        }
    }
}
