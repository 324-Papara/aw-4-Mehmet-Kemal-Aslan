using FluentValidation;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerAddressValidator : AbstractValidator<CustomerAddressRequest>
    {
        public CustomerAddressValidator()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.AddressLine).NotEmpty().WithMessage("Address Line is required.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip Code is required.");
        }
    }
}
