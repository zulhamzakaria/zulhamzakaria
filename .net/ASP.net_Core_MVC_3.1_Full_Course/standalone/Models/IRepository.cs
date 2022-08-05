using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace standalone.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }

        void AddProduct(Product product);
        void DeleteProduct(Product product);
    }
}
