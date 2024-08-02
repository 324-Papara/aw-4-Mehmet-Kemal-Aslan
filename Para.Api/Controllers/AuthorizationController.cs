using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using static Para.Bussiness.Cqrs.Authorization;
using static Para.Schema.Models.Authorization;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorizationController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        [AllowAnonymous]
        //[ResponseHeader("MyCustomHeaderInResponse", "POST")]
        public async Task<ApiResponse<AuthorizationResponse>> Post([FromBody] AuthorizationRequest value)
        {
            var operation = new CreateAuthorizationTokenCommand(value);
            var result = await mediator.Send(operation);
            return result;

        }
    }
}
