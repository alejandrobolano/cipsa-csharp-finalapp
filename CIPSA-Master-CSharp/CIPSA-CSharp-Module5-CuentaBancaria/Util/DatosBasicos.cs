using System.Drawing;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module5_CuentaBancaria.Util
{
    public class DatosBasicos
    {
        public static void CargarDatos()
        {
            CuentaBancaria cuenta1 = new CuentaBancaria();
            cuenta1.PopularDatos(100);
            CuentaBancaria cuenta2 = new CuentaBancaria();
            cuenta2.PopularDatos(100);
            Console.WriteLine("---------------Antes de la transferencia-----------------");
            Escribir(cuenta1);
            Escribir(cuenta2);
            cuenta1.TransferenciaDe(cuenta2, 10);
            Console.WriteLine("---------------Despues de la transferencia---------------");
            Escribir(cuenta1);
            Escribir(cuenta2);
            Console.ReadLine();
        }

        public static void Escribir(CuentaBancaria cuenta)
        {
            Console.WriteLine("Numero de cuenta es {0}", cuenta.ObtenerNumero(), Color.DarkBlue);
            Console.WriteLine("Saldo de cuenta es {0}", cuenta.ObtenerBalance(), Color.DarkBlue);
            Console.WriteLine("Tipo de cuenta es {0}", cuenta.ObtenerTipo(), Color.DarkBlue);
        }

        /// <summary>
        /// Probar si se puede realizar el ingreso del dinero en la cuenta bancaria
        /// </summary>
        /// <param name="cuenta"></param>
        public static void TestIngreso(CuentaBancaria cuenta)
        {
            Console.WriteLine("Entra la cantidad a depositar: ");
            decimal cantidad;
            if (decimal.TryParse(Console.ReadLine(), out cantidad))
            {
                cuenta.Ingresar(cantidad);
            }
            else
            {
                TestIngreso(cuenta);
            }

        }

        /// <summary>
        /// Probar si se puede realizar el retiro del dinero en la cuenta bancaria
        /// </summary>
        /// <param name="cuenta"></param>
        public static void TestRetirar(CuentaBancaria cuenta)
        {
            Console.WriteLine("Entra la cantidad a retirar: ");
            decimal cantidad;
            if (decimal.TryParse(Console.ReadLine(), out cantidad))
            {
                if (!cuenta.Retirar(cantidad))
                {
                    Console.WriteLine("Fondos insuficientes");
                }
            }
            else
            {
                TestRetirar(cuenta);
            }

        }


    }
}
