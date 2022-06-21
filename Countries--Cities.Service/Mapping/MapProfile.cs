using AutoMapper;
using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Service.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {

            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<City, CityWithCountryDTO>();
            CreateMap<Country, CountryWithCityDTO>();


        }



    }
}
