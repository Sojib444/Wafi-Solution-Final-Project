using Kidoo.Learn.Consts.Section;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.CourseSections
{
    public class CreateUpdateCourseSectionDto
    {
        [Required(ErrorMessage = "Thumnail is Required")]
        public IFormFile File { get; set; }

        [Required]
        [StringLength(CourseSectionConsts.MaxTitleLength, ErrorMessage = "Title is can't exceed {1} charater")]
        public string Title { get; set; }

        [Required]
        public double VideoDurationInMinutes { get; set; }

        [Required]
        public int MinAge { get; set; }

        [Required]
        public int MaxAge { get; set; }
        //public ICollection<CreateUpdateCourseTopicDto> CourseTopics { get; set; }
    }
}
