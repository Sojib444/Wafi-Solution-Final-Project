using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using Microsoft.AspNetCore.Mvc;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages.CourseSections
{
    public class EditModal : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]        
        public CreateUpdateCourseSectionDto SectionDto { get; set; }

        private readonly ICourseAppService courseAppService;

        public EditModal(ICourseAppService courseAppService)
        {
            this.courseAppService = courseAppService;
        }

        public void OnGet()
        {
        }
    }
}
