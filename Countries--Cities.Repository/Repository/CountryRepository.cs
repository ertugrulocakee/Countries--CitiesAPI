using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.Repository;
using Countries__Cities.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Repository.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Country>> GetCountriesWithCitiesAsync()
        {
            return await _context.Countries.Include(x => x.Cities).ToListAsync();
        }

        public async Task<Country> GetCountryWithCitiesAsync(int CountryID)
        {
            return await _context.Countries.Include(x => x.Cities).Where(x => x.Id == CountryID).SingleOrDefaultAsync();
        }
    }
}
