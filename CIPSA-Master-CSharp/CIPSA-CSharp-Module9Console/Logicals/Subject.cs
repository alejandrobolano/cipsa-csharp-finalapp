using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module9Console.Logicals
{
    public class Subject
    {
        public string Name { get; set; }
        public string Area { get; set; }

        public Subject(string name, string area)
        {
            Name = name;
            Area = area;
        }
    }
}
