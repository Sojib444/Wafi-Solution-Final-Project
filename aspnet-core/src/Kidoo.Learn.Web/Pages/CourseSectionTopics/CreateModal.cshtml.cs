using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseTopics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages.CourseSectionTopics
{
    public class CreateModal : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid SectionId { get; set; }

        [BindProperty]
        public CreateUpdateCourseTopicDto Topic { get; set; }

        public ICourseAppService _courseAppService { get; }

        public CreateModal(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        public async Task OnGet()
        {
            Topic = new CreateUpdateCourseTopicDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _courseAppService.AddTopicAsync(Topic, SectionId, CourseId);

            return Page();
        }
    }
}
