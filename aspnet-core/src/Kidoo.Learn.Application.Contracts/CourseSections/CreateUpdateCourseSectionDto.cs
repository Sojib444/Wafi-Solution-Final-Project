using Kidoo.Learn.CourseTopics;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.CourseSections
{
    public class CreateUpdateCourseSectionDto
    {
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        //public ICollection<CreateUpdateCourseTopicDto> CourseTopics { get; set; }
    }
}
