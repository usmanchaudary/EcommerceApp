using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceApp.Models;
namespace EcommerceApp.ViewModel
{
    public class MultiTableView
    {
        public IEnumerable<Category>Name  { get; set; }
        public IEnumerable<Unit> UName  { get; set; }
    }
}