using System;
using System.ComponentModel.DataAnnotations;
using VideoClub.Common.Model.Exceptions;
using VideoClub.Common.Model.Utils;

namespace VideoClub.Infrastructure.Repository.Entity
{
    public class Movie : Product
    {
        #region Private properties

        private int _buyYear;
        private int _productionYear;
        private readonly int _yearToday = DateTime.Today.Year;

        #endregion

        #region Public properties
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public int ProductionYear
        {
            get => _productionYear;
            set
            {
                if (value > _yearToday)
                {
                    throw new InvalidYearException(value);
                }
                _productionYear = value;
            }
        }
        [Required]
        public int BuyYear
        {
            get => _buyYear;
            set
            {
                if (value > _yearToday)
                {
                    throw new InvalidYearException(value);
                }

                _buyYear = value;
            }
        }

        #endregion
        
        public Movie()
        {
            if (ProductionYear > BuyYear)
            {
                throw new InvalidCompareYearException();
            }
        }

    }
}
