using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA_CSharp_Module4_4._3_1_to_8
{
    [Serializable]
    class MyCustomException : Exception
    {
        public MyCustomException() : base()
        { }

        public MyCustomException(string message) : base(message)
        { }

        public MyCustomException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public MyCustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
