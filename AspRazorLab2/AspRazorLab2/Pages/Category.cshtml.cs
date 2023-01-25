using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AspRazorLab2.Pages
{
    public class CategoryModel : PageModel
    {
        public List<Category> Categories = new List<Category>()
        {
             new Category() { Id = Guid.NewGuid().ToString(), CategoryName = "Gadgets"},
             new Category() { Id = Guid.NewGuid().ToString(), CategoryName = "Cars"},
             new Category() { Id = Guid.NewGuid().ToString(), CategoryName = "Food"}
        };
        public List<Product> Products = new List<Product>();
        public List<string> searchCategory = new List<string>();
        public string? search { get; set; }
        private readonly ILogger<CategoryModel> _logger;

        public string? selectedcat { get; set; }
        public CategoryModel(ILogger<CategoryModel> logger)
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
            return $"{c.CategoryName}:{c.getProd()}";
        }
        public bool SearchCategory(Category item)
        {
            if (search != null)
            {
                if (item.CategoryName.ToLower().Contains(search.ToLower()))
                {
                    return true;
                }
            }
            return false;
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
            this.search = search;
        }
        public async Task OnPostAddProdAsync(string selectedcat)
        {
            this.selectedcat = selectedcat;
        }
        public void AddProdToCat()
        {
            try
            {
                if (selectedcat != null)
                {
                    List<Product> temp = Categories.Where(o => o.CategoryName == selectedcat).Select(o=>o.products).First();

                    foreach (var item in Products)
                    {
                        Categories.Where(o => o.CategoryName == selectedcat).Select(o => o.products).First().Add(Products.Where(o => o.Name == Request.Form[item.Name]).First());
                    }
                    using (StreamWriter sw = new StreamWriter("categoria.json"))
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(Categories));
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public async Task<IActionResult> OnPostNextPage()
        {
            return RedirectToPage("Index");
        }
    }
}
