using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Commands;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.UserApp.Handlers.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userToUpdate = await _userRepository.GetUserByEmailAsync(request.Email);

                if (userToUpdate == null)
                {
                    return Result<User>.Fail("User not found");
                }

                userToUpdate.FirstName = request.FirstName;
                userToUpdate.LastName = request.LastName;
                userToUpdate.Address = request.Address;
                userToUpdate.City = request.City;
                userToUpdate.State = request.State;
                userToUpdate.Phone = request.Phone;
                userToUpdate.Company = request.Company;

                await _userRepository.UpdateUserAsync(userToUpdate);

                return Result<User>.Success(userToUpdate);
            }
            catch (Exception ex)
            {
                return Result<User>.Fail(ex.Message);
            }
        }
    }
}
