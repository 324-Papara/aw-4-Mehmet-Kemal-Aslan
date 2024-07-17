using AutoMapper;
using Para.Data.Domain;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>();

            CreateMap<CustomerAddress, CustomerAddressResponse>();
            CreateMap<CustomerAddressRequest, CustomerAddress>();

            CreateMap<CustomerPhone, CustomerPhoneResponse>();
            CreateMap<CustomerPhoneRequest, CustomerPhone>();

            CreateMap<CustomerDetail, CustomerDetailResponse>();
            CreateMap<CustomerDetailRequest, CustomerDetail>();
        }
    }
}
