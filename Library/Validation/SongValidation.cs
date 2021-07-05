using NewProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Validation
{
    public class SongValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if (value != null)
            {
                Song song = value as Song;

                if (song != null)
                {
                    for (int symbol = 0; symbol < song.Title.Length; symbol++)
                    {
                        if (symbol >= '1' && symbol <= '9')
                        {
                            return new ValidationResult(FormatErrorMessage(this.ErrorMessage = "there cannot be numbers in the title"));
                        }
                    }


                }
                if (song.DurationMs < 100)
                {
                    return new ValidationResult(FormatErrorMessage(this.ErrorMessage));
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(this.ErrorMessage = "empty song"));
                }
            }
            return ValidationResult.Success;
        }
    }
}
