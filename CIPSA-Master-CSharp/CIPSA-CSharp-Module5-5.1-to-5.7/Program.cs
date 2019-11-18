using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Module5_CuentaBancaria.Util;

namespace CIPSA_CSharp_Module5_5._1_to_5._7
{
    class Program
    {
        static void Main(string[] args)
        {
            Home();
        }

        private static void Home()
        {
            Console.WriteLine("Selecciona el ejercicio: " +
            "\n 1- Mayor entre dos números junto con el Swap del 2do ejercicio" +
            "\n 3- Calcular el factorial" +
            "\n 4- Calcular el factorial con método recursivo" +
            "\n 5- Cuenta bancaria (proy externo)" +
            "\n 50- Calculo con cuenta bancaria compleja (proy externo)" +
            "\n 6- Generar cadena invertida" +
            "\n 7- Convertir en mayuscula el texto de un fichero (proy externo)" +
            "\n 0- Salir");

            var exercise = Helper.GetNumeric(Console.ReadLine());
            if (exercise == -1)
            {
                Home();
            }

            GoToExercise(exercise);
        }

        private static void GoToExercise(int exercise)
        {
            try
            {
                Console.Clear();
                switch (exercise)
                {
                    case 0:
                        break;
                    case 1:
                        Exercise1();
                        break;
                    case 3:
                        Exercise3();
                        break;
                    case 4:
                        Exercise4();
                        break;
                    case 5:
                        DatosBasicos.CargarDatos();
                        ReturnToHome();
                        break;
                    case 50:
                        DatosComplejos.CrearNuevaCuenta();
                        break;
                    case 6:
                        Exercise6();
                        break;
                    case 7:
                        CIPSA_CSharp_Module5_Ficheros.Program.ReadAndWriteFile();
                        break;
                    default:
                        Console.WriteLine("Ejercicio no implementado aún");
                        ReturnToHome();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void Exercise6()
        {
            Console.WriteLine("Escriba una frase para invertirla: ");
            var value = Console.ReadLine();
            Util.Reverse(ref value);
            Console.WriteLine("La cadena invertida es: {0}", value);
            ReturnToHome();
        }

        private static void Exercise4()
        {
            Console.WriteLine("Escriba un número para calcular el factorial");
            var value = Helper.GetNumeric(Console.ReadLine());
            if (value == -1)
            {
                Exercise4();
            }
            int factorial;
            try
            {
                var isCorrectFactorial = Util.RecursiveFactorial(value, out factorial);
                Console.WriteLine(isCorrectFactorial ? $"Factorial de manera recursiva de {value} es: {factorial}" : "No se ha podido calcular el factorial");
                ReturnToHome();
            }
            catch (StackOverflowException)
            {
                GoToExercise(4);
            }

        }

        private static void Exercise3()
        {
            Console.WriteLine("Escriba un número para calcular el factorial");
            var value = Helper.GetNumeric(Console.ReadLine());
            if (value == -1)
            {
                Exercise3();
            }
            int factorial;
            var isCorrectFactorial = Util.Factorial(value, out factorial);
            Console.WriteLine(isCorrectFactorial ? $"Factorial de {value} es: {factorial}" : "No se ha podido calcular el factorial");
            ReturnToHome();
        }

        private static void Exercise1()
        {
            GetFirstValue();
        }

        private static void ReturnToHome()
        {
            Console.WriteLine();
            Console.WriteLine("¿Quiere regresar al inicio? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                Console.Clear();
                Home();
            }
        }

        private static void GetFirstValue()
        {
            Console.WriteLine("Selecciona el primer valor");
            var firstValue = Helper.GetNumeric(Console.ReadLine());
            if (firstValue == -1)
            {
                GetFirstValue();
            }
            GetSecondValue(firstValue);
        }

        private static void GetSecondValue(int firstValue)
        {
            Console.WriteLine("Selecciona el segundo valor");

            var secondValue = Helper.GetNumeric(Console.ReadLine());
            if (secondValue == -1)
            {
                GetSecondValue(firstValue);
            }
            Console.WriteLine($"El mayor valor es: {Util.Bigger(firstValue, secondValue)}");

            Console.WriteLine($"Antes del swap: x = {firstValue} y = {secondValue}");
            Util.Swap(ref firstValue, ref secondValue);
            Console.WriteLine($"Después del swap: x = {firstValue} y = {secondValue}");

            ReturnToHome();
        }
    }
}
