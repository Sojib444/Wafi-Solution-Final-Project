using System;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.CourseTopics
{
    public class CourseTopicDto : EntityDto<Guid>
    {
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public string VideoUrl { get; set; }
    }
}
