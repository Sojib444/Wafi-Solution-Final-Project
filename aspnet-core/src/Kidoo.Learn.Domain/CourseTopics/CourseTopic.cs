using Kidoo.Learn.CourseSections;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities;

namespace Kidoo.Learn.CourseTopics
{
    public class CourseTopic : Entity<Guid>
    {
        public string Title { get; private set; }
        public double VideoDurationInMinutes { get; private set; }
        public Guid CourseSectionId { get; private set; }
        public CourseSection CourseSection { get; private set; }

        //for file 
        public string ThumbnailFileName { get; private set; }
        public string ThumbnailFileType { get; private set; }
        public byte[] ThumbnailFileContent { get; private set; }

        //for file 
        public string VideoFileName { get; private set; }
        public string VideoFileType { get; private set; }
        public byte[] VideoFileContent { get; private set; }


        private CourseTopic() { }
        public CourseTopic(
            Guid id,
            [NotNull] string title,
            double videoDurationInMinutes,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            Guid courseSectionId,
            [NotNull] string videoFileName,
            [NotNull] string videoFileType,
            [NotNull] byte[] videoFileContent) : base(id)
        {
            ThumbnailFileName = thumbnailFileName;
            ThumbnailFileType = thumbnailFileType;
            ThumbnailFileContent = thumbnailFileContent;
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            CourseSectionId = courseSectionId;
            VideoFileName = videoFileName;
            VideoFileType = videoFileType;
            VideoFileContent = videoFileContent;
        }

        public CourseTopic UpdateTopic(
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
            Title = title;
            VideoDurationInMinutes = videoDurationInMinutes;
            CourseSectionId = courseSectionId;
            VideoFileName = videoFileName;
            VideoFileType = videoFileType;
            VideoFileContent = videoFileContent;
            CourseSectionId = courseSectionId;
            ThumbnailFileName = thumbnailFileName;
            ThumbnailFileType = thumbnailFileType;
            ThumbnailFileContent = thumbnailFileContent;

            return this;
        }
    }
}
