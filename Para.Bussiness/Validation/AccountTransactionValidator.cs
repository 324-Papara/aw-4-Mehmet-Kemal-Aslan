using FluentValidation;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class AccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
    {
        public AccountTransactionValidator()
        {

        }
    }
}
