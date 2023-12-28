using System.Collections.Generic;
using System;

namespace Kidoo.Learn.Courses
{
    public class AddSectionDto
    {
        public string ThumbnailUrl { get; private set; }
        public string Title { get; private set; }
        public double VideoDurationInMinutes { get; private set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Guid CourseId { get; private set; }
    }
}
