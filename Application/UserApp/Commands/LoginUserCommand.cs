using IncedoInvest.Application.Services;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IncedoInvest.Application.UserApp.Commands
{
    public class LoginUserCommand : IRequest<LoginDTO>
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
