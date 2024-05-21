using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Cart
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public float Price {  get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }

        public float TotalPrice { get { return Price * Quantity; } }

        public Cart(int ProductId)
        {
            this.ProductId = ProductId;
            Product product = db.Products.FirstOrDefault(x => x.Id == ProductId);
            this.ProductName = product.Name;
            this.Image = db.ProductImages.FirstOrDefault(x => x.ProductId.Id == ProductId).Path;
            this.Quantity = 1;
            this.Price = product.Price;
        }
    }
}