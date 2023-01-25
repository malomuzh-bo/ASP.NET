using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using VideoConference_Asp_MVC.Models;

namespace VideoConference_Asp_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public List<Roles> roles;
        [BindProperty]
        public Users User { get; set; }
        [BindProperty]
        public string key { get; set; }
        public ConferenceDBContext db;
        [BindProperty(Name = "email", SupportsGet = true)]
        public string email { get; set; }
        [BindProperty(Name = "password", SupportsGet = true)]
        public string password { get; set; }
        public HomeController(ConferenceDBContext db)
        {
            this.db = db;
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            User = db.Users.Where(o => o.Email.Equals(email) && o.Password.Equals(password)).FirstOrDefault();
            if (User != null)
            {
                return RedirectToAction("MainPage", "Main", new {id = User.Id });
            }
            else
            {
                return Redirect("/Home/UnvalidError");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(string email, string lastname, string phonenumber, string password,string confpassword)
        {
            if(password == confpassword)
            {
                List<string> emails = db.Users.AsNoTracking().Select(o=>o.Email).ToList();
                bool b = false;
                foreach(var item in emails)
                {
                    if(email == item)
                    {
                        b = true;
                    }
                }
                if(b == false)
                {
                    User = new Users() { Email = email, Password = password, Lastname = lastname, PhoneNumber = phonenumber, RoleId = 2 };
                    //ViewData["user"] = 
                    return RedirectToAction("Authentication", "Home", new { user = JsonSerializer.Serialize(User)});
                }
            }
            return Redirect("/Home/UnvalidError");
            //await db.Users.AddAsync(User);
            //return Redirect("/Home/Authentication");
            
        }
        [HttpGet]
        public IActionResult Authentication(string user)
        {
            Random random = new Random();
            string key = "";
            key = $"{key}{random.Next(100000, 999999)}";
            this.key = key;
            //ViewData["key"] = key;
            ViewData["user"] = user;
            User = JsonSerializer.Deserialize<Users>(user);

            var fromAddress = new MailAddress("robottester51@gmail.com", "Conference");
            var toAddress = new MailAddress(User.Email, "Yehor");
            const string fromPassword = "iokkbczukalzztuv";
            const string subject = "Your Code";
            string body = $"Hello! This is the Authentication code for your registration account in Net Conference!\nHere is you're code: [{key}]";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(5);
            options.IsEssential = true;
            options.Path = "/";

            HttpContext.Response.Cookies.Append("AuthKey", key, options);

            return View();
        }
        public async Task<IActionResult> checking(string key,string user)
        {
            string correct = Request.Cookies["AuthKey"];

            if (correct == key)
            {
                Users a = JsonSerializer.Deserialize<Users>(user);
                await db.Users.AddAsync(a);
                db.SaveChanges();
                return RedirectToAction("MainPage", "Main", new { id = a.Id });
            }
            return Redirect("/Home/UnvalidError");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UnvalidError()
        {
            return View();
        }
    }
}