using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Kidoo.Learn.PuzzleCategories
{
    public class GetPuzzleCategoryListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
