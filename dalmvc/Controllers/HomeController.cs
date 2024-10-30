using dallib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
//add reference dallib
namespace dalmvc.Controllers
{
    public class HomeController : Controller
    {
        CDal dal = new CDal();
        public ActionResult Index()
        {
            
            return View(dal.GetAllEmployees());
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {

            return View(new EmpOrm());
        }
        [HttpPost]
        public ActionResult AddEmployee(EmpOrm empOrm)
        {
            dal.AddEmployee(empOrm);
            return RedirectToAction("Index");
        }
        
        public ActionResult DeleteEmployee(int id)
        {
            dal.DeleteEmployee(id);
          
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ModifyEmployee(int id)
        {
            var up = (from emp in dal.GetAllEmployees() where emp.EmpID==id select emp).FirstOrDefault();
            return View(up);
        }
        [HttpPost]
        public ActionResult ModifyEmployee(EmpOrm empOrm)
        {
            dal.ModifyEmployee(empOrm);

            return RedirectToAction("Index");
        }
        
    }
}
