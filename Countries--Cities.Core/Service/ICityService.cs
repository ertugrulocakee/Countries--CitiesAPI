using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.Service
{
    public interface ICityService : IService<City>
    {

        Task<CustomResponseDTO<CityWithCountryDTO>> TGetCityWithCountryAsync(int CityID);

        Task<CustomResponseDTO<List<CityWithCountryDTO>>> TGetCitiesWithCountryAsync();

    }
}
