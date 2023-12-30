using Kidoo.Learn.Consts.Topic;
using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.CourseTopics
{
    public class CreateUpdateCourseTopicDto
    {
        [Required(ErrorMessage ="Thumbnil Url is Required")]
        public string ThumbnailUrl { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [StringLength(CourseSectionTopicConsts.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        public double VideoDurationInMinutes { get; set; }

        [Required(ErrorMessage = "Video Url is Required")]
        public string VideoUrl { get; set; }
    }
}
