using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.DTOs
{
    public class CountryWithCityDTO : CountryDTO
    {

        public List<CityDTO> Cities { get; set; }   

    }
}
