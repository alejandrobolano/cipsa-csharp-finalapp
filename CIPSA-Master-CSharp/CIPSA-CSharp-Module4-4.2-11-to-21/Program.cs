using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module4_4._2_11_to_21
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
                              "\n 11- Factorial" +
                              "\n 12- How many numbers do you want" +
                              "\n 13- Multiplication of next numbers until 10" +
                              "\n 14- How many numbers are negatives" +
                              "\n 15- Limits of numbers" +
                              "\n 16- Draw rectangle" +
                              "\n 17- Draw triangle" +
                              "\n 18- Draw inverse triangle" +
                              "\n 19- Prime number" +
                              "\n 20- Play game" +
                              "\n 21- Play game by user");

            var exercise = Convert.ToInt32(Console.ReadLine());

            GoToExercise(exercise);
        }

        private static void GoToExercise(int exercise)
        {
            try
            {
                switch (exercise)
                {
                    case 11:
                        Exercise11();
                        break;
                    case 12:
                        Exercise12();
                        break;
                    case 13:
                        Exercise13();
                        break;
                    case 14:
                        Exercise14();
                        break;
                    case 15:
                        Exercise15();
                        break;
                    case 16:
                        Exercise16();
                        break;
                    case 17:
                        Exercise17();
                        break;
                    case 18:
                        Exercise18();
                        break;
                    case 19:
                        Exercise19();
                        break;
                    case 20:
                        Exercise20();
                        break;
                    case 21:
                        Exercise21();
                        break;
                    default:
                        Console.WriteLine("Exercise not implemented yet");
                        ReturnToHome();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void Exercise21()
        {
            var random = new Random();
            Console.WriteLine("Bienvenido al juego de las adivinanzas");
            Console.WriteLine("Usted como usuario primeramente debe decir el rango y luego dice las palabras 'mayor, menor o igual' para adivinarlo");
            Console.WriteLine("Diga el valor mínimo");
            var lowNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Diga el mayor valor");
            var highNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Rango: mín-> {lowNumber} max-> {highNumber}");
            var numberDiviner = random.Next(lowNumber, highNumber);
            var tempLow = lowNumber;
            var tempHigh = highNumber;

            SearchNumeberByUser(numberDiviner, tempLow, random, tempHigh, out var isNext);
            RepeatGame(21);

        }

        private static void SearchNumeberByUser(int numberDiviner, int tempLow, Random random, int tempHigh, out bool isNext)
        {
            isNext = false;
            while (!isNext)
            {
                Console.WriteLine($"¿Es {numberDiviner}?");
                var userAnswer = Console.ReadLine()?.ToLower();

                switch (userAnswer)
                {
                    case "mayor":
                        tempLow = numberDiviner;
                        numberDiviner = random.Next(tempLow, tempHigh);
                        break;
                    case "menor":
                        tempHigh = numberDiviner;
                        numberDiviner = random.Next(tempLow, tempHigh);
                        break;
                    case "igual":
                        isNext = true;
                        Console.WriteLine("¡Gracias por jugar conmigo");
                        break;
                    default:
                        Console.WriteLine("¡Porfa díga si es mayor, menor o igual!");
                        break;
                }
            }
        }

        private static void Exercise20()
        {
            int max = 50;
            var random = new Random();
            var numberSelectedRandom = random.Next(max);
            Console.WriteLine("Bienvenido al juego de las adivinanzas");
            Console.WriteLine($" Va diciendo números entre el 1 al {max} y el programa dice si el valor es por encima o por debajo," +
                              "\n En el caso de que quiera renunciar puede escribir '-1'");

            SearchNumber(numberSelectedRandom, out var isNext);
            RepeatGame(20);
        }

        private static void SearchNumber(int numberSelectedRandom, out bool isNext)
        {
            isNext = false;
            while (!isNext)
            {
                Console.WriteLine("Escriba un número");
                var tempNumber = Convert.ToInt32(Console.ReadLine());
                if (tempNumber == -1)
                {
                    Console.WriteLine("Sentimos que haya querido salir del juego");
                    isNext = true;
                }
                else
                {
                    isNext = WhereIsTheNumber(numberSelectedRandom, tempNumber);
                    if (isNext)
                    {
                        Console.WriteLine("¡Enhorabuena! Ha logrado adivinar el número");
                    }
                }
            }
        }

        private static void RepeatGame(int exercise)
        {
            Console.WriteLine("¿Desea volver a jugar? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                GoToExercise(exercise);
            }
            else
            {
                ReturnToHome();
            }
        }

        private static bool WhereIsTheNumber(int numberSelectedRandom, int numberByPlayer)
        {
            bool isCorrect = numberSelectedRandom == numberByPlayer;
            if (!isCorrect)
            {
                Console.WriteLine(numberSelectedRandom > numberByPlayer
                    ? "El número se encuentra por encima"
                    : "El número se encuentra por debajo");
            }

            return isCorrect;
        }

        private static void Exercise19()
        {
            Console.WriteLine("Escriba un número y verificaremos si es primo o no");
            var number = Convert.ToInt32(Console.ReadLine());
            if (IsPrime(number))
            {
                Console.WriteLine("Es número primo");
            }
            else
            {
                Console.WriteLine("No es un número primo");
            }
            ReturnToHome();
        }

        private static bool IsPrime(int number)
        {
            int temp = 0;
            for (int i = 1; i < (number + 1); i++)
            {
                if (number % i == 0)
                    temp++;
            }
            if (temp != 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void Exercise18()
        {
            Console.WriteLine("Escriba la altura del triángulo");
            var height = Convert.ToInt32(Console.ReadLine());
            for (int i = height; i > 0; i--)
            {
                for (int j = i; j > 0; j--)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            ReturnToHome();
        }
        private static void Exercise17()
        {
            Console.WriteLine("Escriba la altura del triángulo");
            var height = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            ReturnToHome();
        }

        private static void Exercise16()
        {
            Console.WriteLine("Escriba la altura del rectangulo");
            var height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Escriba el ancho del rectangulo");
            var width = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            ReturnToHome();
        }

        private static void Exercise15()
        {
            Console.WriteLine("Escriba diferentes números enteros hasta que se detenga");
            bool stopNegative = false;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Escriba un número");
                var tempNumber = Convert.ToDecimal(Console.ReadLine());
                if (tempNumber < 0)
                {
                    Console.WriteLine("El programa se ha detenido por introducir un número negativo");
                    stopNegative = true;
                    break;
                }
            }

            if (!stopNegative)
            {
                Console.WriteLine("Ha llegado al máximo de número posibles");
            }

            ReturnToHome();
        }

        private static void Exercise14()
        {
            Console.WriteLine("¿Cuántos números quiere introducir?");
            int.TryParse(Console.ReadLine(), out var numbers);
            var negativeCount = 0;
            List<decimal> negativesList = new List<decimal>();
            for (int i = 0; i < numbers; i++)
            {
                Console.WriteLine("Escriba un número");
                var tempNumber = Convert.ToDecimal(Console.ReadLine());
                if (tempNumber < 0)
                {
                    negativeCount++;
                    negativesList.Add(tempNumber);
                }
            }
            Console.WriteLine("La cantidad de números negativos introducidos es: {0}", negativeCount);
            foreach (var negativeNumber in negativesList)
            {
                Console.WriteLine($"{negativeNumber}");
            }
            ReturnToHome();
        }

        private static void Exercise13()
        {
            Console.WriteLine("Escriba un número del 1 al 10");
            var number = Convert.ToInt32(Console.ReadLine());
            for (int i = number + 1; i <= 10; i++)
            {
                var result = i * number;
                Console.WriteLine($"{number} * {i} = {result}");
            }
            ReturnToHome();
        }

        private static void Exercise12()
        {
            Console.WriteLine("¿Cuántos números quiere introducir?");
            var numbers = Convert.ToInt32(Console.ReadLine());
            var addition = 0;
            for (int i = 0; i < numbers; i++)
            {
                Console.WriteLine("Escriba un número");
                var tempNumber = Convert.ToInt32(Console.ReadLine());
                addition += tempNumber;
            }
            Console.WriteLine("La suma total de los números introducidos es: {0}", addition);
            ReturnToHome();
        }

        private static void Exercise11()
        {
            Console.WriteLine("Escriba el número para calcular el factorial");
            var input = Convert.ToInt32(Console.ReadLine());
            var factorial = CalculateFactorial(input);
            Console.WriteLine("El factorial de {0} es: {1}", input, factorial);
            ReturnToHome();
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
