using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}