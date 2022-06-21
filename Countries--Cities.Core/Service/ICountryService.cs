using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.Service
{
    public interface ICountryService : IService<Country>
    {

        Task<CustomResponseDTO<CountryWithCityDTO>> TGetCountryWithCityAsync(int CountryID);

        Task<CustomResponseDTO<List<CountryWithCityDTO>>> TGetCountriesWithCityAsync();


    }
}
