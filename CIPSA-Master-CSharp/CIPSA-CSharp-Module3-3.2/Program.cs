using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module3_3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            var nameRequest = "¿Cómo te llamas?";
            var addressRequest = "¿En dónde vives?";
            var ageRequest = "¿Cuántos años tienes?";
            var studyQuantity = "¿Cuántos años te quedan por estudiar?";
            var studyEnd = "Terminas de estudiar a los";
            var isSportLike = "¿Te gusta el deporte?";
            var isHotToday = "¿Hace calor hoy?";

            Console.WriteLine(nameRequest);
            var name = Console.ReadLine();
            Console.WriteLine(addressRequest);
            var address = Console.ReadLine();
            Console.WriteLine(ageRequest);
            var age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(studyQuantity);
            var quantity = Convert.ToInt32(Console.ReadLine());
            age += quantity;
            Console.WriteLine(studyEnd + " " + age);
            Console.WriteLine(isSportLike);
            Console.ReadLine();
            Console.WriteLine(isHotToday);
            Console.ReadLine();
        }
    }
}
