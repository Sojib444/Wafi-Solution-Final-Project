using Kidoo.Learn.Students;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Kidoo.Learn.DbMigrator.Students
{
    public class StudentStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Student, Guid> _stuentRepository;

        public StudentStoreDataSeederContributor(IRepository<Student, Guid> stuentRepository)
        {
            _stuentRepository = stuentRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _stuentRepository.GetCountAsync() <= 0)
            {
                await _stuentRepository.InsertAsync(new Student(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "Sojib", "Khan",
                    "Md Nazrul Islam",
                    new DateTime(2000, 09, 25),
                    Enums.Gender.Male,
                    "Pabna",
                    "01778553706",
                    "mdsojibhosen444@gmail.com",
                    "BSC",
                    DateTime.Now));

                await _stuentRepository.InsertAsync(new Student(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "Abir", "Khan",
                    "Md Asad Islam",
                    new DateTime(2000, 09, 25),
                    Enums.Gender.Male,
                    "Dhaka",
                    "01778553706",
                    "mdsojibhosen@gmail.com",
                    "BSC",
                    DateTime.Now));
            }
        }
    }
}
