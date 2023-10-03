using IncedoInvest.Application.AdvisorApp.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.AdvisorApp.Handlers.CommandHandlers
{
    public class DeleteAdvisorHandler : IRequestHandler<DeleteAdvisorCommand, Result<string>>
    {
        private readonly IAdvisorRepository _advisorRepository;

        public DeleteAdvisorHandler(IAdvisorRepository advsiorRepository)
        {
            _advisorRepository = advsiorRepository;
        }

        public async Task<Result<string>> Handle(DeleteAdvisorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var advisorToDelete = await _advisorRepository.GetAdvisorByIdAsync(request.AdvisorId);

                if (advisorToDelete == null)
                {
                    return Result<string>.Fail("Advisor not found");
                }

                await _advisorRepository.DeleteAdvisorAsync(advisorToDelete.AdvisorId);

                return Result<string>.Success("Advisor deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Error deleting advisor: {ex.Message}");
            }
        }
    }
}
