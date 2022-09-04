using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        // kullanıcıya data ve mesaj veriliyor
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }

        // sadece data veriliyor
        public ErrorDataResult(T data) : base(data, false)
        {

        }

        // sadece mesaj veriliyor. default data denk gelir
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }

        // kullanıcıya hiçbirşey verilmiyor
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
