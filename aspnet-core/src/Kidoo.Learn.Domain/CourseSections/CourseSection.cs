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
        public string Title { get; private set; }
        public double VideoDurationInMinutes { get; private set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public ICollection<CourseTopic> Topics { get; private set; }
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; }

        //for file 
        public string ThumbnailFileName { get; private set; }
        public string ThumbnailFileType { get; private set; }
        public byte[] ThumbnailFileContent { get; private set; }

        public CourseSection() { }
        public CourseSection(
            Guid id,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId) : base(id)
        {
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            MinAge = minAge;
            MaxAge = maxAge;
            CourseId = courseId;

            // Initialize file-related properties
            ThumbnailFileName = thumbnailFileName;
            ThumbnailFileType = thumbnailFileType;
            ThumbnailFileContent = thumbnailFileContent;
        }

        public CourseSection UpdateSection(
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId)
        {
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            MinAge = minAge;
            MaxAge = maxAge;
            CourseId = courseId;

            // Initialize file-related properties
            ThumbnailFileName = thumbnailFileName;
            ThumbnailFileType = thumbnailFileType;
            ThumbnailFileContent = thumbnailFileContent;

            return this;
        }

        internal CourseSection AddTopic(
            Guid id,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoFileName,
            [NotNull] string videoFileType,
            [NotNull] byte[] videoFileContent,
            Guid courseSectionId,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent)
        {

            Topics.Add(new CourseTopic(
                id, title,
                videoDurationInMinutes,
                videoFileName,
                videoFileType,
                videoFileContent, 
                courseSectionId,
                thumbnailFileName,
                thumbnailFileType,
                thumbnailFileContent));

            return this;
        }

        internal CourseSection UpdateTopic(
            Guid topicId,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string videoFileName,
            [NotNull] string videoFileType,
            [NotNull] byte[] videoFileContent,
            Guid courseSectionId,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent)
        {
            var topic = Topics.FirstOrDefault(t => t.Id == topicId);

            topic.UpdateTopic(
                title, 
                videoDurationInMinutes, 
                videoFileName,
                videoFileType,
                videoFileContent, 
                courseSectionId, 
                thumbnailFileName,
                thumbnailFileType,
                thumbnailFileContent);

            return this;
        }

        public CourseSection DeleteTopic(
            Guid topicId)
        {
            var topic = Topics.FirstOrDefault(t => t.Id == topicId);

            Topics.Remove(topic);

            return this;
        }
    }
}
