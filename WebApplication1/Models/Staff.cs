using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [DefaultValue(0)]
        public float Salary { get; set; } = 0;

        [Required]
        public DateTime Start_working_date { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}