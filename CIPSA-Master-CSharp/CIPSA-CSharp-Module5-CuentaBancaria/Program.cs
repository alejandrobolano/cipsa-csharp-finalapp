using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Common;

namespace CIPSA_CSharp_Module5_CuentaBancaria
{
    class Program
    {
        static void Main(string[] args)
        {
            Home();
        }

        private static void Home()
        {
            Console.WriteLine("Selecciona la sección de ejercicio: " +
            "\n 1- Basico" +
            "\n 2- Complejo" +
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
                        Util.DatosBasicos.CargarDatos();
                        break;
                    case 2:
                        Util.DatosComplejos.CrearNuevaCuenta();
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
    }
}
