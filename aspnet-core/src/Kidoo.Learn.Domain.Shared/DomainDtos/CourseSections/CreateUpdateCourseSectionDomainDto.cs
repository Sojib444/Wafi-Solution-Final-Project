using Kidoo.Learn.DomainDtos.CourseTopics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kidoo.Learn.DomainDtos.CourseSections
{
    public class CreateUpdateCourseSectionDomainDto
    {
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<CreateUpdateCourseTopicDomainDto> CourseTopics { get; set; }
    }
}
