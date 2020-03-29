using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPSA_CSharp_Common;
using CIPSA_CSharp_Module9Console.Models;

namespace CIPSA_CSharp_Module9Console.Implementations
{
    public class StudentHelper
    {

        public static Hashtable SubjectsHashtable()
        {
            var subjectsOfCiencias = new List<string>()
            {
                "Filosofía",
                "Lengua",
                "Matemáticas"
            };
            var subjectsOfHumanidades = new List<string>()
            {
                "Economía",
                "Geografía",
                "Griego",
                "Historia del Arte"
            };
            var subjectsOfArtes = new List<string>()
            {
                "Historia del mundo",
                "Artes escénicas",
                "Diseño"
            };

            var subjects = new Hashtable
            {
                {AreasHighSchool.Ciencias.ToString(), subjectsOfCiencias },
                {AreasHighSchool.Humanidades, subjectsOfHumanidades},
                {AreasHighSchool.Artes, subjectsOfArtes},
            };

            return subjects;
        }

        public static bool AddStudentToClassroom(Classroom classroom, Student student)
        {
            if (classroom == null || student == null) return false;
            student.Classroom = classroom;
            return true;
        }

        public static void ShowSubjects(Student student)
        {
            SchoolHelper.ConsoleWriteLine($"Asignaturas que se encuentra el estudiante {student.Name}: ", Color.DarkGreen);
            foreach (var value in student.Subjects)
            {
                SchoolHelper.ConsoleWriteLine($"Area de {value.Area.ToString()} : {value.Name}", Color.DarkOliveGreen);
            }
        }

        public static int GetAgeToEvaluate(string message)
        {
            while (true)
            {
                Console.WriteLine($"{message}");
                var intToEvaluate = Console.ReadLine();
                if (string.IsNullOrEmpty(intToEvaluate))
                {
                    continue;
                }
                var evaluateAge = Helper.GetNumeric(intToEvaluate);
                if (evaluateAge == -1 || evaluateAge < 0 || evaluateAge > 120)
                {
                    continue;
                }

                return Convert.ToInt32(evaluateAge);
            }
        }

        public static char GetSexToEvaluate(string message)
        {
            while (true)
            {
                Console.WriteLine($"{message}");
                var stringToEvaluate = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(stringToEvaluate) ||
                    !char.TryParse(stringToEvaluate, out var sexResult))
                {
                    continue;
                }

                if (sexResult != 'H' && sexResult != 'M')
                {
                    continue;
                }

                return sexResult;
            }
        }
    }
}
