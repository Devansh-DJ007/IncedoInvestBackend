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
        public int InvestmentTypeID { get; set; }

        [Required(ErrorMessage = "Investment Type Name is required")]
        [StringLength(250, ErrorMessage = "Investment Type Name cannot exceed 250 characters")]
        public string InvestmentTypeName { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        public DateTime CreatedDate { get; set; }

        [StringLength(50, ErrorMessage = "Modified By cannot exceed 50 characters")]
        public string ModifiedBy { get; set; }

        [Required(ErrorMessage = "Modified Date is required")]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }
    }
}
