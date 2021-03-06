using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(us => us.Email).NotEmpty();
            RuleFor(us => us.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(us => us.LastName).NotEmpty().MinimumLength(2);
            //RuleFor(us => us.Password).NotEmpty().MinimumLength(5);

        }
    }
}
