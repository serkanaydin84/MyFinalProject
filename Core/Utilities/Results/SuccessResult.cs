using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        // eğer sonuç true ise ve mesaj verilmek isteniyorsa bu constractor çalışsın
        public SuccessResult(string message) : base(true, message)
        {

        }

        // eğer sonuç true ise ancak mesaj verilmek istenmiyorsa bu constractor çalışsın
        public SuccessResult() : base(true)
        {

        }
    }
}
