using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.BrandName).MinimumLength(3).WithMessage("Marka adı en az 3 karakter uzunluğunda olmalıdır.");
        }
    }
}
