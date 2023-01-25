using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ASP_1411_lab.Pages
{
	public class IndexModel : PageModel
	{
		public List<Book> books = new List<Book>();
		public List<Book> f_books = new List<Book>();
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
            using (StreamReader sr = new StreamReader("books.json"))
            {
                books = JsonConvert.DeserializeObject<List<Book>>(sr.ReadToEnd());
            }
            f_books = books;
        }

		public void OnGet()
		{
			/*using (StreamWriter sw = new StreamWriter("books.json"))
			{
				sw.WriteLine(JsonConvert.SerializeObject(books));
			}*/
		}
		public async Task OnPostSearchAsync(string s_name)
		{
			f_books = books.Where(q => q.BookName.ToLower().Contains(s_name.ToLower())).ToList();
		}

	}
}