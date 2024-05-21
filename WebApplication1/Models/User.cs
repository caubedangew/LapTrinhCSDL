using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(15)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Required]
        public string Avatar {  get; set; }
    }
}