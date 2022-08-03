using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Models
{
    [Table("ProductCategory")]
    public class ProductCategory
    {

        public int ProId { get; set; }
       
        public Product product { get; set; }

        public int CateId { get; set; }

        public Category category { get; set; }


    }
}
