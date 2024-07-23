using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Schema.Models;
using static Para.Bussiness.Cqrs.CustomerAddress;
using static Para.Bussiness.Cqrs.CustomerDetail;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerDetailId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute] int customerDetailId)
        {
            var operation = new GetCustomerDetailByIdQuery(customerDetailId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerDetailRequest value)
        {
            try
            {
                var operation = new CreateCustomerDetailCommand(value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpPut("{customerDetailId}")]
        public async Task<ActionResult> Put(int customerDetailId, [FromBody] CustomerDetailRequest value)
        {
            try
            {
                var operation = new UpdateCustomerDetailCommand(customerDetailId, value);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
        }

        [HttpDelete("{customerDetailId}")]
        public async Task<ApiResponse> Delete(int customerDetailId)
        {
            var operation = new DeleteCustomerDetailCommand(customerDetailId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
