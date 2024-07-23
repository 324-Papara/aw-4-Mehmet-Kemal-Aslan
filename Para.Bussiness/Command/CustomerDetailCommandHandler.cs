using AutoMapper;
using FluentValidation;
using MediatR;
using Para.Base.Response;
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
using static Para.Bussiness.Cqrs.CustomerDetail;

namespace Para.Bussiness.Command
{
    public class CustomerDetailCommandHandler :
        IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
        IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            CustomerDetailValidator customerDetailValidator = new CustomerDetailValidator();
            var validationResult = await customerDetailValidator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mapped = mapper.Map<CustomerDetailRequest, Data.Domain.CustomerDetail>(request.Request);
            await unitOfWork.CustomerDetailRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerDetailResponse>(mapped);
            return new ApiResponse<CustomerDetailResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            CustomerDetailValidator customerDetailValidator = new CustomerDetailValidator();
            var validationResult = await customerDetailValidator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            mapped.Id = request.CustomerId;
            mapped.InsertUser = "system";
            mapped.InsertDate = DateTime.Now;
            unitOfWork.CustomerDetailRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerDetailRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
