using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncedoInvest.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "RoleID is required")]
        public int RoleId { get; set; }

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

        [Required(ErrorMessage = "Pincode is required")]
        [StringLength(6)]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(40)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Company { get; set; }

        [StringLength(6)]
        public string AdvisorId { get; set; }

        [StringLength(6)]
        public string ?ClientId { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Modified Date is required")]
        public DateTime? ModifiedDate { get; set; }

        [Required(ErrorMessage = "Deleted Flag is required")]
        public bool DeletedFlag { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
