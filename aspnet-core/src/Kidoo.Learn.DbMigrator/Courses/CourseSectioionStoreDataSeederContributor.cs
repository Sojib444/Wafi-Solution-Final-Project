using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Kidoo.Learn.DbMigrator.Courses
{
    public class CourseSectioionStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<CourseSection, Guid> courseRepository;

        public CourseSectioionStoreDataSeederContributor(IRepository<CourseSection, Guid> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async  Task SeedAsync(DataSeedContext context)
        {
            if (await courseRepository.CountAsync() <= 0)
            {
                await courseRepository.InsertAsync(new CourseSection(
                    Guid.NewGuid(),
                    "www.youtube.com",
                    "Array",
                    20,
                    20,
                    32,
                    new Guid("33A9C2A2-9F1E-EE66-5875-3A0FBC4655A9")));

                await courseRepository.InsertAsync(new CourseSection(
                   Guid.NewGuid(),
                   "www.vdsc.com",
                   "Tauple",
                   20,
                   20,
                   32,
                   new Guid("33A9C2A2-9F1E-EE66-5875-3A0FBC4655A9")));

                await courseRepository.InsertAsync(new CourseSection(
                   Guid.NewGuid(),
                   "www.glfpl.com",
                   "Recursion",
                   20,
                   20,
                   32,
                   new Guid("33A9C2A2-9F1E-EE66-5875-3A0FBC4655A9")));
            }
        }
    }
}
