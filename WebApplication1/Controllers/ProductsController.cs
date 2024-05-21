using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public List<Cart> GetCart()
        {
            if (!(Session["cart"] is List<Cart> carts))
            {
                carts = new List<Cart>();
                Session["cart"] = carts;
            }
            return carts;
        }

        [AllowAnonymous]
        public ActionResult AddCart(int ProductId, string strURL)
        {
            List<Cart> carts = GetCart();
            Cart product = carts.Find(x => x.ProductId == ProductId);
            if (product == null)
            {
                product = new Cart(ProductId);
                carts.Add(product);
            }
            else
                product.Quantity++;
            Session["totalAmount"] = TotalAmount();
            Session["totalQuantity"] = TotalQuantity();

            return Redirect(strURL);
        }

        private int TotalQuantity()
        {
            return Session["cart"] is List<Cart> carts ? carts.Sum(x => x.Quantity) : 0;
        }

        private float TotalAmount()
        {
            return Session["cart"] is List<Cart> carts ? carts.Sum(x => x.TotalPrice) : 0;
        }

        [AllowAnonymous]
        public ActionResult Cart()
        {
            List<Cart> carts = GetCart();
            ViewBag.TotalAmount =Session["totalAmount"].ToString();
            Session["totalQuantity"] = TotalQuantity();
            return View(carts);
        }

        [AllowAnonymous]
        public ActionResult DeleteCart(int ProductId)
        {
            List<Cart> carts = GetCart();
            Cart product = carts.SingleOrDefault(x => x.ProductId == ProductId);
            if (product != null)
                carts.RemoveAll(x => x.ProductId == ProductId);
            return RedirectToAction("Cart");
        }

        [AllowAnonymous]
        public ActionResult DeleteAllCart()
        {
            List<Cart> carts = GetCart();
            carts.Clear();
            return Redirect("/Home");
        }

        [AllowAnonymous]
        public ActionResult EditCart(int ProductId, int Quantity)
        {
            List<Cart> carts = GetCart();
            Cart product = carts.SingleOrDefault(x => x.ProductId == ProductId);
            if (product != null)
                product.Quantity = Quantity;
            Session["totalAmount"] = TotalAmount();
            Session["totalQuantity"] = TotalQuantity();
            return RedirectToAction("Cart");
        }

        [AllowAnonymous]
        public ActionResult PartialCart()
        {
            return PartialView("_CartPartial");
        }

        [AllowAnonymous]
        // GET: Products
        public ActionResult Index(string firmName = "", int typeOfPhone = 0)
        {
            ViewBag.Firms = db.Firms.ToList();
            ViewBag.Images = (from p in db.ProductImages
                              orderby p.ProductId.Id ascending
                              group p by p.ProductId into productGroup
                              select productGroup.FirstOrDefault()).ToList();
            DbSet<Product> products = db.Products;
            if (firmName != "")
                products.Where(x => x.FirmId.Name == firmName);
            if (typeOfPhone > 0)
                products.Where(x => x.TypeOfPhone == (TypeOfPhone)typeOfPhone);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Firms = db.Firms.ToList();
            Product model = new Product();
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string Name, int Quantity, float Price,
            string Description, int TypeOfPhone, int FirmId, List<HttpPostedFileBase> uploadFile)
        {
            Product product = new Product
            {
                Name = Name,
                Quantity = Quantity,
                Price = Price,
                Description = Description,
                TypeOfPhone = (TypeOfPhone)TypeOfPhone,
                Created_date = DateTime.Now,
                Updated_date = DateTime.Now
            };
            Firm firm = db.Firms.Find(FirmId);
            product.FirmId = firm;
            db.Products.Add(product);
            db.SaveChanges();

            var cloudinary = (Cloudinary)HttpContext.Application["cloudinary"];

            foreach (HttpPostedFileBase file in uploadFile)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.InputStream)
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);

                    string imageUrl = uploadResult.Url.ToString();

                    var productImage = new ProductImage
                    {
                        Path = imageUrl,
                        ProductId = product,
                        Created_date = DateTime.Now,
                        Updated_date = DateTime.Now
                    };

                    db.ProductImages.Add(productImage);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Products");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Quantity,Price,Description,TypeOfPhone,Created_date,Updated_date,Is_active")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }


    }
}
