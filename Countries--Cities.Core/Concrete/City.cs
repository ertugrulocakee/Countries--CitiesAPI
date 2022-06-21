using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.Concrete
{
    public class City : BaseEntity
    {

        public int CountryId { get; set; }

        public Country Country { get; set; }


    }
}
