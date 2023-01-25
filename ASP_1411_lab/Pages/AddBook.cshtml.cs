using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace ASP_1411_lab.Pages
{
	[IgnoreAntiforgeryToken]
	public class AddBookModel : PageModel
	{
		public async Task<IActionResult> OnPostAsync(string bn, string an, string st, string pub, string yr)
		{
			Book obj = new Book();

			obj.Id = Guid.NewGuid().ToString();
			obj.BookName = bn;
			obj.AuthorName = an;
			obj.Style = st;
			obj.Publisher = pub;
			obj.Year = int.Parse(yr);

			List<Book> books = new List<Book>();

			using (StreamReader sr = new StreamReader("books.json"))
			{
				books = JsonConvert.DeserializeObject<List<Book>>(sr.ReadToEnd());
			}
			books.Add(obj);

			using (StreamWriter sw = new StreamWriter("books.json"))
			{
				sw.WriteLine(JsonConvert.SerializeObject(books));
			}

			return RedirectToPage("Index");
		}
	}
}
