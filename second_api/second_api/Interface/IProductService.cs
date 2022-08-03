using second_api.Dto.Product;
using second_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Interface
{
    public interface IProductService
    {
        bool AddProduct(ProductDto request);

        List<GetProductDto> GetProductByCateId(int id, float gia); 
    }
}
