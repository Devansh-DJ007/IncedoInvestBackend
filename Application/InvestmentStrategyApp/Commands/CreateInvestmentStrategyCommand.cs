using IncedoInvest.Application.Services;
using IncedoInvest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IncedoInvest.Application.InvestmentStrategyApp.Command
{
   public class CreateInvestmentStrategyCommand : IRequest<Result<string>>
    {
        [Required]
        public int InvestmentStrategyId { get; set; }

        [Required(ErrorMessage = "Strategy Name is required")]
        [StringLength(200, ErrorMessage = "Strategy Name cannot exceed 200 characters")]
        public string InvestmentStrategyName { get; set; }

        [Required(ErrorMessage = "InvestmentTypeId is required")]
        public int InvestmentTypeId { get; set; }

        [Required(ErrorMessage = "10Y Return is required")]
        public double Return10Y { get; set; }

        [Required(ErrorMessage = "10Y Risk is required")]
        public double Risk10Y { get; set; }

        [Required(ErrorMessage = "1Y Return is required")]
        public double Return1Y { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }
    }
}
