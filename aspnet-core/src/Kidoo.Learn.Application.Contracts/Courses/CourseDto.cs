using Kidoo.Learn.CourseSections;
using Kidoo.Learn.CourseTopics;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Courses
{
    public class CourseDto : EntityDto<Guid>
    {
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfLectures { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<CourseSectionDto> CourseSections { get; set; }
    }
}
