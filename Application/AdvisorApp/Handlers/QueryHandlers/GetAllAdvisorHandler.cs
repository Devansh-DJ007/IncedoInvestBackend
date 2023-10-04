using IncedoInvest.Application.AdvisorApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.AdvisorApp.Handlers.QueryHandlers
{
    public class GetAllAdvisorHandler : IRequestHandler<GetAllAdvisorQuery, List<Advisor>>
    {
        private readonly IAdvisorRepository _advisorRepository;
        private readonly IConfiguration _configuration;

        public GetAllAdvisorHandler(IAdvisorRepository advisorRepository, IConfiguration configuration)
        {
            _advisorRepository = advisorRepository;
            _configuration = configuration;
        }

        public async Task<List<Advisor>> Handle(GetAllAdvisorQuery request, CancellationToken cancellationToken)
        {
            var advisors = await _advisorRepository.GetAllAdvisorAsync();

            return advisors;
        }
    }
}
