using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.UOW;
using ProductManagement.Domain.Validation;
using ProductManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository ProductRepository{ get; }

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            ProductRepository = productRepository;
        }

        public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();

            }catch(DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("IX_Products_Code"))
                    return Result.Failure("Code is already taken");

                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
