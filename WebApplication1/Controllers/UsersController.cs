using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.Security;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data.Entity.Validation;
using System.Text;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private static string Hash(string password)
        {
            Random random = new Random();
            byte[] salt = BitConverter.GetBytes(16);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        // GET: Users
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignIn(string Username, string Password)
        {
            Password = Hash(Password);
            bool existUser = db.Users.Any(x => x.Username == Username && x.Password == Password);
            User currentUser = db.Users.FirstOrDefault(x => x.Username == Username && x.Password == Password);
            if (existUser)
            {
                FormsAuthentication.SetAuthCookie(currentUser.Username, false);
                return Redirect("/Home");
            }
            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [AllowAnonymous]
        // GET: Users/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(string Username, string Password,
            string ConfirmPassword, HttpPostedFileBase file)
        {
            if (ConfirmPassword != Password)
                return Content("Đăng nhập thất bại");

            bool existUser = db.Users.Any(x => x.Username == Username);

            if (!existUser)
            {
                User user = new User
                {
                    Username = Username,
                    Created_date = DateTime.Now,
                    Updated_date = DateTime.Now
                };

                var cloudinary = (Cloudinary)HttpContext.Application["cloudinary"];

                if (file != null && file.ContentLength > 0)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.InputStream)
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);

                    // Get the image URL from Cloudinary upload result
                    string imageUrl = uploadResult.Url.ToString();

                    // Create a ProductImage object
                    user.Avatar = imageUrl;
                }
                else
                    return Content("No image");

                user.Password = Hash(Password);
                db.Users.Add(user);
                db.SaveChanges();

                if (db.Users.Any(x => x.Username == Username))
                {
                    Customer customer = new Customer
                    {
                        User = db.Users.FirstOrDefault(x => x.Username == Username)
                    };

                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
                return RedirectToAction("SignIn");
            }
            return Redirect("SignUp");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Avatar,Created_date,Updated_date,Is_active")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}
