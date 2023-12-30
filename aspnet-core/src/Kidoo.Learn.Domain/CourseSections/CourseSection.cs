using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseTopics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Kidoo.Learn.CourseSections
{
    public class CourseSection : Entity<Guid>
    {
        public string ThumbnailUrl { get; private set; }
        public string Title { get; private set; }
        public double VideoDurationInMinutes { get; private set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<CourseTopic> Topics { get; private set; }
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; }
        public CourseSection() { }
        public CourseSection(
            Guid id,
            [NotNull] string thumbnailUrl,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId) : base(id)
        {
            ThumbnailUrl = thumbnailUrl;
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            MinAge = minAge;
            MaxAge = maxAge;
            CourseId = courseId;
        }

        public CourseSection UpdateSection(
            [NotNull] string thumbnailUrl,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId)
        {
            ThumbnailUrl = thumbnailUrl;
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            MinAge = minAge;
            MaxAge = maxAge;
            CourseId = courseId;

            return this;
        }

        internal CourseSection AddTopic(
            Guid id,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoUrl,
            Guid courseSectionId,
            [NotNull] string thumbnailUrl)
        {

            Topics.Add(new CourseTopic(id, title, videoDurationInMinutes, videoUrl, courseSectionId, thumbnailUrl));

            return this;
        }

        internal CourseSection UpdateTopic(
            Guid topicId,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoUrl,
            Guid courseSectionId,
            [NotNull] string thumbnailUrl)
        {
            var topic = Topics.FirstOrDefault(t => t.Id == topicId);

            topic.UpdateTopic(title, videoDurationInMinutes, videoUrl, courseSectionId, thumbnailUrl);

            return this;
        }

        public CourseSection DeleteTopic(
            Guid topicId,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoUrl,
            Guid courseSectionId,
            [NotNull] string thumbnailUrl)
        {
            var topic = Topics.FirstOrDefault(t => t.Id == topicId);

            Topics.Remove(topic);

            return this;
        }
    }
}
