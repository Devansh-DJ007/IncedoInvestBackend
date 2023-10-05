using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.UserApp.Handlers.QueryHandlers
{
    public class GetClientsByAdvisorIdHandler : IRequestHandler<GetClientsByAdvisorIdQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetClientsByAdvisorIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> Handle(GetClientsByAdvisorIdQuery request, CancellationToken cancellationToken)
        {
            var clients = await _userRepository.GetClientsByAdvisorIdAsync(request.advisorId);

            return clients;
        }
    }
}
