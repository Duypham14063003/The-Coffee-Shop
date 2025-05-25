using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        DoAnEntities db = new DoAnEntities();

        // GET: Cart
        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;
            if (myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }
            return myCart;
        }


        public ActionResult AddToCart(int id)
        {
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (currentProduct == null)
            {
               
                currentProduct = new CartItem(id);
                myCart.Add(currentProduct);
            }
            else
            {
                currentProduct.Number++;
            }
            return RedirectToAction("GetCartInfo", "Cart");
        }

        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
            {
                totalNumber = myCart.Sum(sp => sp.Number);
            }
            return totalNumber;
        }

        private double GetTotalPrice()
        {
            double totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            return totalPrice;
        }
       
        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();
          
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart);
        }

        public ActionResult CartPartial()
        {
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return PartialView();
        }


        [HttpPost]
        public ActionResult DeleteCart(int id)
        {
            List<CartItem> myCar = GetCart();
            var pro = myCar.Where(p => p.ProductID == id).FirstOrDefault();
            if(pro != null)
            {
                myCar.Remove(pro);
                
            }
            return Json(new { redirectTo = Url.Action("GetCartInfo") });
        }
        public ActionResult BuySuccess() 
        {
            return View();
        }
        public  ActionResult SaveOrder()
        {
            var Lcart = (List<CartItem>)Session["GioHang"];
            OrderPro ord = new OrderPro();
            ord.DateOrder=DateTime.Now;
            ord.IDCus = int.Parse(Session["Taikhoan"].ToString());
            ord.AddressDeliverry = Session["DiaChi"].ToString();
            db.OrderProes.Add(ord);
            db.SaveChanges();
            foreach (var item in Lcart)
            {
                OrderDetail detail = new OrderDetail();
                detail.IDProduct=item.ProductID;
                detail.IDOrder = ord.ID;
                detail.Quantity = item.Number;
                detail.UnitPrice = GetTotalPrice();
                db.OrderDetails.Add(detail);

            }
            db.SaveChanges();
            return RedirectToAction("BuySuccess", "Cart");
        }

    }
}