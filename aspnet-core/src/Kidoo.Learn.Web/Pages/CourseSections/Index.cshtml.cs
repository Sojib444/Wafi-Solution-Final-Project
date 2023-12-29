using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Kidoo.Learn.Web.Pages.CourseSections
{
    public class IndexModel : PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        public void OnGetAsync()
        {
            
        }
    }
}
