using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public abstract class BaseModel
    {
        public DateTime Created_date { get; set; }

        public DateTime Updated_date { get; set; }

        public bool Is_active { get; set; } = true;
    }
}