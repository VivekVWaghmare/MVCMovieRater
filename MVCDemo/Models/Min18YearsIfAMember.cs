using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class Min18YearsIfAMember: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == MemberShipType.Unknown || customer.MembershipTypeId == MemberShipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if(customer.BirthDate == null)
            {
                return new ValidationResult("Birthdate is required");
            }

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be atleast 18 years old to go on membership");


        }
    }
}
