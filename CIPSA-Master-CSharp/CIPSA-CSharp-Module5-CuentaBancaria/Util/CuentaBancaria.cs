using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module5_CuentaBancaria.Util
{
    public class CuentaBancaria
    {
        private long cuentaNum;
        private decimal cuentaSaldo;
        private TipoCuenta cuentaTipo;
        private static long sigNumCuenta = 123;
        public static IDictionary<long, CuentaBancaria> listadoCuentaBancarias = new Dictionary<long, CuentaBancaria>();

        private static long SiguienteNumero()
        {
            return sigNumCuenta++;
        }

        internal void PopularDatos(decimal balance)
        {
            cuentaNum = SiguienteNumero();
            cuentaSaldo = balance;
            cuentaTipo = TipoCuenta.Corriente;
        }

        internal long ObtenerNumero()
        {
            return cuentaNum;
        }

        internal decimal ObtenerBalance()
        {
            return cuentaSaldo;
        }

        internal string ObtenerTipo()
        {
            return cuentaTipo.ToString();
        }

        internal bool Retirar(decimal amount)
        {
            bool fondoSuficiente = cuentaSaldo >= amount;
            if (fondoSuficiente)
            {
                cuentaSaldo -= amount;
            }
            return fondoSuficiente;
        }

        internal decimal Ingresar(decimal cantidad)
        {
            cuentaSaldo += cantidad;
            return cuentaSaldo;
        }

        internal bool TransferenciaDe(CuentaBancaria cuentaOrigen, decimal cantidad)
        {
            if (cuentaOrigen != null)
            {
                if (cuentaOrigen.Retirar(cantidad))
                {
                    Ingresar(cantidad);
                }
                else
                {
                    Console.WriteLine($"No se pudo retirar la cantidad requerida de la cuenta {cuentaOrigen.ObtenerNumero()}", Color.DarkRed);
                    return false;
                }
            }

            return true;
        }

        internal bool GuardarDatos()
        {
            try
            {
                listadoCuentaBancarias.Add(ObtenerNumero(), this);
                Console.WriteLine($"Datos bancarios guardados correctamente al numero {ObtenerNumero()}", Color.GreenYellow);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"Error al guardar los datos bancarios del número {ObtenerNumero()}", Color.DarkRed);
                return false;
            }
        }

        internal static CuentaBancaria BuscarCuentaBancaria(long numeroCuenta)
        {
            return listadoCuentaBancarias[numeroCuenta];
        }
    }
}
