using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Domain.UOW
{
    public interface IUnitOfWork
    {
        Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        IProductRepository ProductRepository { get; } 
    }
}
