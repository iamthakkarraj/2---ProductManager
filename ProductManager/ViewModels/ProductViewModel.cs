using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using ProductManager.Models;

namespace ProductManager.ViewModels {
    public class ProductViewModel {

        public IEnumerable<Category> CategoryList { get; set; }        
        
        public Product Product { get; set; }

    }
}