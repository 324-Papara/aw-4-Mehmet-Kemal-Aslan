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
    public class Country
    {
        public record CreateCountryCommand(CountryRequest Request) : IRequest<ApiResponse<CountryResponse>>;
        public record UpdateCountryCommand(int CountryId, CountryRequest Request) : IRequest<ApiResponse>;
        public record DeleteCountryCommand(int CountryId) : IRequest<ApiResponse>;

        public record GetAllCountryQuery() : IRequest<ApiResponse<List<CountryResponse>>>;
        public record GetAllCountryFromCacheQuery() : IRequest<ApiResponse<List<CountryResponse>>>;
        public record GetCountryByIdQuery(int CountryId) : IRequest<ApiResponse<CountryResponse>>;
    }
}
