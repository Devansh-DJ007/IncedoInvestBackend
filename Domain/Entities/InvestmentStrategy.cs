using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Entities
{
    public class InvestmentStrategy
    {
        [Key]
        public int InvestmentStrategyID { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        public int InvestorInfoID { get; set; }

        [Required(ErrorMessage = "Strategy Name is required")]
        [StringLength(200, ErrorMessage = "Strategy Name cannot exceed 200 characters")]
        public string StrategyName { get; set; }

        [Required(ErrorMessage = "Investment Account Number is required")]
        [StringLength(6, ErrorMessage = "Investment Account Number must be 6 characters")]
        public string AccountId { get; set; }

        [Required(ErrorMessage = "Investment Model Number is required")]
        [StringLength(6, ErrorMessage = "Investment Model Number must be 6 characters")]
        public string ModelAPLID { get; set; }

        [Required(ErrorMessage = "Investment Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Investment Amount must be greater than 0")]
        public double InvestmentAmount { get; set; }

        [ForeignKey("InvestmentType")]
        public int InvestmentTypeID { get; set; }

        [StringLength(50, ErrorMessage = "Modified By cannot exceed 50 characters")]
        public string ModifiedBy { get; set; }

        [Required(ErrorMessage = "Modified Date is required")]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }

        [ForeignKey("InvestorInfoID")]
        public InvestorInfo InvestorInfo { get; set; }
    }
}
