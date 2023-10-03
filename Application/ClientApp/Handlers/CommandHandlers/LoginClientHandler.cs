using IncedoInvest.Application.ClientApp.Commands;
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

namespace IncedoInvest.Application.ClientApp.Handlers.CommandHandlers
{
    public class LoginClientHandler : IRequestHandler<LoginClientCommand, Result<string>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IConfiguration _configuration;

        public LoginClientHandler(IClientRepository clientRepository, IConfiguration configuration)
        {
            _clientRepository = clientRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(LoginClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientRepository.GetClientByEmailAsync(request.Email);

                string hashedPassword = "";
                string salt = "zxcvb";
                string saltedPassword = $"{salt}{request.Password}";

                // Computing the hash of the salted password
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    hashedPassword = Convert.ToBase64String(hashBytes);
                }

                if (client == null || !VerifyPassword(client.Password, hashedPassword))
                {
                    return Result<string>.Fail("Invalid username or password");
                }

                var token = GenerateJwtToken(client); ;
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

        private string GenerateJwtToken(Client client)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, client.ClientId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, client.Email),
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
