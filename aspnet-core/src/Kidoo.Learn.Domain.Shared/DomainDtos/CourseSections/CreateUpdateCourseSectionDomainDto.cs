using Kidoo.Learn.DomainDtos.CourseTopics;
using System.Collections.Generic;

namespace Kidoo.Learn.DomainDtos.CourseSections
{
    public class CreateUpdateCourseSectionDomainDto
    {
        public string ThumbnailFileName { get; private set; }
        public string ThumbnailFileType { get; private set; }
        public byte[] ThumbnailFileContent { get; private set; }
        public string Title { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<CreateUpdateCourseTopicDomainDto> CourseTopics { get; set; }
    }
}
