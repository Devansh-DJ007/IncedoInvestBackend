using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Entities
{
    public class InvestmentType
    {
        [Key]
        public int InvestmentTypeId { get; set; }

        [Required(ErrorMessage = "Investment Type Name is required")]
        [StringLength(250, ErrorMessage = "Investment Type Name cannot exceed 250 characters")]
        public string InvestmentTypeName { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }
    }
}
