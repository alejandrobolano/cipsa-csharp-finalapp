using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module3_3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var phraseOne = "Creando frases básicas de muestra para ejercicio";
            var phraseTwo = "Está bien celebrar el éxito, pero es más importante aprender las lecciones del fracaso";
            var phraseThree = "Para ser grande, a veces tienes que correr grandes riesgos";

            Console.WriteLine($" {phraseOne}" +
                              $"\n {phraseTwo}" +
                              $"\n {phraseThree}");
            Console.ReadLine();
        }
    }
}
