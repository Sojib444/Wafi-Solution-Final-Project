using Kidoo.Learn.Courses;
using System;
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
                    "www.youtube.com", 
                    "C#",
                    "Devskill Course",
                    20, 200));

                await _courseRepository.InsertAsync(new Course(
                    Guid.NewGuid(),
                    "www.youtube.com",
                    "C++",
                    "Wafi Course",
                    20, 200));
            }
        }
    }
}
