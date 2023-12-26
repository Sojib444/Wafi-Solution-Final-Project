namespace Kidoo.Learn.Courses
{
    public class CreateUpdateCourseDto
    {
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfLectures { get; set; }
        public double VideoDurationInMinutes { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        //public ICollection<CreateUpdateCourseSectionDto> CourseSections { get; set; }
    }
}
