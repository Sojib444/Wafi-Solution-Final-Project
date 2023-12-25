using System;
using System.Collections.Generic;
using System.Text;

namespace Kidoo.Learn.DomainDtos.CourseTopics
{
    public class CreateUpdateCourseTopicDomainDto
    {
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public string VideoUrl { get; set; }
    }
}
