using AutoMapper;
using Countries__Cities.Core.DTOs;
using Countries__Cities.WEB.Models;

namespace Countries__Cities.WEB.Services
{
    public class APICityService
    {


        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public APICityService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<CityWithCountryViewModel>> GetAllAsync()
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<CityWithCountryDTO>>>("cities/GetCitiesWithCountry");

            return _mapper.Map<List<CityWithCountryViewModel>>(response.Data);

        }

        public async Task<CityViewModel> GetByIdAsync(int id)
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<CityDTO>>($"cities/{id}");
            return _mapper.Map<CityViewModel>(response.Data);

        }

        public async Task<CityViewModel> SaveAsync(CityDTO cityDTO)
        {

            var response = await _httpClient.PostAsJsonAsync("cities", cityDTO);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<CityDTO>>();

            return _mapper.Map<CityViewModel>(responseBody.Data);

        }

        public async Task<bool> UpdateAsync(CityDTO cityDTO)
        {
            var response = await _httpClient.PutAsJsonAsync("cities", cityDTO);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"cities/{id}");

            return response.IsSuccessStatusCode;
        }


    }
}
