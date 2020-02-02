namespace ProductManager.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Product {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(15)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required")]
        [StringLength(15)]
        public string Category { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required")]
        public double Price { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required")]
        public int Quantity { get; set; }

        [Display(Name = "Short Description")]
        [StringLength(25)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required")]
        public string ShortDsc { get; set; }

        [Display(Name = "Long Description")]
        [StringLength(100)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*Required")]
        public string LongDsc { get; set; }

        [Display(Name = "Image")]
        [StringLength(500)]
        public string ImageSm { get; set; }

        [StringLength(500)]
        public string ImageLg { get; set; }

        [NotMapped]
        [Display(Name="Small Image")]
        [Required(ErrorMessage = "*Required")]        
        public HttpPostedFileBase F_ImageSm { get; set; }

        [NotMapped]
        [Display(Name = "Large Image")]
        [Required(ErrorMessage = "*Required")]        
        public HttpPostedFileBase F_ImageLg { get; set; }

    }
}