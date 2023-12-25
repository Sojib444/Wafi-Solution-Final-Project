using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Kidoo.Learn.Files
{
    public class KidooFile : FullAuditedAggregateRoot<Guid>
    {
        public string OrginalFileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public long? Size { get; set; }
    }
}
