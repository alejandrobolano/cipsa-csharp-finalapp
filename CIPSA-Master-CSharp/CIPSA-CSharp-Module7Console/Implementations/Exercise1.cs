using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class Exercise1
    {
        public void ExecuteExercise()
        {
            char[] alphabetArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            foreach (var letter in alphabetArray)
            {
                Console.WriteLine(letter);
            }
        }
    }
}
