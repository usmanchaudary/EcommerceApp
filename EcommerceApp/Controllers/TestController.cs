using System.Web.Mvc;

namespace EcommerceApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult NavigationController()
        {
            return PartialView("_sidebar");
        }
       
    }
}