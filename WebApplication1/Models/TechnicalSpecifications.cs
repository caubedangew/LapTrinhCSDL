using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TechnicalSpecifications : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]
        public string Chip { get; set; }

        [StringLength(10)]
        public string Rom { get; set; }

        [StringLength(10)]
        public string Ram { get; set; }

        [StringLength(10)]
        public string Battery { get; set;}

        [StringLength(10)]
        public string ScreenPropotion { get; set;}

        [Required]
        public virtual Product ProductId { get; set; }
    }
}