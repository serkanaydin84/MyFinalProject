using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        // IDataResult hem sonucu hem mesajı hem de döndüreceği veri/verileri döndürür
        // IDataResult<T> deki T burada List<Product> oldu
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryID(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProducDetails();
        IDataResult<Product> GetById(int productId);

        // IResult hem sonucu hem de mesajı döndürebilir
        IResult Add(Product product);
    }
}
