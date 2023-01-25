using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_0811.Pages
{
    public class TestResultModel : PageModel
    {
        public float score { get; set; }
        public void OnGet(string score)
        {
            this.score = (float.Parse(score) / 30) * 10;
        }
        public async Task<IActionResult> OnPost()
        {
            return RedirectToPage("Index1");
        }
    }
}
