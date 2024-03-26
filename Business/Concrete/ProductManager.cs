using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            if (string.IsNullOrEmpty(product.ProductTitle) || product.ProductTitle.Length > 200)
            {
                //hata mesajı verecek  -
                Console.WriteLine("1");
            }
            if (product.CategoryId == null)
            {
                //hata mesajı verecek  -

                Console.WriteLine("2");

            }
            if (product.StockQuantity <= 0)
            {
                //hata mesajı verecek  -

                Console.WriteLine("3");

            }
            if (product.StockQuantity < //category.minstock)
            {
                //hata mesajı verecek  -

                Console.WriteLine("4");
            }
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryID(int categoryId)
        {
            return _productDal.GetAll(p => p.Category.CategoryId == categoryId);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
