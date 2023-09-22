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
    public class LoginAdvisorHandler : IRequestHandler<LoginAdvisorCommand, Result<string>>
    {
        private readonly IAdvisorRepository _advisorRepository;
        private readonly IConfiguration _configuration;

        public LoginAdvisorHandler(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }

        public async Task<Result<string>> Handle(LoginAdvisorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Implement advisor login logic using _advisorRepository
                var advisor = await _advisorRepository.GetAdvisorByUsernameAsync(request.Username);

                if (advisor == null || !VerifyPassword(request.Password, advisor.Password))
                {
                    return Result<string>.Fail("Invalid username or password");
                }

                // Generate and return a JWT token for the authenticated advisor
                var token = GenerateJwtToken(advisor);
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }

        // Implement your password verification logic here (e.g., using bcrypt)
        // For simplicity, this example does not include actual password hashing.
        // You should use a secure hashing algorithm in your production code.
        private bool VerifyPassword(string password, string passwordHash)
        {
            return password == passwordHash;
        }

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
