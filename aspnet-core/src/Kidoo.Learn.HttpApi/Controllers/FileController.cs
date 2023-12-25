using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using Volo.Abp.BlobStoring;
using Kidoo.Learn.Files.Dtos;
using Kidoo.Learn.Files;

namespace Kidoo.Learn.Controllers;

/* Inherit your controllers from this class.
 */
public class FileController : AbpControllerBase
{
    private readonly IFileAppService _fileAppService;

    public FileController(IFileAppService fileAppService)
    {
        _fileAppService = fileAppService;
    }


    [HttpPost]
    [Route("file/upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("Invalid file");
        }

        // Convert IFormFile to byte array
        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        var fileId = await _fileAppService.UploadAsync(new Files.Dtos.SaveBlobInputDto
        {
            Name = file.FileName,
            Content = fileBytes,
            Size = fileBytes.Length
        });

        return Ok(new
        {
            Message = fileId
        });
    }
}


public class FileUploadModel
{
    public IFormFile File { get; set; }
}
