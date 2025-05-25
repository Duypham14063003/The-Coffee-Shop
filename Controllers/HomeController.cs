using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        Random random = new Random();
        DoAnEntities database = new DoAnEntities();
        public ActionResult Index()
        {
            ViewBag.SuccessMessage = "Thành công!";
            return View();
        }

        public ActionResult QCsanpham()
        {
            var newestProducts = database.Products.Take(6); 
            return PartialView(newestProducts);
        }

        public ActionResult footer()
        {
            return PartialView();
        }
      
    }
}