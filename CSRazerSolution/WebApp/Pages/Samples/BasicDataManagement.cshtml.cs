using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Samples
{
    public class BasicDataManagementModel : PageModel
    {
        //  attribute to connect to control
        //  BindProperty is an annotation that handles two-way data tranfer
        //      TWO-WAY means output to form for display AND input from the form processing
        //  We need a [BindProperty] on each property immediately in front to display the value entered
        [BindProperty]
        public int Num { get; set; }
        public string Feedback { get; set; }
        
        [BindProperty]
        public string MassText { get; set; }

        [BindProperty]
        public int FavouriteCourse { get; set; }

        public void OnGet()
        {
            if (Num == 0)
            {
                Feedback = "executing the OnGet method for the Get request";
            }

            else

            {
                Feedback = $"You entered the value {Num} display by OnGet";
            }
        }

        public void OnPost()
        {
            if (Num == 0)
            {
                Feedback = "executing the OnGet method for the Get request";
            }

            else

            {
                Feedback = $"You entered the value {Num} (display by OnPost)\n" +
                $" your mass text is {MassText} and I like the {FavouriteCourse} course";
            }
        }
    }
}
