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
            if(request != null)
            {
                var newProduct = new Product()
                {
                    Price = request.Price,
                    ProductName = request.ProductName,
                    Created = DateTime.Now,
                    SoLuong = request.SoLuong
                };
                _dbContext.products.Add(newProduct);
                _dbContext.SaveChanges();
                
                foreach (var item in request.CategoryId)
                {
                    if(_dbContext.categories.Where(x => x.Id == item.CateId).Count() > 0)
                    {
                        var newProductCategory = new ProductCategory()
                        {
                            ProId = newProduct.Id,
                            CateId = item.CateId
                        };
                        _dbContext.productCategories.Add(newProductCategory);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        if(item.CategoryName != null)
                        {
                            var newCategory = new Category()
                            {
                                Created = DateTime.Now,
                                CategoryName = item.CategoryName,
                            };
                            _dbContext.categories.Add(newCategory);
                            _dbContext.SaveChanges();
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public List<GetProductDto> GetProductByCateId(int id, float gia)
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
                return rs;
            }
            return null;

            //var s = from product in _dbContext.products
            //        join productCategorie in _dbContext.productCategories on product.Id equals productCategorie.ProId
            //        where ((id == 0 || productCategorie.CateId == id) && (gia == 0 || product.Price == gia))
            //        select (new GetProductDto
            //        {
            //            Id = product.Id,
            //            Price = product.Price,
            //            ProductName = product.ProductName,
            //            SoLuong = product.SoLuong
            //        });

            //return s.ToList();

        }
    }
}
