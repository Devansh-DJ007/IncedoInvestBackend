using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.InvestmentInfoApp.Queries
{
    public class GetAllInvestmentInfoQuery : IRequest<Result<List<InvestmentInfo>>>
    {
        
    }
}
