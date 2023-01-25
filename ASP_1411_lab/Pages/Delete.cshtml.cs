using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace ASP_1411_lab.Pages
{
	public class DeleteModel : PageModel
	{
		public List<Book> books = new List<Book>();
		public Book obj;

		public DeleteModel()
		{
			using (StreamReader sr = new StreamReader("books.json"))
			{
				books = JsonConvert.DeserializeObject<List<Book>>(sr.ReadToEnd());
			}
		}
		public async Task<IActionResult> OnGetAsync(string id)
		{
			obj = new Book();

			obj = books.Where(q => q.Id == id).First();

			books.Remove(obj);

            using (StreamWriter sw = new StreamWriter("books.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(books));
            }

            return RedirectToPage("Index");
		}
	}
}
