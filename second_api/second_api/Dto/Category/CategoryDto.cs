using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Dto.Category
{
    public class CategoryDto
    {
        
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public DateTime Created { get; set; }
    }

    public class CategoryDtoAdd
    {

        public string CategoryName { get; set; }
    }
}
