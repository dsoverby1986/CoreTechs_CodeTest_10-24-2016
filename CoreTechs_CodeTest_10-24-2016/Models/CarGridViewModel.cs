using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreTechs_CodeTest_10_24_2016.Models
{
    public class CarGridViewModel
    {
        public IPagedList<Car> Cars { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Term { get; set; }
    }
}