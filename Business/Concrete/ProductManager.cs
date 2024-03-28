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
            try
            {
                _productDal.Add(product);
                return new SuccessResult(Messages.Added);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IResult Delete(Product product)
        {
            try
            {
                if (product == null)
                {
                    return new ErrorResult(Messages.DataNotFound);
                }
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
            try
            {
                var result = _productDal.GetAll();
                if (!result.Any())
                {
                    return new SuccessDataResult<List<Product>>(Messages.DataNotFound);
                }
                return new SuccessDataResult<List<Product>>(result, Messages.DataListed);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Product>>(ex.Message);
            }



        }

        public IDataResult<Product> GetById(int productId)
        {
            try
            {
                var result = _productDal.Get(p => p.ProductId == productId);

                if (result == null)
                {
                    return new SuccessDataResult<Product>(Messages.DataNotFound);
                }
                return new SuccessDataResult<Product>(result, Messages.DataListed);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Product>(ex.Message);
            }
        }

        public IDataResult<Product> GetLiveProductById(int productId)
        {
            try
            {
                var result = _productDal.Get(
                p => p.ProductId == productId &&
                     p.CategoryId != null &&
                     p.StockQuantity > p.Category.MinStockQuantity
            );

                if (result == null)
                {
                    return new SuccessDataResult<Product>(Messages.DataNotFound);
                }

                return new SuccessDataResult<Product>(result, Messages.DataListed);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Product>(ex.Message);
            }
        }

        public IDataResult<List<ProductDetailDto>> GetLiveProducts(string? keyWord, int minStockVal = 0, int maxStockVal = 0)
        {

            try
            {
                var result = _productDal.GetLiveProducts(keyWord, minStockVal, maxStockVal);
                if (result == null)
                {
                    return new ErrorDataResult<List<ProductDetailDto>>(Messages.DataNotFound);
                }
                return new SuccessDataResult<List<ProductDetailDto>>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(ex.Message);
            }

        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            try
            {
                if (product == null)
                {
                    return new ErrorResult(Messages.DataNotFound);
                }
                _productDal.Update(product);
                return new SuccessResult(Messages.Updated);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
