using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Files.Dtos
{
    public class FileOutputDto : EntityDto<Guid>
    {
        public string OriginalFileName { get; set; }
        public string Extension { get; set; }
        public string DownloadUrl { get; set; }
        public string FilePath { get; set; }
        public long? Size { get; set; }
        public byte[] Content { get; set; }
    }
}
