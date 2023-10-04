using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Entities
{
    public class InvestmentInfo
    {
        [Key]
        public int InvestorInfoId { get; set; }

        [Required(ErrorMessage = "AdvisorId is required")]
        public int AdvisorId { get; set; }

        [Required(ErrorMessage = "ClientId is required")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "InvestmentAmount is required")]
        public double InvestmentAmount { get; set; }

        [Required(ErrorMessage = "InvestmentTypeId is required")]
        public int InvestmentTypeId { get; set; }

        [Required(ErrorMessage = "Accepted status is required")]
        public bool Accepted { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }

        [ForeignKey("AdvisorId")]
        public Advisor Advisor { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("InvestmentTypeId")]
        public InvestmentType InvestmentType { get; set; }
    }
}
