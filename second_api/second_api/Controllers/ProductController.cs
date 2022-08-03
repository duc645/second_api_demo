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
        public List<GetProductDto> GetProductByCateId(int id, float gia)
        {
            var rs = _productService.GetProductByCateId(id, gia);
            return rs;
        }
    }
}
