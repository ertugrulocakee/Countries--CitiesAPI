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
    public class CityService : Service<City>, ICityService
    {

        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(IGenericRepository<City> repository, IUnitOfWork unitOfWork, ICityRepository cityRepository, IMapper mapper) : base(repository, unitOfWork)
        {

            _cityRepository = cityRepository;   
            _mapper = mapper;       

        }

        public async Task<CustomResponseDTO<List<CityWithCountryDTO>>> TGetCitiesWithCountryAsync()
        {
            var cities = await _cityRepository.GetCitiesWithCountryAsync();
            var cityDTOs = _mapper.Map<List<CityWithCountryDTO>>(cities);
            return CustomResponseDTO<List<CityWithCountryDTO>>.Success(200, cityDTOs);  
        }

        public async Task<CustomResponseDTO<CityWithCountryDTO>> TGetCityWithCountryAsync(int CityID)
        {
            var city = await _cityRepository.GetCityWithCountryAsync(CityID);
            var cityDTO = _mapper.Map<CityWithCountryDTO>(city);
            return CustomResponseDTO<CityWithCountryDTO>.Success(200, cityDTO);
        }
    }
}
