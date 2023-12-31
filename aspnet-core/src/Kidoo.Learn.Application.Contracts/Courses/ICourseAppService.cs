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
        //Course Feature 
        Task<CourseDto> CreateCourseAsync(CreateUpdateCourseDto input);
        Task<CourseDto> UpdateCourseAsync(UpdateCourseDto input, Guid courseId);
        Task<CourseDto> GetCourseAsync(Guid courseId);
        Task<PagedResultDto<CourseDto>> GetListAsync();
        Task DeleteCourseAsync(Guid courseId);

        //CourseSection Feature
        Task<PagedResultDto<CourseSectionDto>> GetCouresSectionsListAsync(Guid Courseid);
        Task<CourseSectionDto> GetCourseSectionAsync(Guid courseId, Guid sectionId);
        Task AddSectionAsync(CreateUpdateCourseSectionDto input, Guid courseId);
        Task UpdateSectionAsync(CreateUpdateCourseSectionDto input, Guid courseId, Guid sectionId);
        Task DeleteSectionAsync(Guid courseId, Guid sectionId);

        //CourseSectionTopic Feature
        Task<PagedResultDto<CourseTopicDto>> GetCourseSectionTopicListAsync(Guid courseId, Guid sectionId);
        Task<CourseTopicDto> GetCourseSectionTopicAsync(Guid courseId, Guid sectionId,Guid topicId);
        Task AddTopicAsync(CreateUpdateCourseTopicDto input, Guid sectionId, Guid courseId);
        Task UpdateTopicAsync(CreateUpdateCourseTopicDto input, Guid courseId, Guid sectionId, Guid topicId);
        Task DeleteTopicAsync(Guid courseId, Guid sectionId, Guid topicId);

    }
}
