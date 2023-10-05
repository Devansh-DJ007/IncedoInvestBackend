using IncedoInvest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.UserApp.Queries
{
    public class GetUsersByRoleIDQuery : IRequest<List<User>>
    {
        public int roleId { get; set; }
    }
}
