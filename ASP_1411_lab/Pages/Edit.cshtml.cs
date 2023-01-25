using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;

namespace ASP_1411_lab.Pages
{
	[IgnoreAntiforgeryToken]
	public class EditModel : PageModel
	{
		[BindProperty]
		public Book? Book { get; set; }
		public List<Book> books = new List<Book>();
		public EditModel()
		{
            using (StreamReader sr = new StreamReader("books.json"))
            {
                books = JsonConvert.DeserializeObject<List<Book>>(sr.ReadToEnd());
            }
        }
		public async Task<IActionResult> OnGetAsync(string id)
		{
			Book = books.Where(q => q.Id == id).First();
			if (Book == null)
				return NotFound();
			return Page();
		}
		public async Task<IActionResult> OnPostAsync(string id, string b_name, string a_name, string publ, string st, string year)
		{
			books.Where(q => q.Id == id).First().BookName = b_name;
			books.Where(q => q.Id == id).First().AuthorName = a_name;
			books.Where(q => q.Id == id).First().Publisher = publ;
			books.Where(q => q.Id == id).First().Style = st;
			books.Where(q => q.Id == id).First().Year = int.Parse(year);

			using (StreamWriter sw = new StreamWriter("books.json"))
			{
				sw.WriteLine(JsonConvert.SerializeObject(books));
			}

			return RedirectToPage("Index");
		}
	}
}
