using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        // eğer sonuç false ise ve mesaj verilmek isteniyorsa bu constractor çalışsın
        public ErrorResult(string message) : base(false, message)
        {

        }

        // eğer sonuç false ise ancak mesaj verilmek istenmiyorsa bu constractor çalışsın
        public ErrorResult() : base(false)
        {

        }
    }
}
