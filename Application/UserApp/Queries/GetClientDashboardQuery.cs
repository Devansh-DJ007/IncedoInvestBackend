using IncedoInvest.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.UserApp.Queries
{
    public class GetClientDashboardQuery : IRequest<List<ClientDashboardDTO>>
    {
        public string ClientId { get; set; }
    }
}
