using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoreTechs_CodeTest_10_24_2016.Models;
using CoreTechs_CodeTest_10_24_2016.Helpers;

namespace CoreTechs_CodeTest_10_24_2016.Controllers
{
    public class CarsController : Controller
    {
        private HCL2Entities db = new HCL2Entities();

        // GET: Cars
        public ActionResult Index(CarsIndexViewModel model)
        {
            IQueryable<Car> cars = db.Cars;
            model = new CarsIndexViewModel()
            {
                Makes = cars.Select(c => new SelectListItem() { Value = c.make, Text = c.make }).Distinct().ToList(),
                Models = new List<SelectListItem>(),
                GridModel = new CarGridViewModel()
                {
                    Cars = PagedListHelpers.GetNextCars(null, null, null, 0, 10)
                }
            };
            return View(model);
        }

        public ActionResult GetNextCars(string make, string model, string term, int? page, int pageSize = 10)
        {
            var pageIndex = page - 1 ?? 0;
            CarGridViewModel returnModel = new CarGridViewModel()
            {
                Cars = PagedListHelpers.GetNextCars(make, model, term, pageIndex, pageSize),
                Make = make,
                Model = model,
                Term = term
            };
            return PartialView("_CarsGrid", returnModel);
        }

        public ActionResult GetMakes(string term)
        {
            var makes = db.Cars.Where(c => c.make.Contains(term) || c.model_name.Contains(term) || c.model_year.Contains(term) || c.model_trim.Contains(term)).Select(c => c.make).Distinct().OrderBy(c => c).ToList();
            List<SelectListItem> makeList = makes.Select(m => new SelectListItem() { Value = m, Text = m }).ToList();
            return PartialView("_MakesDropDownList", makeList);
        }

        public ActionResult GetModels(string make)
        {
            var models = db.Cars.Where(c => c.make.Equals(make)).Select(c => c.model_name).Distinct().ToList();
            List<SelectListItem> modelList = models.Select(m => new SelectListItem() { Value = m, Text = m }).ToList();
            return PartialView("_ModelsDropDownList", modelList);
        }

        public ActionResult SearchCars(string term)
        {
            CarGridViewModel model = new CarGridViewModel()
            {
                Cars = PagedListHelpers.GetNextCars(null, null, term, 0, 10),
                Term = term
            };
            return PartialView("_CarsGrid", model);
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,make,model_name,model_trim,model_year,body_style,engine_position,engine_cc,engine_num_cyl,engine_type,engine_valves_per_cyl,engine_power_ps,engine_power_rpm,engine_torque_nm,engine_torque_rpm,engine_bore_mm,engine_stroke_mm,engine_compression,engine_fuel,top_speed_kph,zero_to_100_kph,drive_type,transmission_type,seats,doors,weight_kg,length_mm,width_mm,height_mm,wheelbase,lkm_hwy,lkm_mixed,lkm_city,fuel_capacity_l,sold_in_us,co2,make_display")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,make,model_name,model_trim,model_year,body_style,engine_position,engine_cc,engine_num_cyl,engine_type,engine_valves_per_cyl,engine_power_ps,engine_power_rpm,engine_torque_nm,engine_torque_rpm,engine_bore_mm,engine_stroke_mm,engine_compression,engine_fuel,top_speed_kph,zero_to_100_kph,drive_type,transmission_type,seats,doors,weight_kg,length_mm,width_mm,height_mm,wheelbase,lkm_hwy,lkm_mixed,lkm_city,fuel_capacity_l,sold_in_us,co2,make_display")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
