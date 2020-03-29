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
        #region Métodos para rellenar objetos por defecto para la respuesta de los ejercicios del módulo 9
        public static List<Classroom> FillClassroomsExample()
        {
            return new List<Classroom>
            {
                new Classroom
                {
                    Number = 99,
                    NameClass = "Alfa"
                },
                new Classroom
                {
                    Number = 98,
                    NameClass = "Beta"
                }
            }; 
        }
        public static Classroom FillClassroomExample()
        {
            return new Classroom
            {
                Number = 97,
                NameClass = "Salsa"
            };
        }
        #endregion

        public static Classroom GetClassroom(int number, List<Classroom> classrooms)
        {
            return classrooms.FirstOrDefault(classroom => classroom.Number == number) ;
        }
        public static Classroom GetClassroom(string nameClass, List<Classroom> classrooms)
        {
            return classrooms.FirstOrDefault(classroom => classroom.NameClass.ToLowerInvariant().Equals(nameClass.ToLowerInvariant())) ;
        }
        public static bool AddStudent(Classroom classroom, Student student)
        {
            if (classroom == null || student == null) return false;
            student.Classroom = classroom;
            return true;
        }

    }
}
