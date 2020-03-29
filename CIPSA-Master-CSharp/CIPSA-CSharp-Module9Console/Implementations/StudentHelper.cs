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
        #region Métodos para rellenar objetos por defecto para la respuesta de los ejercicios del módulo 9
        public static List<Student> FillStudentsExample()
        {
            var classrooms = ClassroomHelper.FillClassroomsExample();
            return new List<Student>{
                new Student
                {
                    Name = "Alejandro",
                    LastName = "Bolaño",
                    Age = 27,
                    Sex = 'H',
                    ExamNote = 10,
                    Classroom = classrooms.FirstOrDefault(classroom => classroom.Number == 99)
                },
                new Student
                {
                    Name = "Fernanda",
                    LastName = "Perez",
                    Age = 25,
                    Sex = 'M',
                    ExamNote = 6,
                    Classroom = classrooms.FirstOrDefault(classroom => classroom.Number == 98)
                },
                new Student
                {
                    Name = "Susana",
                    LastName = "Gonzalez",
                    Age = 26,
                    Sex = 'M',
                    ExamNote = 7,
                    Classroom = classrooms.FirstOrDefault(classroom => classroom.Number == 98)
                },
                new Student
                {
                    Name = "Lia",
                    LastName = "Bolaño",
                    Age = 23,
                    Sex = 'M',
                    ExamNote = 10,
                    Classroom = classrooms.FirstOrDefault(classroom => classroom.Number == 99)
                },
                new Student
                {
                    Name = "Leydi",
                    LastName = "Bolaño",
                    Age = 24,
                    Sex = 'M',
                    ExamNote = 8,
                    Classroom = classrooms.FirstOrDefault(classroom => classroom.Number == 99)
                },
                new Student
                {
                    Name = "Manuel",
                    LastName = "Méndez",
                    Age = 27,
                    Sex = 'H',
                    ExamNote = 10,
                    Classroom = classrooms.FirstOrDefault(classroom => classroom.Number == 99)
                },
            };
        }
        
        #endregion


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
        public static decimal GetAverageOfNotes(List<Student> students)
        {
            return Convert.ToDecimal(students.Sum(student => student.ExamNote)) / students.Count;
        }

        public static int GetMensQuantity(List<Student> students)
        {
            return students.Count(student => student.Sex.ToString().ToUpperInvariant().Equals("H"));
        }
        public static int GetWomensQuantity(List<Student> students)
        {
            return students.Count(student => student.Sex.ToString().ToUpperInvariant().Equals("M"));
        }

        public static List<Student> GetStudentsByClassroom(List<Student> students, Classroom classroom)
        {
            return students.Where(student => student.Classroom.Equals(classroom)).ToList();
        }
    }
}
