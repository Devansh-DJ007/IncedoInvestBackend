using IncedoInvest.Application.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Handlers.CommandHandlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly IUserRepository _advisorRepository;
        private readonly IConfiguration _configuration;

        public RegisterUserHandler(IUserRepository advisorRepository, IConfiguration configuration)
        {
            _advisorRepository = advisorRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if a user with the same email already exists
                if (await _advisorRepository.UserExistsAsync(request.Email))
                {
                    return Result<string>.Fail("Username already exists");
                }

                Users user;
                string hashedPassword;

                string salt = "zxcvb";

                // Concatenating the salt with the user's password
                string saltedPassword = $"{salt}{request.Password}";

                // Computing the hash of the salted password
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    hashedPassword = Convert.ToBase64String(hashBytes);
                }
                user = new Users
                {
                    Email = request.Email,
                    Password = hashedPassword,
                    RoleID = request.RoleID,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    Phone = request.Phone,
                    AdvisorID = "111111",
                    AgentID = "111111",
                    ClientID = "111111",
                    Company = request.Company,
                    SortName = $"{request.LastName}{request.FirstName}",
                    Active = true,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = _configuration["Jwt:Issuer"],
                    ModifiedDate = DateTime.Now,
                    DeletedFlag = false
                };

                await _advisorRepository.AddUserAsync(user);

                // Generate and return a JWT token for the newly registered advisor
                var token = GenerateJwtToken(user);
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Success(ex.Message);
            }
        }

        // Implement JWT token generation logic using _configuration
        // Return the generated token
        private string GenerateJwtToken(Users user)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
