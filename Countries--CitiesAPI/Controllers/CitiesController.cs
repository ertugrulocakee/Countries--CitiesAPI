using AutoMapper;
using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using Countries__Cities.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Countries__CitiesAPI.Controllers
{ 
    public class CitiesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICityService _service;

        public CitiesController(IMapper mapper, ICityService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCitiesWithCountry()
        {
            return CreateActionResult(await _service.TGetCitiesWithCountryAsync());
        }

        [HttpGet("[action]/{CityID}")]
        public async Task<IActionResult> GetCityWithCountry(int CityID)
        {
            return CreateActionResult(await _service.TGetCityWithCountryAsync(CityID));
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {

            var cities = await _service.GetAllAsync();

            var cityDTOs = _mapper.Map<List<CityDTO>>(cities);

            return CreateActionResult(CustomResponseDTO<List<CityDTO>>.Success(200,cityDTOs));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {

            var city = await _service.GetByIdAsync(id);

            var cityDto = _mapper.Map<CityDTO>(city);

            return CreateActionResult(CustomResponseDTO<CityDTO>.Success(200, cityDto));

        }


        [HttpPost]
        public async Task<IActionResult> PostCity(CityDTO cityDTO)
        {

            var city = _mapper.Map<City>(cityDTO);

            await _service.AddAsync(city);

            var cityDto = _mapper.Map<CityDTO>(city);

            return CreateActionResult(CustomResponseDTO<CityDTO>.Success(201,cityDto));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity(CityDTO cityDTO)
        {

            var city = _mapper.Map<City>(cityDTO);
            await _service.UpdateAsync(city);
       
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {

            var city = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(city);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }




    }
}
