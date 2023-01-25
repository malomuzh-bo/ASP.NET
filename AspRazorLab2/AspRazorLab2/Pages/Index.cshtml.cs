using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AspRazorLab2.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> product = new List<Product>()
        { 
            new Product() { Id = Guid.NewGuid().ToString(), Name = "Iphone", Price=1000 },
            new Product() { Id = Guid.NewGuid().ToString(), Name = "Samsung", Price=1200 },
            new Product() { Id = Guid.NewGuid().ToString(), Name = "Nokio", Price=5000 }
        };
        public List<string> searchProd = new List<string>(); 
        private readonly ILogger<IndexModel> _logger;
        public string? search { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            try
            {
                using (StreamReader sr = new StreamReader("products.json"))
                {
                    product = JsonConvert.DeserializeObject<List<Product>>(sr.ReadToEnd());
                }
            }
            catch (Exception ex)
            {

            }
        }
        public string getProduct(Product prod)
        {
            return $"{prod.Name}:${prod.Price}";
        }
        public bool SearchProd(Product item)
        {
            if (search != null)
            {
                if (item.Name.ToLower().Contains(search.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        public async Task OnPostAsync(string prod,string price)
        {
            product.Add(new Product() { Id = Guid.NewGuid().ToString(), Name = prod, Price = int.Parse(price) });
            
            using(StreamWriter sw = new StreamWriter("products.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(product));
            }
        }
        public async Task OnPostSearchAsync(string search)
        {
            this.search = search;
        }
        public async Task<IActionResult> OnPostNextPageAsync()
        {
            return RedirectToPage("Category");
        }
    }
}