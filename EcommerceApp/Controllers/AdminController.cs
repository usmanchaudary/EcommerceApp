using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceApp.Models;


namespace EcommerceApp.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {


        DataClasses1DataContext dc = new DataClasses1DataContext();
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Adminlte()
        {
            
            var data = dc.AspNetUsers.Where(x=>x.UserName!="robin@gmail.com").ToList();
            return View(data);
        }
        public ActionResult Add()
        {
            string name = Request["name"];
            string pt = Request["Password"];
            AspNetUser p = new AspNetUser();
            p.UserName = name;
            p.PasswordHash = pt;
            dc.AspNetUsers.InsertOnSubmit(p);
            dc.SubmitChanges();
            return RedirectToAction("Adminlte");

        }
        public ActionResult delete(string id)
        {
            var s = dc.AspNetUsers.First(x => x.Id == id);
            dc.AspNetUsers.DeleteOnSubmit(s);
            dc.SubmitChanges();
            return RedirectToAction("Adminlte");

        }
        public ActionResult Edit(string id)
        {
            return View(dc.AspNetUsers.First(x => x.Id == id));

        }
        public ActionResult EditOk(AspNetUser P)
        {
            var a = dc.AspNetUsers.First((s => s.Id == P.Id));
            a.UserName = Request["name"];
            a.PasswordHash = Request["pt"];
            //dc.Projects.InsertOnSubmit(a);
            dc.SubmitChanges();
            return RedirectToAction("Adminlte");
        }

        public ActionResult Units()
        {
            var data = dc.Units.ToList();
            return View(data);
        }
        public ActionResult UnitsOk()
        {
            string name = Request["name"];
            Unit u = new Unit();
            u.Name = name;
            dc.Units.InsertOnSubmit(u);
            dc.SubmitChanges();
            return RedirectToAction("Units");
        }

        public ActionResult EditUnit(int id)
        {
            var data = dc.Units.First(x => x.Id == id);
            return View(data);
        }

        public ActionResult UnitOk(Unit U)
        {
            var Data = dc.Units.First(x => x.Id == U.Id);
            Data.Name = Request["name"];
            dc.SubmitChanges();
            return RedirectToAction("Units");
        }

        public ActionResult deleteUnit(int id)
        {
            var s = dc.Units.First(x => x.Id == id);
            dc.Units.DeleteOnSubmit(s);
            dc.SubmitChanges();
            return RedirectToAction("Units");
        }

        public ActionResult Category()
        {

            return View(dc.Categories.ToList());
        }

        public ActionResult AddCategory()
        {
            string name = Request["name"];
            Category c = new Category();
            c.Name = name;
            dc.Categories.InsertOnSubmit(c);
            dc.SubmitChanges();
            return RedirectToAction("Category");
        }

        public ActionResult EditCategory(int id)
        {
            return View(dc.Categories.First(x => x.Id == id));
        }

        public ActionResult EditCategoryOk(Category C)
        {
            var a = dc.Categories.First(x => x.Id == C.Id);
            a.Name = Request["name"];
            dc.SubmitChanges();
            return RedirectToAction("Category");

        }

        public ActionResult deleteCategory(int id)
        {
            var c = dc.Categories.First(x => x.Id == id);
            dc.Categories.DeleteOnSubmit(c);
            dc.SubmitChanges();
            return RedirectToAction("Category");
        }

        public ActionResult Product()
        {

            var data = dc.Units.ToList();
            var data1 = dc.Categories.ToList();
            SelectList list = new SelectList(data, "Name", "Name");
            SelectList list1 = new SelectList(data1, "Name", "Name");
            ViewBag.l = list;
            ViewBag.l1 = list1;

            return View();
        }
        [HttpPost]
        public ActionResult UploadStudentPhoto(HttpPostedFileBase PostedFile)
        {
            string name = Request["name"];
            string category = Request["Categories"];
            string unit = Request["Units"];
            string price = Request["price"];
            float priceInFloat = Convert.ToInt32(price);
            string filePath = "";
            filePath = Server.MapPath("~/File/");
            if (PostedFile == null)
            {
                PostedFile = Request.Files["userFile"];
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = filePath + Path.GetFileName(PostedFile.FileName);
            PostedFile.SaveAs(filePath);
            Product p = new Product();
            p.Name = name;
            p.Category = category;
            p.Unit = unit;
            p.ImagePath = filePath;
            p.Price = priceInFloat;
            dc.Products.InsertOnSubmit(p);
            dc.SubmitChanges();
            ViewBag.Message = "Product saved \n You can see this in Products page ";
            return View();
        }

        public ActionResult AllSales()
        {
            List<string> strList = new List<string>();
            strList.Add("Pending");
            strList.Add("Delievred");
            SelectList list = new SelectList(strList);
            ViewBag.l = list;
            var sales = dc.Sales.ToList();
            return View(sales);
        }
        public ActionResult change(int id)
        {
            var a = dc.Sales.First(x => x.Id ==id);
            a.Status = Request["Exemplo"];
            dc.SubmitChanges();
            return RedirectToAction("AllSales");

        }
        public ActionResult ProductWise()
        {
            var data = dc.Products.ToList();
            return View(data);
        }
        public ActionResult deleteProduct(int id)
        {
            var c = dc.Products.First(x => x.Id == id);
            dc.Products.DeleteOnSubmit(c);
            dc.SubmitChanges();
            return RedirectToAction("ProductWise");
        }
        public ActionResult SearchByDate()
        {
            string str = Request["dates"];
            DateTime from = Convert.ToDateTime(str);
            string str1 = Request["to"];

            DateTime to = Convert.ToDateTime(str1);

            var sales = dc.Sales.Where(x => x.Date >= from).Where(x => x.Date <= to).ToList();
            return View(sales);
        }



    }
}