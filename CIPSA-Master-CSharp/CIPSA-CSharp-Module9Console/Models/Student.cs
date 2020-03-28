using System.Collections;
using System.Collections.Generic;
using CIPSA_CSharp_Module9Console.Implementations;

namespace CIPSA_CSharp_Module9Console.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public char Sex { get; set; }
        public int Age { get; set; }
        public int ExamNote { get; set; }
        public Hashtable Subjects { get; }

        public Classroom Classroom { get; set; }

        public Student()
        {
            Name = string.Empty;
            LastName = string.Empty;
            Sex = char.MinValue;
            Age = 0;
            ExamNote = 0;
            Subjects = StudentHelper.SubjectsHashtable();
        }

        public Student(string name, string lastName, char sex, int age)
        {
            Name = name;
            LastName = lastName;
            Sex = sex;
            Age = age;
            Subjects = StudentHelper.SubjectsHashtable();
        }

        

    }
}
