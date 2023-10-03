using IncedoInvest.Application.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Handlers.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<Users>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Users>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userToUpdate = await _userRepository.GetUserByEmailAsync(request.Email);

                if (userToUpdate == null)
                {
                    return Result<Users>.Fail("User not found");
                }

                userToUpdate.FirstName = request.FirstName;
                userToUpdate.LastName = request.LastName;
                userToUpdate.Address = request.Address;
                userToUpdate.City = request.City;
                userToUpdate.State = request.State;
                userToUpdate.Phone = request.Phone;
                userToUpdate.Company = request.Company;

                await _userRepository.UpdateUserAsync(userToUpdate);

                return Result<Users>.Success(userToUpdate);
            }
            catch (Exception ex)
            {
                return Result<Users>.Fail(ex.Message);
            }
        }
    }
}
