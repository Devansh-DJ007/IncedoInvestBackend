using IncedoInvest.Application.Commands;
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
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Handlers.CommandHandlers
{
    public class RegisterAdvisorHandler : IRequestHandler<RegisterAdvisorCommand, Result<string>>
    {
        private readonly IAdvisorRepository _advisorRepository;
        private readonly IConfiguration _configuration;

        public RegisterAdvisorHandler(IAdvisorRepository advisorRepository, IConfiguration configuration)
        {
            _advisorRepository = advisorRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(RegisterAdvisorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if a user with the same username already exists
                if (await _advisorRepository.AdvisorExistsAsync(request.Username))
                {
                    return Result<string>.Fail("Username already exists");
                }

                // You should hash the password before storing it in the database
                // For simplicity, this example does not include actual password hashing.
                // You should use a secure hashing algorithm in your production code.

                var advisor = new AdvisorDetails
                {
                    Username = request.Username,
                    Password = request.Password, // Hash the password in a real application
                                                 // Set other registration fields...
                };

                await _advisorRepository.AddAdvisorAsync(advisor);

                // Generate and return a JWT token for the newly registered advisor
                var token = GenerateJwtToken(advisor);
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }

        // Implement JWT token generation logic using _configuration
        // Return the generated token
        private string GenerateJwtToken(AdvisorDetails advisor)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, advisor.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, advisor.Username),
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
    }
}
