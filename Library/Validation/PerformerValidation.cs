using NewProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Validation
{
    public class PerformerValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //nickname birthdate
            if (value != null)
            {
                Performer performer = value as Performer;
                if (performer != null)
                {
                    if (value != null)
                    {
                        string userName = value.ToString();
                        for (int i = 0; i < userName.Length; i++)
                        {
                            char symbol = userName[i];
                            if ((symbol >= 'a' && symbol <= 'z') || (symbol >= 'A' && symbol <= 'Z'))
                            {
                                this.ErrorMessage = "Имя не должно содержать латинские символы";
                                return new ValidationResult(FormatErrorMessage(ErrorMessage));
                            }
                        }
                        return ValidationResult.Success;
                    }
                    return new ValidationResult(FormatErrorMessage(this.ErrorMessage = "empty value"));
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(this.ErrorMessage = "empty performer"));
                }
            }
            return ValidationResult.Success;
        }
    }
}
