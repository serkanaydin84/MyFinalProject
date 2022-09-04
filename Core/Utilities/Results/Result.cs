using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // this(success)'in anlamı;
        // Success = success; bu kodu her iki constractor'da da yazmamış gerekiyordu ancak DRY (Don't repeat yoruself) göre
        // birinci constractor'da yukarıdaki kodu yazmadık ve
        // success'i ikinci constratctor'da gönderdik ve çalıştır dedik. Böylece;
        // Success success; kodunu her iki constractor'da da çalıştırmış olduk
        // Developer bu sayede ister sadece sonucu döndürür ya da hem sonucu hem de mesaj döndürür
        public Result(bool success, string message) : this(success)
        {
            // normalde getter lar readonly dir ancak;
            // readonly ler constractor dan set edilebilir
            Message = message;
        }

        // overloading - aşırı yükleme
        public Result(bool success)
        {
            // normalde getter lar readonly dir ancak;
            // readonly ler constractor dan set edilebilir
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
