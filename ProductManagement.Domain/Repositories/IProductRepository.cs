using ProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetAsync(int id);
        Product Add(Product product);
    }
}
