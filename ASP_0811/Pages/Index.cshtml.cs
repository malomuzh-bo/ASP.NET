using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_0811.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            var checkBox = Request.Form["cb"];
            if (checkBox == "Subscribe")
            {
                Message = "Thank you for subscribing!";
            }
        }
        public void OnGet()
        {
            
        }
    }
}