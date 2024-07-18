using AutoMapper;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Schema.Models;
using MediatR;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Bussiness.Validation;
using System.ComponentModel.DataAnnotations;

namespace Para.Bussiness.Command
{
    public class CustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
    IRequestHandler<UpdateCustomerCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            CustomerValidator customerValidator = new CustomerValidator();
            var validationResult = await customerValidator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.CustomerNumber = new Random().Next(1000000, 9999999);
            await unitOfWork.CustomerRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerResponse>(mapped);
            return new ApiResponse<CustomerResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            CustomerValidator customerValidator = new CustomerValidator();
            var validationResult = await customerValidator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mapped = mapper.Map<CustomerRequest, Customer>(request.Request);
            mapped.Id = request.CustomerId;
            mapped.CustomerNumber = 12;
            unitOfWork.CustomerRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
