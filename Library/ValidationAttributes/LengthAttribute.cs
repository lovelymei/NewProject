using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Validation
{
    public class LengthAttribute : Attribute, IModelValidator
    {
        public int MaxLen { get; set; }
        public int MinLen { get; set; }
        public string ErrMes { get; set; }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (!(context.Model is string)) return Enumerable.Empty<ModelValidationResult>();

            var str = (string)context.Model;

            if (str.Length < MinLen || str.Length > MaxLen)
            {
                return new List<ModelValidationResult>
                    {
                       new ModelValidationResult(context.ModelMetadata.PropertyName, $"{ErrMes} ({MinLen}..{MaxLen})")
                    };
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}
