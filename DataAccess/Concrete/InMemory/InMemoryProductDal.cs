using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        // Bu tanımlama tüm metodların dışında global olarak tanımlandığı için _ ile tanımladık
        List<Product> _products;
        public InMemoryProductDal()
        {
            // Veriler Oracle, Sql, Postres, MongoDb den gelsin hiç önemli değil
            _products = new List<Product> {
                new Product{ProductId=1, CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                new Product{ProductId=2, CategoryId=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                new Product{ProductId=3, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product{ProductId=4, CategoryId=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                new Product{ProductId=4, CategoryId=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = null;
            // Listeden elemanı silmek için 2 yol var;
            // I.YOL - Listeyi döngü ile tek tek dolaşmak
            /*
            foreach (var p in _products)
            {
                if (product.ProductId==p.ProductId)
                {
                    productToDelete = p;
                }
            }
            _products.Remove(productToDelete);
            */

            // 2.YOL - LINQ kullanmak
            // LINQ - Language Integrated Query - Dile Gömülü Sorgulama
            // SingleOrDefault Bu kod tek bir eleman bulmaya yarar
            // p => p.ProductId == product.ProductId demek;
            // p.ProductId si benim gönderdiğimi product.ProductId sine eşit ise bunu p ye eşitle
            productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        // TÜM ÜRÜNLERİ DÖNDÜRÜR
        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        // ÜRÜNLERİ KATEGORİYE GÖRE FİLTRELER
        public List<Product> GetAllByCategory(int categoryId)
        {
            // istediğim kategoriye uygun kategorileri liste olarak döndür
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void Update(Product product)
        {
            // Gönderdiğim productId sine sahip olan listedeki product ı bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
