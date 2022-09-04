using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        // kullanıcıya data ve mesaj veriliyor
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }

        // sadece data veriliyor
        public SuccessDataResult(T data) : base(data, true)
        {

        }

        // sadece mesaj veriliyor. default data denk gelir
        public SuccessDataResult(string message) : base(default, true, message)
        {

        }

        // kullanıcıya hiçbirşey verilmiyor
        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
