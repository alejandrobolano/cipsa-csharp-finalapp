using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module_4_4._2_1_to_10
{
    class Program
    {
        public static readonly string Name = "What is your name?";
        public static readonly string LastName = "What is your lastname? ";
        public static readonly string Age = "What is your age? ";
        public static readonly string Note = "What is your note in the exam? - Range of 1 to 10";
        public static readonly string RandomNumber = "Tell us one number? - Range of 2 to 5";
        public static readonly string Failed = "Fail";
        public static readonly string Approved = "Approved";
        public static readonly string Notable = "Notable";
        public static readonly string Excellent = "Excellent";
        public static readonly string Honor = "Honor medal";

        static void Main(string[] args)
        {
            Home();
        }

        private static void Home()
        {
            Console.WriteLine("Write the exercise number you want:" +
                              "\n 1- Name, lastname and age" +
                              "\n 2- What number is bigger" +
                              "\n 4- Notes of exam (Mix with Ex-3)" +
                              "\n 5- Random number" +
                              "\n 7- Difference between two years" +
                              "\n 8- Difference between three numbers" +
                              "\n 9- Repeat text 100 times" +
                              "\n 10- Multiplication table");

            var exercise = Convert.ToInt32(Console.ReadLine());

            try
            {
                switch (exercise)
                {
                    case 1:
                        Exercise1();
                        break;
                    case 2:
                        Exercise2();
                        break;
                    case 4:
                        Exercise4();
                        break;
                    case 5:
                        Exercise5();
                        break;
                    case 7:
                        Exercise7();
                        break;
                    case 8:
                        Exercise8();
                        break;
                    case 9:
                        Exercise9();
                        break;
                    case 10:
                        Exercise10();
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

        private static void Exercise10()
        {
            Console.WriteLine("Escriba un número del 1 al 10");
            var number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("A continuación se mostrará la tabla de multiplicación del número: {0}", number);
            for (int i = 1; i <= 10; i++)
            {
                var result = i * number;
                Console.WriteLine($"{number} * {i} = {result}");
            }
            ReturnToHome();
        }

        private static void Exercise8()
        {

            Console.WriteLine("Escribe un número");
            var firstNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Escribe otro número");
            var secondNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Escribe otro número más");
            var thirdNumber = Convert.ToInt32(Console.ReadLine());

            if (firstNumber == secondNumber && firstNumber == thirdNumber)
            {
                Console.WriteLine("Los tres números son iguales");
            }
            else if (firstNumber == secondNumber || firstNumber == thirdNumber || secondNumber == thirdNumber)
            {
                Console.WriteLine("Hay dos números que son iguales");
            }
            else
            {
                Console.WriteLine("Los tres números son diferentes");
            }
            ReturnToHome();
        }

        private static void Exercise2()
        {
            Console.WriteLine("Escriba el primer número");
            var firstNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Escriba el segundo número");
            var secondNumber = Convert.ToInt32(Console.ReadLine());

            if (firstNumber == secondNumber)
            {
                Console.WriteLine("Ambos números son iguales");
            }
            else
            {
                var bigger = Math.Max(firstNumber, secondNumber);
                Console.WriteLine("El mayor número es {0}", bigger);
            }

            Console.ReadLine();
            ReturnToHome();
        }

        private static void Exercise9()
        {
            Console.WriteLine("Escriba el tipo de bucle que desee:" +
                              "\n 1- For" +
                              "\n 2- While" +
                              "\n 3- Do While");
            var operation = Convert.ToInt32(Console.ReadLine());
            switch (operation)
            {
                case 1:
                    ForEachHomework();
                    break;
                case 2:
                    WhileHomework();
                    break;
                case 3:
                    DoWhileHomework();
                    break;
                default:
                    Console.WriteLine("Operación incorrecta");
                    break;

            }
            ReturnToHome();
        }

        private static void DoWhileHomework()
        {
            int cont = 0;
            do
            {
                Console.WriteLine("No lanzaré avioncito de papel en clase");
                cont++;
            } while (cont < 100);
        }

        private static void WhileHomework()
        {
            int cont = 0;
            while (cont < 100)
            {
                Console.WriteLine("No lanzaré avioncito de papel en clase");
                cont++;
            }
        }

        private static void ForEachHomework()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("No lanzaré avioncito de papel en clase");
            }
        }

        private static void Exercise7()
        {
            Console.WriteLine("¿Cuál es el año actual?");
            var yearActual = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Diga un año cualquiera");
            var yearRandom = Convert.ToInt32(Console.ReadLine());
            int calculate = 0;
            if (yearActual < yearRandom)
            {
                calculate = yearRandom - yearActual;
                Console.WriteLine("Para llegar al año {0} faltan {1} años", yearRandom, calculate);
            }
            else
            {
                if (yearRandom < 0)
                {
                    calculate = yearActual + Math.Abs(yearRandom);
                }
                else
                {
                    calculate = yearActual - yearRandom;
                }
            }

            Console.WriteLine("Desde el año {0}, han pasado {1} años", yearRandom, calculate);
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

        private static void Exercise5()
        {
            Console.WriteLine(RandomNumber);
            var number = Convert.ToInt32(Console.ReadLine());
            CalculateBigLowAndAverage(number);
            ReturnToHome();
        }

        private static void Exercise4()
        {
            Console.WriteLine(Note);
            var note = Convert.ToInt32(Console.ReadLine());
            if (note > 10 && note < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            GetNoteOfExam(note);
            ReturnToHome();
        }

        private static void Exercise1()
        {
            Console.WriteLine(Name);
            var name = Console.ReadLine();
            Console.WriteLine(LastName);
            var lastName = Console.ReadLine();
            Console.WriteLine(Age);
            var age = Convert.ToInt32(Console.ReadLine());
            if (age > 150)
            {
                throw new ArgumentOutOfRangeException();
            }

            ReturnToHome();
        }

        private static void CalculateBigLowAndAverage(int number)
        {
            var sum = 0;
            var random = new Random();
            var randomNumber = random.Next(100);
            var biggerNumber = randomNumber;
            var lowerNumber = randomNumber;
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("The random number is {0} - total of numbers {1}", randomNumber, number);
                Console.ReadLine();
                biggerNumber = Math.Max(biggerNumber, randomNumber);
                lowerNumber = Math.Min(lowerNumber, randomNumber);
                sum += randomNumber;
                random = new Random();
                randomNumber = random.Next(100);
            }

            Console.WriteLine("The bigger number is: {0}", biggerNumber);
            Console.WriteLine("The lower number is: {0}", lowerNumber);
            Console.WriteLine("The average is: {0}", sum / number);
        }

        private static void GetNoteOfExam(int note)
        {
            switch (note)
            {
                case 6:
                    Console.WriteLine(Approved);
                    break;
                case 7:
                case 8:
                    Console.WriteLine(Notable);
                    break;
                case 9:
                    Console.WriteLine(Excellent);
                    break;
                case 10:
                    Console.WriteLine(Honor);
                    break;
                default:
                    Console.WriteLine(Failed);
                    break;
            }
        }
    }
}
