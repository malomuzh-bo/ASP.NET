using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ASP_1411_lab.Pages
{
    public class DetailsModel : PageModel
    {
        public Book book { get; set; }
        public void OnGet(string id)
        {
            List<Book> books = new List<Book>();
            using (StreamReader sr = new StreamReader("books.json"))
            {
                books = JsonConvert.DeserializeObject<List<Book>>(sr.ReadToEnd());
            }
            book = books.Where(q => q.Id == id).First();
        }
    }
}
