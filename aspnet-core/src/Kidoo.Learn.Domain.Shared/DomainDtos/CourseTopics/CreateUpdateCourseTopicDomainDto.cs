namespace Kidoo.Learn.DomainDtos.CourseTopics
{
    public class CreateUpdateCourseTopicDomainDto
    {
        public string ThumbnailFileName { get; set; }
        public string ThumbnailFileType { get; set; }
        public byte[] ThumbnailFileContent { get; set; }
        public string Title { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public string VideoFileName { get; set; }
        public string VideoFileType { get; set; }
        public byte[] VideoFileContent { get; set; }
    }
}
