using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Common;
using CIPSA_CSharp_Module7Console.Implementations;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module7Console
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
            $"\n 1- Recitar el abecedario (castellano) de la A a la Z" +
            $"\n 2- Revertir lista de 10 números introducidos por el usuario (Incluye el ejercicio 3, mostrar los mayores que 22)" +
            $"\n 4- Buscar números repetidos dentro de un array con números aleatorio," +
            $"\n 5- Sumar y encontrar el mayor y menor de una lista dada por el usuario" +
            $"\n 6- Dado dos series de números introducidos, realizar una comparativa" +
            "\n 0- Salir");

            var exercise = Helper.GetNumeric(Console.ReadLine());
            if (exercise == -1)
            {
                Console.Clear();
                Home();
            }

            GoToExercise(exercise);
        }

        public static void ReturnToHome()
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
        public static void GoToExercise(int exercise)
        {
            try
            {
                Console.Clear();
                switch (exercise)
                {
                    case 0:
                        break;
                    case 1:
                        new Exercise1().ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 2:
                        new Exercise2().ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 4:
                        new Exercise4().ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 5:
                        new Exercise5().ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 6:
                        new Exercise6().ExecuteExercise();
                        ReturnToHome();
                        break;
                    default:
                        Console.WriteLine("Ejercicio no implementado aún");
                        ReturnToHome();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, Color.DarkRed);
            }
        }
    }
}
