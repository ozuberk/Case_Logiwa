﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<ProductDetailDto>> GetLiveProducts(string? keyWord, int minStockVal = 0, int maxStockVal = 0);
        IDataResult<Product> GetById(int productId);
        IDataResult<Product> GetLiveProductById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
    }
}
