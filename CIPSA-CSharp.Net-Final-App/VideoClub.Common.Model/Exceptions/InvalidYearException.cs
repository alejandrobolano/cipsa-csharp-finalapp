using System;

namespace VideoClub.Common.Model.Exceptions
{
    [Serializable]
    public class InvalidYearException : Exception
    {
        /// <summary>
        /// Return an exception because the year entered is bigger than the actual year
        /// </summary>
        public InvalidYearException()
            : base($"Imposible que el año introducido sea superior al actual")
        {

        }

        /// <summary>
        /// Return an exception because the year entered is bigger than the actual year
        /// </summary>
        /// <param name="year"></param>
        public InvalidYearException(int year)
            : base($"Imposible que el año introducido {year} sea superior al actual")
        {
            
        }
    }

    [Serializable]
    public class InvalidCompareYearException : Exception
    {
        /// <summary>
        /// Return an exception because the production´s year entered is bigger than the buy´s year
        /// </summary>
        public InvalidCompareYearException()
            : base("Imposible año de producción sea mayor que el año de compra del producto")
        {

        }
    }
}
