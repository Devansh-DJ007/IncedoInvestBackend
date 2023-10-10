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
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        
        public RegisterUserHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if a user with the same email already exists
                if (await _userRepository.UserExistsAsync(request.Email))
                {
                    return Result<string>.Fail("Username already exists");
                }

                User user;
                string hashedPassword;
                string advisorId = "", clientId = "";

                if (request.RoleId == 1)
                {
                    advisorId = await GenerateAdvisorIdAsync();
                }

                if(request.RoleId == 2)
                {
                    clientId = await GenerateClientIdAsync();
                    advisorId = await GetRandomAdvisorId();
                }

                string salt = "zxcvb";

                // Concatenating the salt with the user's password
                string saltedPassword = $"{salt}{request.Password}";

                // Computing the hash of the salted password
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    hashedPassword = Convert.ToBase64String(hashBytes);
                }

                user = new User
                {
                    Email = request.Email,
                    Password = hashedPassword,
                    RoleId = request.RoleId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    Pincode = request.Pincode,
                    Phone = request.Phone,
                    Company = request.Company,
                    AdvisorId = advisorId,
                    ClientId = clientId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedFlag = false
                };

                await _userRepository.AddUserAsync(user);

                // Generate and return a JWT token for the newly registered user
                var token = GenerateJwtToken(user);
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Success(ex.Message);
            }
        }

        public async Task<string> GenerateAdvisorIdAsync()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);
            string advisorId = "A" + randomNumber.ToString();

            if(await _userRepository.CheckUserExistsWithAdvisorIdAsync(advisorId))
            {
                return await GenerateAdvisorIdAsync();
            }

            return advisorId;
        }

        public async Task<string> GenerateClientIdAsync()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);
            string advisorId = "C" + randomNumber.ToString();

            if (await _userRepository.CheckUserExistsWithClientIdAsync(advisorId))
            {
                return await GenerateClientIdAsync();
            }

            return advisorId;
        }

        public async Task<string> GetRandomAdvisorId()
        {
            List<User> users = await _userRepository.GetUsersByRoleIdAsync(1);

            if (users.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, users.Count);
                string advisorId = users[randomIndex].AdvisorId;
                return advisorId;
            }
            return "A00000";
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
