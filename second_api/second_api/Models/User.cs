using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }

        
        public DateTime Birth { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new List<UserRole>();
        }
    }
}
