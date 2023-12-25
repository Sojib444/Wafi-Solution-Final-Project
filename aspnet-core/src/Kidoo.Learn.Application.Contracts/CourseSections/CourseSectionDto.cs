using Kidoo.Learn.CourseTopics;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.CourseSections
{
    public class CourseSectionDto : EntityDto<Guid>
    {
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<CourseTopicDto> CourseTopics { get; set; }
    }
}
