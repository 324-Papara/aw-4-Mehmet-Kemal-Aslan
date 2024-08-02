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
    public record CreateUserCommand(UserRequest Request) : IRequest<ApiResponse<UserResponse>>;
    public record UpdateUserCommand(int UserId, UserRequest Request) : IRequest<ApiResponse>;
    public record DeleteUserCommand(int UserId) : IRequest<ApiResponse>;

    public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
    public record GetUserByIdQuery(int UserId) : IRequest<ApiResponse<UserResponse>>;
    public record GetUserByCustomerIdQuery() : IRequest<ApiResponse<List<UserResponse>>>;
}
