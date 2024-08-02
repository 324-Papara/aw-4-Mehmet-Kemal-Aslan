using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.RabbitMQ;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Para.Bussiness.RabbitMQ.EmailQueueConsumer;

namespace Para.Bussiness.Command
{
    public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IRequestHandler<UpdateUserCommand, ApiResponse>,
    IRequestHandler<DeleteUserCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<UserRequest, User>(request.Request);
            await unitOfWork.UserRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<UserResponse>(mapped);

            var emailPublisher = new EmailQueuePublisher();
            var emailMessage = new EmailMessage
            {
                Subject = "Welcome!",
                Email = request.Request.Email,
                Content = "Thank you for registering."
            };
            var message = JsonSerializer.Serialize(emailMessage);
            emailPublisher.Publish(message);
            emailPublisher.Dispose();

            return new ApiResponse<UserResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<UserRequest, User>(request.Request);
            mapped.Id = request.UserId;
            unitOfWork.UserRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.UserRepository.Delete(request.UserId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
