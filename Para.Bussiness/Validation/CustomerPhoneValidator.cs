using FluentValidation;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerPhoneValidator : AbstractValidator<CustomerPhoneRequest>
    {
        public CustomerPhoneValidator()
        {
            RuleFor(x => x.CountyCode).NotEmpty().WithMessage("Country Code is required.").Length(3).WithMessage("Country Code is 3 digits.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required.").MaximumLength(10).WithMessage("Maximum length of phone is 10.");
        }
    }
}
