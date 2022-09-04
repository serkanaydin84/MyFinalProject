using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        // Bu komut ile ister Memory de ister EntityFramework ile çalışabilirsin
        IProductDal _productDal;

        // bu constractor ile hangi veritabanı olursa olsun çalışır. Sadece istediğimiz veritabanını seçmemiz gerekiyor
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            // buisness codes
            if (product.ProductName.Length < 2)
            {
                // magic strings
                return new ErrorResult(Messages.ProductNameInValid);
            }
            _productDal.Add(product);
            return new SuccessResult("Ürün eklendi");
        }

        public IDataResult<List<Product>> GetAll()
        {
            // hergün saat 22'de sistemi kapatmak istiyoruz yani kullanıcıya 22'den sonra hata versin
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            // İş Kodları yazılır - buisness codes
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        // KATEGORİYE GÖRE ÜRÜNLER GET EDİLİYOR
        public IDataResult<List<Product>> GetAllByCategoryID(int id)
        {
            return  new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        // ÜRÜNÜN ID SİNE GÖRE DETAYI GET EDİLİYOR
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        // FİYATI BELİRLENEN ALT VE ÜST SINIRINDAKİ ÜRÜNLERİ GET EDİYOR
        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProducDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
