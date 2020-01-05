using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemo.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MemberShipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        [Required]
        public Byte? MembershipTypeId { get; set; }

        [Display(Name="Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}
