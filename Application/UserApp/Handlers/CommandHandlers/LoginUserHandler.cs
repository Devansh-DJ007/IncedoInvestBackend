using IncedoInvest.Application.Services;
using IncedoInvest.Application.UserApp.Commands;
using IncedoInvest.Domain.Entities;
using IncedoInvest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IncedoInvest.Application.UserApp.Handlers.CommandHandlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginUserHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);

                string hashedPassword = "";
                string salt = "zxcvb";
                string saltedPassword = $"{salt}{request.Password}";

                // Computing the hash of the salted password
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    hashedPassword = Convert.ToBase64String(hashBytes);
                }

                if (user == null || !VerifyPassword(user.Password, hashedPassword))
                {
                    return new LoginDTO
                    {
                        Token = null,
                        RoleId = 0,
                        AdvisorId = null,
                        ClientId = null
                    };
                }

                var token = GenerateJwtToken(user);
                return new LoginDTO
                {
                    Token = token,
                    RoleId = user.RoleId,
                    AdvisorId = user.AdvisorId,
                    ClientId = user.ClientId
                };
            }
            catch (Exception ex)
            {
                return new LoginDTO
                {
                    Token = null,
                    RoleId = 0,
                    AdvisorId = null,
                    ClientId = null
                };
            }
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
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
