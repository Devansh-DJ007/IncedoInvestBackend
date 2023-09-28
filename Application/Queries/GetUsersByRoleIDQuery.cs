using IncedoInvest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Queries
{
    public class GetUsersByRoleIDQuery : IRequest<List<Users>>
    {
        public int RoleId { get; }
        public GetUsersByRoleIDQuery(int roleId)
        {
            RoleId = roleId;
        }
    }
}
