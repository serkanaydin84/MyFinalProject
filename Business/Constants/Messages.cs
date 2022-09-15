using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    // new'lememek için static olarak yazıyoruz
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductUpdated = "Ürün güncellendi";
        public static string ProductDeleted = "Ürün silindi";
        public static string ProductNameInValid = "Ürün ismi geçersiz";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Kategoriye eklenebilecek ürün sayısına ulaştınız";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";

        public static string MaintenanceTime = "Sistem bakımda";

        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
    }
}
