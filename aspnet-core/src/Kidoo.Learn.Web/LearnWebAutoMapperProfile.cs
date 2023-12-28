using AutoMapper;
using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;

namespace Kidoo.Learn.Web;

public class LearnWebAutoMapperProfile : Profile
{
    public LearnWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<UpdateCourseDto, CourseDto>().ReverseMap();
        CreateMap<CourseSection, CreateUpdateCourseSectionDto>().ReverseMap();
    }
}
