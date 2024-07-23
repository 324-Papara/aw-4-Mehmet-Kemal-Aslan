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
    public class CustomerDetail
    {
        public record CreateCustomerDetailCommand(CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
        public record UpdateCustomerDetailCommand(int CustomerId, CustomerDetailRequest Request) : IRequest<ApiResponse>;
        public record DeleteCustomerDetailCommand(int CustomerId) : IRequest<ApiResponse>;

        public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
        public record GetCustomerDetailByIdQuery(int CustomerId) : IRequest<ApiResponse<CustomerDetailResponse>>;
    }
}
