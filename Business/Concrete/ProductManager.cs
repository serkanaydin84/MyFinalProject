using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.CCS;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //ÖNEMLİ NOT: Bir Manager class'ına kendi DAL class'ı hariç başka bir DAL class'ı enjekte edilemez
        //ANCAK, başka bir Service enjekte edilebilir

        // Bu komut ile ister Memory de ister EntityFramework ile çalışabilirsin
        IProductDal _productDal;
        ICategoryService _categoryService;

        // bu constractor ile hangi veritabanı olursa olsun çalışır. Sadece istediğimiz veritabanını seçmemiz gerekiyor
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //business codes
            //iş kurallarını tek tek Core katmanındaki BusinessRules class'ına yolluyoruz
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName)
                //CheckIfProductCountOfCategoryCorrect(product.CategoryId)
                //CheckIfCategoryLimitExceded()
                );

            //herhangi bir kuralda hata varsa
            if (result != null)
            {
                //hata kuralını döndür
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Product>> GetAll()
        {
            // her gün saat 22'de sistemi kapatmak istiyoruz yani kullanıcıya 22'den sonra hata versin
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            // İş Kodları yazılır - business codes
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        // KATEGORİYE GÖRE ÜRÜNLER GET EDİLİYOR
        public IDataResult<List<Product>> GetAllByCategoryID(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
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


        //////////////////////////////////////////////////////////////////////////////////
        // *******************************************************************************
        //                          İŞ KURALLARI
        // *******************************************************************************
        //////////////////////////////////////////////////////////////////////////////////
        ///
        /// 
        //Bir kategoriye belirlenen sayıdan fazla ürünün eklenmemesini sağlayan kontrol
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        //Aynı isimli birden fazla ürünün kaydedilmemesi sağlayan kontrol
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetList();
            if (result.Data.Count>35)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }
    }
}
