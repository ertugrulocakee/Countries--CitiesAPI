using AutoMapper;
using Countries__Cities.Core.DTOs;
using Countries__Cities.WEB.Models;

namespace Countries__Cities.WEB.Services
{
    public class APICountryService
    {

        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public APICountryService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<CountryWithCityViewModel>> GetAllAsync()
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<CountryWithCityDTO>>>("countries/GetCountriesWithCity");

            return _mapper.Map<List<CountryWithCityViewModel>>(response.Data);

        }

        public async Task<CountryWithCityViewModel> GetByIdAsync(int id){ 
         

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<CountryWithCityViewModel>>($"countries/{id}");
            return _mapper.Map<CountryWithCityViewModel>(response.Data); 

        }

        public async Task<CountryViewModel> SaveAsync(CountryDTO countryDTO)
        {

            var response = await _httpClient.PostAsJsonAsync("countries", countryDTO);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<CountryDTO>>();

            return _mapper.Map<CountryViewModel>(responseBody.Data);

        }

        public async Task<bool> UpdateAsync(CountryDTO countryDTO)
        {
            var response = await _httpClient.PutAsJsonAsync("countries", countryDTO);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"countries/{id}");

            return response.IsSuccessStatusCode;
        }




    }
}
