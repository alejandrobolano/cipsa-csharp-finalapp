using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Module9Console.Models;

namespace CIPSA_CSharp_Module9Console.Implementations
{
    class AcademyHelper
    {
        #region Métodos para rellenar objetos por defecto para la respuesta de los ejercicios del módulo 9
        public static List<Academy> FillAcademies()
        {
            var academyExample = new Academy
            {
                Number = 98,
                Classrooms = new List<Classroom>()
            };
            AddClassroom(academyExample, ClassroomHelper.FillClassroomExample());

            return new List<Academy>()
            {
                new Academy()
                {
                    Number = 99,
                    Classrooms = ClassroomHelper.FillClassroomsExample()
                },
                academyExample
            };
        }

        #endregion

        public static bool AddClassroom(Academy academy, Classroom classroom)
        {
            if (academy == null || classroom == null) return false;
            academy.Classrooms.Add(classroom);
            return true;
        }

        public static int GetAverageOfClassroom(List<Academy> academies)
        {
            return GetQuantityOfClassroom(academies) / academies.Count;
        }
        public static int GetQuantityOfClassroom(List<Academy> academies)
        {
            return academies.Sum(academy => academy.Classrooms.Count);
        }

        public static int GetMensQuantity(List<Academy> academies, List<Student> students)
        {
            var quantity = 0;
            academies.ForEach(academy =>
                academy.Classrooms.ForEach(classroom =>
            {
                var studentsByClassroom = StudentHelper.GetStudentsByClassroom(students, classroom);
                quantity += StudentHelper.GetMensQuantity(studentsByClassroom);
            }));
            return quantity;
        }

        public static int GetWomensQuantity(List<Academy> academies, List<Student> students)
        {
            var quantity = 0;
            academies.ForEach(academy =>
                academy.Classrooms.ForEach(classroom =>
            {
                var studentsByClassroom = StudentHelper.GetStudentsByClassroom(students, classroom);
                quantity += StudentHelper.GetWomensQuantity(studentsByClassroom);
            }));
            return quantity;
        }
    }
}
