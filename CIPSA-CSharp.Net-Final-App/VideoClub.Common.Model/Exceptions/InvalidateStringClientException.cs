using System;

namespace VideoClub.Common.Model.Exceptions
{
    public class InvalidateStringClientException : Exception
    {
        /// <summary>
        /// Return an exception when the string entered is not just alphabetic string
        /// </summary>
        public InvalidateStringClientException()
            : base($"Invalid string of client")
        {

        }
        /// <summary>
        /// Return an exception when the string entered is not just alphabetic string
        /// </summary>
        /// <param name="value"></param>
        public InvalidateStringClientException(string value)
            : base($"Invalid string of client: {value}")
        {

        }
    }
}
