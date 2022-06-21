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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context ) : base(context)
        {
        }

        public async Task<List<City>> GetCitiesWithCountryAsync()
        {
            return await _context.Cities.Include(x=>x.Country).ToListAsync();     
        }

        public Task<City> GetCityWithCountryAsync(int CityID)
        {
            return _context.Cities.Include(x => x.Country).Where(x => x.Id == CityID).SingleOrDefaultAsync();
        }
    }
}
