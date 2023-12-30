using Kidoo.Learn.Consts.Course;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.Courses
{
    public class CreateUpdateCourseDto
    {
        [Required]
        public IFormFile Thumbnail { get; set; }

        [Required(ErrorMessage = "Tittle is required")]
        [StringLength(CourseConsts.MaxTitleLength, ErrorMessage = "Title cannot exceed {1} characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tittle is required")]
        [StringLength(CourseConsts.MaxDescriptionLength, ErrorMessage = "Title cannot exceed {1} characters")]
        public string Description { get; set; }

        [Required]
        public int NumberOfLectures { get; set; }

        [Required]
        public double VideoDurationInMinutes { get; set; }

        [Required]
        public int MinAge { get; set; }

        [Required]
        public int MaxAge { get; set; }
        //public ICollection<CreateUpdateCourseSectionDto> CourseSections { get; set; }
    }
}
