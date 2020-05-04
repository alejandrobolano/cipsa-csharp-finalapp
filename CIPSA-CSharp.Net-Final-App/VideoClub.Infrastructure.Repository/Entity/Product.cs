using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoClub.Common.Model.Enums;
using VideoClub.Infrastructure.Repository.Contracts;

namespace VideoClub.Infrastructure.Repository.Entity
{
    public class Product : IEntity
    {
        [Key]
        [Column(Order = 1)]
        [Index(IsUnique = true)]
        public string Id { get; set; }
        [Key]
        [Column(Order = 2)]
        [Index(IsUnique = true)]
        public string Title { get; set; }
        public int QuantityDisc { get; set; }
        public StateProductEnum State { get; set; }
        [Required]
        public decimal Price { get; set; }



    }
}
