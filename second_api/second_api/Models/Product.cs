using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public double Price { get; set; }

        [Required]
        [StringLength(100)]

        public string ProductName { get; set; }

        public int SoLuong { set; get; }

        public DateTime Created { get; set; }

        public ICollection<ProductCategory> productCategories { get; set; }

        public Product()
        {
            productCategories = new List<ProductCategory>();
        }



    }
}
