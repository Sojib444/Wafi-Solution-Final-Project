using Kidoo.Learn.CourseSections;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Kidoo.Learn.Courses
{ 
    public class Course : FullAuditedAggregateRoot<Guid>  
    {
        public string ThumbnailUrl { get; private set; } 
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int NumberOfLectures { get; private set; }
        public double VideoDurationInMinutes { get; private set; }
        public int MinAge { get; private set; }
        public int MaxAge { get; private set; }
        public ICollection<CourseSection> Sections { get; private set; }
        private Course() { }
        public Course(
            Guid id,
            [NotNull] string thumbnailUrl,
            [NotNull] string title,
            [NotNull] string description,
            int numberOfLectures,
            double videoDurationInMinutes) : base(id)
        {
            ThumbnailUrl = thumbnailUrl;
            Title = title;
            Description = description;
            NumberOfLectures = numberOfLectures;
            VideoDurationInMinutes = videoDurationInMinutes;
        }

        public Course Update(
            [NotNull] string thumbnailUrl,
            [NotNull] string title,
            [NotNull] string description,
            [NotNull] int numberOfLectures,
            [NotNull] double videoDurationInMinutes) 
        {
            ThumbnailUrl = thumbnailUrl;
            Title = title;
            Description = description;
            NumberOfLectures = numberOfLectures;
            VideoDurationInMinutes = videoDurationInMinutes;
            return this;
        }

        internal Course AddSection(
            Guid id,
            [NotNull] string thumbnailUrl,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId)
        {

            Sections.Add(new CourseSection(id, thumbnailUrl, title, videoDurationInMinutes, minAge, maxAge, courseId));

            return this;
        }

        public Course UpdateSection(
            Guid sectionId,
            [NotNull] string thumbnailUrl,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId)
        {
            var section = GetSection(sectionId);

            section.UpdateSection(thumbnailUrl, title, videoDurationInMinutes, minAge, maxAge, courseId);

            return this;
        }

        public Course AddTopic(
            Guid topicId,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoUrl,
            Guid courseSectionId,
            [NotNull] string thumbnailUrl)
        {
            var section = GetSection(courseSectionId);

            section.AddTopic(topicId, title, videoDurationInMinutes, videoUrl, courseSectionId, thumbnailUrl);
            return this;
        }

        public Course UpdateTopic(
            Guid topicId,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoUrl,
            Guid courseSectionId,
            [NotNull] string thumbnailUrl)
        {
            var section = GetSection(courseSectionId);

            section.UpdateTopic(topicId, title, videoDurationInMinutes, videoUrl, courseSectionId, thumbnailUrl);

            return this;
        }

        private CourseSection GetSection(Guid sectionId)
        {
            return Sections.FirstOrDefault(t => t.Id == sectionId) ??
                   throw new EntityNotFoundException(typeof(CourseSection), sectionId);
        }
    }
}
