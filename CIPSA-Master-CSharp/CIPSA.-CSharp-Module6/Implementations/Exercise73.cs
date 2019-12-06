using CIPSA_CSharp_Common;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    public class Exercise73
    {
        public void ExecuteExercise()
        {
            Console.WriteLine($"Escriba pares de números y la operación a realizar para guardar el cálculo en el fichero {Util.USER_MATH_FILE}");
            CalculatePairNumberAndWriteInFile();
        }

        private static void CalculatePairNumberAndWriteInFile()
        {
            try
            {
                var firstValue = Util.GetFirstValue();
                var secondValue = Util.GetSecondValue();
                var operatorValue = GetOperatorValue();
                var result = 0.0m;
                bool isDividedByZero = false;
                var operation = string.Empty;

                switch (operatorValue)
                {
                    case "+":
                        result = firstValue + secondValue;
                        break;
                    case "-":
                        result = firstValue - secondValue;
                        break;
                    case "*":
                        result = firstValue * secondValue;
                        break;
                    case "/":
                        if (secondValue != 0)
                        {
                            result = firstValue / secondValue;
                        }
                        else
                        {
                            isDividedByZero = true;
                        }
                        break;
                }
                if (!isDividedByZero)
                {
                    operation = $"{firstValue.ToString()} {operatorValue} {secondValue.ToString()} = {decimal.Round(result, 2).ToString()}";
                    Console.WriteLine($"La operacion realizada es {operation}", Color.DarkGreen);
                }
                else
                {
                    Console.WriteLine("Lo sentimos, pero no se pudo realizar la operación, ha sido capturada una división por 0", Color.DarkOrange);
                    operation = "Cálculo incorrecto dado que ha sido una división por cero";
                }
                Helper.WriteFile(Util.USER_MATH_FILE, operation, true);
                Console.WriteLine("¿Quiere ingresar un nuevo par de números al inicio? si(s) / no(n)");
                var decision = Console.ReadLine()?.ToLower();
                if (decision != null && (decision.Equals("s") || decision.Equals("si")))
                {
                    CalculatePairNumberAndWriteInFile();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Lo sentimos, pero no se pudo realizar la operación", Color.DarkRed);
            }
        }

        private static string GetOperatorValue()
        {
            Console.WriteLine("Introduzca un operador válido para realizar el cálculo (+,-,*,/)");
            var value = Console.ReadLine();
            if (value.Equals("+") || value.Equals("-") || value.Equals("*") || value.Equals("/"))
            {
                return value;
            }
            return GetOperatorValue();
        }
    }
}
