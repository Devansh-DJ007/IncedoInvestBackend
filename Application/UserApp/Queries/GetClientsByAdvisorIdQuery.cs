using IncedoInvest.Domain.Entities;
using MediatR;

namespace IncedoInvest.Application.UserApp.Queries
{
    public class GetClientsByAdvisorIdQuery : IRequest<List<User>>
    {
        public string advisorId { get; set; }
    }
}
