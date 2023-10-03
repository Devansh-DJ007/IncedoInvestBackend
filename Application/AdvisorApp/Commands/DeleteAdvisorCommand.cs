using IncedoInvest.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.AdvisorApp.Commands
{
    public class DeleteAdvisorCommand : IRequest<Result<string>>
    {
        public int AdvisorId { get; set; }
    }
}
