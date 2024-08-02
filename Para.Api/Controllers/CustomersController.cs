using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Validation;
using Para.Schema.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<CustomerResponse>> Get([FromRoute] int customerId)
        {
            var operation = new GetCustomerByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post([FromBody] CustomerRequest value)
        {
            try
            {
                var operation = new CreateCustomerCommand(value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpPut("{customerId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Put(int customerId, [FromBody] CustomerRequest value)
        {
            try
            {
                var operation = new UpdateCustomerCommand(customerId, value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpDelete("{customerId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(int customerId)
        {
            var operation = new DeleteCustomerCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetByName")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Get([FromQuery] string? name)
        {
            try
            {
                var operation = new GetCustomerByName(name);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }
    }
}
