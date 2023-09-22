using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Commands
{
    public class RegisterAdvisorCommand : IRequest<Result<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
