using Kidoo.Learn.Courses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kidoo.Learn.Web.Pages.Courses
{
    public class EditModal : KidooPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public CreateUpdateCourseDto Course { get; set; }

        private ICourseAppService _courseAppService;

        public EditModal(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        public async Task OnGetAsync()
        {
            var course = await _courseAppService.GetCourseAsync(Id);

            Course = ObjectMapper.Map<CourseDto, CreateUpdateCourseDto>(course);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _courseAppService.UpdateCourseAsync(Course, Id);

            return Page();
        }
    }
}
