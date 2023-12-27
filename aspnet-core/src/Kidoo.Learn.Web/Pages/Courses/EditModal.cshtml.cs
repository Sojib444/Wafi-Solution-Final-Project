using Kidoo.Learn.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Kidoo.Learn.Web.Pages.Courses
{
    public class EditModal : PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateCourseDto Course { get; set; }

        private ICourseAppService _courseAppService;

        public EditModal(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        public async Task OnGetAsync()
        {

        }
    }
}
