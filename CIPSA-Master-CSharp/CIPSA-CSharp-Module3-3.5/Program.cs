using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module3_3._5
{
    class Program
    {
        static void Main(string[] args)
        {
            var pow = Math.Pow(5, 2);
            var a = pow + pow;
            Console.WriteLine("Resultado del a: " + a);
            var b = Math.Sin(2.21) + Math.Cos(3.4);
            Console.WriteLine("Resultado del b: " + b);
            var c = (2 + Math.Sqrt(25)) / 7;
            Console.WriteLine("Resultado del c: " + c);
        }
    }
}
