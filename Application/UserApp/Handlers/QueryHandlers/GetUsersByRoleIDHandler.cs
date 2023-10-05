using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace IncedoInvest.Application.UserApp.Handlers.QueryHandlers
{
    public class GetUsersByRoleIDHandler : IRequestHandler<GetUsersByRoleIDQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public GetUsersByRoleIDHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<List<User>> Handle(GetUsersByRoleIDQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByRoleIdAsync(request.roleId);

            return users;
        }
    }
}
