using IncedoInvest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.AdvisorApp.Queries
{
    public class GetAllAdvisorQuery : IRequest<List<Advisor>>
    {

    }
}
