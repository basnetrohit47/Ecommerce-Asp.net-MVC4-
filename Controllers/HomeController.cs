using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityHomeNpl.Models;

namespace CityHomeNpl.Controllers
{
    public class HomeController : Controller
    {
        DB_9DFC85_cityhomeEntities context = new DB_9DFC85_cityhomeEntities();
        
      



        public ActionResult Index()
        {


            Multi objmulti = new Multi();
            //List<Dinner> GetItem = new List<Dinner>();
            //Lunch

            var tblfeature = context.Tblproducts.Where(a => a.FrontDisplay == "yes").ToList();
            var lstfeature = new List<feature>();
            foreach (var item in tblfeature)
            {
                lstfeature.Add(new feature() { Id = item.Productid, Name = item.Modelname, price = item.Price, image = item.Image, description = item.Description, });
            }
            var tblLatest = context.Tblproducts.Take(5);
            var lstLatest = new List<Latest>();
            foreach (var item in tblLatest)
            {
                lstLatest.Add(new Latest() { Id = item.Productid, Name = item.Modelname, price = item.Price, image = item.Image, description = item.Description });
            }
            objmulti.Latest = lstLatest;
            objmulti.Feature = lstfeature;


            return View(objmulti);
        }

       


        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult More(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var detail = context.Tblproducts.Where(c => c.Productid == id).ToList();
                return View(detail);
            }
        }
        public ActionResult Category(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                Multi obj = new Multi();

                var tblcat = context.Tblcats.ToList();

                obj.Category = tblcat;
                var tbldetails = context.Tblproducts.Where(c => c.Categoryid == id).ToList();
                obj.Details = tbldetails;
                var tblsinglecat = context.Tblcats.Where(c => c.CategoryId == id).ToList();
                obj.singlecat = tblsinglecat;
                return View(obj);
            }
        }
    }
}
