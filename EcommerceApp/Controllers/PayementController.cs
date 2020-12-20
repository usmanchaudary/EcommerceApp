using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EcommerceApp.Models;
using Microsoft.AspNet.Identity;
using Stripe;

namespace EcommerceApp.Controllers
{
    [Authorize]
    public class PayementController : Controller
    {
        // GET: Payement
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public ActionResult Create()
        {
            return View();
        }

        // POST: cart/Create
        [HttpPost]
        public ActionResult CreateOk(chargeDTO chargedto)
        {
            StripeConfiguration.ApiKey = "sk_test_TIHCzOZXvaXOKyDP5BobGFEa0070RW0e9C";

            //create customer
            var CustomerOptions = new CustomerCreateOptions
            {
                Description = chargedto.cardName,
                Source = chargedto.stripeToken,
                Email = chargedto.Email,
                Metadata = new Dictionary<string, string>()
                {
                    {"Phone Number",chargedto.Phone }
                }
            };
            var model = new chargeViewModel();
            var customerService = new CustomerService();
            Customer customer = customerService.Create(CustomerOptions);
            var a = dc.Carts.AsEnumerable().Where(x => x.UserName == User.Identity.GetUserName()).Sum(x=>x.Product.Price);
            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(a),    //here to implement OverAll Value
                Currency = "usd",
                Description = "Charge for jenny.rosen@example.com",
                /*Source = chargedto.stripeToken*/  // obtained with Stripe.js,
                CustomerId = customer.Id
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

           
            model.chargeId = charge.Id;
            return RedirectToAction("Sales","Client");

        }
    }

    
}