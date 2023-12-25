using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
