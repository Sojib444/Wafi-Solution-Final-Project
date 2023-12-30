using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseTopics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages.CourseSectionTopics
{
    public class EditModal : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid SectionId { get; set; }

        [BindProperty]
        public CreateUpdateCourseTopicDto Topic {get;set;}


        private readonly ICourseAppService _courseAppService;

        public EditModal(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        public async Task OnGetAsync()
        {
            var topic = await _courseAppService.GetCourseSectionTopicAsync(CourseId, SectionId, Id);

            Topic = ObjectMapper.Map<CourseTopicDto, CreateUpdateCourseTopicDto>(topic);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _courseAppService.UpdateTopicAsync(Topic, CourseId, SectionId, Id);

            return Page();
        }
    }
}
