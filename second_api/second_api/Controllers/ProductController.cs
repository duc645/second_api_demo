using Microsoft.AspNetCore.Mvc;
using second_api.Dto.Product;
using second_api.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addProduct")]
        public bool add(ProductDto request)
        {
            var rs = _productService.AddProduct(request);
            return rs;
        }

        [HttpGet("GetProductByCateId")]
        public List<GetProductDto> GetProductByCateId(int id, float gia, int pageIndex = 1, int take = 5)
        {
            var rs = _productService.GetProductByCateId(id, gia, pageIndex, take);
            return rs;
        }

        [HttpPut("updateProduct")]
        public bool UpdateProduct(List<UpdateProductDto> request)
        {
            var rs = _productService.UpdateProduct(request);
            return rs;
        }

        [HttpDelete("deleteProduct")]
        public bool DeleteProduct(List<int> request)
        {
            var rs = _productService.DeleteProduct(request);
            return rs;

        }
    }
}
