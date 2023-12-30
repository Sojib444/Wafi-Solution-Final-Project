using Kidoo.Learn.DomainDtos.CourseSections;
using Kidoo.Learn.DomainDtos.CourseTopics;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Courses
{
    public class CourseManager : DomainService, ICourseManager
    {
        private readonly IRepository<Course, Guid> _courseRepository;
        public CourseManager(IRepository<Course, Guid> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> CreateCourseAsync(
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            [NotNull] string description,
            int numberOfLectures,
            double videoDurationInMinutes)
        {
            Check.NotNullOrWhiteSpace(title, nameof(title));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var isExistTitle = await _courseRepository.AnyAsync(x => x.Title == title);
            if (isExistTitle)
                throw new BusinessException($"Course already exist with '{title}' title");

            return new Course(
                GuidGenerator.Create(),
                thumbnailFileName,
                thumbnailFileType,
                thumbnailFileContent,
                title,
                description,
                numberOfLectures,
                videoDurationInMinutes);
        }

        public async Task<Course> UpdateCourseAsync(Course course,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            [NotNull] string description,
            int numberOfLectures,
            double videoDurationInMinutes)
        {
            return course.Update(thumbnailFileName,thumbnailFileType,thumbnailFileContent, title, description, numberOfLectures, videoDurationInMinutes);
        }

        public async Task AddSectionAsync(Course course, CreateUpdateCourseSectionDomainDto courseSectionDomainDto, Guid courseId)
        {
            course.AddSection(
                GuidGenerator.Create(),
                courseSectionDomainDto.ThumbnailFileName,
                courseSectionDomainDto.ThumbnailFileType,
                courseSectionDomainDto.ThumbnailFileContent,
                courseSectionDomainDto.Title,
                courseSectionDomainDto.VideoDurationInMinutes,
                courseSectionDomainDto.MinAge,
                courseSectionDomainDto.MaxAge,
                courseId
            );
        }

        public async Task AddTopicAsync(Course course, CreateUpdateCourseTopicDomainDto courseTopicDomainDto, Guid sectionId)
        {
            if (!course.Sections.Select(x => x.Id).Contains(sectionId))
                throw new UserFriendlyException($"The section id {sectionId} doesn't belong to the course {course.Title}");

            course.AddTopic(
                GuidGenerator.Create(),
                courseTopicDomainDto.Title,
                courseTopicDomainDto.VideoDurationInMinutes,
                courseTopicDomainDto.VideoFileName,
                courseTopicDomainDto.VideoFileType,
                courseTopicDomainDto.VideoFileContent,
                sectionId,
                courseTopicDomainDto.ThumbnailFileName,
                courseTopicDomainDto.ThumbnailFileType,
                courseTopicDomainDto.ThumbnailFileContent
            );
        }
    }
}
