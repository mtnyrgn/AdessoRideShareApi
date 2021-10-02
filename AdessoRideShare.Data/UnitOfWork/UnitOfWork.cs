using System;
using System.Threading.Tasks;
using AdessoRideShare.Core.UnitOfWork;
using AdessoRideShare.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdessoRideShare.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(AdessoDbContext adessoDbContext)
        {
            _context = adessoDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommmitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
