using AutoMapper;
using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using Countries__Cities.Core.Repository;
using Countries__Cities.Core.Service;
using Countries__Cities.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Service.Services
{
    public class CountryService : Service<Country>, ICountryService
    {
  
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(IGenericRepository<Country> repository, IUnitOfWork unitOfWork,ICountryRepository countryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<List<CountryWithCityDTO>>> TGetCountriesWithCityAsync()
        {
            var countries = await _countryRepository.GetCountriesWithCitiesAsync();
            var countryDTOs = _mapper.Map<List<CountryWithCityDTO>>(countries);
            return CustomResponseDTO<List<CountryWithCityDTO>>.Success(200,countryDTOs);
        }

        public async Task<CustomResponseDTO<CountryWithCityDTO>> TGetCountryWithCityAsync(int CountryID)
        {
            var country = await _countryRepository.GetCountryWithCitiesAsync(CountryID);
            var countryDTO = _mapper.Map<CountryWithCityDTO>(country);
            return CustomResponseDTO<CountryWithCityDTO>.Success(200, countryDTO);
        }
    }
}
