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
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int NumberOfLectures { get; private set; }
        public double VideoDurationInMinutes { get; private set; }
        public int MinAge { get; private set; }
        public int MaxAge { get; private set; }
        public ICollection<CourseSection> Sections { get; private set; }

        //for file 
        public string ThumbnailFileName { get; private set; }
        public string ThumbnailFileType { get; private set; }
        public byte[] ThumbnailFileContent { get; private set; }

        private Course() { }
        public Course(
            Guid id,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            [NotNull] string description,
            int numberOfLectures,
            double videoDurationInMinutes) : base(id)
        {
            Title = title;
            Description = description;
            NumberOfLectures = numberOfLectures;
            VideoDurationInMinutes = videoDurationInMinutes;

            // Initialize file-related properties
            ThumbnailFileName = thumbnailFileName;
            ThumbnailFileType = thumbnailFileType;
            ThumbnailFileContent = thumbnailFileContent;

        }

        public Course Update(
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            [NotNull] string description,
            [NotNull] int numberOfLectures,
            [NotNull] double videoDurationInMinutes) 
        {            
            Title = title;
            Description = description;
            NumberOfLectures = numberOfLectures;
            VideoDurationInMinutes = videoDurationInMinutes;

            // Initialize file-related properties
            ThumbnailFileName = thumbnailFileName;
            ThumbnailFileType = thumbnailFileType;
            ThumbnailFileContent =thumbnailFileContent;

            return this;
        }

        internal Course AddSection(
            Guid id,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId)
        {

            Sections.Add(new CourseSection(
                id, 
                thumbnailFileName,
                thumbnailFileType,
                thumbnailFileContent, 
                title, 
                videoDurationInMinutes, 
                minAge,maxAge, courseId));

            return this;
        }

        public Course UpdateSection(
            Guid sectionId,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            double videoDurationInMinutes,
            int minAge,
            int maxAge,
            Guid courseId)
        {
            var section = GetSection(sectionId);

            section.UpdateSection(thumbnailFileName,thumbnailFileType,thumbnailFileContent, title, videoDurationInMinutes, minAge, maxAge, courseId);

            return this;
        }

        public Course DeleteSection(
            Guid sectionId)
        {
            var section = GetSection(sectionId);

            Sections.Remove(section);

            return this;
        }

        public Course AddTopic(
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
            var section = GetSection(courseSectionId);

            section.AddTopic(
                topicId, 
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

        public Course UpdateTopic(
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
            var section = GetSection(courseSectionId);

            section.UpdateTopic(topicId,
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

        private CourseSection GetSection(Guid sectionId)
        {
            return Sections.FirstOrDefault(t => t.Id == sectionId) ??
                   throw new EntityNotFoundException(typeof(CourseSection), sectionId);
        }
    }
}
