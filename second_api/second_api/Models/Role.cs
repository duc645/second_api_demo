using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            UserRoles = new List<UserRole>();
        }
    }
}
