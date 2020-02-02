using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManager.Models;
using ProductManager.ViewModels;

namespace ProductManager.Controllers{

    public class AddProductController : Controller {

        private DatabaseContext db;        

        public AddProductController(){
            db = new DatabaseContext();
        }

        //Default Method
        public ActionResult Create() {
            return IsLoggedIn(); // check wther the user is logged in or not
        }        

        //Form Handler
        [HttpPost]
        public ActionResult Create(Product Product) {
            if (ModelState.IsValid) { 
                    try { 
                        VerifyModel(Product); //call verify method to add remaining fields
                        db.Products.Add(Product); 
                        db.SaveChanges();                         
                        ViewBag.MTitle = "Inserted !"; //Notification Title
                        ViewBag.Message = "Product Added Successfully"; //Notification Message
                        ModelState.Clear();
                    } catch (Exception E) {
                        ViewBag.ETitle = "Error !"; //Error title
                        ViewBag.Error = "Error While Uploading The Data"; //Error message
                        ModelState.Clear();
                    }              
            } else {
                ViewBag.ETitle = "Invalid!";
                ViewBag.Error = "Please Fill in All Required Fields";
            }
            return Create();           
        }     

        //Main Method 
        private ActionResult LoadModelForCreate() {            
            ViewBag.IsUpdate = "false";
            var CategoryList = db.Categories.ToList();
            var ViewModel = new ProductViewModel {
                CategoryList = CategoryList,
                Product = new Product()
            };
            return View(ViewModel);
        }

        public ActionResult Edit(int id) {                       
            Product Product = db.Products.Single(model => model.Id == id);
            var CategoryList = db.Categories.ToList();
            var ViewModel = new ProductViewModel {
                CategoryList = CategoryList,
                Product = Product
            };
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Edit(Product Product) {
            var OldProduct = db.Products.Single(m => m.Id == Product.Id);
            if (ModelState.IsValid) {                                            
                db.Products.Remove(db.Products.Single(m => m.Id == Product.Id));
                VerifyModel(Product);
                db.Products.Add(Product);
                db.SaveChanges();
                ViewBag.MTitle = "Updated !"; ViewBag.Message = "Product Updated Successfully";
                ModelState.Clear();
            } else {
                ViewBag.ETitle = "Error !"; ViewBag.Error = "Failed to Update Product";
            }
            return Edit(Product.Id);
        }

        //Verify Model Before Adding To The Database
        private void VerifyModel(Product Product) {

            //Get Image Files From Model
            HttpPostedFileBase F1 = Product.F_ImageLg;
            HttpPostedFileBase F2 = Product.F_ImageSm;

            //get Server Path
            string path = Server.MapPath("~/Uploads/");

            //If Server Path Does't Exists Create Server Path
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            //Get Current TimeStamp
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            //Set Image URL In The Model
            Product.ImageSm = "F1" +timestamp + Path.GetExtension(F1.FileName);
            Product.ImageLg = "F2" +timestamp + Path.GetExtension(F1.FileName);

            //Set Product ID
            Product.Id = int.Parse(timestamp);

            //Save Images To The Server
            F1.SaveAs(path+Product.ImageSm);
            F2.SaveAs(path+Product.ImageLg);

        }

        //Check Session For User Login        
        private ActionResult IsLoggedIn() {
            //If Login True Return Main
            if ((String)Session["IsLoggedIn"] == "true") { return LoadModelForCreate(); } 
            //If Login False Redirect To Login Page
            else if ((String)Session["IsLoggedIn"] == "false") { return RedirectToAction("Index", "Login"); } 
            //If Login Not Set, Set Login False And Redirect To Login Page            
            else { Session["IsLoggedIn"] = "false"; return RedirectToAction("Index", "Login"); }
        }        

    }

}
