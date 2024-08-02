using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Schema.Models;
using static Para.Bussiness.Cqrs.CustomerAddress;
using static Para.Bussiness.Cqrs.CustomerPhone;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPhonesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerPhonesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerPhoneId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute] int customerPhoneId)
        {
            var operation = new GetCustomerPhoneByIdQuery(customerPhoneId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post([FromBody] CustomerPhoneRequest value)
        {
            try
            {
                var operation = new CreateCustomerPhoneCommand(value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpPut("{customerPhoneId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Put(int customerPhoneId, [FromBody] CustomerPhoneRequest value)
        {
            try
            {
                var operation = new UpdateCustomerPhoneCommand(customerPhoneId, value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpDelete("{customerPhoneId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(int customerPhoneId)
        {
            var operation = new DeleteCustomerPhoneCommand(customerPhoneId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
