using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string MyName { get; set; }

        public void OnGet()
        {
            Random rnd = new Random();
            int value = rnd.Next(0,100);    //  100 is NOT included
            if (value % 2 == 0)
            {
                MyName = $"Chris ({value}) welcome to your Razon Pages";
            }
            else
            {
                MyName = null;
            }

        }
    }
}