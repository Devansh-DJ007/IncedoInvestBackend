using IncedoInvest.Application.ProposedInvestmentApp.Queries;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.ProposedInvestmentApp.Handlers.QueryHandlers
{
    public class GetAllProposedInvestmentsHandler : IRequestHandler<GetAllPropsedInvestmentsQuery, List<ProposedInvestment>>
    {
        private readonly IProposedInvestmentRepository _repository;

        public GetAllProposedInvestmentsHandler(IProposedInvestmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProposedInvestment>> Handle(GetAllPropsedInvestmentsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllProposedInvestmentsAsync();
        }
    }
}
