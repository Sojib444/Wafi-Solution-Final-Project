using AutoMapper; 
using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using Kidoo.Learn.CourseTopics;
using Kidoo.Learn.DomainDtos.CourseSections;
using Kidoo.Learn.DomainDtos.CourseTopics;
using Kidoo.Learn.Files;
using Kidoo.Learn.Files.Dtos;
using Kidoo.Learn.Instructors;
using Kidoo.Learn.Instructors.Dtos;
using Kidoo.Learn.Questions;
using Kidoo.Learn.Questions.Dtos;
using Kidoo.Learn.Questions.QuestionOptionDtos; 
using Kidoo.Learn.Students;
using Kidoo.Learn.Students.Dtos;
namespace Kidoo.Learn;

public class LearnApplicationAutoMapperProfile : Profile
{
    public LearnApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        #region Course Mapping
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<CreateUpdateCourseDto, Course>().ReverseMap();
        CreateMap<CourseTopic, CourseTopicDto>().ReverseMap();
        CreateMap<CourseSection, CourseSectionDto>().ReverseMap();
        CreateMap<CourseSection, CourseTopicDto>().ReverseMap();
        CreateMap<CreateUpdateCourseSectionDto, CreateUpdateCourseSectionDomainDto>().ReverseMap();
        CreateMap<CreateUpdateCourseTopicDto, CreateUpdateCourseTopicDomainDto>().ReverseMap();
        #endregion

        #region Question mapping
        CreateMap<Question, QuestionDto>().ReverseMap(); 
        CreateMap<QuestionOptionDto, UpdateQuestionOptionDto>().ReverseMap(); 
        CreateMap<QuestionDto, UpdateQuestionDto>().ReverseMap(); 
        CreateMap<QuestionDto, CreateUpdateQuestionDto>().ReverseMap(); 
        CreateMap<QuestionOption, QuestionOptionDto>().ReverseMap(); 
        CreateMap<QuestionOptionDto, CreateUpdateQuestionOptionDto>().ReverseMap(); 
        CreateMap<CreateUpdateQuestionOptionDto, QuestionOption>().ReverseMap();
        #endregion

        #region Instructor mapping
        CreateMap<Instructor, InstructorDto>().ReverseMap();
        #endregion

        #region Student mapping
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, StudentProfileDto>().ReverseMap();
        CreateMap<StudentDto, CreateUpdateStudentDto>().ReverseMap();
        CreateMap<StudentDto, UpdateStudentDto>().ReverseMap();
        #endregion

        #region file mapping
        CreateMap<KidooFile, FileOutputDto>().ReverseMap(); 
        #endregion


    }
}
