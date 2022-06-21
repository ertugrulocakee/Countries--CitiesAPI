using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Core.DTOs
{
    public class BaseDTO
    {

        public int Id { get; set; }

        public string name { get; set; }

        public int population { get; set; }

        public string imageUrl { get; set; }


    }
}
