using Kidoo.Learn.DomainDtos.CourseSections;
using Kidoo.Learn.DomainDtos.CourseTopics;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Kidoo.Learn.Courses
{
    public interface ICourseManager : IDomainService
    {
        Task<Course> CreateCourseAsync(
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            [NotNull] string description,
            int numberOfLectures,
            double videoDurationInMinutes);
        Task<Course> UpdateCourseAsync(Course course,
            [NotNull] string thumbnailFileName,
            [NotNull] string thumbnailFileType,
            [NotNull] byte[] thumbnailFileContent,
            [NotNull] string title,
            [NotNull] string description,
            int numberOfLectures,
            double videoDurationInMinutes);
        Task AddSectionAsync(Course course, CreateUpdateCourseSectionDomainDto courseSectionDomainDto, Guid courseId);
        Task AddTopicAsync(Course course, CreateUpdateCourseTopicDomainDto courseTopicDomainDto, Guid sectionId);
    }
}
