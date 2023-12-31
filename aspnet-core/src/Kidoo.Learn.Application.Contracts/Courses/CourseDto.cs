using Kidoo.Learn.CourseSections;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Courses
{
    public class CourseDto : EntityDto<Guid>
    {
        public FormFile Thumbnail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfLectures { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<CourseSectionDto> CourseSections { get; set; }
    }
}
