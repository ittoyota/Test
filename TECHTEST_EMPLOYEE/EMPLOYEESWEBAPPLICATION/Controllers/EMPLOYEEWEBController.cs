using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMPLOYEESWEBAPPLICATION.Models;
using System.Net.Http;

namespace EMPLOYEESWEBAPPLICATION.Controllers
{
    
    public class EMPLOYEEWEBController : Controller
    {
        
        public ActionResult Index()
        {
            IEnumerable<EMPLOYEEDETAILS> emplist;
            EMPLOYEEDETAILS ee;
            HttpResponseMessage response;
            if (Session["EMPROLE"].ToString() == "Admin")
            {
                response = APIGLOABALFORMAT.WebApiClient.GetAsync("EMPDETAILS").Result;
                emplist = response.Content.ReadAsAsync<IEnumerable<EMPLOYEEDETAILS>>().Result;
                return View(emplist);
            }
            else
            {
                response = APIGLOABALFORMAT.WebApiClient.GetAsync("EMPDETAILS/" + Session["USERID"].ToString()).Result;
                ee = response.Content.ReadAsAsync<EMPLOYEEDETAILS>().Result;
                return View("IndexForUser", ee);
            }
        }
        public ActionResult IndexForUser()
        {
            return View();
        }

        public ActionResult ADDOREDIT(int id = 0)
        {
            if (id == 0)
            {
                return View(new EMPLOYEEDETAILS());
            }
            else
            {
                HttpResponseMessage response = APIGLOABALFORMAT.WebApiClient.GetAsync("EMPDETAILS/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EMPLOYEEDETAILS>().Result);
            }
        }
        [HttpPost]
        public ActionResult ADDOREDIT(EMPLOYEEDETAILS emp)
        {
            if (emp.EMPLOYEEID == 0)
            {
                HttpResponseMessage response = APIGLOABALFORMAT.WebApiClient.PostAsJsonAsync("EMPDETAILS", emp).Result;
                TempData["SavedAlert"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = APIGLOABALFORMAT.WebApiClient.PutAsJsonAsync("EMPDETAILS/" + emp.EMPLOYEEID, emp).Result;
                TempData["UpdateAlert"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = APIGLOABALFORMAT.WebApiClient.DeleteAsync("EMPDETAILS/" + id.ToString()).Result;
            TempData["DeleteAlert"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
        // GET: EMPLOYEEWEB
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EMPLOYEEDETAILS emp)
        {
            if (emp.USERNAME == "" && emp.USERPASSWORD == "")
            {
                TempData["Invalid"] = "Invalid Username or Password";
                return View();
            }
            else
            {
                HttpResponseMessage response = APIGLOABALFORMAT.WebApiClient.PostAsJsonAsync("LoginUser", emp).Result;
                EMPLOYEEDETAILS ee = response.Content.ReadAsAsync<EMPLOYEEDETAILS>().Result;

                if (ee == null)
                {
                    TempData["LoginFailedAlert"] = "Username or Password is invalid";
                    return View();
                }
                else
                {
                    Session["USERID"] = ee.EMPLOYEEID;
                    Session["USERNAME"] = ee.NAME;
                    Session["EMPROLE"] = ee.EMPROLE;
                    return RedirectToAction("Index", "EMPLOYEEWEB");
                }
            }
        }
        public ActionResult LOGOUTUSER()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "EMPLOYEEWEB");
        }
    }
}