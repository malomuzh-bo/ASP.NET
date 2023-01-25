using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ASP_1011.Pages
{
	public class SecondPageModel : PageModel
	{
		public List<Category> Categories = new List<Category>()
		{
			new Category() { Id = Guid.NewGuid().ToString(), CategoryName = "For bed"},
			new Category() { Id = Guid.NewGuid().ToString(), CategoryName = "Electronics"},
			new Category() { Id = Guid.NewGuid().ToString(), CategoryName = "Guns"} // ןונרו, שמ ג דמכמגף ןנטירכמ מ_מ
		};
		public List<Product> Products = new List<Product>();
		public List<string> searchCateg = new List<string>();
		public string? Search { get; set; }
		private readonly ILogger<SecondPageModel> _logger;

		public string? sel_categ { get; set; }
		public SecondPageModel(ILogger<SecondPageModel> logger)
		{
			_logger = logger;
			try
			{
				using (StreamReader sr = new StreamReader("categories.json"))
				{
					Categories = JsonConvert.DeserializeObject<List<Category>>(sr.ReadToEnd());
				}
				using (StreamReader sr = new StreamReader("products.json"))
				{
					Products = JsonConvert.DeserializeObject<List<Product>>(sr.ReadToEnd());
				}
			}
			catch (Exception ex)
			{

			}
		}
		public string getCategory(Category c)
		{
			return $"{c.CategoryName}: {c.getProd()}";
		}
		public bool searchCategory(Category c)
		{
			if (Search != null)
			{
				if (c.CategoryName.ToUpper().Contains(Search.ToUpper()))
				{
					return true;
				}
			}
			return false;
		}
		public void OnGet()
		{
		}
		public async Task OnPostAsync(string category, string price)
		{
            Categories.Add(new Category() { Id = Guid.NewGuid().ToString(), CategoryName = category });

            using (StreamWriter sw = new StreamWriter("categories.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(Categories));
            }
        }
		public async Task OnPostSearchAsync(string search)
		{
			Search = search;
		}
		public async Task OnPostAddProductAsync(string sel_category)
		{
			sel_categ = sel_category;
		}
		public void AddProdToCategory()
		{
            try
            {
                if (sel_categ != null)
                {
                    ICollection<Product> temp = (List<Product>)Categories.Where(o => o.CategoryName == sel_categ).Select(o => o.Products).First();

                    foreach (var item in Products)
                    {
                        Categories.Where(o => o.CategoryName == sel_categ).Select(o => o.Products).First().Add(Products.Where(o => o.Name == Request.Form[item.Name]).First());
                    }
                    using (StreamWriter sw = new StreamWriter("categories.json"))
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(Categories));
                    }
                }
            }
            catch (Exception)
            {

            }
        }
		public async Task<IActionResult> OnPostHomeAsync()
		{
			return RedirectToPage("Index");
		}
	}
}
