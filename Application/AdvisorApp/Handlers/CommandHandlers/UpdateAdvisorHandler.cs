using IncedoInvest.Application.AdvisorApp.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.AdvisorApp.Handlers.CommandHandlers
{
    public class UpdateAdvisorHandler : IRequestHandler<UpdateAdvisorCommand, Result<Advisor>>
    {
        private readonly IAdvisorRepository _advisorRepository;

        public UpdateAdvisorHandler(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }

        public async Task<Result<Advisor>> Handle(UpdateAdvisorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var advisorToUpdate = await _advisorRepository.GetAdvisorByEmailAsync(request.Email);

                if (advisorToUpdate == null)
                {
                    return Result<Advisor>.Fail("User not found");
                }

                advisorToUpdate.FirstName = request.FirstName;
                advisorToUpdate.LastName = request.LastName;
                advisorToUpdate.Address = request.Address;
                advisorToUpdate.City = request.City;
                advisorToUpdate.State = request.State;
                advisorToUpdate.Phone = request.Phone;
                advisorToUpdate.Company = request.Company;

                await _advisorRepository.UpdateAdvisorAsync(advisorToUpdate);

                return Result<Advisor>.Success(advisorToUpdate);
            }
            catch (Exception ex)
            {
                return Result<Advisor>.Fail(ex.Message);
            }
        }
    }
}
