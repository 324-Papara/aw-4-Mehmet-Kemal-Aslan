using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Validation;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Query
{
    public class CustomerQueryHandler :
    IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>,
    IRequestHandler<GetCustomerByName, ApiResponse<List<CustomerResponse>>>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            List<Customer> entityList = await unitOfWork.CustomerRepository.GetAll();
            var mappedList = mapper.Map<List<CustomerResponse>>(entityList);
            return new ApiResponse<List<CustomerResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerRepository.GetById(request.CustomerId);
            var mapped = mapper.Map<CustomerResponse>(entity);
            return new ApiResponse<CustomerResponse>(mapped);
        }

        public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByName request, CancellationToken cancellationToken)
        {
            CustomerByNameValidator customerByNameValidator = new CustomerByNameValidator();
            var validationResult = await customerByNameValidator.ValidateAsync(request.Name);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var customersByName = await unitOfWork.CustomerRepository.Where(c => c.FirstName == request.Name);

            var customers = customersByName.Distinct().ToList();

            var response = mapper.Map<List<CustomerResponse>>(customers);

            return new ApiResponse<List<CustomerResponse>>(response);
        }
    }
}
