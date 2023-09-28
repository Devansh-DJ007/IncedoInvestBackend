using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [StringLength(15, ErrorMessage = "Role Name cannot exceed 15 characters")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Role Active Status is required")]
        public bool Active { get; set; }
    }
}
