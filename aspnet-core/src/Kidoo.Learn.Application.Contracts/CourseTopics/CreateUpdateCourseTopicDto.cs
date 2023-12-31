using Kidoo.Learn.Consts.Topic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.CourseTopics
{
    public class CreateUpdateCourseTopicDto
    {
        [Required(ErrorMessage = "Video is Required")]
        public IFormFile Thumbnail { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [StringLength(CourseSectionTopicConsts.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        public double VideoDurationInMinutes { get; set; }

        [Required(ErrorMessage = "Video is Required")]
        public IFormFile Video { get; set; }
    }
}
