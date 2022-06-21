using AutoMapper;
using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using Countries__Cities.Core.Service;
using Countries__CitiesAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Countries__CitiesAPI.Controllers
{
    public class CountriesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICountryService _service;

        public CountriesController(IMapper mapper, ICountryService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCountriesWithCity()
        {

             return CreateActionResult(await _service.TGetCountriesWithCityAsync());    

        }

        [ServiceFilter(typeof(NotFoundFilter<Country>))]
        [HttpGet("[action]{CountryID}")]
        public async Task<IActionResult> GetCountryWithCity(int CountryID)
        {
             
             return CreateActionResult(await _service.TGetCountryWithCityAsync(CountryID));

        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {

            var countryList = await _service.GetAllAsync();

            var countryListDto = _mapper.Map<List<CountryDTO>>(countryList);

            return CreateActionResult(CustomResponseDTO<List<CountryDTO>>.Success(200, countryListDto));

        }


        [ServiceFilter(typeof(NotFoundFilter<Country>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {

            var country = await _service.GetByIdAsync(id);

            var countryDto = _mapper.Map<CountryDTO>(country);

            return CreateActionResult(CustomResponseDTO<CountryDTO>.Success(200, countryDto));

        }

        [HttpPost]
        public async Task<IActionResult> PostCountry(CountryDTO countryDTO)
        {

            var country = _mapper.Map<Country>(countryDTO);

            await _service.AddAsync(country);

            var countryDto = _mapper.Map<CountryDTO>(country);

            return CreateActionResult(CustomResponseDTO<CountryDTO>.Success(201, countryDto));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(CountryDTO countryDTO)
        {

            var country = _mapper.Map<Country>(countryDTO);
            await _service.UpdateAsync(country);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {

            var country = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(country);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }

         

    }
}
