using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.ModelValidators
{
    public class AuthorAgeRangeAttribute:ValidationAttribute
    {
        public int Minimum { get; set; } = 1;
        public int Maximum { get; set; } = 0; //No Limit

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //get a reference to author object
            var author = validationContext.ObjectInstance as Author;

            if (author == null) //current object is not an author
                return ValidationResult.Success; //I don't care. It's not my job

            var lastDate = author.DeathDate ?? DateTime.Today;

            var age = (lastDate - author.BirthDate).TotalDays / 365; //approx age in years

            if (age < Minimum)
            {
                return new ValidationResult($"Age should be minimum {Minimum} years. Found {(int)age} years");
            }
            else if (Maximum > 0 && age > Maximum)
            {
                return new ValidationResult($"Age shouldn't be more than {Maximum} years. Found {(int)age} years");
            }
            else
                return ValidationResult.Success; //well we are good to go
        }
    }
}
