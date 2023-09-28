using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Entities
{
    public class InvestorInfo
    {
        [Key]
        public int InvestorInfoID { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Investment Name is required")]
        [StringLength(200, ErrorMessage = "Investment Name cannot exceed 200 characters")]
        public string InvestmentName { get; set; }

        [Required(ErrorMessage = "Active status is required")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        public DateTime CreatedDate { get; set; }

        [StringLength(50, ErrorMessage = "Modified By cannot exceed 50 characters")]
        public string ModifiedBy { get; set; }

        [Required(ErrorMessage = "Modified Date is required")]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }

        [ForeignKey("UserID")]
        public Users Users { get; set; }
    }
}
