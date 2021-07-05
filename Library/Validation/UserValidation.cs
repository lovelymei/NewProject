using NewProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Validation
{
    public class UserValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //name surname birthdate
            if (value != null)
            {
                Listener user = value as Listener;
                if (user != null)
                {
                    if ((user.Name.Length < 2) || (user.Surname.Length < 2))
                        return new ValidationResult(FormatErrorMessage(this.ErrorMessage = "too small name(surname)"));
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(this.ErrorMessage = "empty user"));
                }
            }
            return ValidationResult.Success;
        }
    }
}
