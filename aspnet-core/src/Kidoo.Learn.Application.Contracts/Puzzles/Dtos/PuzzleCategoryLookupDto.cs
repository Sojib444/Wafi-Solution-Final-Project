using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.Puzzles.Dtos
{
    public class PuzzleCategoryLookupDto : EntityDto<Guid>
    {
        public string Title { get; set; }
    }
}
