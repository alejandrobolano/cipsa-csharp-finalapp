using CIPSA._CSharp_Module6.Contracts;
using CIPSA._CSharp_Module6.Implementations;
using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n ****" +
                "\n " +
                "\n Probar a partir del ejercicio 71" +
                "\n " +
                "\n ****" +
                "\n ", Color.Aquamarine);
            Home();
        }

        private static void Home()
        {
            Console.WriteLine("Selecciona el ejercicio: " +
            $"\n 1- (5.1.1) Escritura de las frases del usuario en fichero {Util.USER_FILE}" +
            "\n 2- (5.2.1) Leer las primeras 3 lineas del fichero del ejercicio 1" +
            $"\n 31- (5.3.1) Mostrar todo el contenido del fichero creado {Util.USER_PHRASES_FILE}" +
            "\n 32- (5.3.2) Leer fichero dado y tener pausa en la linea 25" +
            $"\n 41- (8.4.1) Escribir las frases tecleadas en el fichero {Util.USER_REGISTER_FILE}" +
            "\n 51- (8.5.1) Escribir pares de numeros para calcular el resultado y escribirlo en el fichero Sumas.txt" +
                      "\n - Verificando si el fichero existe (5.6.1) -" +
                      "\n - El resultado se redondea a dos cifras decimales -" +
            "\n 71- (5.7.1) Dado dos ficheros dado por el usuario, convertir en MAYUS todo el contenido del fichero 1" +
            $"\n 73- (5.7.3) Dado pares de números, calcular según la operación y guardarlo en un fichero {Util.USER_MATH_FILE}" +
                      "\n - Verificando que los valores de entrada sean numeros (5.7.2) -" +
                      "\n - El resultado se redondea a dos cifras decimales -" +
            "\n 0- Salir");

            var exercise = Helper.GetNumeric(Console.ReadLine());
            if (exercise == -1)
            {
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
                        new ExerciseModule6(new Exercise1()).ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 2:
                        new ExerciseModule6(new Exercise2()).ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 31:
                        new ExerciseModule6(new Exercise31()).ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 32:
                        new ExerciseModule6(new Exercise32()).ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 41:
                        new ExerciseModule6(new Exercise41()).ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 51:
                        new ExerciseModule6(new Exercise51()).ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 71:
                        new ExerciseModule6(new Exercise71()).ExecuteExercise();
                        ReturnToHome();
                        break;
                    case 73:
                        new ExerciseModule6(new Exercise73()).ExecuteExercise();
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
