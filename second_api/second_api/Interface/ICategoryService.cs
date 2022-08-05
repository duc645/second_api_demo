using second_api.Dto.Category;
using second_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Interface
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategory(float gia, int pageIndex, int take);
        bool AddCategory(CategoryDtoAdd request);

        List<Category> AddListCategory(List<CategoryDtoAdd> request);

        bool UpdateListCategory(List<CategoryDto> request);

        bool DeleteListCategory(List<int> request);
    }
}
