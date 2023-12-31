using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.DependencyInjection;

namespace Kidoo.Learn.Files.Dtos
{
    public interface IFileAppService : IApplicationService, ITransientDependency
    {
        Task<Guid> UploadAsync(SaveBlobInputDto input);
        Task<bool> IsExistAsync(string name);
        Task<BlobDto> GetBlobAsync(GetBlobRequestDto input);
        Task DeleteBlob(string name, Guid id);
        Task<IRemoteStreamContent> GetFileAsync(Guid fileId);
        Task<FileOutputDto> GetAsync(Guid fileId);
    }
}
