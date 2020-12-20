using System;
using System.Linq;
using System.Web.Mvc;
using EcommerceApp.Models;
using Microsoft.AspNet.Identity;

namespace EcommerceApp.Controllers
{
    public class ClientController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: Client
        public ActionResult Index()
        {
            var data = dc.Products.ToList();
            return View(data);
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddtoCart(Product z)
        {
            try
            {

                var username = User.Identity.GetUserName();
                string str = Request["quantity"];
                int quantity = 1;
                Cart obj = new Cart();

                var data = dc.Carts.Where(x => x.Id == z.Id).FirstOrDefault();

                obj.ProductId = z.Id;
                obj.UserName = username;

                obj.Quantity = quantity;
                obj.orderDate = DateTime.Today;
                dc.Carts.InsertOnSubmit(obj);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                
            }
          

            return RedirectToAction("Index");
           
        }

        public ActionResult Delete()
        {
            var username = User.Identity.GetUserName();
            var c = dc.Carts.Where(x => x.UserName==username).ToList();
            return View(c);
        }
        public ActionResult DeleteCart(int id)
        {
            var s = dc.Carts.First(x => x.Id == id);
            dc.Carts.DeleteOnSubmit(s);
            dc.SubmitChanges();
            return RedirectToAction("Delete");
        }

        public ActionResult CheckOut()
        {
            var username = User.Identity.GetUserName();
            var c = dc.Carts.Where(x => x.UserName == username).ToList();
           
            return View(c);
        }

        public ActionResult Sales()
        {
            var str = from a in dc.Carts where a.UserName==User.Identity.GetUserName() select a;
            foreach (var val in str) // iterator the data from the list and insert them into the listSecond
            {
                Sale ls = new Sale {Sales_person_name = val.UserName, Amount = Convert.ToInt32(val.Product.Price),Status = "Pending",Unit = val.Product.Unit,Category = val.Product.Category,gst = 16.3 ,Discount = 5,Date = DateTime.Today};
                dc.Sales.InsertOnSubmit(ls);
                dc.Carts.DeleteOnSubmit(val);  
                dc.SubmitChanges();
            }
            return View("AddtoCart");
        }
        public ActionResult MyOrders()
        {
            var Orders = dc.Sales.Where(x => x.Sales_person_name == User.Identity.GetUserName()).ToList();
            return View(Orders);
        }


    }
}