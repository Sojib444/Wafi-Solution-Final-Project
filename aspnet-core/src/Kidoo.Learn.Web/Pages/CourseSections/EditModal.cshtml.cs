using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages.CourseSections
{
    public class EditModal : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        [BindProperty]        
        public CreateUpdateCourseSectionDto Section { get; set; }

        private readonly ICourseAppService _courseAppService;

        public EditModal(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        public async Task OnGetAsync()
        {
            var courseSectionDto = await _courseAppService.GetCourseSectionAsync(CourseId,Id);

            Section = ObjectMapper.Map<CourseSectionDto,CreateUpdateCourseSectionDto>(courseSectionDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _courseAppService.UpdateSectionAsync(Section, CourseId, Id);

            return Page();
        }
    }
}
