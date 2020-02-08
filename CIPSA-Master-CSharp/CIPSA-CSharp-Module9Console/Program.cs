using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CIPSA_CSharp_Common;
using CIPSA_CSharp_Module9Console.Logicals;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module9Console
{
    class Program
    {
        private static readonly string STUDENT_NAME = "Nombre del estudiante:";
        private static readonly string STUDENT_LASTNAME = "Apellidos del estudiante:";
        private static readonly string STUDENT_AGE = "Edad del estudiante:";
        private static readonly string STUDENT_SEX = "Sexo del estudiante: (H/M)";
        static void Main(string[] args)
        {
            var students = new List<Student>();
            var firstStudent = FirstStudent();
            ShowBasicInfoStudent(firstStudent);
            students.Add(firstStudent);
            var secondStudent = SecondStudent();
            ShowBasicInfoStudent(secondStudent);
            students.Add(secondStudent);


            Console.WriteLine("¿Quiere introducir las notas de los estudiantes? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                SetExamNote(students);
                Console.WriteLine("A continuación se muestra los datos de los alumnos con sus notas", Color.DarkGreen);
                foreach (var student in students)
                {
                    ShowFullInfoStudent(student);
                }

            }
            else
            {
                foreach (var student in students)
                {
                    ShowBasicInfoStudentWithSubjects(student);
                }
                
            }
        }

        private static void SetExamNote(List<Student> students)
        {
            Console.WriteLine($"Para la notas tenga en cuenta que debe estar entre los valores 0 y 10, " +
                              $"incluyendo ambos números" +
                              $" y en caso de ser fuera de rango, se toma el más próximo)", Color.DarkOrange);

            foreach (var student in students)
            {
                var note = GetNote(student.Name);
                student.ExamNote = note;
            }

        }

        private static int GetNote(string name)
        {
            Console.WriteLine($"Escriba la nota de {name}: ");
            var note = Helper.GetNumeric(Console.ReadLine());
            if (note == -1)
            {
                GetNote(name);
            }
            if (note < 0)
            {
                note = 0;
            }
            else if (note > 10)
            {
                note = 10;
            }

            return note;
        }

        private static Student FirstStudent()
        {
            Console.WriteLine("Complete los siguientes datos: (Constructor predeterminado)");
            var student = new Student();
            var name = GetStringToEvaluate(STUDENT_NAME);
            student.Name = name;
            var lastName = GetStringToEvaluate(STUDENT_LASTNAME);
            student.LastName = lastName;
            var sex = GetSexToEvaluate(STUDENT_SEX);
            student.Sex = sex;
            var age = GetAgeToEvaluate(STUDENT_AGE);
            student.Age = age;
            return student;
        }
        private static Student SecondStudent()
        {
            Console.WriteLine("Complete los siguientes datos para el próximo estudiante: (Constructor con params)");
            var name = GetStringToEvaluate(STUDENT_NAME);
            var lastName = GetStringToEvaluate(STUDENT_LASTNAME);
            var sex = GetSexToEvaluate(STUDENT_SEX);
            var age = GetAgeToEvaluate(STUDENT_AGE);
            return new Student(name, lastName, sex, age);
        }

        private static string GetStringToEvaluate(string message)
        {
            while (true)
            {
                Console.WriteLine($"{message}");
                var stringToEvaluate = Console.ReadLine();
                if (string.IsNullOrEmpty(stringToEvaluate) || stringToEvaluate.Any(char.IsDigit))
                {
                    continue;
                }

                return stringToEvaluate;
            }
        }
        private static char GetSexToEvaluate(string message)
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

                if (sexResult != 'H' && sexResult !='M')
                {
                    continue;
                }

                return sexResult;
            }
        }

        private static int GetAgeToEvaluate(string message)
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

        private static void ShowBasicInfoStudent(Student student)
        {
            Console.WriteLine($"Datos del estudiante:" +
                              $"\n Nombre: {student.Name}" +
                              $"\n Apellido: {student.LastName}" +
                              $"\n Sexo: {student.Sex.ToString().ToUpper()}" +
                              $"\n Edad: {student.Age}" +
                              $"\n ", Color.DarkBlue);
        }
        private static void ShowFullInfoStudent(Student student)
        {
            Console.WriteLine($"Datos del estudiante:" +
                              $"\n Nombre: {student.Name}" +
                              $"\n Apellido: {student.LastName}" +
                              $"\n Sexo: {student.Sex.ToString().ToUpper()}" +
                              $"\n Edad: {student.Age}" +
                              $"\n Nota del examen: {student.ExamNote}" +
                              $"\n ", Color.DarkGreen);

            ShowSubjects(student);
            Console.WriteLine();
        }
        private static void ShowBasicInfoStudentWithSubjects(Student student)
        {
            Console.WriteLine($"Datos del estudiante:" +
                              $"\n Nombre: {student.Name}" +
                              $"\n Apellido: {student.LastName}" +
                              $"\n Sexo: {student.Sex.ToString().ToUpper()}" +
                              $"\n Edad: {student.Age}" +
                              $"\n ", Color.DarkGreen);

            ShowSubjects(student);
            Console.WriteLine();
        }

        private static void ShowSubjects(Student student)
        {
            Console.WriteLine($"Asignaturas que se encuentra el estudiante {student.Name}: ", Color.DarkGreen);
            foreach (var key in student.Subjects.Keys)
            {
                foreach (var value in (List<string>) student.Subjects[key])
                {
                    Console.WriteLine($"Area de {key} : {value}", Color.DarkOliveGreen);
                }
            }
        }
    }
}
