using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using second_api.Dto.Category;
using second_api.Interface;
using second_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetAllCategory")]
        public List<CategoryDto> GetAllCategory(float gia, int pageIndex = 1, int take = 3)
        {

            var request = _categoryService.GetAllCategory(10, pageIndex, take);
            return request;
        }

        [HttpPost("AddCategory")]
        public bool AddCategory(CategoryDtoAdd request)
        {
            var rs = _categoryService.AddCategory(request);



            return true;
        }

        [HttpPost("AddCategories")]
        public IActionResult AddListCategory(List<CategoryDtoAdd> request){
            var listNewCate = _categoryService.AddListCategory(request);
            if(listNewCate.Count() != 0)
            {
                return Ok(listNewCate);
            }
            return BadRequest();

        }

        [HttpPut("UpdateCate")]
        public bool UpdateCategory (List<CategoryDto> request)
        {
            var rs = _categoryService.UpdateListCategory(request);
            return true;
        }

        [HttpDelete("DeleteCate")]
        public bool DeleteCate (List<int> request)
        {
            var rs = _categoryService.DeleteListCategory(request);
            return rs;
        }

    }
}
