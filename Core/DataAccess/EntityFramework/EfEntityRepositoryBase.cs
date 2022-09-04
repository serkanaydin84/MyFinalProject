using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{ 
    // TEntity ve TContext dememizin nedeni;
    // EfProductDal, EfCategoryDal, EfCustomerDal gibi class'larda hep aynı kodları kullanıyoruz.
    // Sadece değişen kısımlar;
    // Tablo adı yani Product, Category, Customer ve
    // Context'ler değişiyor.
    // DRY - Don't Repeat Yourself mantığına göre
    // Class isimlerini ve context'leri değiştirerek aynı kodları yazmaktan kurtuluyoruz
    //
    // Bu class sayesinden projesimize yeni bir tablo ve class eklediğimizde
    // ekleme, silme, güncelleme get gibi kodları tekrar tekrar yazmayacağız
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity>
        where TEntity: class, IEntity, new()
        where TContext: DbContext, new()
    {
        public void Add(TEntity entity)
        {
            // Normalde C#'da çöp toplayıcısı (garbage collection) belli zamanlarda gelir ve bellek temizlik yapar ancak;
            // using nesnesi ile yazdığımız kodlar using bitince anında çöp toplayıcısı gelir ve beni bellekten at der.
            // Çünkü Context nesnesi biraz belleği yoran pahalı bir nesnedir. Burada kullanılan using'e;
            // IDispossable pattern implementation of C# denir.
            using (TContext context = new TContext())
            {
                // referansı yakalıyoruz
                var addedEntity = context.Entry(entity);

                // yakalanan referans aslında eklenecek bir nesne onu belirtiyoruz
                addedEntity.State = EntityState.Added;

                // burada da nesneyi ekliyoruz
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                // yakalanan referans silinecek nesne olarak belirtiyoruz
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                // yakalanan referans update edilecek nesne olarak belirtiyoruz
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                // bu satır aslında filter'a göre Tek satırlık Product nesnesi döndürecek
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // eğer filter null ise ilk kısım çalışır, değilse ikinci kısım çalışır
                return filter == null
                    ? context.Set<TEntity>().ToList()                   // filter null ise burası çalışır 
                    : context.Set<TEntity>().Where(filter).ToList();    // filter var ise burası çalışır
            }
        }
    }
}
