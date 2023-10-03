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
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUserRepository _advisorRepository;
        private readonly IConfiguration _configuration;

        public LoginUserHandler(IUserRepository advisorRepository, IConfiguration configuration)
        {
            _advisorRepository = advisorRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Implement advisor login logic using _advisorRepository
                var advisor = await _advisorRepository.GetUserByEmailAsync(request.Email);

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
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }
    }
}
