using HelloMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace HelloMVC.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        List<Customer> customers;

        public HomeController()
        {
            customers = cache["customer"] as List<Customer>;
            if(customers == null)
            {
                customers = new List<Customer>();
            }
        }

        public void SaveCache()
        {
            cache["customer"] = customers;
        }

        public ActionResult Index() //Defualt View
        {
            Debug.WriteLine("ART");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MyProperty = "MyProperty";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer==null)
            {
                return HttpNotFound();
            }
             return View(customer);
        }


        public ActionResult AddCustomer()
        {
            return View();
        }

        public ActionResult EditCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer,string id)
        {
            Customer customerToEdit = customers.FirstOrDefault(c => c.Id == id);
            if (customerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                customerToEdit.Name = customer.Name;         
                customerToEdit.Telephone = customer.Telephone;
                SaveCache();
                return RedirectToAction("CustomerList");
            }
            
        }


        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer); //กรอกไม่ผ่านให้อยู่หน้าเดิม
            }
            customer.Id = Guid.NewGuid().ToString();
            Debug.WriteLine("customer", customer);
            customers.Add(customer);
            SaveCache();
            return RedirectToAction("CustomerList");
        }

        public ActionResult CustomerList()
        {
            return View(customers);
        }
    }
} 