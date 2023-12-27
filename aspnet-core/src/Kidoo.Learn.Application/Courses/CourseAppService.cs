﻿using Kidoo.Learn.CourseSections;
using Kidoo.Learn.CourseTopics;
using Kidoo.Learn.DomainDtos.CourseSections;
using Kidoo.Learn.DomainDtos.CourseTopics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Kidoo.Learn.Courses
{
    public class CourseAppService : ApplicationService, ICourseAppService
    {
        private readonly ICourseManager _courseManager;
        private readonly IRepository<Course, Guid> _courseRepository;
        public CourseAppService(ICourseManager courseManager, IRepository<Course, Guid> courseRepository)
        {
            _courseManager = courseManager;
            _courseRepository = courseRepository;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateUpdateCourseDto input)
        {
            var isExistTitle = await _courseRepository.AnyAsync(x => x.Title == input.Title);
            if (isExistTitle)
                throw new BusinessException($"Course already exist with '{input.Title}' title");

            var course = await _courseManager.CreateCourseAsync(input.ThumbnailUrl, input.Title, input.Description, input.NumberOfLectures, input.VideoDurationInMinutes);
            var result = await _courseRepository.InsertAsync(course);

            return ObjectMapper.Map<Course, CourseDto>(result);
        }

        public async Task<CourseDto> UpdateCourseAsync(CreateUpdateCourseDto input, Guid courseId)
        {
            var entity = _courseRepository.FindAsync(courseId);
            if(entity == null)
                throw new UserFriendlyException("Update failed, Couldn't find the requested data.");

            var courseTitle = _courseRepository.FindAsync(x => x.Title == input.Title);
            if(courseTitle != null)
                throw new BusinessException("Update failed, Course title already Exixts.");

            var course = ObjectMapper.Map<CreateUpdateCourseDto, Course>(input);

            await _courseRepository.UpdateAsync(course);

            return ObjectMapper.Map<Course, CourseDto>(course);
        }

        public async Task AddSectionAsync(CreateUpdateCourseSectionDto input, Guid courseId)
        {
            var course = await (await _courseRepository.GetQueryableAsync()).Where(x => x.Id == courseId).Include(x => x.Sections).FirstOrDefaultAsync();

            var courseSectionDomainDto = ObjectMapper.Map<CreateUpdateCourseSectionDto, CreateUpdateCourseSectionDomainDto>(input);

            await _courseManager.AddSectionAsync(course, courseSectionDomainDto, courseId);

            await _courseRepository.UpdateAsync(course);
        }

        public async Task UpdateSectionAsync(CreateUpdateCourseSectionDto input, Guid courseId, Guid sectionId)
        {
            var course = await (await _courseRepository.GetQueryableAsync()).Where(x => x.Id == courseId)
                                    .Include(x => x.Sections).FirstOrDefaultAsync();

            course.UpdateSection(sectionId, input.ThumbnailUrl, input.Title, input.VideoDurationInMinutes, input.MinAge, input.MaxAge, courseId);

            await _courseRepository.UpdateAsync(course);
        }

        public async Task AddTopicAsync(CreateUpdateCourseTopicDto input, Guid sectionId, Guid courseId)
        {
            var course = await (await _courseRepository.GetQueryableAsync()).Where(x => x.Id == courseId).Include(x => x.Sections)
                .ThenInclude(x => x.Topics).FirstOrDefaultAsync();

            var courseTopicDomainDto = ObjectMapper.Map<CreateUpdateCourseTopicDto, CreateUpdateCourseTopicDomainDto>(input);

            await _courseManager.AddTopicAsync(course, courseTopicDomainDto, sectionId);

            await _courseRepository.UpdateAsync(course);
        }

        public async Task UpdateTopicAsync(CreateUpdateCourseTopicDto input, Guid courseId, Guid sectionId, Guid topicId)
        {
            var course = await (await _courseRepository.GetQueryableAsync()).Where(x => x.Id == courseId)
                                    .Include(x => x.Sections).ThenInclude(x => x.Topics).FirstOrDefaultAsync();

            course.UpdateTopic(topicId, input.Title, input.VideoDurationInMinutes, input.VideoUrl, sectionId, input.ThumbnailUrl);

            await _courseRepository.UpdateAsync(course);
        }

        public async Task<PagedResultDto<CourseDto>> GetListAsync()
        {
            var course = await _courseRepository.GetQueryableAsync();

            var courseDtos = ObjectMapper.Map<IQueryable<Course>, List<CourseDto>>(course);

            var totalCount = courseDtos.Count();

            return new PagedResultDto<CourseDto>(totalCount, courseDtos);
        }
    }
}
