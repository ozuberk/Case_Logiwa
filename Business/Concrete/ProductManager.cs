using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new Result(true, Messages.Added);
        }

        public IResult Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
                return new Result(true, Messages.Deleted);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IDataResult<List<Product>> GetAll()
        {
            var result = _productDal.GetAll();
            if (!result.Any())
            {
                return new SuccessDataResult<List<Product>>(Messages.DataEmpty);
            }

            return new SuccessDataResult<List<Product>>(result, Messages.DataListed);

        }

        public IDataResult<Product> GetById(int productId)
        {
            var result = _productDal.Get(p => p.ProductId == productId);

            if (result == null)
            {
                return new SuccessDataResult<Product>(Messages.DataEmpty);
            }
            return new SuccessDataResult<Product>(result, Messages.DataListed);
        }

        public IDataResult<Product> GetLiveProductById(int productId)
        {
            var result = _productDal.Get(
                p => p.ProductId == productId &&
                     p.CategoryId != null &&
                     p.StockQuantity > p.Category.MinStockQuantity
            );

            if (result == null)
            {
                return new SuccessDataResult<Product>(Messages.DataEmpty);
            }

            return new SuccessDataResult<Product>(result, Messages.DataListed);
        }

        public IDataResult<List<ProductDetailDto>> GetLiveProducts(string? keyWord, int minStockVal = 0, int maxStockVal = 0)
        {

            var result = _productDal.GetLiveProducts(keyWord, minStockVal, maxStockVal);
            return new SuccessDataResult<List<ProductDetailDto>>(result);
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            try
            {

                _productDal.Update(product);
                return new Result(true, Messages.Updated);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
