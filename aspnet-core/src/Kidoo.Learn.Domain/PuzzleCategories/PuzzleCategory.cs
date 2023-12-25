using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Kidoo.Learn.PuzzleCategories
{
    public class PuzzleCategory : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ThumbnailUrl { get; private set; }
        private PuzzleCategory(){}
        public PuzzleCategory(Guid id, string title, string description, string thumbnailUrl) : base(id)
        {
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
        }

        public PuzzleCategory UpdateAsync(Guid id, string title, string description, string thumbnailUrl)
        {
            Id = id;
            Title = title;
            Description = description;
            ThumbnailUrl = thumbnailUrl;

            return this;
        }
    }
}
