using FluentValidation;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.").MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.").MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(x => x.IdentityNumber).NotEmpty().WithMessage("Identity Number is required.").Length(11).WithMessage("Identity number must be 11 characters.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Valid email is required.");
            //RuleFor(x => x.CustomerNumber).GreaterThan(0).WithMessage("Customer Number must be greater than 0.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required.");
        }
    }
}
