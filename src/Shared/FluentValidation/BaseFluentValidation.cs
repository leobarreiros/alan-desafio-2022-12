using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.Extensions.FluentValidation.DefaultsValidationMessages;

namespace Shared.FluentValidation
{
    public abstract class BaseFluentValidation<T> : AbstractValidator<T>
    {
        public ValidationResult TryValidate(T request)
        {
            if (request == null)
                return new ValidationResult(new List<ValidationFailure>()
                {
                    new ValidationFailure("Request", REQUEST_FORMATO_INVALIDO)
                });

            return base.Validate(request);
        }
    }
}
