using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
