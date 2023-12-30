using AutoMapper;
using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using Kidoo.Learn.CourseTopics;

namespace Kidoo.Learn.Web;

public class LearnWebAutoMapperProfile : Profile
{
    public LearnWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<UpdateCourseDto, CourseDto>().ReverseMap();
        CreateMap<CourseSection, CreateUpdateCourseSectionDto>().ReverseMap();
        CreateMap<CourseSection, CreateUpdateCourseSectionDto>().ReverseMap();
        CreateMap<CourseSectionDto, CreateUpdateCourseSectionDto>().ReverseMap();
        CreateMap<CourseTopicDto, CreateUpdateCourseTopicDto>().ReverseMap();
    }
}
