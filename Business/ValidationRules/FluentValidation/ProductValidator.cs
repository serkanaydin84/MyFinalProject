using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        //kurallar buraya yazılır
        public ProductValidator()
        {
            //product name boş olamaz
            //product name en az 2 karakter olmalı
            //unit price 0 olamaz
            //unit price 0 dan düşük olamaz
            //category Id'si 1 olan ürünlerin price 10 eşit ve düşük olamaz
            //StartWithA kuralını biz koyduk yani salladık. Ürün isimleri A ile başlamalı. Biliyorum saçma bir kural
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalıdır");
        }

        private bool StartWithA(string arg)
        {
            //A ile başlıyorsa true döner, yoksa false döner
            return arg.StartsWith("A");
        }
    }
}
