using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_0811.Pages
{
    public class AboutModel : PageModel
    {
        public List<string> countries = new List<string>()
        {
            "Poland", "Ukraine", "United States of America", "Portugal", "Singapore", "Miami", "New Zeland", "French Polynesia"
        };
        public void OnGet()
        {

        }
    }
}
