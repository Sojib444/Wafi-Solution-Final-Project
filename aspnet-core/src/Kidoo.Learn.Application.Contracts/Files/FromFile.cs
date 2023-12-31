using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Kidoo.Learn.Courses
{
    public class FormFile : IFormFile
    {
        public string ContentType { get; }
        public string ContentDisposition { get; }
        public IHeaderDictionary Headers { get; }
        public long Length { get; }
        public string Name { get; }
        public string FileName { get; }

        public FormFile(string fileName, string fileType, byte[] contentDisposition)
        {
            FileName = fileName;
            ContentType = fileType;
            ContentDisposition = contentDisposition.ToString();
            Name = fileName;
        }

        public void CopyTo(Stream target)
        {
            throw new System.NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Stream OpenReadStream()
        {
            throw new System.NotImplementedException();
        }
    }
}
