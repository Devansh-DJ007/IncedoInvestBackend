using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Application.Services
{
    public class LoginDTO
    {
        public string Token { get; set; }
        public int RoleId { get; set; }
        public string AdvisorId { get; set; }
        public string ClientId { get; set;}
    }
}
