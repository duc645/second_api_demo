using second_api.Dto.Product;
using second_api.Interface;
using second_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace second_api.Service
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _dbContext;
        public ProductService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddProduct(ProductDto request)
        {
            //if(request != null)
            //{
            //    var newProduct = new Product()
            //    {
            //        Price = request.Price,
            //        ProductName = request.ProductName,
            //        Created = DateTime.Now,
            //        SoLuong = request.SoLuong
            //    };
            //    _dbContext.products.Add(newProduct);
            //    _dbContext.SaveChanges();

            //    foreach (var item in request.CategoryId)
            //    {
            //        if(_dbContext.categories.Where(x => x.Id == item.CateId).Count() > 0)
            //        {
            //            var newProductCategory = new ProductCategory()
            //            {
            //                ProId = newProduct.Id,
            //                CateId = item.CateId
            //            };
            //            _dbContext.productCategories.Add(newProductCategory);
            //            _dbContext.SaveChanges();
            //        }
            //        else
            //        {
            //            if(item.CategoryName != null)
            //            {
            //                var newCategory = new Category()
            //                {
            //                    Created = DateTime.Now,
            //                    CategoryName = item.CategoryName,
            //                };
            //                _dbContext.categories.Add(newCategory);
            //                _dbContext.SaveChanges();
            //            }
            //        }
            //    }
            //    return true;
            //}
            //return false;

            var categoryId = string.Empty;

            if(request != null)
            {
                foreach(var item in request.CategoryId)
                {
                    if (!string.IsNullOrEmpty(item.CategoryName) && string.IsNullOrEmpty(item.CateId))
                    {
                        var newCategory = new Category
                        {
                            CategoryName = item.CategoryName
                        };
                        categoryId += newCategory.Id + "#";
                        _dbContext.categories.Add(newCategory);
                        _dbContext.SaveChanges();
                    }
                    else if(!string.IsNullOrEmpty(item.CateId))
                    {
                        if(_dbContext.categories.Where(x => x.Id == int.Parse(item.CateId)).Count() != 0)
                        {
                            categoryId += item.CateId + "#";
                        }
                    }
                }
                if(!string.IsNullOrEmpty(categoryId))
                {
                    var newProduct = new Product
                    {
                        Price = request.Price,
                        SoLuong = request.SoLuong,
                        ProductName = request.ProductName
                    };
                    _dbContext.products.Add(newProduct);
                    _dbContext.SaveChanges();
                    var categoryList = categoryId.Split("#");
                    foreach (var item in categoryList)
                    {
                        if ((_dbContext.categories.Where(C => C.Id == int.Parse(item)).FirstOrDefault() != null))
                        {
                            var newPro_Cate = new ProductCategory
                            {
                                ProId = newProduct.Id,
                                CateId = int.Parse(item),
                        };
                            _dbContext.productCategories.Add(newPro_Cate);
                            _dbContext.SaveChanges();
                        }

                    }
                    return true;
                }

            }
            return false;

        }

        public bool DeleteProduct(List<int> request)
        {
            int count = 0;
            if(request != null)
            {
                foreach(var item in request)
                {
                    var pro_cate = _dbContext.productCategories.Where(pc => pc.ProId == item).ToList();
                    foreach(var c in pro_cate)
                    {
                        _dbContext.Remove(c);
                        _dbContext.SaveChanges();
                    }
                    var pro = _dbContext.products.Where(p => p.Id == item).FirstOrDefault();
                    if(pro != null)
                    {
                        _dbContext.Remove(pro);
                        _dbContext.SaveChanges();
                        count++;
                    }
                }
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<GetProductDto> GetProductByCateId(int id, float gia, int pageIndex, int take)
        {
            var productcategories = _dbContext.productCategories.Where(pc => pc.CateId == id).ToList();
            if (productcategories != null)
            {
                var rs = new List<GetProductDto>();
                foreach (var item in productcategories)
                {
                    var oldProduct = _dbContext.products.Where(p => p.Id == item.ProId).FirstOrDefault();
                    if (oldProduct != null)
                    {
                        var newPro = new GetProductDto()
                        {
                            Id = oldProduct.Id,
                            Price = oldProduct.Price,
                            ProductName = oldProduct.ProductName,
                            SoLuong = oldProduct.SoLuong
                        };
                        rs.Add(newPro);
                    }
                }
                return rs.Skip(take * (pageIndex - 1)).Take(take).ToList();

            }
            return null;

            //var s = (from product in _dbContext.products
            //        join productCategorie in _dbContext.productCategories on product.Id equals productCategorie.ProId
            //        where ((id == 0 || productCategorie.CateId == id) && (gia == 0 || product.Price == gia))
            //        orderby (product.Price)
            //        select (new GetProductDto
            //        {
            //            Id = product.Id,
            //            Price = product.Price,
            //            ProductName = product.ProductName,
            //            SoLuong = product.SoLuong
            //        })).Skip(take * (pageIndex - 1)).Take(take);

            //return s.ToList();

        }

        public bool UpdateProduct(List<UpdateProductDto> request)
        {
            int Flag = 0;
            if(request.Count() > 0)
            {
                foreach (var item in request)
                {
                    var pro = _dbContext.products.Where(x => x.Id == item.Id).FirstOrDefault();
                    if (pro != null)
                    {
                        pro.Price = item.Price != 0 ? item.Price : pro.Price;
                        pro.ProductName = item.ProductName != null ? item.ProductName : pro.ProductName;
                        pro.SoLuong = item.SoLuong != 0 ?item.SoLuong :pro.SoLuong;
                        _dbContext.SaveChanges();
                        int count = 0;
                        foreach(var ct in item.CategoryId)
                        {
                            count++;
                        }
                        if(count>0)
                        {

                           //them danh muc neu ko co o cho nay


                            var deleteProcductCategory = _dbContext.productCategories.Where(x => x.ProId == item.Id).ToList();
                            foreach (var delete in deleteProcductCategory)
                            {
                                _dbContext.Remove(delete);
                                _dbContext.SaveChanges();
                            }
                            _dbContext.SaveChanges();
                            foreach (var category in item.CategoryId)
                            {
                                var newProCartegory = new ProductCategory
                                {
                                    CateId = int.Parse(category.CateId),
                                    ProId = pro.Id,
                                };
                                _dbContext.productCategories.Add(newProCartegory);
                                _dbContext.SaveChanges();
                            }
                            Flag++;
                        }
                    }
                }
                if (Flag > 0)
                {
                    return true;
                } 
            }
            return false;
        }
    }


}
