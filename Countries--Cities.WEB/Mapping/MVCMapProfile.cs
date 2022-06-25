using AutoMapper;
using Countries__Cities.Core.DTOs;
using Countries__Cities.WEB.Models;

namespace Countries__Cities.WEB.Mapping
{
    public class MVCMapProfile : Profile
    {

        public MVCMapProfile()
        {

            CreateMap<CountryDTO, CountryViewModel>();
            CreateMap<CityDTO, CityViewModel>();
            CreateMap<CountryWithCityDTO, CountryWithCityViewModel>();
            CreateMap<CityWithCountryDTO, CityWithCountryViewModel>();

        }


    }
}
