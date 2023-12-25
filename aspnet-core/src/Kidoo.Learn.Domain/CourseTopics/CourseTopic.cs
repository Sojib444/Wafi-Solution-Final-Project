using Kidoo.Learn.CourseSections;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities;

namespace Kidoo.Learn.CourseTopics
{
    public class CourseTopic : Entity<Guid>
    {
        public string ThumbnailUrl { get; private set; }
        public string Title { get; private set; }
        public double VideoDurationInMinutes { get; private set; }
        public string VideoUrl { get; private set; }
        public Guid CourseSectionId { get; private set; }
        public CourseSection CourseSection { get; private set; }
        private CourseTopic() { }
        public CourseTopic(
            Guid id,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoUrl,
            Guid courseSectionId,
            [NotNull] string thumbnailUrl) : base(id)
        {
            ThumbnailUrl = thumbnailUrl;
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            CourseSectionId = courseSectionId;
            VideoUrl = videoUrl;
        }

        public CourseTopic UpdateTopic(
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoUrl,
            Guid courseSectionId,
            [NotNull] string thumbnailUrl)
        {
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            VideoUrl = videoUrl;
            CourseSectionId = courseSectionId;
            ThumbnailUrl = thumbnailUrl;

            return this;
        }
    }
}
