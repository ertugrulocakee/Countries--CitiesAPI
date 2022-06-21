using Countries__Cities.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.Repository
{
    public interface ICityRepository : IGenericRepository<City>
    {

        Task<City> GetCityWithCountry(int CityID);

        Task<List<City>> GetCitiesWithCountry();  

    }
}
