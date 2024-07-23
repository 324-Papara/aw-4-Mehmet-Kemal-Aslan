using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Para.Bussiness.Cqrs.CustomerAddress;
using static Para.Bussiness.Cqrs.CustomerDetail;

namespace Para.Bussiness.Query
{
    public class CustomerDetailQueryHandler :
    IRequestHandler<GetAllCustomerDetailQuery, ApiResponse<List<CustomerDetailResponse>>>,
    IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            List<Data.Domain.CustomerDetail> entityList = await unitOfWork.CustomerDetailRepository.GetAll();
            var mappedList = mapper.Map<List<CustomerDetailResponse>>(entityList);
            return new ApiResponse<List<CustomerDetailResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerDetailRepository.GetById(request.CustomerId);
            var mapped = mapper.Map<CustomerDetailResponse>(entity);
            return new ApiResponse<CustomerDetailResponse>(mapped);
        }
    }
}
