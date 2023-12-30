using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Kidoo.Learn.Web.Pages.CourseSectionTopics
{
    public class IndexModel : PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid SectionId { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SectionName { get; set; }

        public void OnGet()
        {
        }
    }
}
