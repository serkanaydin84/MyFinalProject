using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        // Burada yani Concrete klasöründeki class lar DB deki bir tabloya karşılık geliyor
        // O yüzden Çıplak Class kalmaması gerekiyor.
        // Yani her class ı bir interface vb. referans tipine bağlamak gerekiyor
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
