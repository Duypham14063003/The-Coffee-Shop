using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{

    public class AdminController : Controller
    {
        DoAnEntities database = new DoAnEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["TenAD"] == null) return RedirectToAction("Error", "Users");
            var products = database.Products;

            return View(products.ToList());

        }

        // xu ly phan user
        public ActionResult qlUser()
        {
            var users = database.Customers;
            return View(users);
        }
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            Customer cus = database.Customers.Where(p => p.IDCus == id).FirstOrDefault();
            if (cus != null)
            {
                database.Customers.Remove(cus);
                database.SaveChanges();

            }

            return Json(new { redirectTo = Url.Action("qlUser") });

        }

        // end useer



        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TenAD"] == null) return RedirectToAction("Error", "Users");
            ViewBag.Category = new SelectList(database.Categories, "IDCate", "NameCate");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,NamePro,DecriptionPro,Category,Price,ImagePro")] Product product, HttpPostedFileBase ImagePro)
        {
            if (ModelState.IsValid)
            {
                if (ImagePro != null)
                {
                    var fileName = Path.GetFileName(ImagePro.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    product.ImagePro = fileName;
                    ImagePro.SaveAs(path);
                }
                database.Products.Add(product);
                database.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(database.Categories, "IDCate", "NameCate", product.Category);
            return View(product);
        }


        // GET: Products/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (Session["TenAD"] == null) return RedirectToAction("Error", "Users");
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = database.Products.Find(id);
            database.Products.Remove(product);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteProducts(int id)
        {
            Product pro = database.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if (pro != null)
            {
                database.Products.Remove(pro);
                database.SaveChanges();
            }

            return Json(new { redirectTo = Url.Action("Index") });
        }
        public ActionResult qlOrder()
        {
            List<OrderDetail>OrderList= database.OrderDetails.ToList();
            List<OrderDetailModel> orderDetailModels = OrderList.Select(
                x => new OrderDetailModel
                {
                    IdOrd =x.IDOrder,
                    IDPro=x.IDProduct,
                    NamePro=x.Product.NamePro,
                    NameCusOrd=x.OrderPro.Customer.NameCus,
                    AddressOrd=x.OrderPro.AddressDeliverry,
                    QuanityOrd=x.Quantity,
                    PriceOrd=x.UnitPrice,
                    DateOrd=x.OrderPro.DateOrder
                }

                ).ToList();
            return View(orderDetailModels);
        }



    }
}