using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module7Console.Implementations
{
    public class CustomComparer :IComparer
    {
        private readonly IComparer _comparer = new Comparer(System.Globalization.CultureInfo.CurrentCulture);
        public int Compare(object x, object y)
        {
            return _comparer.Compare(Convert.ToInt32(x), Convert.ToInt32(y));
        }
    }
}
