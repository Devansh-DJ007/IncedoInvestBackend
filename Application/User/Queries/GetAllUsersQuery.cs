using IncedoInvest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.User.Queries
{
    public class GetAllUsersQuery : IRequest<List<Users>>
    {

    }
}
