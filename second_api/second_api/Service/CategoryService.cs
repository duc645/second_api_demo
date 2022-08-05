using second_api.Dto.Category;
using second_api.Interface;
using second_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly MyDbContext _dbContext;

        public CategoryService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddCategory(CategoryDtoAdd request)
        {
            if(request != null)
            {
                var newCate = new Category
                {
                    CategoryName = request.CategoryName,
                    Created = DateTime.Now
                };
                var rs = _dbContext.categories.Add(newCate);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Category> AddListCategory(List<CategoryDtoAdd> request)
        {
            var listViewCate = new List<Category>();
            if(request.Count() > 0)
            {
                foreach (var item in request)
                {
                    if (item.CategoryName != null)
                    {
                        var newCate = new Category
                        {
                            CategoryName = item.CategoryName,
                            Created = DateTime.Now
                        };
                        _dbContext.Add(newCate);
                        _dbContext.SaveChanges();
                        listViewCate.Add(newCate);
                    }    
                }
            }
            return listViewCate;
        }

        public bool DeleteListCategory(List<int> request)
        {
            var ProString = string.Empty;

            var ListCateCanDelete = new List<int>();
            if (request.Count > 0)
            {
                foreach (var item in request)
                {
                    var listCate_Pro = _dbContext.productCategories.Where(pc => pc.CateId == item).ToList();
                    if (listCate_Pro.Count() > 0)
                    {
                        ListCateCanDelete.Add(item);
                        //foreach(var item_Cate_Pro in listCate_Pro)
                        //{
                        //    //ProString += item_Cate_Pro.ProId.ToString() + "#";
                        //    _dbContext.Remove(item_Cate_Pro);
                        //    _dbContext.SaveChanges();
                        //}

                        //var listIdProInt = ProString.Split("#");
                        //foreach (var pro in listIdProInt)
                        //{
                        //}
                    }
                }

                foreach (var cateCanDel in ListCateCanDelete)
                {
                    var rs = _dbContext.categories.Where(c => c.Id == cateCanDel).FirstOrDefault();
                    if (rs != null)
                    {
                        _dbContext.Remove(rs);
                        _dbContext.SaveChanges();
                    }
                }
                return true;
            }
            return false;

            //CODE LONG - 
            //var check = true;
            //if(request.Count() > 0)
            //{
            //    foreach (var item in request)
            //    {
            //        var category = _dbContext.categories.Where(x => x.Id == item).FirstOrDefault();
            //        if(category != null)
            //        {
            //            var oldProcategory = _dbContext.productCategories.Where(x => x.CateId == item).ToList();
            //            if(oldProcategory.Count() > 0)
            //            {
            //                foreach (var productCategory in oldProcategory)
            //                {
            //                    var product = _dbContext.products.Where(x => x.Id == productCategory.ProId).Count();
            //                    if(product == 1)
            //                    {
            //                        check = false;
            //                    }
            //                }
            //                if (check == true)
            //                {
            //                    foreach (var deleteProductCategory in oldProcategory)
            //                    {
            //                        _dbContext.Remove(deleteProductCategory);
            //                        _dbContext.SaveChanges();
            //                    }
            //                    _dbContext.Remove(category);
            //                    _dbContext.SaveChanges();
            //                }
            //                return true;
            //            }
            //        }
            //    }
            //}
            //return false;
        }

        public List<CategoryDto> GetAllCategory(float gia, int pageIndex, int take)
        {
            //var rs = _dbContext.categories.OrderBy(c => c.CategoryName).Skip(take * (pageIndex - 1)).Take(take).ToList();

            var rs = _dbContext.categories.Select(x => new CategoryDto {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Created = x.Created
            }).ToList();


            return rs.Skip(take * (pageIndex - 1)).Take(take).ToList();
        }


        public bool UpdateListCategory(List<CategoryDto> request)
        {
            int flag = 0;
            if (request != null)
            {
                foreach (var item in request)
                {
                    var CateUpdate = _dbContext.categories.Where(c => c.Id == item.Id).FirstOrDefault();
                    if (CateUpdate != null)
                    {
                        CateUpdate.CategoryName = string.IsNullOrEmpty(item.CategoryName) ? CateUpdate.CategoryName : item.CategoryName;
                        CateUpdate.Created = DateTime.UtcNow;
                        _dbContext.SaveChanges();
                        flag = 1;
                    }
                }
                if (flag > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}