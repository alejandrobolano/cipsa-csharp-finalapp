using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CIPSA_CSharp_Module9Console.Models;

namespace CIPSA_CSharp_Module9Console.Implementations
{
    internal class SchoolHelper
    {
        public static void ConsoleWriteLine(string message, Color color)
        {
            Console.WriteLine(message, color);
        }

        public static void ShowInformationAboutApp()
        {
            Colorful.Console.WriteAscii("App de Escuela");
            Colorful.Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════╗");
            Colorful.Console.WriteLine("║  Aplicación de consola de gestión de Escuela                                ║");
            Colorful.Console.WriteLine("║                                                                             ║");
            Colorful.Console.WriteLine("║  Relación entre objetos:                                                    ║");
            Colorful.Console.WriteLine("║    * Alumno   1 ------ * Clase                                              ║");
            Colorful.Console.WriteLine("║    * Alumno   1 ------ * Asignaturas                                        ║");
            Colorful.Console.WriteLine("║       - Para este ejemplo se usó una asignación simple                      ║");
            Colorful.Console.WriteLine("║                                                                             ║");
            Colorful.Console.WriteLine("║  Nota: Suposición en que las aulas son nombradas y no enumeradas            ║");
            Colorful.Console.WriteLine("║                                                                             ║");
            Colorful.Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
            ShowSpinner();
        }

        private static void ShowSpinner()
        {
            var counter = 0;
            for (var i = 0; i < 50; i++)
            {
                switch (counter % 4)
                {
                    case 0:
                        Console.Write("/");
                        break;
                    case 1:
                        Console.Write("-");
                        break;
                    case 2:
                        Console.Write("\\");
                        break;
                    case 3:
                        Console.Write("|");
                        break;
                }

                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                counter++;
                Thread.Sleep(50);
            }
        }

        public static List<Subject> FillSubjects()
        {
            var subjects = new List<Subject>()
            {
                new Subject()
                {
                    Name = "Filosofía",
                    Area = AreasHighSchool.Ciencias
                },
                new Subject()
                {
                    Name = "Lengua",
                    Area = AreasHighSchool.Ciencias
                },
                new Subject()
                {
                    Name = "Matemáticas",
                    Area = AreasHighSchool.Ciencias
                },
                new Subject()
                {
                    Name = "Economía",
                    Area = AreasHighSchool.Humanidades
                },
                new Subject()
                {
                    Name = "Geografía",
                    Area = AreasHighSchool.Humanidades
                },
                new Subject()
                {
                    Name = "Griego",
                    Area = AreasHighSchool.Humanidades
                },
                new Subject()
                {
                    Name = "Historia del Arte",
                    Area = AreasHighSchool.Humanidades
                },
                new Subject()
                {
                    Name = "Historia del mundo",
                    Area = AreasHighSchool.Artes
                },
                new Subject()
                {
                    Name = "Artes escénicas",
                    Area = AreasHighSchool.Artes
                },
                new Subject()
                {
                    Name = "Diseño",
                    Area = AreasHighSchool.Artes
                }
            };

            return subjects;
        }

        public static string GetStringToEvaluate(string message)
        {
            while (true)
            {
                Console.WriteLine($"{message}");
                var stringToEvaluate = Console.ReadLine();
                if (String.IsNullOrEmpty(stringToEvaluate) || stringToEvaluate.Any(Char.IsDigit))
                {
                    continue;
                }

                return stringToEvaluate;
            }
        }

        public static decimal GetAverageOfNotes(List<Student> students)
        {
            return Convert.ToDecimal(students.Sum(student => student.ExamNote))/students.Count;
        }

        public static int GetMensQuantity(List<Student> students)
        {
            return students.Count(student => student.Sex.ToString().ToUpperInvariant().Equals("H"));
        }
        public static int GetWomensQuantity(List<Student> students)
        {
            return students.Count(student => student.Sex.ToString().ToUpperInvariant().Equals("M"));
        }
    }
}
