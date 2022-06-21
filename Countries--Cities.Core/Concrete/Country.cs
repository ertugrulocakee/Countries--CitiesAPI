using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.Concrete
{
    public class Country : BaseEntity
    {

        public string capital { get; set; }

        public ICollection<City> Cities { get; set; }


    }
}
