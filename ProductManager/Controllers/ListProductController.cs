using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using ProductManager.Models;

namespace ProductManager.Controllers{

    public class ListProductController : Controller{

        private DatabaseContext db;

        public ListProductController() {
            db = new DatabaseContext();
        }

        //Default Method
        public ActionResult Index(int? no) {
            return IsLoggedIn(no);
        }

        private ActionResult Main(int? no) {
            return View(db.Products.ToList().ToPagedList(no ?? 1, 3));
        }

        private ActionResult Edit(int id) {
            return RedirectToAction("Edit", "AddProduct");
        }

        public ActionResult Delete(int id) {
            ViewBag.Message = "Product with id : "+id+" Deleted.";
            db.Products.Remove(db.Products.Single(m => m.Id == id));
            db.SaveChanges();
            return View("Index", db.Products.ToList().ToPagedList(1, 3));
        }

        [HttpGet]
        public ActionResult Search(string option, string search, int? no) {
            if (option == "Category") {
                //Index action method will return a view with a products records based on what a user specify the value in textbox  
                return View("Index",db.Products.Where(model => model.Category == search || search == null).ToList().ToPagedList(no ?? 1, 3));
            } else if (option == "Name") {
                return View("Index",db.Products.Where(model => model.Name.Contains(search) || search == null).ToList().ToPagedList(no ?? 1, 3));
            } else {
                return Main(no);
            }
        }
        
        //Check Session For User Login        
        private ActionResult IsLoggedIn(int? no) {        
            //If Login True Return The View
            if ((String)Session["IsLoggedIn"] == "true") { return Main(no); } 
            //If Login False Redirect To Login Page
            else if ((String)Session["IsLoggedIn"] == "false") { return RedirectToAction("Index", "Login"); }
            //If Login Not Set, Set Login=false And Redirect to Login Page            
            else { Session["IsLoggedIn"] = "false"; return RedirectToAction("Index", "Login"); }
        }

    }

}