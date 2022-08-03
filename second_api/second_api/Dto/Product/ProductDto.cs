using second_api.Dto.ProductCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public double Price { get; set; }

        [Required]
        [StringLength(100)]

        public string ProductName { get; set; }

        public int SoLuong { set; get; }
        public List<AddProductCategoryDto> CategoryId { get; set; }
    }

    public class GetProductDto
    {
        public int Id { get; set; }
        public double Price { get; set; }

        [Required]
        [StringLength(100)]

        public string ProductName { get; set; }

        public int SoLuong { set; get; }
    }
}
