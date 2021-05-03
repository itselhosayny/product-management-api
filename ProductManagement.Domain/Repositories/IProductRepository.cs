using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();
        Product Add(Product product);
    }
}
