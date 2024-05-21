using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Product : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string Name { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; } = 0;

        [DefaultValue(0)]
        public float Price { get; set; } = 0;

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public virtual Firm FirmId { get; set; }

        [Required]
        public TypeOfPhone TypeOfPhone { get; set; }

    }
}