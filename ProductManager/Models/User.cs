namespace ProductManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        [StringLength(10)]
        public string Username { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }
    }
}
