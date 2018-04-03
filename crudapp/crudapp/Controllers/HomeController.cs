using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crudapp.Models;

namespace crudapp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Getemployee()
        {
            using (Database1Entities1 dc = new Database1Entities1())
           {
             var data1 = dc.employees.OrderBy(A => A.First_name).ToList();
             return Json( new { data = data1 }, JsonRequestBehavior.AllowGet);
          }
        }

        [HttpGet]
        public ActionResult Save( int id)
        {

            using (Database1Entities1 dc = new Database1Entities1())
               {
                    var data1 = dc.employees.Where(a=> a.Id == id).FirstOrDefault();
                    return View (data1);
                 
               }
        }

        [HttpPost]
        public ActionResult Save (employee emp)
        { 
            bool status = false;
            if(ModelState.IsValid)
            {
                using (Database1Entities1 dc = new Database1Entities1())
               {
                   if (emp.Id > 0)
                   {

                       var data1 = dc.employees.Where(a => a.Id == emp.Id).FirstOrDefault();
                       if (data1 != null)
                       {
                           data1.Id = emp.Id;
                           data1.First_name = emp.First_name;
                           data1.Lastname = emp.Lastname;
                           data1.city = emp.city;
                           data1.contact = emp.contact;
                           data1.salary = emp.salary;
                       }
                   }
                   else
                   {
                       dc.employees.Add(emp);

                   }
                  dc.SaveChanges();
                  status=true;
               }
            }
            return new JsonResult { Data = new { status = status } };
           // return Json(new { status = status }, JsonRequestBehavior.AllowGet);
            //Response.Redirect("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (Database1Entities1 dc = new Database1Entities1())
            {
                var v = dc.employees.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
   

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (Database1Entities1 dc = new Database1Entities1())
            {
                var v = dc.employees.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.employees.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status} };
        }
    }
}
    

