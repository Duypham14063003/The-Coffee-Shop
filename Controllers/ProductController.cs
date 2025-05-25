using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        Random random = new Random();
        DoAnEntities database = new DoAnEntities();

        // GET: Product
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult PageCF()
        {
            var productCF = database.Products.Where(p => p.Category == "01");
            return View(productCF);
        }

        public ActionResult PageTra()
        {
            var productTra = database.Products.Where(p => p.Category == "02");
            return View(productTra);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = database.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Qcsanpham()
        {
            var sanpham = database.Products.ToList();
            var sanphamRandom = sanpham.OrderBy(x => random.Next()).Take(7).ToList();
            return PartialView(sanphamRandom);
        }

        public ActionResult Menu(string Searching)
        {
            if (Searching != null)
            {
                Session["search"] = Searching;
            }
            return View(database.Products.Where(x => x.NamePro.Contains(Searching) || Searching == null).ToList());

        }


    }
}