using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // Temel voidler için başlangıç
    public interface IResult
    { 
        // yapılan iş in sonucu true mu false mu onu tutar
        bool Success { get; }

        // yapılan işlemin sonucunda kullanıcıya geri döndürülecek mesaj true ya da false olması farketmez
        // ÖRN: ürün eklendi, ürün eklenemedi, bağlantı hatası ...
        string Message { get; }
    }
}
