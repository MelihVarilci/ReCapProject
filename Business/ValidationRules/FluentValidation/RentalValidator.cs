using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(ren => ren.CustomerId).NotEmpty();
            RuleFor(ren => ren.CarId).NotEmpty();
            RuleFor(ren => ren.RentDate).NotEmpty();
        }
    }
}
