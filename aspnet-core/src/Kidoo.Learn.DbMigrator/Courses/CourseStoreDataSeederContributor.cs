using Kidoo.Learn.Courses;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Kidoo.Learn.DbMigrator.Courses
{
    public class CourseStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Course, Guid> _courseRepository;

        public CourseStoreDataSeederContributor(IRepository<Course, Guid> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if(await _courseRepository.CountAsync() <= 0 )
            {
                await _courseRepository.InsertAsync(new Course(
                    Guid.NewGuid(),
                    "sojib.jpg",
                    ".jpg",
                    GetFileContent("C:/Users/Tafsir Computer/Desktop/sojib.jpg"),
                    "C#",
                    "Devskill Course",
                    20, 
                    200));

                byte[] GetFileContent(string filePath)
                {
                    return File.ReadAllBytes(filePath);
                }
            }
        }
    }
}
