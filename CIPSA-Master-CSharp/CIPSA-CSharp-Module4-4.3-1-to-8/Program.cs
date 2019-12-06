using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module4_4._3_1_to_8
{
    class Program
    {
        static void Main(string[] args)
        {
            Home();
        }

        private static void Home()
        {
            Console.WriteLine("Write the exercise number you want:" +
                              "\n 1- Convert text by keyboard to int (Mix with exercise 2)" +
                              "\n 3- Convert Fahrenheit to Celsius" +
                              "\n 5- Division between two numbers (Mix with exercise 6)" +
                              "\n 7- Factorial of number between 0 and 15 (Mix with exercise 8)");
            var exercise = Convert.ToInt32(Console.ReadLine());

            GoToExercise(exercise);
        }

        private static void GoToExercise(int exercise)
        {
            try
            {
                switch (exercise)
                {
                    case 1:
                        Exercise1MixWithEx2();
                        break;
                    case 3:
                        Exercise3();
                        break;
                    case 5:
                        Exercise5MixWithEx6();
                        break;
                    case 7:
                        Exercise7();
                        break;
                    default:
                        Console.WriteLine("Exercise not implemented yet");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void Exercise7()
        {
            Console.WriteLine("Escriba el número para calcular el factorial (0 < x < 15)");
            var input = Console.ReadLine();
            int number = 0;
            if (!ParseToInt(input, ref number))
            {
                GoToExercise(7);
            }

            try
            {
                CheckRange0To15(number);
                var factorial = CalculateFactorial(number);
                Console.WriteLine("El factorial de {0} es: {1}", number, factorial);
                ReturnToHome();
            }
            catch (MyCustomException e)
            {
                Console.WriteLine($"Excepción customizada lanzada: {e}");
                GoToExercise(7);
            }
        }

        private static int CalculateFactorial(int number)
        {
            int factorial = number;
            for (int i = number - 1; i > 0; i--)
            {
                factorial *= i;
            }

            return factorial;
        }

        private static void CheckRange0To15(int number)
        {
            if (number < 0 || number > 15)
            {
                // Aquí se lanzaba en el ejercicio 7, la excepción ArgumentOutOfRangeException
                throw new MyCustomException("El Rango debe ser entre 0 y 15");
            }
        }

        private static void Exercise5MixWithEx6()
        {

            Console.WriteLine("Escriba el primer número: ");
            var firstInput = Console.ReadLine();
            if (firstInput == "")
            {
                firstInput = null;
            }
            int firstNumber = 0;
            if (!ParseToInt(firstInput, ref firstNumber))
            {
                GoToExercise(5);
            }

            Console.WriteLine("Escriba el segundo número: ");
            var secondInput = Console.ReadLine();
            if (secondInput == "")
            {
                secondInput = null;
            }
            int secondNumber = 0;
            if (!ParseToInt(secondInput, ref secondNumber))
            {
                GoToExercise(5);
            }

            try
            {
                var division = firstNumber / secondNumber;
                Console.WriteLine(firstNumber + "/" + secondNumber + "=" + division);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("División por cero");
                GoToExercise(5);

            }

            ReturnToHome();
        }

        private static bool ParseToInt(string input, ref int tryToInt)
        {
            try
            {
                tryToInt = int.Parse(input ?? throw new InvalidOperationException());
                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato incorrecto valor");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Valor nulo");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Memoria overflow");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid operation");
            }

            return false;

        }

        private static void Exercise3()
        {
            Console.WriteLine("Escriba los grados en Fahrenheit para convertirlo en Celsius");
            var input = float.TryParse(Console.ReadLine(), out var fahrenheit);

            try
            {
                var celsius = input ? ConvertFahrenheitToCelsius(fahrenheit) : throw new FormatException();
                Console.WriteLine(celsius);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ha introducido algún caracter inválido para poder convertir");
                ReturnToHome();
            }
            ReturnToHome();
        }

        private static float ConvertFahrenheitToCelsius(float fahrenheit)
        {
            return (fahrenheit - 32f) * 5 / 9;
        }

        private static void Exercise1MixWithEx2()
        {
            Console.WriteLine("Escriba el número deseado (se intentará convertir a INT)");
            var textByKeyboard = 0;
            try
            {
                textByKeyboard = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Número incorrecto, caracteres inválidos");
                ReturnToHome();
            }
            finally
            {
                Console.WriteLine(textByKeyboard);
            }
            ReturnToHome();

        }

        private static void ReturnToHome()
        {
            Console.WriteLine("Do you want return to home? yes(y) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("y") || decision.Equals("yes")))
            {
                Home();
            }
        }
    }
}
