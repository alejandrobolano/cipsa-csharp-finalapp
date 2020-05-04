using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoClub.Common.Model.Utils;
using VideoClub.Infrastructure.Repository.Contracts;

namespace VideoClub.Infrastructure.Repository.Entity
{
    public class Rental : IEntity
    {
        [Key]
        [Index(IsUnique = true)]
        public string Id { get; set; }

        [ForeignKey("Product")]
        [Column(Order = 1)]
        public string ProductId { get; set; }
        [ForeignKey("Product")]
        [Column(Order = 2)]
        public string ProductTitle { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Client")]
        [Column(Order = 3)]
        public string ClientId { get; set; }
        [ForeignKey("Client")]
        [Column(Order = 4)]
        public string ClientAccreditation { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        public DateTime StartRental { get; set; }
        [Required]
        public DateTime FinishRental { get; set; }
        
    }
}
