using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using CityHomeNpl.Filters;
using CityHomeNpl.Models;

namespace CityHomeNpl.Controllers
{
   
    public class AccountController : Controller
    {
        DB_9DFC85_cityhomeEntities context = new DB_9DFC85_cityhomeEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbluser user)
        {

            if (ModelState.IsValid)
            {
                var a = context.Tblusers.Where(i => i.Username.Equals(user.Username) && i.Password.Equals(user.Password)).FirstOrDefault();

                if (a != null)
                {

                    Session["loginId"] = a.Id.ToString();
                    return RedirectToAction("Addproduct", "Account");
                }
                else
                {
                    ViewBag.message = "Username and Password is Incorrect";
                    ModelState.AddModelError("", "Username and Password is Incorrect");
                }
            }
            return View();
        }
        public ActionResult AdminPanel()
        {
            if (Session["loginId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Addproduct()
        {

            if (Session["loginId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Addproduct(Tblproduct getproduct, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                                   + file.FileName);

                    getproduct.Image = file.FileName;
                }

                context.Tblproducts.Add(getproduct);
                context.SaveChanges();
                return RedirectToAction("details");
            }
            return View();
        }
        public ActionResult delete(int? id)
        {
            if (Session["loginId"] != null)
            {

                var getproduct = context.Tblproducts.Find(id);
                return View(getproduct);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult delete(int id, FormCollection collection)
        {

            Tblproduct tb = context.Tblproducts.Where(u => u.Productid == id).FirstOrDefault();
            string path = "~/Images/" + tb.Image;
            var imagepath = tb.Image;
            if (imagepath != null)
            {
                System.IO.File.Delete(Server.MapPath(path));
            }
            context.Tblproducts.Remove(tb);
            context.SaveChanges();
            return RedirectToAction("details");
        }

        public ActionResult edit(int? id)
        {
            if (Session["loginId"] != null)
            {


                var getproduct = context.Tblproducts.Find(id);
                return View(getproduct);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult edit(Tblproduct getproduct, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                Tblproduct tb = context.Tblproducts.Where(u => u.Productid == getproduct.Productid).FirstOrDefault();
                tb.Modelname = getproduct.Modelname;
                tb.Price = getproduct.Price;
                tb.Categoryid = getproduct.Categoryid;
                tb.Description = getproduct.Description;
                tb.FrontDisplay = getproduct.FrontDisplay;
                if (file != null)
                {

                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                                   + file.FileName);

                    tb.Image = file.FileName;
                }


                context.SaveChanges();
                return RedirectToAction("details", "Account");
            }
            return View();
        }

        public ActionResult details()
        {
            if (Session["loginId"] != null)
            {

                var a = context.Tblproducts.ToList();
                return View(a);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



      

    }
}
