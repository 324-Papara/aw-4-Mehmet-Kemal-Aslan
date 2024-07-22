using MediatR;
using Para.Base.Response;
using Para.Data.Domain;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs
{
    public class CustomerAddress
    {
        public record CreateCustomerAddressCommand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
        public record UpdateCustomerAddressCommand(int CustomerId, CustomerAddressRequest Request) : IRequest<ApiResponse>;
        public record DeleteCustomerAddressCommand(int CustomerId) : IRequest<ApiResponse>;

        public record GetAllCustomerAddressQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
        public record GetCustomerAddressByIdQuery(int CustomerId) : IRequest<ApiResponse<CustomerAddressResponse>>;
    }
}
