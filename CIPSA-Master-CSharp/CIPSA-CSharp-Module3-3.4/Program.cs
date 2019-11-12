using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module3_3._4
{
    class Program
    {
        public static readonly string Name = "¿Cuál es su nombre?";
        public static readonly string Hello = "¡Hola! ";
        public static readonly string HourWorked = "¿Cuántas horas has trabajado?";
        public static readonly string PricePerHour = "¿Cuánto cobra por hora?";
        public static readonly string GrossSalary = "Su salario bruto es: ";
        public static readonly string NetSalary = "Su salario neto es: ";
        public static readonly string IrpfInfo = "Su retención es de: ";
        public static readonly double Irpf = 0.05;

        static void Main(string[] args)
        {
            Console.WriteLine(Name);
            var name = Console.ReadLine();
            Console.WriteLine(Hello + name);
            Console.WriteLine(HourWorked);
            var hourWorked = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(PricePerHour);
            var pricePerHour = Convert.ToDouble(Console.ReadLine());
            CalculateSalary(hourWorked, pricePerHour);
        }

        private static void CalculateSalary(double hourWorked, double pricePerHour)
        {
            var totalAmount = hourWorked * pricePerHour;
            var realAmount = totalAmount - (totalAmount * Irpf);
            Console.WriteLine(GrossSalary + totalAmount);
            Console.WriteLine(NetSalary + realAmount);
            Console.WriteLine(IrpfInfo + Irpf * 100 + "%");
        }
    }
}
