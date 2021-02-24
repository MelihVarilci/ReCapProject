using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(cus => cus.UserId).NotEmpty();
            RuleFor(cus => cus.CompanyName).MinimumLength(2).WithMessage("Şirket adı en az 2 karakter uzunluğunda olmalıdır.");
        }
    }
}
