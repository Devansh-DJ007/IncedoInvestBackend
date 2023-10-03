using IncedoInvest.Application.AdvisorApp.Commands;
using IncedoInvest.Application.ClientApp.Commands;
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

namespace IncedoInvest.Application.ClientApp.Handlers.CommandHandlers
{
    public class RegisterClientHandler : IRequestHandler<RegisterClientCommand, Result<string>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IConfiguration _configuration;

        public RegisterClientHandler(IClientRepository clientRepository, IConfiguration configuration)
        {
            _clientRepository = clientRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _clientRepository.ClientExistsAsync(request.Email))
                {
                    return Result<string>.Fail("Email already exists");
                }

                Client client;
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
                client = new Client
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

                await _clientRepository.AddClientAsync(client);

                // Generate and return a JWT token for the newly registered client
                var token = GenerateJwtToken(client);
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Success(ex.Message);
            }
        }

        // Implement JWT token generation logic using _configuration
        // Return the generated token
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
