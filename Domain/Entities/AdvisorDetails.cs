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
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        //public string Username { get; set; }

        //public string Gender { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(10, ErrorMessage = "Must be 10 digit", MinimumLength = 10)]
        //public string PhoneNumber { get; set; }
    }
}
