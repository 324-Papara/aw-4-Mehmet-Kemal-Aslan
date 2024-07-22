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
    public class CustomerPhone
    {
        public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
        public record UpdateCustomerPhoneCommand(int CustomerId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;
        public record DeleteCustomerPhoneCommand(int CustomerId) : IRequest<ApiResponse>;

        public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
        public record GetCustomerPhoneByIdQuery(int CustomerId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
    }
}
