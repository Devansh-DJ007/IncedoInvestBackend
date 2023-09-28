using IncedoInvest.Application.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Handlers.QueryHandlers
{
    public class GetUsersByRoleIDHandler : IRequestHandler<GetUsersByRoleIDQuery, List<Users>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public GetUsersByRoleIDHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<List<Users>> Handle(GetUsersByRoleIDQuery request, CancellationToken cancellationToken)
        {
            int roleID = request.RoleId;

            var users = await _userRepository.GetUsersByRoleIdAsync(roleID);

            return users;
        }
    }
}
