using Countries__Cities.Core.UnitOfWorks;
using Countries__Cities.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {

        protected readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {

            _context.SaveChanges();

        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
