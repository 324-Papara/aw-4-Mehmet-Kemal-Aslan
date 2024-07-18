using FluentValidation;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerByNameValidator : AbstractValidator<string>
    {
        public CustomerByNameValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("at least 1 character")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.")
                .Matches(@"^[^0-9]*$").WithMessage("First name cannot contain numbers."); ;
        }
    }
}
