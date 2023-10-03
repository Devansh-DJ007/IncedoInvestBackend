using IncedoInvest.Application.AdvisorApp.Commands;
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

namespace IncedoInvest.Application.AdvisorApp.Handlers.CommandHandlers
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
                if (await _advisorRepository.AdvisorExistsAsync(request.Email))
                {
                    return Result<string>.Fail("Email already exists");
                }

                Advisor advisor;
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
                advisor = new Advisor
                {
                    Email = request.Email,
                    Password = hashedPassword,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    Phone = request.Phone,
                    Company = request.Company,
                    Active = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedFlag = false
                };

                await _advisorRepository.AddAdvisorAsync(advisor);

                // Generate and return a JWT token for the newly registered advisor
                var token = GenerateJwtToken(advisor);
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Success(ex.Message);
            }
        }

        // Implement JWT token generation logic using _configuration
        // Return the generated token
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
