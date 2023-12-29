using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages.CourseSections
{
    public class CreateModal : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        [BindProperty]
        public  CreateUpdateCourseSectionDto courseSection { get; set; }

        private readonly ICourseAppService _courseAppService;

        public CreateModal(ICourseAppService courseAppService)
        {
           _courseAppService = courseAppService;
        }

        public async Task OnGetAsync()
        {
            courseSection = new CreateUpdateCourseSectionDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _courseAppService.AddSectionAsync(courseSection, CourseId);

            return Page();
        }
    }
}
