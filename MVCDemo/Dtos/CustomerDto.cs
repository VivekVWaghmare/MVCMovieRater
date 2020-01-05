using MVCDemo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemo.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Required]
        public Byte? MembershipTypeId { get; set; }

        public MemberShipTypeDto MembershipType { get; set; }

        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}
