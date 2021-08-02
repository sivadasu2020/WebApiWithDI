using System;
using System.Collections.Generic;
using WebapiWithDI.Common;

namespace WebapiWithDI.Abstractions
{
    public interface IDal
    {
        List<Product> GetProducts();
        bool Create(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}
