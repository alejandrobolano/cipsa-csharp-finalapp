using System;
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

        public Student()
        {
            Name = string.Empty;
            LastName = string.Empty;
            Sex = char.MinValue;
            Age = 0;
            ExamNote = 0;
        }

        public Student(string name, string lastName, char sex, int age)
        {
            Name = name;
            LastName = lastName;
            Sex = sex;
            Age = age;
        }


    }
}
