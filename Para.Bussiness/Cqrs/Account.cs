using MediatR;
using Para.Base.Response;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs
{
    public record CreateAccountCommand(AccountRequest Request) : IRequest<ApiResponse<AccountResponse>>;
    public record UpdateAccountCommand(int AccountId, AccountRequest Request) : IRequest<ApiResponse>;
    public record DeleteAccountCommand(int AccountId) : IRequest<ApiResponse>;

    public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
    public record GetAccountByIdQuery(int AccountId) : IRequest<ApiResponse<AccountResponse>>;
    public record GetAccountByCustomerIdQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
}
