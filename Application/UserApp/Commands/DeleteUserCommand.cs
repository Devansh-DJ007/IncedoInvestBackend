using IncedoInvest.Application.Services;
using MediatR;

namespace IncedoInvest.Application.UserApp.Commands
{
    public class DeleteUserCommand : IRequest<Result<string>>
    {
        public int UserId { get; set; }
    }
}