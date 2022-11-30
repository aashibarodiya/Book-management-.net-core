using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.ModelValidators
{
    public class ExistingAuthorId: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || value == "")
                return ValidationResult.Success;

            var id= value as string;
            var service = validationContext.GetService(typeof(IAuthorService)) as IAuthorService;

            if (service == null)
                throw new ArgumentException("FATAL Error: Not Author Service configured");

            try
            {
                var author = service.GetAuthorById(id).Result;
                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult($"Invalid Author Id '{id}'");
            }

        }
    }
}
