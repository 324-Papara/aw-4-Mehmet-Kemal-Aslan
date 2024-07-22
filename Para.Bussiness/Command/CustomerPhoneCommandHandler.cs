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
using static Para.Bussiness.Cqrs.CustomerPhone;

namespace Para.Bussiness.Command
{
    public class CustomerPhoneCommandHandler :
        IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
        IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            CustomerPhoneValidator customerPhoneValidator = new CustomerPhoneValidator();
            var validationResult = await customerPhoneValidator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mapped = mapper.Map<CustomerPhoneRequest, Data.Domain.CustomerPhone>(request.Request);
            await unitOfWork.CustomerPhoneRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerPhoneResponse>(mapped);
            return new ApiResponse<CustomerPhoneResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            CustomerPhoneValidator customerPhoneValidator = new CustomerPhoneValidator();
            var validationResult = await customerPhoneValidator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
            mapped.Id = request.CustomerId;
            mapped.InsertUser = "system";
            mapped.InsertDate = DateTime.Now;
            unitOfWork.CustomerPhoneRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerPhoneRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
