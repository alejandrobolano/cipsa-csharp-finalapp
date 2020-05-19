using System;
using System.ComponentModel.DataAnnotations;
using VideoClub.Common.Model.Exceptions;

namespace VideoClub.Infrastructure.Repository.Entity
{
    public class Movie : Product
    {
       
        #region Public properties
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public int ProductionYear { get; set; }
        [Required]
        public int BuyYear { get; set; }

        #endregion
        
       
    }
}
