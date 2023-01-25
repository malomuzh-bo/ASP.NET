using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_0811.Pages
{
    public class Index1Model : PageModel
    {
        public int score { get; set; }

        public async Task<IActionResult> OnPost()
        {
            for (int i = 0; i < 10; i++)
            {
                score += int.Parse(Request.Form[$"q{i + 1}"]);
            }
            return RedirectToPage("Index1_1", new { score });
        }
        public void OnGet()
        {

        }
    }
}