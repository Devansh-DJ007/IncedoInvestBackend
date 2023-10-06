using IncedoInvest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentStrategyApp.Queries
{
    public class GetAllInvestmentStrategyQuery : IRequest<List<InvestmentStrategy>>
    {

    }
}
