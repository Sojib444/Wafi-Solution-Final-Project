using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using System.Linq;
using Volo.Abp.Content;
using Volo.Abp.Threading;
using Volo.Abp;
using Kidoo.Learn.Files.Dtos;
using Kidoo.Learn.Files;
using Microsoft.AspNetCore.Authorization;

namespace Kidoo.Learn.Files;

public class FileAppService : ApplicationService, IFileAppService
{
    private readonly IBlobContainer<KidooFileContainer> _blobContainer;
    private readonly IRepository<KidooFile, Guid> _fileRepository;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public FileAppService(
        IBlobContainer<KidooFileContainer> blobContainer,
        ICancellationTokenProvider cancellationTokenProvider,
        IRepository<KidooFile, Guid> fileRepository)
    {
        _blobContainer = blobContainer;
        _cancellationTokenProvider = cancellationTokenProvider;
        _fileRepository = fileRepository;
    }

    public async Task<FileOutputDto> GetAsync(Guid fileId)
    {
        var fileEntity = await _fileRepository.FindAsync(x => x.Id == fileId);
        if (fileEntity == null) throw new UserFriendlyException("file not found");

        return ObjectMapper.Map<KidooFile, FileOutputDto>(fileEntity);
    }

    public async Task<Guid> UploadAsync(SaveBlobInputDto input)
    {
        var extension = Path.GetExtension(input.Name);
        var userId = CurrentUser.Id;
        var randomGuid = GuidGenerator.Create();
        var filePath = $"ExamQuestion/{userId}/{randomGuid}{extension}";

        await _blobContainer.SaveAsync(filePath, input.Content);
        var insertedFileId = await InsertFileAsync(input.Name, extension, filePath, Convert.ToInt32(input.Size));

        return insertedFileId;
    }

    public async Task<IRemoteStreamContent> GetFileAsync(Guid fileId)
    {
        var file = await _fileRepository.FindAsync(x => x.Id == fileId);
        if (file == null) throw new UserFriendlyException("No file found!");

        var returnType = GetContentTypeFromExtension(file.Extension);

        var isExist = await _blobContainer.ExistsAsync(file.FilePath);
        if (!isExist)
        {
            throw new UserFriendlyException("File doesn't exist or deleted");
        }

        var stream = await _blobContainer.GetAsync(file.FilePath, _cancellationTokenProvider.Token);

        var result = new RemoteStreamContent(stream, file.FilePath, returnType);

        return result;
    }
    private string GetContentTypeFromExtension(string extension)
    {
        switch (extension.ToLower())
        {
            case ".png":
                return "image/png";
            case ".jpg":
                return "image/jpg";
            case ".jpeg":
                return "image/jpeg";
            case ".gif":
                return "image/gif";
            default:
                return "application/octet-stream";
        }
    }

    private async Task<Guid> InsertFileAsync(string originalFile, string extension, string path, long? size)
    {
        var obj = new KidooFile()
        {
            OrginalFileName = originalFile,
            Extension = extension,
            FilePath = path,
            Size = size
        };
        await _fileRepository.InsertAsync(obj);

        return obj.Id;
    }
    public async Task<IRemoteStreamContent> DownloadAsync(string name)
    {
        var isExist = await _blobContainer.ExistsAsync(name);
        if (!isExist)
        {
            throw new UserFriendlyException("File doesn't exist or deleted");
        }

        var stream = await _blobContainer.GetAsync(name, _cancellationTokenProvider.Token);
        return new RemoteStreamContent(stream, name, "application/octet-stream");
    }

    public async Task<bool> IsExistAsync(string name)
    {
        if (name.IsNullOrWhiteSpace()) return false;

        return await _blobContainer.ExistsAsync(name);
    }

    public async Task<BlobDto> GetBlobAsync(GetBlobRequestDto input)
    {
        var blob = await _blobContainer.GetAllBytesOrNullAsync(input.Name);

        return new BlobDto
        {
            Name = input.Name,
            Content = blob
        };
    }

    public async Task DeleteBlob(string name, Guid id)
    {
        var isExistFile = await _blobContainer.GetAsync(name, _cancellationTokenProvider.Token);
        if (isExistFile == null)
        {
            throw new UserFriendlyException("No file found!");
        }
        await _blobContainer.DeleteAsync(name);

        var isExistFileInDb = await _fileRepository.AnyAsync(x => x.Id == id);
        if (!isExistFileInDb)
        {
            throw new UserFriendlyException("No file found!");
        }
        await _fileRepository.DeleteAsync(id);

    }
}

[BlobContainerName("kidoo")]
public class KidooFileContainer
{

}