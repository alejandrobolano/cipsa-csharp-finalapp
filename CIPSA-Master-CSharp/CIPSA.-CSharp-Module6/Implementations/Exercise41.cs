using CIPSA._CSharp_Module6.Contracts;
using CIPSA_CSharp_Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CIPSA._CSharp_Module6.Implementations
{
    public class Exercise41 : IExercisesModule6
    {
        public void ExecuteExercise()
        {
            Console.WriteLine($"Escriba el texto para guardarlo en el fichero {Util.USER_REGISTER_FILE}");
            Helper.WriteFileWhileWriteInConsole(Util.USER_REGISTER_FILE);
            Console.WriteLine($"Puede chequear el fichero {Util.USER_REGISTER_FILE} con el texto añadido", Color.DarkGreen);
        }
    }
}
