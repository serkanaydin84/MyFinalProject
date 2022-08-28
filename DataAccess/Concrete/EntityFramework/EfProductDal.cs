using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            // Normalde C#'da çöp toplayıcısı belli zamanlarda gelir ve bellek temizlik yapar ancak;
            // using nesnesi ile yazdığımız kodlar using bitince anında çöp toplayıcısı gelir ve beni bellekten at der.
            // Çünkü Context nesnesi biraz belleği yoran pahalı bir nesnedir. Burada kullanılan using'e;
            // IDispossable pattern implementation of C# denir.
            using (NorthwindContext context = new NorthwindContext())
            {
                // referansı yakalıyoruz
                var addedEntity = context.Entry(entity);

                // yakalanan referans aslında eklenecek bir nesne onu belirtiyoruz
                addedEntity.State = EntityState.Added;

                // burada da nesneyi ekliyoruz
                context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                // yakalanan referans silinecek nesne olarak belirtiyoruz
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                // yakalanan referans update edilecek nesne olarak belirtiyoruz
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // bu satır aslında filter'a göre Tek satırlık Product nesnesi döndürecek
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // eğer filter null ise ilk kısım çalışır, değilse ikinci kısım çalışır
                return filter == null 
                    ? context.Set<Product>().ToList()                   // filter null ise burası çalışır 
                    : context.Set<Product>().Where(filter).ToList();    // filter var ise burası çalışır
            }
        }
    }
}
