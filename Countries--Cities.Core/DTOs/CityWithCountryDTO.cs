using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.DTOs
{
    public class CityWithCountryDTO : CityDTO
    {

        public CountryDTO Country { get; set; } 

    }
}
