using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_0811.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		public List<string> countries = new List<string>()
		{
			"Poland", "Ukraine", "United States of America", "Portugal", "Singapore", "Miami", "New Zeland", "French Polynesia"
		};

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}
		public void OnGet(string input_name)
		{
			ViewData["p"] = input_name;
		}
	}
}