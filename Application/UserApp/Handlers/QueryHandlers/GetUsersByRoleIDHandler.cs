using IncedoInvest.Application.UserApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.UserApp.Handlers.QueryHandlers
{
    public class GetUsersByRoleIDHandler : IRequestHandler<GetUsersByRoleIDQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByRoleIDHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetUsersByRoleIDQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByRoleIdAsync(request.roleId);
            return users;
        }
    }
}
