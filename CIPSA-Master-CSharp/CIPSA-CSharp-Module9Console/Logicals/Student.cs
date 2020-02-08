using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module9Console.Logicals
{
    public class Student
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public char Sex { get; set; }
        public int Age { get; set; }
        public int ExamNote { get; set; }
        public Hashtable Subjects { get; }

        public Student()
        {
            Name = string.Empty;
            LastName = string.Empty;
            Sex = char.MinValue;
            Age = 0;
            ExamNote = 0;
            Subjects = SubjectsHashtable();
        }

        public Student(string name, string lastName, char sex, int age)
        {
            Name = name;
            LastName = lastName;
            Sex = sex;
            Age = age;
            Subjects = SubjectsHashtable();
        }

        public Hashtable SubjectsHashtable()
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


    }
}
