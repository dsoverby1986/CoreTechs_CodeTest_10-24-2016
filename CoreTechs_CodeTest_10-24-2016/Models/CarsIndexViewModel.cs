using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoreTechs_CodeTest_10_24_2016.Models

{
    public class CarsIndexViewModel
    {
        public List<SelectListItem> Makes { get; set; }
        public List<SelectListItem> Models { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public CarGridViewModel GridModel { get; set; }
    }
}