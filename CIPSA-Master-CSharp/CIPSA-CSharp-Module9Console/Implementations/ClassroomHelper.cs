using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Module9Console.Models;

namespace CIPSA_CSharp_Module9Console.Implementations
{
    public class ClassroomHelper
    {
        public static Classroom GetClassroom(int number, List<Classroom> classrooms)
        {
            return classrooms.FirstOrDefault(classroom => classroom.Number == number) ;
        }
        public static Classroom GetClassroom(string nameClass, List<Classroom> classrooms)
        {
            return classrooms.FirstOrDefault(classroom => classroom.NameClass.ToLowerInvariant().Equals(nameClass.ToLowerInvariant())) ;
        }


    }
}
