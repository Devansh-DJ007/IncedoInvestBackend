using IncedoInvest.Domain.Entities;
using MediatR;

namespace IncedoInvest.Application.UserApp.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {

    }
}
