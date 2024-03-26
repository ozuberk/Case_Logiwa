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
        public List<ProductDetailDto> GetProductDetails()
        {
            using (LogiwaContext context = new LogiwaContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto { ProductId = p.ProductId, ProductTitle = p.ProductTitle, CategoryName = c.CategoryName, StockQuantity = p.StockQuantity, Description = p.Description };
                return result.ToList();
            }
        }
    }
}
