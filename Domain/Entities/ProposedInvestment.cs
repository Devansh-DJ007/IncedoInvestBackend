using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Entities
{
    public class ProposedInvestment
    {
        [Key]
        public int PropesedInvestmentId { get; set; }

        [Required(ErrorMessage = "InvestmentInfoId is required")]
        public int InvestmentInfoId { get; set; }

        [Required(ErrorMessage = "InvestmentStrategyId is required")]
        public int InvestmentStrategyId { get; set; }

        [Required(ErrorMessage = "AcceptedFlag is required")]
        public bool AcceptedFlag { get; set; }

        [ForeignKey("InvestmentInfoId")]
        public InvestmentInfo InvestmentInfo { get; set; }

        [ForeignKey("InvestmentStrategyId")]
        public InvestmentStrategy InvestmentStrategy { get; set; }
    }
}
