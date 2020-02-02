using System;
using System.Linq;
using System.Web.Mvc;
using ProductManager.Models;

namespace ProductManager.Controllers {

    public class LoginController : Controller {         
        
        //Default Method
        public ActionResult Index() {
            return IsLoggedIn();
        }

        //Form Handler
        [HttpPost]
        public ActionResult Index(User user) {
            DatabaseContext db = new DatabaseContext(); //Create DB Reference
            if (ModelState.IsValid) { //If Submitted Model is Correct 
                var Result = db.Users.Where(r => r.Username == user.Username); //Search for Username
                if (Result.Any()) { //If Username Exists 
                    if (Result.Where(r => r.Password == user.Password).Any()) { //If Password Correct
                        Session["IsLoggedIn"] = "true"; //Set Session Login True
                        Session["Username"] = user.Username; //Set Session Username 
                        return IsLoggedIn(); //Verify Login
                    } else { ViewBag.message = "Wrong Password"; } // If Password Wrong 
                } else { ViewBag.message = "Wrong Username"; } // If Username Wrong
            } return View(); //Retrun View
        }

        //Check Session For User Login
        private ActionResult IsLoggedIn() {            
            //If Login True Redirect To Home
            if ((String)Session["IsLoggedIn"] == "true") { return RedirectToAction("Index", "Home"); }
            //If Login False Return View
            else if((String)Session["IsLoggedIn"] == "false") { return View(); } 
            //If Login Not Set, Set Login=false And Return View
            else { Session["IsLoggedIn"] = "false"; return View(); }             
        }

    }

}