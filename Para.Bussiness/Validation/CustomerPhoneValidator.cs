﻿using FluentValidation;
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
            RuleFor(x => x.CountyCode).NotEmpty().Length(3).WithMessage("Country Code is required.");
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(10).WithMessage("Phone is required.");
        }
    }
}
