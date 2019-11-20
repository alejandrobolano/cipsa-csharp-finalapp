using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA._CSharp_Module6
{
    class Util
    {
        public static readonly string USER_FILE = "RegistroDeUsuario.txt";
        public static readonly string USER_PHRASES_FILE = "Frases.txt";
        public static readonly string USER_REGISTER_FILE = "Registro.txt";
        public static readonly string USER_MATH_FILE = "Calculadora.txt";
        public static readonly string USER_ADDITION_TEMP = @"c:/Resultados/Sumas.txt";

        public static void EmptyFile(int excersise)
        {
            Console.WriteLine("El fichero del ejercicio anterior está vacio o no se encuentra:" +
                $"\n ¿Quiere volver a realizar el ejercicio {excersise}? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                Console.Clear();
                Program.GoToExercise(excersise);
            }
        }

        public static decimal GetFirstValue()
        {
            Console.WriteLine("Introduzca el primer valor");
            var value = Console.ReadLine();

            if (Helper.GetDecimal(value) != -1)
            {
                return Convert.ToDecimal(value);
            }
            return GetFirstValue();
        }

        public static decimal GetSecondValue()
        {
            Console.WriteLine("Introduzca el segundo valor");
            var value = Console.ReadLine();

            if (Helper.GetDecimal(value) != -1)
            {
                return Convert.ToDecimal(value);
            }
            return GetSecondValue();
        }
    }
}
