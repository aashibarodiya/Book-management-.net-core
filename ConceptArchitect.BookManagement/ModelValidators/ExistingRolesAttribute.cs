using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.ModelValidators
{
    public class ExistingRolesAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is List<string>)
                return IsExistingRoles(value as List<string>, validationContext);
            else if (value is List<Role>)
            {
                var roles = value as List<Role>;
                var roleNames = roles.Select(r => r.Name).ToList();
                return IsExistingRoles(roleNames, validationContext);
            }
            else
                return ValidationResult.Success;
        }

        public ValidationResult? IsExistingRoles(List<string> roles, ValidationContext validationContext)
        {
            var service = validationContext.GetService(typeof(IUserService)) as IUserService;
            if (service == null)
                throw new ArgumentException("FATAL Error. User Service Not Found");

            var existingRoles = service.GetAllRoles().Result;
            foreach(var role in roles)
            {
                if (!existingRoles.Contains(role))
                    return new ValidationResult($"Invalid Role :{role}");
            }

            return ValidationResult.Success;
        }


    }
}
