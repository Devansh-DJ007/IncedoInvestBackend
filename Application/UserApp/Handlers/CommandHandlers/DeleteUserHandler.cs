using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Commands;
using IncedoInvest.Domain.Interfaces;
using MediatR;

namespace IncedoInvest.Application.UserApp.Handlers.CommandHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<string>>
    {


        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userToDelete = await _userRepository.GetUserByIdAsync(request.UserId);

                if (userToDelete == null)
                {
                    return Result<string>.Fail("User not found");
                }

                await _userRepository.DeleteUserAsync(userToDelete.UserId);

                return Result<string>.Success("User deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Error deleting user: {ex.Message}");
            }
        }
    }
}
