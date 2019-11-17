using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module5_CuentaBancaria.Util
{
    public class DatosComplejos
    {

        private static void NuevaCuentaBancaria(out CuentaBancaria cuentaBancaria)
        {

            Console.WriteLine("Introduce el Saldo de Cuenta");
            decimal balance;
            if (decimal.TryParse(Console.ReadLine(), out balance))
            {
                cuentaBancaria = new CuentaBancaria();
                cuentaBancaria.PopularDatos(balance);
            }
            else
            {
                NuevaCuentaBancaria(out cuentaBancaria);
            }

        }
        public static void CrearNuevaCuenta()
        {
            CuentaBancaria cuenta;
            NuevaCuentaBancaria(out cuenta);
            DatosBasicos.Escribir(cuenta);
            cuenta.GuardarDatos();
            Console.WriteLine("¿Desea crear nueva cuenta? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                CrearNuevaCuenta();
            }
            else
            {
                if (CuentaBancaria.listadoCuentaBancarias.Count > 1)
                {
                    Console.WriteLine("¿Desea crear una transferencia? si(s) / no(n)");
                    decision = Console.ReadLine()?.ToLower();
                    if (decision != null && (decision.Equals("s") || decision.Equals("si")))
                    {
                        Console.WriteLine("Para crear la transferencia: ");
                        AgregarCuentasOrigenDestino();
                    }
                }

            }
        }

        private static void AgregarCuentasOrigenDestino()
        {
            var cuentaOrigen = ObtenerCuentaOrigen();
            var cuentaDestino = ObtenerCuentaDestino();

            if (cuentaOrigen.ObtenerNumero() != cuentaDestino.ObtenerNumero())
            {
                Console.WriteLine("Escriba la cantidad a transferir");
                decimal cantidad;
                if (decimal.TryParse(Console.ReadLine(), out cantidad))
                {
                    if (cuentaDestino.TransferenciaDe(cuentaOrigen, cantidad))
                    {
                        DatosBasicos.Escribir(cuentaDestino);
                    }
                }
            }
            else
            {
                Console.WriteLine("Las cuentas deben ser diferentes, por favor, vuelva a introducir los datos", Color.DarkGoldenrod);
                AgregarCuentasOrigenDestino();
            }
        }

        private static CuentaBancaria ObtenerCuentaOrigen()
        {
            var cuentaOrigen = new CuentaBancaria();
            try
            {
                Console.WriteLine("Escriba la cuenta de origen");
                long numeroOrigen;
                if (long.TryParse(Console.ReadLine(), out numeroOrigen))
                {
                    cuentaOrigen = CuentaBancaria.BuscarCuentaBancaria(numeroOrigen);
                    if (cuentaOrigen == null)
                    {
                        Console.WriteLine("Numero de cuenta origen incorrecto", Color.DarkRed);
                        ObtenerCuentaOrigen();
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Excepcion: Numero de cuenta origen incorrecto", Color.DarkRed);
                ObtenerCuentaOrigen();
            }
            return cuentaOrigen;
        }
        private static CuentaBancaria ObtenerCuentaDestino()
        {
            var cuentaDestino = new CuentaBancaria();
            try
            {
                Console.WriteLine("Escriba la cuenta de destino");
                long numeroDestino;
                if (long.TryParse(Console.ReadLine(), out numeroDestino))
                {
                    cuentaDestino = CuentaBancaria.BuscarCuentaBancaria(numeroDestino);
                    if (cuentaDestino == null)
                    {
                        Console.WriteLine("Numero de cuenta destino incorrecto", Color.DarkRed);
                        ObtenerCuentaDestino();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Excepcion: Numero de cuenta destino incorrecto", Color.DarkRed);
                ObtenerCuentaDestino();
            }
            return cuentaDestino;
        }

    }
}
