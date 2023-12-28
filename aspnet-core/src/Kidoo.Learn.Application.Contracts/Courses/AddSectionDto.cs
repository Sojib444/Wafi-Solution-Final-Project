using Kidoo.Learn.Consts.Section;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kidoo.Learn.Courses
{
    public class AddSectionDto
    {
        [Required]
        public string ThumbnailUrl { get; private set; }

        [Required(ErrorMessage ="Title can't null")]
        [StringLength(CourseSectionConsts.MaxTitleLength)]
        public string Title { get; private set; }

        [Required]
        public double VideoDurationInMinutes { get; private set; }

        [Required]
        public int MinAge { get; set; }

        [Required]
        public int MaxAge { get; set; }

        [Required]
        public Guid CourseId { get; private set; }
    }
}
