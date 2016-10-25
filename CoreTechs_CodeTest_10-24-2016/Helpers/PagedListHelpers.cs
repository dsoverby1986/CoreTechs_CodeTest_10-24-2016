using CoreTechs_CodeTest_10_24_2016.Models;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreTechs_CodeTest_10_24_2016.Helpers
{
    public class PagedListHelpers
    {
        public static IPagedList<Car> GetNextCars(string make, string model, string term, int pageIndex = 0, int pageSize = 10)
        {
            HCL2Entities db = new HCL2Entities();
            IQueryable<Car> cars;
            if (!string.IsNullOrWhiteSpace(term))
                cars = db.Cars.Where(c => c.make.Contains(term) || c.model_name.Contains(term) || c.model_year.Contains(term) || c.model_trim.Contains(term)).OrderBy(c => c.make);
            else
            {
                cars = db.Cars.OrderBy(c => c.make);
                if (!string.IsNullOrWhiteSpace(make))
                    cars = cars.Where(c => c.make.Equals(make)).OrderBy(c => c.model_name).ThenByDescending(c => c.model_year);
                if (!string.IsNullOrWhiteSpace(model))
                    cars = cars.Where(c => c.model_name.Equals(model));
            }
            var totalCount = cars.Count();
            return cars.ToPagedList(pageIndex, pageSize, totalCount);
        }
    }
}