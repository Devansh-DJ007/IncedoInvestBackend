using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Entities
{
    public class AdvisorDetails
    {
        [Key]
        public int UserID { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(40)]
        public string Phone { get; set; }

        [StringLength(6)]
        public string AdvisorID { get; set; }

        [StringLength(6)]
        public string AgentID { get; set; }

        [StringLength(6)]
        public string ClientID { get; set; }

        [StringLength(150)]
        public string Company { get; set; }

        [Required(ErrorMessage = "Sort Name is required")]
        [StringLength(100)]
        public string SortName { get; set; }

        [Required(ErrorMessage = "Active status is required")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }
    }
}
