using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Common
{
    public class Helper
    {

        /// <summary>
        /// Validate if the value is int
        /// </summary>
        /// <param name="value"></param>
        /// <returns>-1 if the value is not a number</returns>
        public static int GetNumeric(string value)
        {
            bool isString = false;
            int result;
            try
            {
                if (value == "" || value == null)
                {
                    return -1;
                }
                foreach (char c in value)
                {
                    if (c < '0' || c > '9')
                    {
                        isString = true;
                    }
                }
                result = isString ? -1 : Convert.ToInt32(value);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Excepción por número demasiado grande", Color.DarkRed);
                return -1;
            }
            return result;
        }
    }
}
