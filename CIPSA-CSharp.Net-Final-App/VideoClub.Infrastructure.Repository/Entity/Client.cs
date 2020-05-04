using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Utils;
using VideoClub.Infrastructure.Repository.Contracts;

namespace VideoClub.Infrastructure.Repository.Entity
{
    public class Client : IEntity
    {
        #region Private properties

        private string _name;
        private string _lastName;
        private string _phoneContact;
        private string _phoneAux;

        #endregion

        #region Public properties
        [Key]
        [Column(Order = 1)]
        [Index(IsUnique = true)]
        public string Id { get; set; }
        [Required]
        public string Name
        {
            get => _name;
            set
            {
                CommonHelper.ValidateStringClient(value);
                _name = value;
            }
        }
        [Required]
        public string LastName
        {
            get => _lastName;
            set
            {
                CommonHelper.ValidateStringClient(value);
                _lastName = value;
            }
        }
        [Required]
        public string Address { get; set; }
        public AccreditationEnum AccreditationType { get; set; }
        [Key]
        [Column(Order = 2)]
        [Index(IsUnique = true)]
        public string Accreditation { get; set; }
        [Required]
        public string PhoneContact
        {
            get => _phoneContact;
            set
            {
                //CommonHelper.ValidatePhoneNumberClient(value);
                _phoneContact = value;
            }
        }
        public string PhoneAux
        {
            get => _phoneAux;
            set
            {
                //if (string.IsNullOrEmpty(value))
                //{
                //    CommonHelper.ValidatePhoneNumberClient(value);
                //}
                _phoneAux = value;
            }
        }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public StateClientEnum State { get; set; }
        public int RentalQuantity { get; set; }
        public bool IsVip { get; set; }
        public int Discount { get; set; }

        #endregion

        
    }
}
