using IncedoInvest.Domain.Entities;
using MediatR;

namespace IncedoInvest.Application.UserApp.Queries
{
    public class GetUsersByRoleIDQuery : IRequest<List<User>>
    {
        public int roleId { get; set; }
    }
}
