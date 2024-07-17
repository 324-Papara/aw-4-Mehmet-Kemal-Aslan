using FluentValidation;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerDetailValidator : AbstractValidator<CustomerDetailRequest>
    {
        public CustomerDetailValidator()
        {
            RuleFor(x => x.FatherName).NotEmpty().WithMessage("Father Name is required.").MaximumLength(50).WithMessage("Father name cannot exceed 50 characters."); ;
            RuleFor(x => x.MotherName).NotEmpty().WithMessage("Mother Name is required.").MaximumLength(50).WithMessage("Mother name cannot exceed 50 characters."); ;
            RuleFor(x => x.EducationStatus).NotEmpty().WithMessage("Education Status is required.");
            RuleFor(x => x.MontlyIncome).NotEmpty().WithMessage("Montly Income is required.");
            RuleFor(x => x.Occupation).NotEmpty().WithMessage("Occupation is required.");
        }
    }
}
