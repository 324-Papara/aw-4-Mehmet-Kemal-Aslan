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
    public record CreateCustomerCommand(CustomerRequest Request) : IRequest<ApiResponse<CustomerResponse>>;
    public record UpdateCustomerCommand(int CustomerId, CustomerRequest Request) : IRequest<ApiResponse>;
    public record DeleteCustomerCommand(int CustomerId) : IRequest<ApiResponse>;

    public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
    public record GetCustomerByIdQuery(int CustomerId) : IRequest<ApiResponse<CustomerResponse>>;
    public record GetCustomerByName(string Name) : IRequest<ApiResponse<List<CustomerResponse>>>;
}
