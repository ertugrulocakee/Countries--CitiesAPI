using Countries__Cities.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.Repository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {

        Task<Country> GetCountryWithCitiesAsync(int CountryID);

        Task<List<Country>> GetCountriesWithCitiesAsync();

    }
}
