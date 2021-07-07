using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Validation
{
    public class MyRangeAttribute : Attribute, IModelValidator
    {
        public int MaxNum { get; set; }
        public int MinNum { get; set; }
        public string ErrorMessage { get; set; }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (!(context.Model is int)) return Enumerable.Empty<ModelValidationResult>();

            var number = (int)context.Model;

            if (number < MinNum || number > MaxNum)
            {
                return new List<ModelValidationResult>
                    {
                        new ModelValidationResult(context.ModelMetadata.PropertyName, $"{ErrorMessage} ({MinNum}..{MaxNum})")
                    };
            }

            return Enumerable.Empty<ModelValidationResult>();
        }

    }
}
