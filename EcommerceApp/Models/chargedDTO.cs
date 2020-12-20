using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceApp.Models
{
    public class chargeDTO
    {
        public string  cardName { get; set; }
        public string  Email { get; set; }
        public string Phone { get; set; }
        public string  stripeToken { get; set; }
    }
}