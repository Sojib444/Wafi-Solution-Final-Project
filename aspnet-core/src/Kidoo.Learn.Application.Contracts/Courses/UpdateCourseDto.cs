using Kidoo.Learn.Consts.Course;
using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.Courses
{
    public class UpdateCourseDto
    {
        [Required(ErrorMessage = "Thumbnail is required")]
        public FormFile Thumbnail { get; set; }

        [Required]
        [StringLength(CourseConsts.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(CourseConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public int NumberOfLectures { get; set; }

        [Required]
        public double VideoDurationInMinutes { get; set; }

        [Required]
        public int MinAge { get; set; }

        [Required]
        public int MaxAge { get; set; }
    }
}
