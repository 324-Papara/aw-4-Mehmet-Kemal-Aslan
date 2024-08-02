using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Schema.Models;
using static Para.Bussiness.Cqrs.CustomerAddress;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerAddressesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerAddressId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute] int customerAddressId)
        {
            var operation = new GetCustomerAddressByIdQuery(customerAddressId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post([FromBody] CustomerAddressRequest value)
        {
            try
            {
                var operation = new CreateCustomerAddressCommand(value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpPut("{customerAddressId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Put(int customerAddressId, [FromBody] CustomerAddressRequest value)
        {
            try
            {
                var operation = new UpdateCustomerAddressCommand(customerAddressId, value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpDelete("{customerAddressId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(int customerAddressId)
        {
            var operation = new DeleteCustomerAddressCommand(customerAddressId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
