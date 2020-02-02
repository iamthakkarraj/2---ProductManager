using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManager.Controllers{
    public class HomeController : Controller{

        //Display Welcome Screen
        public ActionResult Index(){
            ViewBag.username = Session["Username"]; //get Username from the session
            return IsLoggedIn();                    //Check User Login
        }
    
        //Logout The User
        public ActionResult Logout(){
            Session["IsLoggedIn"] = "false";
            return RedirectToAction("Index", "Login");
        }

        //Redirect To Prodcut Add Controller
        public ActionResult AddProduct() {
            return RedirectToAction("Index", "AddProduct");
        }

        //Redirect To Product List Controller
        public ActionResult ListProduct() {
            return RedirectToAction("Index", "ListProduct");
        }

        //Check Session For User Login        
        private ActionResult IsLoggedIn() {
            //If Login True Return The View
            if ((String)Session["IsLoggedIn"] == "true") { return View(); } 
            //If Login False Redirect To Login Page
            else if ((String)Session["IsLoggedIn"] == "false") { return RedirectToAction("Index", "Login"); } 
            //If Login Not Set, Set Login False And Redirect TO Login Page
            else { Session["IsLoggedIn"] = "false"; return RedirectToAction("Index", "Login"); }
        }

    }
}