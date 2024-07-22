using AutoMapper;
using FluentValidation;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Validation;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Para.Bussiness.Cqrs.CustomerAddress;
using CustomerAddress = Para.Data.Domain.CustomerAddress;

namespace Para.Bussiness.Command
{
    public class CustomerAddressCommandHandler
    {
        public class CustomerCommandHandler :
        IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper mapper;

            public CustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitOfWork = unitOfWork;
                this.mapper = mapper;
            }

            public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
            {
                CustomerAddressValidator customerAddressValidator = new CustomerAddressValidator();
                var validationResult = await customerAddressValidator.ValidateAsync(request.Request);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException(errors);
                }
                var mapped = mapper.Map<CustomerAddressRequest, Data.Domain.CustomerAddress>(request.Request);
                await unitOfWork.CustomerAddressRepository.Insert(mapped);
                await unitOfWork.Complete();

                var response = mapper.Map<CustomerAddressResponse>(mapped);
                return new ApiResponse<CustomerAddressResponse>(response);
            }

            public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
            {
                CustomerAddressValidator customerAddressValidator = new CustomerAddressValidator();
                var validationResult = await customerAddressValidator.ValidateAsync(request.Request);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException(errors);
                }
                var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
                mapped.Id = request.CustomerId;
                mapped.InsertUser = "system";
                mapped.InsertDate = DateTime.Now;
                unitOfWork.CustomerAddressRepository.Update(mapped);
                await unitOfWork.Complete();
                return new ApiResponse();
            }

            public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
            {
                await unitOfWork.CustomerAddressRepository.Delete(request.CustomerId);
                await unitOfWork.Complete();
                return new ApiResponse();
            }
        }
    }
}
