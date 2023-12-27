using Kidoo.Learn.CourseSections;
using Kidoo.Learn.CourseTopics;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Kidoo.Learn.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        Task<CourseDto> CreateCourseAsync(CreateUpdateCourseDto input);
        Task<CourseDto> UpdateCourseAsync(UpdateCourseDto input, Guid courseId);
        Task<CourseDto> GetCourseAsync(Guid courseId);
        Task<PagedResultDto<CourseDto>> GetListAsync();

        Task AddSectionAsync(CreateUpdateCourseSectionDto input, Guid courseId);
        Task UpdateSectionAsync(CreateUpdateCourseSectionDto input, Guid courseId, Guid sectionId);
        Task AddTopicAsync(CreateUpdateCourseTopicDto input, Guid sectionId, Guid courseId);
        Task UpdateTopicAsync(CreateUpdateCourseTopicDto input, Guid courseId, Guid sectionId, Guid topicId);
    }
}
