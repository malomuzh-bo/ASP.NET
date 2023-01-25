using Microsoft.AspNetCore.Mvc;
using SharknessShop.Models;
using System.Diagnostics;

namespace SherknessShop.Controllers
{
	public class HomeController : Controller
	{
		ShopContext db = new ShopContext();
		public List<Product> Products = new List<Product>();

		public HomeController(ShopContext db)
		{
			this.db = db;
			Products = db.Products.ToList();
		}
		public IActionResult Registration()
		{
			return View();
		}
		public IActionResult ForKids()
        {
			return View();
        }
		public IActionResult Trand()
		{
			return View();
		}
		public IActionResult Index()
		{
			/*db.UserStates.Add(new UserState() { Name = "Admin" });
			db.SaveChanges();
			db.Users.Add(new SharknessShop.Models.User { Login = "admin", Pass = "admin", UserStateId = 1});
			db.SaveChanges();
			db.OrderStates.Add(new OrderState() { StateName = "Completed" });
			db.OrderStates.Add(new OrderState() { StateName = "Uncompleted" });
			db.OrderStates.Add(new OrderState() { StateName = "Processing" });
			db.SaveChanges();
			db.UserStates.Add(new UserState() { Name = "Client" });
			db.UserStates.Add(new UserState() { Name = "Manager" });
			db.SaveChanges();
			db.ProductCategories.Add(new ProductCategory() { CategoryName = "Watch" });
			db.ProductCategories.Add(new ProductCategory() { CategoryName = "Straps" });
			db.SaveChanges();
			db.Products.Add(new Product() { Name = "Watch1", Amount = 53, Info = "Lorem ipsum. . .", Price = 999, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch2", Amount = 35, Info = "Lorem ipsum. . .", Price = 799, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch3", Amount = 17, Info = "Lorem ipsum. . .", Price = 1299, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch4", Amount = 44, Info = "Lorem ipsum. . .", Price = 1999, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch5", Amount = 29, Info = "Lorem ipsum. . .", Price = 1999, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch6", Amount = 46, Info = "Lorem ipsum. . .", Price = 899, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch7", Amount = 15, Info = "Lorem ipsum. . .", Price = 699, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch8", Amount = 3, Info = "Lorem ipsum. . .", Price = 1999, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch9", Amount = 5, Info = "Lorem ipsum. . .", Price = 2099, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch10", Amount = 10, Info = "Lorem ipsum. . .", Price = 1999, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch11", Amount = 60, Info = "Lorem ipsum. . .", Price = 1499, ProductCategoryId = 1 });
			db.Products.Add(new Product() { Name = "Watch12", Amount = 33, Info = "Lorem ipsum. . .", Price = 999, ProductCategoryId = 1 });
			db.SaveChanges();
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/1.jpg", ProductId = 1 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/2.jpg", ProductId = 2 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/3.jpg", ProductId = 3 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/4.jpg", ProductId = 4 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/5.jpg", ProductId = 5 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/6.jpg", ProductId = 6 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/7.jpg", ProductId = 7 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/8.jpg", ProductId = 8 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/9.jpg", ProductId = 9 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/10.jpg", ProductId = 10 });
			db.ProductPics.Add(new ProductPic() { PathName = "/Photos/11.jpg", ProductId = 11 });
			db.SaveChanges();
			db.Storages.Add(new Storage() { Price = 5000 });
			db.Storages.Add(new Storage() { Price = 7609 });
			db.Storages.Add(new Storage() { Price = 5866 });
			db.SaveChanges();*/
			/*
			 * pp.where.select pathname
			 * source
			 */
			ViewData["Photos"] = db.ProductPics.ToList();

			return View(Products);
		}
		public async Task<IActionResult> Information(int id)
		{
			Product pr = await db.Products.FindAsync(id);
			if (pr != null)
            {
				ViewBag.Photos = db.ProductPics.Where(q => q.ProductId == id).ToList();
				return View(pr);
            }
			return View("Information");
		}
        [HttpPost]
        public async Task<IActionResult> Check(User user)
        {
            if (ModelState.IsValid)
            {
                User tmp = user;
                db.Users.Add(tmp);
                await db.SaveChangesAsync();
                return Redirect("Index");
            }
            return View();
        }
        /*public IActionResult ToCart()
		{
			var cookie = Request.Cookies["Cart"];
			if (cookie != null)
			{
				var val = cookie.Value;
			}
		}*/

        public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}