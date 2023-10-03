using IncedoInvest.Application.AdvisorApp.Commands;
using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IncedoInvest.Application.AdvisorApp.Handlers.CommandHandlers
{
    public class LoginAdvisorHandler : IRequestHandler<LoginAdvisorCommand, Result<string>>
    {
        private readonly IAdvisorRepository _advisorRepository;
        private readonly IConfiguration _configuration;

        public LoginAdvisorHandler(IAdvisorRepository advisorRepository, IConfiguration configuration)
        {
            _advisorRepository = advisorRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(LoginAdvisorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var advisor = await _advisorRepository.GetAdvisorByEmailAsync(request.Email);

                string hashedPassword = "";
                string salt = "zxcvb";
                string saltedPassword = $"{salt}{request.Password}";

                // Computing the hash of the salted password
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    hashedPassword = Convert.ToBase64String(hashBytes);
                }

                if (advisor == null || !VerifyPassword(advisor.Password, hashedPassword))
                {
                    return Result<string>.Fail("Invalid username or password");
                }

                var token = GenerateJwtToken(advisor);
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        private string GenerateJwtToken(Advisor advisor)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, advisor.AdvisorId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, advisor.Email),
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
