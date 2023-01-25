using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_0811.Pages
{
    public class Index1_2Model : PageModel
    {
        public int score { get; set; }
        public void OnGet(string score)
        {
            this.score = int.Parse(score);
        }
        public async Task<IActionResult> OnPost(int score)
        {
            for (int i = 0; i < 10; i++)
            {
                score += int.Parse(Request.Form[$"q{i + 1}"]);
            }
            return RedirectToPage("TestResult", new { score });
        }
    }
}
