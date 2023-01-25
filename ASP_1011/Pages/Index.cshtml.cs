using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace ASP_1011.Pages
{
	public class IndexModel : PageModel
	{
		public List<Product>? products = new List<Product>()
		{
			new Product(){Id = Guid.NewGuid().ToString(), Name = "Pillow", Price = 29.9f },
			new Product(){Id = Guid.NewGuid().ToString(), Name = "Plaid", Price = 34.9f },
			new Product(){Id = Guid.NewGuid().ToString(), Name = "Bed", Price = 399 }
		};
		public string? Search { get; set; }
		public List<string> srchProd = new List<string>();

		private readonly ILogger<IndexModel> _logger;
		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
			try
			{
				using (StreamReader sr = new StreamReader("products.json"))
				{
					products = JsonConvert.DeserializeObject<List<Product>>(sr.ReadToEnd());
				}
			}
			catch(Exception ex)
			{
				RedirectToPage("Error");
			}
		}
		public string getProd(Product product)
		{
            return $"{product.Name}: {product.Price}";
        }
		public bool searchProduct(Product pr)
		{
			if (Search != null)
			{
				if (pr.Name.ToUpper().Contains(Search.ToUpper()))
				{
					return true;
				}
			}
			return false;
		}
		public void OnGet()
		{
		}

		public async Task OnPostAsync(string pr_name, string price)
		{
			products.Add(new Product() { Id = Guid.NewGuid().ToString(), Name = pr_name, Price = float.Parse(price) });

			using (StreamWriter sw = new StreamWriter("products.json"))
			{
				sw.WriteLine(JsonConvert.SerializeObject(products));
			}
		}
		public async Task OnPostSearchAsync(string srch_prod)
		{
			Search = srch_prod;
		}
		public async Task<IActionResult> OnPostSecondPageAsync()
		{
			return RedirectToPage("SecondPage");
		}
	}
}