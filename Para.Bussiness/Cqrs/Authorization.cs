using MediatR;
using Para.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Para.Schema.Models.Authorization;

namespace Para.Bussiness.Cqrs
{
    public class Authorization
    {
        public record CreateAuthorizationTokenCommand(AuthorizationRequest Request) : IRequest<ApiResponse<AuthorizationResponse>>;
    }
}
