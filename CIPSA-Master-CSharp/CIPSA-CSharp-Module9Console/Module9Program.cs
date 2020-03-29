using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CIPSA_CSharp_Common;
using CIPSA_CSharp_Module9Console.Implementations;
using CIPSA_CSharp_Module9Console.Models;
using Console = Colorful.Console;

namespace CIPSA_CSharp_Module9Console
{
    internal class Module9Program
    {
        private const string StudentName = "Nombre del estudiante:";
        private const string StudentLastname = "Apellidos del estudiante:";
        private const string StudentAge = "Edad del estudiante:";
        private const string StudentSex = "Sexo del estudiante: (H/M)";
        private static List<Classroom> _classrooms;
        private static List<Student> _students;

        private static void Main(string[] args)
        {
            _classrooms = new List<Classroom>();
            _students = new List<Student>();
            SchoolHelper.ShowInformationAboutApp();
            AddClassroom(true);
            BasicWorkWithStudents();

            #region Ejercicio 6
            ShowAverageOfExamNotes();
            ShowQuantityBySex();
            AddMoreStudents();
            #endregion

            #region Ejercicio 7
            ShowInformationOfExampleOfAcademy();
            #endregion

        }

        private static void ShowInformationOfExampleOfAcademy()
        {
            Console.WriteLine("¿Desea mostrar la información cargada por defecto de la Academia? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                var academies = AcademyHelper.FillAcademies();
                var students = StudentHelper.FillStudentsExample();

                var averageOfClassrooms = AcademyHelper.GetAverageOfClassroom(academies);
                var quantityOfClassrooms = AcademyHelper.GetQuantityOfClassroom(academies);
                SchoolHelper.ConsoleWriteLine($"El promedio de aulas por academias es de: {averageOfClassrooms} " +
                                              $"para un total de {quantityOfClassrooms} aulas y {academies.Count} academias",Color.Aquamarine);

                var mensQuantity = AcademyHelper.GetMensQuantity(academies, students);
                var womensQuantity = AcademyHelper.GetWomensQuantity(academies, students);

                SchoolHelper.ConsoleWriteLine($"El promedio en general de hombre es de: {mensQuantity} " +
                                              $"y de mujeres es de: {womensQuantity}", Color.Aquamarine);


            }
        }

        private static void AddMoreStudents()
        {
            while (true)
            {
                Console.WriteLine("¿Desea agregar más estudiantes? si(s) / no(n)");
                var decision = Console.ReadLine()?.ToLower();
                if (decision != null && (decision.Equals("s") || decision.Equals("si")))
                {
                    var newStudent = AddStudentWithConstructorDefault();
                    if (!_classrooms.Any())
                    {
                        SchoolHelper.ConsoleWriteLine("No existe ninguna clase aún,¿Desea agregar nueva aula? si(s) / no(n)", Color.White);
                        var decisionClassroom = Console.ReadLine()?.ToLower();
                        if (decisionClassroom != null && (decisionClassroom.Equals("s") || decisionClassroom.Equals("si")))
                        {
                            AddClassroom(false);
                        }
                    }
                    AddPossibleStudentIntoClassroom(newStudent);
                    ShowBasicInfoStudent(newStudent,false);
                }
                break;
            }

        }

        private static void ShowAverageOfExamNotes()
        {
            var average = StudentHelper.GetAverageOfNotes(_students);
            if (average == 0) return;
            Console.WriteLine("¿Quiere ver el promedio de las notas de los exámenes? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                SchoolHelper.ConsoleWriteLine($"El promedio de las notas es: {average}", Color.White);
            }

        }
        private static void ShowQuantityBySex()
        {
            Console.WriteLine("¿Quiere ver la cantidad de hombres y mujeres que existe? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                var mens = StudentHelper.GetMensQuantity(_students);
                var womens = StudentHelper.GetWomensQuantity(_students);
                SchoolHelper.ConsoleWriteLine($"La cantidad de hombres que hay es: {mens}" +
                                              $" y la cantidad de mujeres es: {womens}" +
                                              $" para un total de {_students.Count} estudiantes", Color.White);

            }
        }

        private static void AddStudentToClassroom(Student student)
        {
            while (true)
            {
                SchoolHelper.ConsoleWriteLine($"Diga el nombre o número del aula al que desea agregar el estudiante {student.Name}", Color.White);
                var classroomInput = Console.ReadLine();
                if (!classroomInput.Equals(string.Empty))
                {
                    var valueClassroom = Helper.GetNumeric(classroomInput);
                    Classroom classroom;
                    if (valueClassroom == -1)
                    {
                        classroom = ClassroomHelper.GetClassroom(classroomInput, _classrooms);
                        if (classroom == null)
                        {
                            SchoolHelper.ConsoleWriteLine($"No existe una aula con el ese nombre, intente nuevamente",
                                Color.White);
                            continue;
                        }
                    }
                    else
                    {
                        classroom = ClassroomHelper.GetClassroom(valueClassroom, _classrooms);
                        if (classroom == null)
                        {
                            SchoolHelper.ConsoleWriteLine($"No existe una aula con el ese nombre, intente nuevamente",
                                Color.White);
                            continue;
                        }
                    }
                    ClassroomHelper.AddStudent(classroom, student);
                }
                break;
            }

        }

        private static void AddClassroom(bool showIntro)
        {
            if (showIntro)
            {
                SchoolHelper.ConsoleWriteLine("Antes de todo, escriba nuevas aulas: ", Color.White);
            }
            var numberIterator = 1;
            while (true)
            {
                SchoolHelper.ConsoleWriteLine($"Diga el nombre del aula {numberIterator} y en caso de no seguir, deje en blanco", Color.White);
                var nameClass = Console.ReadLine();
                if (nameClass != null && nameClass.Equals(string.Empty))
                {
                    break;
                }
                if (Helper.GetNumeric(nameClass) != -1) continue;
                if (ClassroomHelper.GetClassroom(nameClass, _classrooms) != null)
                {
                    SchoolHelper.ConsoleWriteLine($"Ya existe una aula con el nombre {nameClass}, intente con otro nombre",
                        Color.White);
                    continue;
                }
                _classrooms.Add(new Classroom()
                {
                    Number = numberIterator,
                    NameClass = nameClass
                });
                numberIterator++;
            }

        }

        private static void BasicWorkWithStudents()
        {

            var firstStudent = AddStudentWithConstructorDefault();
            _students.Add(firstStudent);
            AddPossibleStudentIntoClassroom(firstStudent);
            ShowBasicInfoStudent(firstStudent, false);

            var secondStudent = AddStudentWithParamsInConstructor();
            _students.Add(secondStudent);
            AddPossibleStudentIntoClassroom(secondStudent);
            ShowBasicInfoStudent(secondStudent, false);

            TryAddNotesAndShowInformation();
        }

        private static void TryAddNotesAndShowInformation()
        {
            Console.WriteLine("¿Quiere introducir las notas de los estudiantes? si(s) / no(n)");
            var decision = Console.ReadLine()?.ToLower();
            if (decision != null && (decision.Equals("s") || decision.Equals("si")))
            {
                AddExamNote(_students);
                Console.WriteLine("A continuación se muestra los datos de los alumnos con sus notas", Color.DarkGreen);
                foreach (var student in _students)
                {
                    ShowFullInfoStudent(student);
                }
            }
            else
            {
                foreach (var student in _students)
                {
                    ShowBasicInfoStudent(student, true);
                }
            }
        }

        private static void AddPossibleStudentIntoClassroom(Student student)
        {
            if (_classrooms.Any())
            {
                Console.WriteLine("¿Quiere agregar el estudiante en alguna clase? si(s) / no(n)");
                var decision = Console.ReadLine()?.ToLower();
                if (decision != null && (decision.Equals("s") || decision.Equals("si")))
                {
                    AddStudentToClassroom(student);
                }
            }

        }

        private static void AddExamNote(IEnumerable<Student> students)
        {
            Console.WriteLine($"Para la notas tenga en cuenta que debe estar entre los valores 0 y 10, " +
                              $"incluyendo ambos números" +
                              $" y en caso de ser fuera de rango, se toma el más próximo)", Color.DarkOrange);

            foreach (var student in students)
            {
                var note = AddNote(student.Name);
                student.ExamNote = note;
            }

        }

        private static int AddNote(string name)
        {
            Console.WriteLine($"Escriba la nota de {name}: ");
            var note = Helper.GetNumeric(Console.ReadLine());
            if (note == -1)
            {
                AddNote(name);
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

        private static Student AddStudentWithConstructorDefault()
        {
            Console.WriteLine("Complete los siguientes datos: (Constructor predeterminado)");
            var student = new Student();
            var name = SchoolHelper.GetStringToEvaluate(StudentName);
            student.Name = name;
            var lastName = SchoolHelper.GetStringToEvaluate(StudentLastname);
            student.LastName = lastName;
            var sex = StudentHelper.GetSexToEvaluate(StudentSex);
            student.Sex = sex;
            var age = StudentHelper.GetAgeToEvaluate(StudentAge);
            student.Age = age;
            return student;
        }
        private static Student AddStudentWithParamsInConstructor()
        {
            Console.WriteLine("Complete los siguientes datos para el próximo estudiante: (Constructor con params)");
            var name = SchoolHelper.GetStringToEvaluate(StudentName);
            var lastName = SchoolHelper.GetStringToEvaluate(StudentLastname);
            var sex = StudentHelper.GetSexToEvaluate(StudentSex);
            var age = StudentHelper.GetAgeToEvaluate(StudentAge);
            return new Student(name, lastName, sex, age);
        }

        private static void ShowBasicInfoStudent(Student student, bool showSubjects)
        {
            var classroom = student.Classroom == null
                ? "Sin aula asignada aún"
                : student.Classroom.NameClass;
            Console.WriteLine($"Datos del estudiante:" +
                              $"\n Nombre: {student.Name}" +
                              $"\n Apellido: {student.LastName}" +
                              $"\n Sexo: {student.Sex.ToString().ToUpper()}" +
                              $"\n Edad: {student.Age}" +
                              $"\n Clase: {classroom}" +
                              $"\n ", Color.DarkBlue);

            if (showSubjects && student.Subjects.Any())
            {
                StudentHelper.ShowSubjects(student);
                Console.WriteLine();
            }

        }
        private static void ShowFullInfoStudent(Student student)
        {
            var classroom = student.Classroom == null
                ? "Sin aula asignada aún"
                : student.Classroom.NameClass;
            Console.WriteLine($"Datos del estudiante:" +
                              $"\n Nombre: {student.Name}" +
                              $"\n Apellido: {student.LastName}" +
                              $"\n Sexo: {student.Sex.ToString().ToUpper()}" +
                              $"\n Edad: {student.Age}" +
                              $"\n Nota del examen: {student.ExamNote}" +
                              $"\n Clase: {classroom}" +
                              $"\n ", Color.DarkGreen);

            StudentHelper.ShowSubjects(student);
            Console.WriteLine();
        }

    }
}
