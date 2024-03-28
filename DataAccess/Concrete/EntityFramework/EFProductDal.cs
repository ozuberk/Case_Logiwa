using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDal : EfEntityRepositoryBase<Product, LogiwaContext>, IProductDal
    {
        public List<ProductDetailDto> GetLiveProducts(string? keyWord, int minStockVal = 0, int maxStockVal = 0)
        {
            using (LogiwaContext context = new LogiwaContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             where p.StockQuantity > c.MinStockQuantity &&
                             p.CategoryId != null &&
                             p.Category.MinStockQuantity != 0 &&
                             (
                              keyWord == null ||
                              p.ProductTitle.Contains(keyWord) ||
                              p.Description.Contains(keyWord) ||
                              p.Category.CategoryName.Contains(keyWord)
                             ) &&
                             (
                              p.StockQuantity < maxStockVal ||
                              p.StockQuantity > minStockVal
                              )
                             select new ProductDetailDto { ProductId = p.ProductId, ProductTitle = p.ProductTitle, CategoryName = c.CategoryName, StockQuantity = p.StockQuantity, Description = p.Description };
                return result.ToList();
            }
        }
    }
}
