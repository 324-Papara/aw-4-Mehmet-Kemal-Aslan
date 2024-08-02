using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;
using Para.Schema.Models;
using static Para.Bussiness.Cqrs.Country;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CountriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("Cache")]
        [Authorize(Roles = "admin,customer,User")]
        public async Task<ApiResponse<List<CountryResponse>>> GetAll()
        {
            var operation = new GetAllCountryFromCacheQuery();
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpGet]
        [Authorize(Roles = "admin,customer,User")]
        public async Task<ApiResponse<List<CountryResponse>>> Get()
        {
            var operation = new GetAllCountryQuery();
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpGet("{CountryId}")]
        [Authorize(Roles = "admin,customer,User")]
        [ResponseCache(Duration = 10000, Location = ResponseCacheLocation.Any)]
        public async Task<ApiResponse<CountryResponse>> Get([FromRoute] int CountryId)
        {
            var operation = new GetCountryByIdQuery(CountryId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin,customer,User")]
        public async Task<ApiResponse<CountryResponse>> Post([FromBody] CountryRequest value)
        {
            var operation = new CreateCountryCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{CountryId}")]
        [Authorize(Roles = "admin,customer,User")]
        public async Task<ApiResponse> Put(int CountryId, [FromBody] CountryRequest value)
        {
            var operation = new UpdateCountryCommand(CountryId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{CountryId}")]
        [Authorize(Roles = "admin,customer,User")]
        public async Task<ApiResponse> Delete(int CountryId)
        {
            var operation = new DeleteCountryCommand(CountryId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}