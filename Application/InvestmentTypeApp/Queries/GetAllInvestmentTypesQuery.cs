using IncedoInvest.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace IncedoInvest.Application.InvestmentTypeApp.Queries
{
    public class GetAllInvestmentTypesQuery : IRequest<List<InvestmentType>> { }
}
