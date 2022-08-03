using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]

        public string CategoryName { get; set; }

        public DateTime Created { get; set; }

        public ICollection<ProductCategory> productCategories { get; set; }

        public Category()
        {
            productCategories = new List<ProductCategory>();
        }


    }
}
