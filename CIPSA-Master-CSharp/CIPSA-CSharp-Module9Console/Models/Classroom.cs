using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module9Console.Models
{
    public class Classroom
    {
        public int Number { get; set; }
        public string NameClass { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Classroom classroom) && Number == classroom.Number && string.Equals(NameClass, classroom.NameClass);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Number * 397) ^ (NameClass != null ? NameClass.GetHashCode() : 0);
            }
        }
    }
}
