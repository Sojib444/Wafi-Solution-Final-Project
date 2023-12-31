using Kidoo.Learn.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Kidoo.Learn.Web.Pages.Courses
{
    public class CreateModal : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public CreateUpdateCourseDto Course { get; set; }

        private ICourseAppService _courseAppService;

        public CreateModal(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        public async Task OnGetAsync()
        {
            Course = new CreateUpdateCourseDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _courseAppService.CreateCourseAsync(Course);

            return Page();
        }
    }
}
