using IncedoInvest.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.UserApp.Commands
{
    public class DeleteUserCommand : IRequest<Result<string>>
    {
        public int UserId { get; set; }
    }
}