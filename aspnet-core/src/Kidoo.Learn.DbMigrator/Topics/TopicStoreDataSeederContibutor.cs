using Kidoo.Learn.CourseTopics;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Kidoo.Learn.DbMigrator.Topics
{
    public class TopicStoreDataSeederContibutor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<CourseTopic, Guid> _topicRepository;

        public TopicStoreDataSeederContibutor(IRepository<CourseTopic,Guid> repository)
        {
            _topicRepository = repository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if(await _topicRepository.GetCountAsync() <= 0)
            {
                await _topicRepository.InsertAsync(new CourseTopic(
                    Guid.NewGuid(),
                    "Introducton",
                    20,
                    "www.youtube.com",
                    new Guid("9365D3E9-BFBE-452E-85A9-2142E629A9F0"),
                    "www.google.com"));

                await _topicRepository.InsertAsync(new CourseTopic(
                   Guid.NewGuid(),
                   "Begining",
                   20,
                   "www.youtube.com",
                   new Guid("9365D3E9-BFBE-452E-85A9-2142E629A9F0"),
                   "www.google.com"));

                await _topicRepository.InsertAsync(new CourseTopic(
                  Guid.NewGuid(),
                  "Advanced",
                  20,
                  "www.youtube.com",
                  new Guid("9365D3E9-BFBE-452E-85A9-2142E629A9F0"),
                  "www.google.com"));

                await _topicRepository.InsertAsync(new CourseTopic(
                  Guid.NewGuid(),
                  "Conclution",
                  20,
                  "www.youtube.com",
                  new Guid("9365D3E9-BFBE-452E-85A9-2142E629A9F0"),
                  "www.google.com"));
            }
        }
    }
}
