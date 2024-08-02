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
    public record CreateAccountTransactionCommand(AccountTransactionRequest Request) : IRequest<ApiResponse<AccountTransactionResponse>>;
    public record UpdateAccountTransactionCommand(int AccountTransactionId, AccountTransactionRequest Request) : IRequest<ApiResponse>;
    public record DeleteAccountTransactionCommand(int AccountTransactionId) : IRequest<ApiResponse>;

    public record GetAllAccountTransactionQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
    public record GetAccountTransactionByIdQuery(int AccountTransactionId) : IRequest<ApiResponse<AccountTransactionResponse>>;
    public record GetAccountTransactionByCustomerIdQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
}
