using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.ModelValidators
{
    public class UniqueAuthorIdAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var id = value as string;
            if (string.IsNullOrEmpty(id))
                return ValidationResult.Success;

            var authorService =(IAuthorService) validationContext.GetService(typeof(IAuthorService));
            if (authorService == null)
                throw new ArgumentException("No Valid IAuthorService found");

            try
            {

                var author = authorService.GetAuthorById(id).Result;
                return new ValidationResult($"Duplicate Id. {id} is associated to {author.Name}");
            }
            catch
            {
                return ValidationResult.Success;
            }

        }
    }
}
